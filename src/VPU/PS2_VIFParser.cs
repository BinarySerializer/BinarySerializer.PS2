using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinarySerializer.PS2 {
    public class PS2_VIFParser
    {
        // Settings
        public bool IsVIF1 { get; set; } = false;
        public bool CountMaskedBytes { get; set; } = false;

        // Registers
        public bool DBF { get; set; }
        public uint MODE { get; set; }
        public uint TOPS { get; set; }
        public uint ITOPS { get; set; }
        public uint TOP { get; set; }
        public uint BASE { get; set; }
        public uint OFST { get; set; }
        public byte CYCLE_CycleLength { get; set; } // CL
        public byte CYCLE_WriteLength { get; set; } // WL
        public uint[] ROW { get; set; }
        public uint[] COL { get; set; }
        public uint MASK { get; set; }

        public bool IsExecutingMicroProgram { get; set; } = false;

        public uint ExpectedUnpackDataSize { get; set; }

        public uint MemorySize => IsVIF1 ? (uint)0x4000 : 0x1000;
        public Writer Writer { get; set; }

        // Based on https://github.com/PCSX2/pcsx2/blob/943b513a525a819c96ec83ed7a505d95633c2255/pcsx2/Vif_Codes.cpp
        // Based on https://psi-rockin.github.io/ps2tek/#vifcommands
        public void ExecuteCommand(PS2_VIFCommand command, bool executeFull) {
            if (command.VIFCode.IsUnpack) {
                ExecuteUnpackCommand(command, executeFull);
                return;
            }

            switch (command.VIFCode.CMD) {
                case VIFcode.Command.STMOD:
                    MODE = (uint)BitHelpers.ExtractBits64(command.VIFCode.IMMEDIATE, 2, 0);
                    break;
                case VIFcode.Command.BASE:
                    BASE = (uint)BitHelpers.ExtractBits64(command.VIFCode.IMMEDIATE, 10, 0);
                    break;
                case VIFcode.Command.OFFSET:
                    OFST = (uint)BitHelpers.ExtractBits64(command.VIFCode.IMMEDIATE, 10, 0);
                    DBF = false;
                    TOPS = BASE;
                    break;
                case VIFcode.Command.ITOP:
                    ITOPS = (uint)BitHelpers.ExtractBits64(command.VIFCode.IMMEDIATE, 10, 0);
                    break;
                case VIFcode.Command.STCYCL:
                    CYCLE_CycleLength = (byte)BitHelpers.ExtractBits64(command.VIFCode.IMMEDIATE, 8, 0);
                    CYCLE_WriteLength = (byte)BitHelpers.ExtractBits64(command.VIFCode.IMMEDIATE, 8, 8);
                    break;
                case VIFcode.Command.STROW:
                    ROW = command.ROW;
                    break;
                case VIFcode.Command.STCOL:
                    COL = command.COL;
                    break;
                case VIFcode.Command.STMASK:
                    MASK = command.MASK;
                    break;
                case VIFcode.Command.MSCAL:
                case VIFcode.Command.MSCALF:
                case VIFcode.Command.MSCNT:
                    ExecuteMicroProgram();
                    break;
                case VIFcode.Command.NOP:
                    break;
                default:
                    throw new Exception($"Unparsed VIF command {command.VIFCode.CMD}");
            }
        }

        public void ExecuteMicroProgram() {
            IsExecutingMicroProgram = true;
            if (IsVIF1) {
                TOP = TOPS;
                if (DBF) {
                    // DBF is set, so set tops with base, and clear the stat DBF flag
                    TOPS = BASE;
                    DBF = false;
                } else {
                    // it is not, so set tops with base + offset, and set stat DBF flag
                    TOPS = BASE + OFST;
                    DBF = true;
                }
            }
        }

        public void CreateStream() {
            MemoryStream ms = new MemoryStream(new byte[MemorySize]);
            Writer = new Writer(ms, isLittleEndian: true, leaveOpen: true);
        }

        public void ExecuteUnpackCommand(PS2_VIFCommand command, bool executeFull) {
            VIFcode_Unpack unpack = command.VIFCode.GetUnpack();

            uint wl = CYCLE_WriteLength != 0 ? (uint)CYCLE_WriteLength : 256;
            bool isFill = CYCLE_CycleLength < wl;

            uint sourceAddress = 0;
            uint targetAddress = unpack.ADDR;
            if (IsVIF1 && unpack.FLG) targetAddress += TOPS;
            targetAddress = (uint)((targetAddress << 4) & (IsVIF1 ? 0x3ff0 : 0xff0));
            var cl = 0;

            bool doMask = unpack.M;

            uint count = unpack.VN switch {
                VIFcode_Unpack.UnpackVN.S => 1,
                VIFcode_Unpack.UnpackVN.V2 => 2,
                VIFcode_Unpack.UnpackVN.V3 => 3,
                VIFcode_Unpack.UnpackVN.V4 => 4,
                _ => throw new Exception($"Unknown VIF Unpack command for data type {unpack.VN}-{unpack.VL}")
            };
            uint size = unpack.VL switch {
                VIFcode_Unpack.UnpackVL.VL_8 => 1,
                VIFcode_Unpack.UnpackVL.VL_16 => 2,
                VIFcode_Unpack.UnpackVL.VL_32 => 4,
                _ => throw new Exception($"Unknown VIF Unpack command for data type {unpack.VN}-{unpack.VL}")
            };
            uint vSize = size * count;

            ExpectedUnpackDataSize = unpack.SIZE * vSize;
            uint usedBytes = 0;

            // Based on https://github.com/PCSX2/pcsx2/blob/943b513a525a819c96ec83ed7a505d95633c2255/pcsx2/x86/newVif_Unpack.cpp#L231
            // Based on https://github.com/PCSX2/pcsx2/blob/943b513a525a819c96ec83ed7a505d95633c2255/pcsx2/Vif_Unpack.cpp#L37
            if (executeFull) {
                if (Writer == null) CreateStream();
            }
            for (int i = 0; i < unpack.SIZE; i++) {
                if (executeFull) {
                    Writer.BaseStream.Position = targetAddress;
                }
                // Execute unpack
                uint curUsedBytes = ExecuteSingleUnpack(command, unpack, executeFull, cl, sourceAddress);
                usedBytes += curUsedBytes;
                targetAddress += 16;
                cl++;
                if (isFill) {
                    if (cl <= CYCLE_CycleLength) {
                        sourceAddress += CountMaskedBytes ? vSize : usedBytes;
                    } else if (cl == CYCLE_WriteLength) {
                        cl = 0;
                    }
                } else {
                    sourceAddress += CountMaskedBytes ? vSize : usedBytes;
                    if (cl >= CYCLE_WriteLength) {
                        targetAddress = (uint)(targetAddress + (CYCLE_CycleLength - CYCLE_WriteLength) * 16);
                        cl = 0;
                    }
                }
            }
            if(doMask)
                ExpectedUnpackDataSize = usedBytes;
        }

        public uint ExecuteSingleUnpack(PS2_VIFCommand command, VIFcode_Unpack unpack, bool executeFull, int cl, uint sourceAddress) {
            uint count = unpack.VN switch {
                VIFcode_Unpack.UnpackVN.S => 1,
                VIFcode_Unpack.UnpackVN.V2 => 2,
                VIFcode_Unpack.UnpackVN.V3 => 3,
                VIFcode_Unpack.UnpackVN.V4 => 4,
                _ => throw new Exception($"Unknown VIF Unpack command for data type {unpack.VN}-{unpack.VL}")
            };
            uint size = unpack.VL switch {
                VIFcode_Unpack.UnpackVL.VL_8 => 1,
                VIFcode_Unpack.UnpackVL.VL_16 => 2,
                VIFcode_Unpack.UnpackVL.VL_32 => 4,
                _ => throw new Exception($"Unknown VIF Unpack command for data type {unpack.VN}-{unpack.VL}")
            };

            uint mode = unpack.VL != VIFcode_Unpack.UnpackVL.VL_5 ? MODE : 0;
            bool[] useData = new bool[4];

            for (uint i = 0; i < 4; i++) {
                uint currentSourceAddress = sourceAddress;
                if (unpack.VN == VIFcode_Unpack.UnpackVN.V2 && i % 2 == 1) {
                    currentSourceAddress += size;
                } else if (unpack.VN == VIFcode_Unpack.UnpackVN.V3 || unpack.VN == VIFcode_Unpack.UnpackVN.V4) {
                    currentSourceAddress += i * size;
                }
                if (WriteXYZW(command, unpack, executeFull, cl, mode, size, i, currentSourceAddress)) {
                    switch (unpack.VN) {
                        case VIFcode_Unpack.UnpackVN.S:
                            useData[0] = true;
                            break;
                        case VIFcode_Unpack.UnpackVN.V2:
                            useData[i % 2] = true;
                            break;
                        case VIFcode_Unpack.UnpackVN.V3:
                            if(i < 3) useData[i] = true;
                            break;
                        case VIFcode_Unpack.UnpackVN.V4:
                            useData[i] = true;
                            break;
                    }
                }
            }
            return (uint)useData.Where(u => u == true).Count();
        }
        public bool WriteXYZW(PS2_VIFCommand command, VIFcode_Unpack unpack, bool executeFull, int cl, uint mode, uint size, uint offnum, uint sourceAddress) {
            bool doMask = unpack.M;
            int maskMode = 0;
            if (doMask) {
                maskMode = (int)BitHelpers.ExtractBits64(MASK, 2, Math.Min(cl, 3) * 8 + (int)offnum * 2);
            }
            bool useData = maskMode == 0;
            switch (maskMode) {
                case 0: // 0 - Data
                    break;
                case 1: // 1 - MaskRow
                    break;
                case 2: // 2 - MaskCol
                    break;
                case 3: // 3 - Write protect
                    // Do nothing
                    break;
            }
            return useData;
        }
    }
}