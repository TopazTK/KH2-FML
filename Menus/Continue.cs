using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace KH2FML
{
    public class Continue
    {
        public class Entry
        {
            public ushort Opcode;
            public ushort Label;

            public ushort[] Export()
            {
                var _returnList = new List<ushort>()
                {
                    Opcode,
                    Label,
                };

                return _returnList.ToArray();
            }
        }

        public ObservableCollection<Entry> Children;

        public Continue()
        {
            var _entContinue = new Entry()
            {
                Opcode = 0x0002,
                Label = 0x8AB0
            };

            var _entLoad = new Entry()
            {
                Opcode = 0x0001,
                Label = 0x8AAF
            };

            Children = new ObservableCollection<Entry>()
            {
                _entContinue,
                _entLoad
            };

            Children.CollectionChanged += Submit;

            Submit();
        }
        public void Submit(object? sender = null, NotifyCollectionChangedEventArgs e = null)
        {
            var _continueOptions = Hypervisor.Read<ulong>(Variables.PINT_GameOverOptions);

            Hypervisor.Write(_continueOptions + 0x34A, (short)Children.Count, true);

            if (Children.Count > 4)
                return;

            for (int i = 0; i < 4; i++)
            {
                if (i < Children.Count)
                {
                    var _childExport = Children[i].Export();
                    var _childWrite = _childExport.SelectMany(BitConverter.GetBytes).ToArray();
                    Hypervisor.Write(_continueOptions + 0x34C + (ulong)(0x04 * i), _childWrite, true);
                }

                else
                    Hypervisor.Write(_continueOptions + 0x34C + (ulong)(0x04 * i), 0x00, true);

            }
        }
    }
}