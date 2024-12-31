using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public static class IO
    {
        public static nint FUNC_FINDFILE;
        public static nint FUNC_OBJENTRYGET;
        public static nint FUNC_GETFILESIZE;
        public static nint FUNC_FREETASKMGR;
        public static nint FUNC_CREATETASKMGR;

        /// <summary>
        /// Creates a Task Manager to use with advanced game functions.
        /// May God bless your soul and Lord give you strength if you ever have to use this function.
        /// </summary>
        public static void CreateTASKMGR()
        {
            var _taskActual = (long)Hypervisor.Read<ulong>(Variables.ADDR_TaskManager);

            if (_taskActual == 0x00)
            {
                var _taskMGR = Hypervisor.MemoryOffset + (ulong)Variables.SharpHook[FUNC_CREATETASKMGR].ExecuteJMP<long>(BSharpConvention.MicrosoftX64, (long)(Hypervisor.PureAddress + 0x9A0730), 0x8000);
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
        /// Gets the size of a file from the filesystem.
        /// Generally used by the game to either see if a file exists, or to allocate memory for a file.
        /// </summary>
        /// <param name="Input">The name of the file as in the filesystem.</param>
        /// <returns>Size of the file in bytes, 0 if the file is not found.</returns>
        public static int GetFileSize(string Input) => Variables.SharpHook[FUNC_GETFILESIZE].Execute<int>(Input);

        /// <summary>
        /// Gets the absolute memory location to a file descriptor in the Cache Buffer.
        /// Care and attention is requested at all "Buffer" functions. It's delicate.
        /// </summary>
        /// <param name="Input">The name of the file to find in the Cache Buffer.</param>
        /// <returns>The absolute memory location of the descriptor, "0x00" if not found.</returns>
        public static ulong FindFileBuffer(string Input)
        {
            var _memoryOffset = Hypervisor.PureAddress & 0x7FFF00000000;
            var _filePointer = Variables.SharpHook[FUNC_FINDFILE].Execute<uint>(BSharpConvention.MicrosoftX64, Input, -1);
            return _filePointer == 0x00 ? 0x00 : _memoryOffset + _filePointer;
        }

        /// <summary>
        /// Fetches the given Object ID from 00objentry.bin! This should mostly eliminate manual labor.
        /// Could be a bit slow, optimization is advised when using this function.
        /// </summary>
        /// <param name="ObjectID">The ID of the Object as it is in 00objentry.bin</param>
        /// <returns>The absolute memory location of the object in 00objentry.bin, "0x00" if not found.</returns>
        public static ulong FetchObject(short ObjectID)
        {
            var _fetchObject = Variables.SharpHook[FUNC_OBJENTRYGET].Execute(ObjectID);

            if (_fetchObject == IntPtr.Zero)
                return 0x00;

            else
                return Hypervisor.MemoryOffset + (ulong)_fetchObject;
        }
    }
}
