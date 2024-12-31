using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public static class Sound
    {
        public static nint FUNC_PLAYSFX;
        public static nint FUNC_PLAYVSB;
        public static nint FUNC_KILLBGM;

        /// <summary>
        /// Plays the given VSB file. "VSB" being an old PS2 term for "Cutscene Sound".
        /// Can be used back-to-back with no reprecussions, no initialization needed.
        /// </summary>
        /// <param name="VSB">The abolute memory position of the VSB.</param>
        /// <param name="Size">The size of the VSB file.</param>
        /// <param name="Volume">Volume of the sound to play, from 1 - 100.</param>
        /// <param name="Pan">Optional, the pan of the sound to play.</param>
        public static void PlayVSB(long VSB, int Size, int Volume = 100, int Pan = 0x00)
        {
            var _volumeCalc = 0xA3 * Volume;
            Variables.SharpHook[FUNC_PLAYVSB].ExecuteJMP(BSharpConvention.MicrosoftX64, VSB, Size, _volumeCalc, Pan);
        }

        /// <summary>
        /// Plays a given sound effect. Seems to work everywhere except for the Title Screen.
        /// If a continuous sound is played, it cannot be stopped.
        /// </summary>
        /// <param name="SoundID">The ID of the sound to be played.</param>
        public static void PlaySFX(int SoundID) => Variables.SharpHook[FUNC_PLAYSFX].Execute(BSharpConvention.MicrosoftX64, SoundID, 1);

        /// <summary>
        /// Immediately kills the Background Music.
        /// </summary>
        public static void KillBGM() => Variables.SharpHook[FUNC_KILLBGM].Execute();
    }
}
