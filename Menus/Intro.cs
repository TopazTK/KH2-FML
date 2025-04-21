using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace KH2FML
{
    public class Intro
    {
        public class Entry
        {
            public uint Count;
            public uint Flair;
            public uint Title;
            public ObservableCollection<uint> Buttons;
            public ObservableCollection<uint> Descriptions;

            public Entry(uint Count, uint Flair, uint Title, uint[] Buttons, uint[] Descriptions)
            {
                this.Count = Count;
                this.Flair = Flair;
                this.Title = Title;
                this.Buttons = new ObservableCollection<uint>();
                this.Descriptions = new ObservableCollection<uint>();

                foreach (var _button in Buttons)
                    this.Buttons.Add(_button);

                foreach (var _description in Descriptions)
                    this.Descriptions.Add(_description);

                this.Buttons.CollectionChanged += Update;
                this.Descriptions.CollectionChanged += Update;
            }

            public void Update(object? sender = null, NotifyCollectionChangedEventArgs e = null) => Count = (uint)(sender as ObservableCollection<uint>).Count;

            public uint[] Export()
            {
                var _returnList = new List<uint>()
                {
                    Count,
                    Flair,
                    Title
                };

                for (int i = 0; i < 4; i++)
                {
                    if (i < Count)
                        _returnList.Add(Buttons[i]);

                    else
                        _returnList.Add(0xFFFFFFFF);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (i < Count)
                        _returnList.Add(Descriptions[i]);

                    else
                        _returnList.Add(0xFFFFFFFF);
                }

                return _returnList.ToArray();
            }
        }

        public ObservableCollection<Entry> Children;

        public Intro()
        {
            var _entDifficulty = new Entry(4, 0xC330, 0xC380, [0xC331, 0xC332, 0xC333, 0xCE33], [0xC334, 0xC335, 0xC336, 0xCE34]);
            var _entVibration = new Entry(2, 0xC337, 0xC381, [0xC338, 0xC339], [0xC33A, 0xC33B]);

            Children = new ObservableCollection<Entry>()
            {
                _entDifficulty,
                _entVibration,
            };

            if (Variables.MemoryKH["INTRO_MEMORY"] == 0xDEADBEEF)
                Variables.MemoryKH.Allocate("INTRO_MEMORY", 0x400);

            Children.CollectionChanged += Submit;

            Submit();
        }

        public void Submit(object? sender = null, NotifyCollectionChangedEventArgs e = null)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                var _childExport = Children[i].Export();
                var _childWrite = _childExport.SelectMany(BitConverter.GetBytes).ToArray();

                Hypervisor.Write(Variables.MemoryKH["INTRO_MEMORY"] + (ulong)(i * 0x2C), _childWrite, true);
            }

            byte _lastIndex = (byte)(Children.Count - 1);
            var _menuOffset = (uint)(Variables.MemoryKH["INTRO_MEMORY"] - Hypervisor.PureAddress);

            Hypervisor.Write(_menuOffset + 0x200, new byte[0x10]);

            Hypervisor.Write(Variables.HFIX_IntroOffsets[0] + 0x253, _menuOffset);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[0] + 0x233, _menuOffset + 0x04);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[0] + 0x276, _menuOffset + 0x0C);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[0] + 0x406, _menuOffset + 0x1C);

            Hypervisor.RedirectLEA(Variables.HFIX_IntroOffsets[1] + 0x1DA, _menuOffset);
            Hypervisor.RedirectLEA(Variables.HFIX_IntroOffsets[1] + 0x3D7, _menuOffset);
            Hypervisor.RedirectLEA(Variables.HFIX_IntroOffsets[1] + 0x0AF, _menuOffset + 0x08);
            Hypervisor.RedirectLEA(Variables.HFIX_IntroOffsets[2] + 0x03D, _menuOffset + 0x1C);

            Hypervisor.Write(Variables.HFIX_IntroOffsets[3] + 0x097, (byte)Children.Count);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[3] + 0x1F5, (byte)Children.Count);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[3] + 0x531, (byte)Children.Count);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[4] + 0x1EF, (byte)Children.Count);

            Hypervisor.Write(Variables.HFIX_IntroOffsets[0] + 0x3F7, _lastIndex);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[1] + 0x3CB, _lastIndex);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[2] + 0x031, _lastIndex);
            Hypervisor.Write(Variables.HFIX_IntroOffsets[3] + 0x08E, _lastIndex);

            Hypervisor.Write(Variables.HFIX_IntroOffsets[0] + 0x3B5, _menuOffset + 0x200);

            Hypervisor.RedirectLEA(Variables.HFIX_IntroOffsets[1] + 0x0A8, _menuOffset + 0x200);
            Hypervisor.RedirectLEA(Variables.HFIX_IntroOffsets[4] + 0x1F2, _menuOffset + 0x200);
            Hypervisor.RedirectMOV(Variables.HFIX_IntroOffsets[5] + 0x2BF, _menuOffset + 0x200);

            Hypervisor.RedirectMOV(Variables.HFIX_IntroOffsets[6] + 0x09, _menuOffset + 0x200);
            Hypervisor.RedirectCMP(Variables.HFIX_IntroOffsets[6] + 0x17, _menuOffset + 0x204);
        }
    }
}