using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;


namespace KH2FML
{
    internal class Operators
    {
        static nint FUNC_FINDFILE;
        static nint FUNC_GETFILESIZE;

        public static byte[] FetchStringMSG(ulong StartMSG, ushort StringID)
        {
            var _msnAbsolute = Hypervisor.Read<ulong>(StartMSG);

            var _checkFirst = Hypervisor.Read<int>(_msnAbsolute, true);
            var _checkSecond = Hypervisor.Read<int>(_msnAbsolute - 0x30, true);

            if (_checkFirst != 0x01 || _checkSecond != 0x01524142)
                return null;

            var _fetchCount = Hypervisor.Read<int>(_msnAbsolute + 0x04, true);
            var _fetchData = Hypervisor.Read<byte>(_msnAbsolute + 0x08, _fetchCount * 0x08, true);

            var _offsetLocal = _fetchData.FindValue<int>(StringID);

            var _offsetString = Hypervisor.Read<int>(_msnAbsolute + _offsetLocal + 0x0C, true);

            int _readOffset = 0;
            List<byte> _returnList = new List<byte>();

            while (true)
            {
                var _byte = Hypervisor.Read<byte>(_msnAbsolute + (ulong)(_offsetString + _readOffset), true);

                _returnList.Add(_byte);

                if (_byte == 0x00)
                    break;

                else
                    _readOffset++;
            }

            return _returnList.ToArray();
        }

        public static ulong FetchPointerMSG(ulong StartMSG, ushort StringID)
        {
            var _msnAbsolute = Hypervisor.Read<ulong>(StartMSG);

            var _checkFirst = Hypervisor.Read<int>(_msnAbsolute, true);
            var _checkSecond = Hypervisor.Read<int>(_msnAbsolute - 0x30, true);

            if (_checkFirst != 0x01 || _checkSecond != 0x01524142)
                return 0x00;

            var _fetchCount = Hypervisor.Read<int>(_msnAbsolute + 0x04, true);
            var _fetchData = Hypervisor.Read<byte>(_msnAbsolute + 0x08, _fetchCount * 0x08, true);

            var _offsetLocal = _fetchData.FindValue<int>(StringID);

            var _offsetString = Hypervisor.Read<uint>(_msnAbsolute + _offsetLocal + 0x0C, true);

            return _msnAbsolute + _offsetString;
        }

        public static ulong FetchBufferFile(string Input)
        {
            var _memoryOffset = Hypervisor.PureAddress & 0x7FFF00000000;
            var _filePointer = Variables.SharpHook[FUNC_FINDFILE].Execute<uint>(BSharpConvention.MicrosoftX64, Input, -1);
            return _filePointer == 0x00 ? 0x00 : _memoryOffset + _filePointer;
        }

        public static int GetFileSize(string Input) => Variables.SharpHook[FUNC_GETFILESIZE].Execute<int>(Input);
    }
}
