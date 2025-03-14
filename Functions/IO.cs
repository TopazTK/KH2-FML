using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public static class IO
    {
        public static nint FUNC_FILEREAD;
        public static nint FUNC_CACHEREADREQ;
        public static nint FUNC_CACHEFIND;
        public static nint FUNC_AREAALLOC;
        public static nint FUNC_GETFILESIZE;
        public static nint FUNC_FREETASKMGR;
        public static nint FUNC_CREATETASKMGR;

        /// <summary>
        /// Creates a Task Manager to use with advanced game functions.
        /// May God bless your soul and Lord give you strength if you ever have to use this function.
        /// </summary>
        public static void CreateTASKMGR()
        {
            var _taskActual = Hypervisor.Read<ulong>(Variables.ADDR_TaskManager);

            if (_taskActual == 0x00)
            {
                var _taskMGR = Variables.SharpHook[FUNC_CREATETASKMGR].ExecuteJMP<ulong>(BSharpConvention.MicrosoftX64, Hypervisor.PureAddress + 0x9A0730, 0x8000);
                Hypervisor.Write(Variables.ADDR_TaskManager, _taskMGR);
            }
        }

        /// <summary>
        /// Frees the Task Manager.
        /// Using this function may cause the symptom of saying "Thank GOD it's over!"
        /// </summary>
        public static void FreeTASKMGR()
        {
            var _taskActual = (long)Hypervisor.Read<ulong>(Variables.ADDR_TaskManager);

            if (_taskActual != 0x00)
            {
                Variables.SharpHook[FUNC_FREETASKMGR].ExecuteJMP(BSharpConvention.MicrosoftX64, _taskActual);
                Hypervisor.Write<ulong>(Variables.ADDR_TaskManager, 0x00);
            }
        }

        /// <summary>
        /// Allocates memory, then loads the given file.
        /// </summary>
        /// <param name="Input">Name of the file to load.</param>
        /// <returns>Memory Address of the file if loaded, 0x00 if the load has failed.</returns>
        public static ulong LoadFile(string Input)
        {
            var _fileSize = GetFileSize(Input);

            if (Variables.MemoryKH[Input] == 0xDEADBEEF)
            {
                Variables.MemoryKH.Allocate(Input, _fileSize);

                if (Variables.SharpHook[FUNC_FILEREAD].ExecuteJMP<int>(BSharpConvention.MicrosoftX64, Input, Variables.MemoryKH[Input]) > 0x00)
                    return Variables.MemoryKH[Input];

                else
                    return 0;
            }

            else
                return Variables.MemoryKH[Input];
        }

        /// <summary>
        /// Gets the size of a file from the filesystem.
        /// Generally used by the game to either see if a file exists, or to allocate memory for a file.
        /// </summary>
        /// <param name="Input">The name of the file as in the filesystem.</param>
        /// <returns>Size of the file in bytes, 0 if the file is not found.</returns>
        public static int GetFileSize(string Input) => Variables.SharpHook[FUNC_GETFILESIZE].Execute<int>(Input);

        /// <summary>
        /// Inserts a memory region to the Cache Buffer to make use of later.
        /// This does NOT load a file into said region, unless the region is permanent. This is for a good reason.
        /// Care and attention is requested at all "Buffer" functions. It's delicate.
        /// </summary>
        /// <param name="Input">The filename of the region.</param>
        /// <param name="Priority">Load priority. If this value is "-1", the region is permanent.</param>
        /// <returns>"TRUE" if success, "FALSE" otherwise.</returns>
        public static bool InsertToBuffer(string Input, int Priority = 1)
        {
            var _fileSize = GetFileSize(Input);

            if (_fileSize > 0)
            {
                Variables.SharpHook[FUNC_CACHEREADREQ].Execute(BSharpConvention.MicrosoftX64, Input, Priority, _fileSize);
                return true;
            }

            else
                return false;
        }

        /// <summary>
        /// Gets the absolute memory location to a file descriptor in the Cache Buffer.
        /// Care and attention is requested at all "Buffer" functions. It's delicate.
        /// </summary>
        /// <param name="Input">The name of the file to find in the Cache Buffer.</param>
        /// <returns>The absolute memory location of the descriptor, "0x00" if not found.</returns>
        public static ulong FindFileBuffer(string Input)
        {
            var _filePointer = Variables.SharpHook[FUNC_CACHEFIND].Execute<ulong>(BSharpConvention.MicrosoftX64, Input, -1);
            return _filePointer;
        }
    }
}
