using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2FML
{
    public class ShopFace
    {
        public static ulong FIRST_OFFSET;
        public static ulong SECOND_OFFSET;
        public static ulong THIRD_OFFSET;

        public struct Entry
        {
            public short ObjectID;
            public string Name;
        }

        public ObservableCollection<Entry> Children;

        public ShopFace()
        {
            var _shopfaceSize = IO.GetFileSize("00shopface.bin");

            Children = new ObservableCollection<Entry>();

            if (_shopfaceSize != 0x00)
            {
                var _loadAddr = IO.LoadFile("00shopface.bin");
                var _count = Hypervisor.Read<int>(_loadAddr, true);

                for (uint i = 0; i < _count; i++)
                {
                    var _child = new Entry();

                    _child.ObjectID = Hypervisor.Read<short>(_loadAddr + 0x10 + 0x10 * i, true);
                    _child.Name = Hypervisor.ReadString(_loadAddr + 0x02 + 0x10 + 0x10 * i, true);

                    if (_child.Name.Length > 14)
                        throw new InvalidDataException("SHOPFACE -- NAME STRING TOO LONG!");

                    Children.Add(_child);
                }

                var _checkMemory = Variables.MemoryKH["SHOPFACE_STRUCT"];

                if (_checkMemory == 0xDEADBEEF)
                {
                    Variables.MemoryKH.Allocate("SHOPFACE_NAMES", 0x10 * _count);
                    Variables.MemoryKH.Allocate("SHOPFACE_STRUCT", 0x10 * _count);
                }

                for (uint i = 0; i < _count; i++)
                {
                    var _currentChild = Children.ElementAt((int)i);

                    Hypervisor.Write(Variables.MemoryKH["SHOPFACE_STRUCT"] + 0x10 * i, _currentChild.ObjectID, true);
                    Hypervisor.Write(Variables.MemoryKH["SHOPFACE_STRUCT"] + (0x10 * i) + 0x08, Variables.MemoryKH["SHOPFACE_NAMES"] + 0x10 * i, true);

                    Hypervisor.Write(Variables.MemoryKH["SHOPFACE_NAMES"] + 0x10 * i, _currentChild.Name, true);
                }

                var _structOffset = (uint) (Variables.MemoryKH["SHOPFACE_STRUCT"] - Hypervisor.PureAddress);

                Hypervisor.RedirectLEA(FIRST_OFFSET + 0x53, _structOffset);
                Hypervisor.RedirectLEA(THIRD_OFFSET + 0x50, _structOffset);
                Hypervisor.RedirectLEA(SECOND_OFFSET + 0x28, _structOffset);

                Hypervisor.RedirectLEA(FIRST_OFFSET + 0x62, _structOffset + 0x08);
                Hypervisor.RedirectLEA(THIRD_OFFSET + 0x41, _structOffset + 0x08);
                Hypervisor.RedirectLEA(SECOND_OFFSET + 0x83, _structOffset + 0x08);
            }
        }
    }
}
