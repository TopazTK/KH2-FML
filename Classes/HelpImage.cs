using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2FML
{
    public class HelpImage
    {
        public struct Entry
        {
            public string Name;
            public uint Struct;
        }

        public ObservableCollection<Entry> Children;
        public ObservableCollection<ushort> Strings;

        public HelpImage()
        {
            var _helpImageSize = IO.GetFileSize("00helpimage.bin");

            Children = new ObservableCollection<Entry>();
            Strings = new ObservableCollection<ushort>();

            if (_helpImageSize != 0x00)
            {
                var _loadAddr = IO.LoadFile("00helpimage.bin");

                var _structCount = Hypervisor.Read<int>(_loadAddr, true);
                var _structStart = Hypervisor.Read<uint>(_loadAddr + 0x04, true);

                var _stringCount = Hypervisor.Read<int>(_loadAddr + 0x08, true);
                var _stringStart = Hypervisor.Read<uint>(_loadAddr + 0x0C, true);

                for (uint i = 0; i < _structCount; i++)
                {
                    var _child = new Entry();

                    _child.Name = Hypervisor.ReadString(_loadAddr + _structStart + (0x10 * i), true);
                    _child.Struct = Hypervisor.Read<uint>(_loadAddr + _structStart + 0x0c + (0x10 * i), true);

                    if (_child.Name.Length > 12)
                        throw new InvalidDataException("ERROR ON HELPIMAGE: NAME TOO LONG!");

                    Children.Add(_child);
                }

                for (uint i = 0; i < _stringCount; i++)
                {
                    var _string = Hypervisor.Read<ushort>(_loadAddr + _stringStart + (0x02 * i), true);
                    Strings.Add(_string);
                }

                var _checkMemory = Variables.MemoryKH["HELPIMAGE_STRUCT"];

                if (_checkMemory == 0xDEADBEEF)
                {
                    Variables.MemoryKH.Allocate("HELPIMAGE_STRUCT", 0x10 * _structCount);
                    Variables.MemoryKH.Allocate("HELPIMAGE_STRING", 0x04 * _stringCount);
                    Variables.MemoryKH.Allocate("HELPIMAGE_FNAMES", 0x10 * _structCount);
                }

                for (uint i = 0; i < _structCount; i++)
                {
                    var _currentChild = Children.ElementAt((int)i);

                    Hypervisor.Write(Variables.MemoryKH["HELPIMAGE_STRUCT"] + 0x10 * i, Variables.MemoryKH["HELPIMAGE_FNAMES"] + 0x10 * i, true);
                    Hypervisor.Write(Variables.MemoryKH["HELPIMAGE_STRUCT"] + 0x10 * i + 0x08, _currentChild.Struct, true);

                    Hypervisor.Write(Variables.MemoryKH["HELPIMAGE_FNAMES"] + 0x10 * i, _currentChild.Name, true);
                }

                for (uint i = 0; i < _stringCount; i++)
                {
                    var _currentString = Strings.ElementAt((int)i);
                    Hypervisor.Write(Variables.MemoryKH["HELPIMAGE_STRING"] + 0x02 * i, _currentString, true);
                }

                var _structOffset = (uint) (Variables.MemoryKH["HELPIMAGE_STRUCT"] - Hypervisor.PureAddress);
                var _stringOffset = (uint) (Variables.MemoryKH["HELPIMAGE_STRING"] - Hypervisor.PureAddress);

                Hypervisor.RedirectLEA((ulong)Popup.FUNC_SHOWHELP + 0x03A, _structOffset);
                Hypervisor.RedirectLEA((ulong)Popup.FUNC_SHOWHELP + 0x053, _structOffset + 0x08);

                Hypervisor.RedirectLEA(Variables.HFIX_HelpimageOffsets[0] + 0x0A0, _structOffset);
                Hypervisor.RedirectLEA(Variables.HFIX_HelpimageOffsets[1] + 0x032, _structOffset);
                Hypervisor.RedirectLEA(Variables.HFIX_HelpimageOffsets[3] + 0x045, _structOffset);
                Hypervisor.RedirectLEA(Variables.HFIX_HelpimageOffsets[4] + 0x024, _structOffset);

                Hypervisor.RedirectLEA(Variables.HFIX_HelpimageOffsets[1] + 0x028, _structOffset + 0x08);
                Hypervisor.RedirectLEA(Variables.HFIX_HelpimageOffsets[6] + 0x026, _structOffset + 0x08);
                Hypervisor.RedirectLEA(Variables.HFIX_HelpimageOffsets[7] + 0x00C, _structOffset + 0x08);

                Hypervisor.RedirectMOVZX(Variables.HFIX_HelpimageOffsets[5] + 0x171, _structOffset + 0x08);
                Hypervisor.RedirectMOVZX(Variables.HFIX_HelpimageOffsets[6] + 0x01F, _structOffset + 0x08);
                Hypervisor.RedirectMOVZX(Variables.HFIX_HelpimageOffsets[4] + 0x030, _structOffset + 0x08);
                Hypervisor.RedirectMOVZX(Variables.HFIX_HelpimageOffsets[4] + 0x05A, _structOffset + 0x08);

                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[8] + 0x1DE + 0x04, _stringOffset);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[2] + 0x099 + 0x04, _structOffset);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[5] + 0x1B5 + 0x04, _structOffset + 0x08);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[5] + 0x101 + 0x03, _structOffset + 0x0A);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[5] + 0x3F6 + 0x03, _structOffset + 0x0A);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[8] + 0x1D6 + 0x04, _structOffset + 0x0A);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[5] + 0x0F9 + 0x04, _structOffset + 0x0A);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[5] + 0x3EA + 0x05, _structOffset + 0x0A);
                Hypervisor.Write(Variables.HFIX_HelpimageOffsets[2] + 0x029 + 0x04, _structOffset + 0x0B);
            }
        }
    }
}
