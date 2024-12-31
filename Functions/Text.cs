namespace KH2FML
{
    internal class Text
    {
        public static nint FUNC_MESSAGEGETDATA;

        /// <summary>
        /// Gets a string corresponding to the given ID, and converts it to a human readable format.
        /// In any conflicting scenario: "SYS.bar" has priority over "[WORLD].bar"!
        /// </summary>
        /// <param name="StringID">The ID of the String to fetch.</param>
        /// <returns>The fetched string, "FAKE" if not found.</returns>
        public static string GetStringHuman(short StringID)
        {
            var _messageOffset = Variables.SharpHook[FUNC_MESSAGEGETDATA].Execute(StringID);

            if (_messageOffset == IntPtr.Zero)
                return "FAKE";

            var _messageAbsolute = Hypervisor.MemoryOffset + (ulong)_messageOffset;

            ulong _readOffset = 0;
            List<byte> _returnList = new List<byte>();

            while (true)
            {
                var _byte = Hypervisor.Read<byte>(_messageAbsolute + _readOffset, true);

                _returnList.Add(_byte);

                if (_byte == 0x00)
                    break;

                else
                    _readOffset++;
            }

            return _returnList.ToArray().FromKHSCII();
        }

        /// <summary>
        /// Gets a string corresponding to the given ID in KHSCII.
        /// In any conflicting scenario: "SYS.bar" has priority over "[WORLD].bar"!
        /// </summary>
        /// <param name="StringID">The ID of the String to fetch.</param>
        /// <returns>The fetched string in KHSCII, "FAKE" if not found.</returns>
        public static byte[] GetStringLiteral(short StringID)
        {
            var _messageOffset = Variables.SharpHook[FUNC_MESSAGEGETDATA].Execute(StringID);

            if (_messageOffset == IntPtr.Zero)
                return [ 0x33, 0x2E, 0x38, 0x32 ];

            var _messageAbsolute = Hypervisor.MemoryOffset + (ulong)_messageOffset;

            ulong _readOffset = 0;
            List<byte> _returnList = new List<byte>();

            while (true)
            {
                var _byte = Hypervisor.Read<byte>(_messageAbsolute + _readOffset, true);

                _returnList.Add(_byte);

                if (_byte == 0x00)
                    break;

                else
                    _readOffset++;
            }

            return _returnList.ToArray();
        }

        /// <summary>
        /// Gets the absolute memory location to a string corresponding to the given ID.
        /// In any conflicting scenario: "SYS.bar" has priority over "[WORLD].bar"!
        /// </summary>
        /// <param name="StringID">The ID of the String to fetch.</param>
        /// <returns>The absolute memory location of the fetched string, "0x00" if not found.</returns>
        public static ulong GetStringPointer(short StringID)
        {
            var _messageOffset = Variables.SharpHook[FUNC_MESSAGEGETDATA].Execute(StringID);

            if (_messageOffset == IntPtr.Zero)
                return 0x00;

            return Hypervisor.MemoryOffset + (ulong)_messageOffset;
        }
    }
}
