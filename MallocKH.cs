using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public class MallocKH
    {
        public static nint FUNC_CREATEALLOC;

        private ulong _allocInstance;
        private ulong _functionSpace;

        private nint _allocFunction;
        private nint _freeFunction;

        private Dictionary<string, ulong> _memoryBlock;

        public ulong this[string Input]
        {
            get
            {
                var _tagCheck = _memoryBlock.ContainsKey(Input);

                if (!_tagCheck)
                    return 0xDEADBEEF;

                var _tagValue = Hypervisor.Read<ulong>(_memoryBlock[Input], true);

                var _tagValid = _tagValue != 0xCAFEEFACCAFEEFAC &&
                                _tagValue != 0xEFACCAFEEFACCAFE;

                return _tagValid ? _memoryBlock[Input] : 0xDEADBEEF;

            }

            set => _memoryBlock[Input] = value;
        }

        /// <summary>
        /// Creates the MallocKH instance in given memory address.
        /// The given memory address must be unused and static.
        /// </summary>
        /// <param name="Input">The address of the instance. Must be offsetted.</param>
        /// <param name="Size">The maximum memory size the allocater can use (in bytes). 0x10000 by default.</param>
        public MallocKH(ulong Input, long Size = 0x10000)
        {
            _memoryBlock = new Dictionary<string, ulong>();

            _allocInstance = Variables.SharpHook[FUNC_CREATEALLOC].ExecuteJMP<ulong>(BSharpConvention.MicrosoftX64, Hypervisor.PureAddress + Input, Size);
            _functionSpace = Hypervisor.Read<ulong>(_allocInstance, true);

            _allocFunction = (IntPtr) (Hypervisor.Read<ulong>(_functionSpace + 0x08, true) - Hypervisor.PureAddress);
            _freeFunction = (IntPtr) (Hypervisor.Read<ulong>(_functionSpace + 0x10, true) - Hypervisor.PureAddress);
        }

        /// <summary>
        /// Allocates a memory address in the block with the given tag.
        /// It is recommended that the tag be a file name for ease of use.
        /// </summary>
        /// <param name="Input">The tag in which the area will be allocated with.</param>
        /// <param name="Size">Size of memory to allocate (in bytes).</param>
        public void Allocate(string Input, int Size)
        {
            var _checkMemory = this[Input];
            
            if (_checkMemory == 0xDEADBEEF)
            {
                var _allocMemory = Variables.SharpHook[_allocFunction].ExecuteJMP<ulong>(BSharpConvention.MicrosoftX64, _allocInstance, Size);

                if (_memoryBlock.ContainsKey(Input))
                    _memoryBlock[Input] = _allocMemory;

                else
                    _memoryBlock.Add(Input, _allocMemory);

                Hypervisor.Write(_allocMemory, Size, true);
                Terminal.Log("Allocated memory at 0x" + _allocMemory.ToString("X12") + " for \"" + Input + "\" successfully!", 0);
            }
        }

        /// <summary>
        /// Frees the specified tag in the block.
        /// </summary>
        /// <param name="Input">Tag of the allocated memory int the block.</param>
        public void Free(string Input)
        {
            var _checkMemory = this[Input];

            if (_checkMemory != 0xDEADBEEF)
            {
                Variables.SharpHook[_freeFunction].ExecuteJMP(BSharpConvention.MicrosoftX64, _allocInstance, _checkMemory);
                Terminal.Log("Memory at location 0x" + _checkMemory.ToString("X12") + " for \"" + Input + "\" has been freed successfully!", 0);
            }

           _memoryBlock.Remove(Input);
        }
    }
}
