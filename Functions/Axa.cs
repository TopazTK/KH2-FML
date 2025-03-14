using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public static class Axa
    {
        public static IntPtr FUNC_SUSPENDTASK;
        public static IntPtr FUNC_RESUMETASK;
        public static IntPtr FUNC_SOUNDPAUSE;

        /// <summary>
        /// Suspends the game completely.
        /// </summary>
        public static void Suspend(bool SuspendAudio = true)
        {
            if (SuspendAudio)
                Variables.SharpHook[FUNC_SOUNDPAUSE].Execute(BSharpConvention.MicrosoftX64, 0x01, 0x01, 0x00);

            Variables.SharpHook[FUNC_SUSPENDTASK].Execute();
            Thread.Sleep(50);
        }

        /// <summary>
        /// Resumes the suspended game.
        /// </summary>
        public static void Resume(bool SuspendAudio = true)
        {
            if (SuspendAudio)
                Variables.SharpHook[FUNC_SOUNDPAUSE].Execute(BSharpConvention.MicrosoftX64, 0x00, 0x01, 0x00);

            Variables.SharpHook[FUNC_RESUMETASK].Execute();
            Thread.Sleep(50);
        }
    }
}
