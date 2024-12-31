using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public class Effect
    {
        public static nint FUNC_PAXSTART;
        public static nint FUNC_PAXKILLALL;

        /// <summary>
        /// Plays a given effect ID in the given PAX.
        /// The PAX in question MUST be initialized or the game will NOT like it.
        /// </summary>
        /// <param name="PAX">The absolute memory location of the pointer to PAX file.</param>
        /// <param name="EffectID">The ID of the effect to play.</param>
        public static void PlayFromPAX(long PAX, int EffectID) => Variables.SharpHook[FUNC_PAXSTART].Execute(BSharpConvention.MicrosoftX64, PAX, EffectID, 1, 0, 0);

        /// <summary>
        /// Kills every effect that belong to the given PAX that's currently playing.
        /// The PAX in question MUST be initialized or the game will NOT like it.
        /// </summary>
        /// <param name="PAX">The absolute memory location of the pointer to PAX file.</param>
        // public static void KillAllPAX(long PAX) => Variables.SharpHook[0x2C6490].Execute(PAX);
    }
}
