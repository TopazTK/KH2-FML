namespace KH2FML
{
    public static class Sound
    {
        static nint FUNC_PLAYSFX;
        static nint FUNC_KILLBGM;

        public static void PlaySFX(int SoundID) => Variables.SharpHook[FUNC_PLAYSFX].Execute(SoundID);
        public static void KillBGM() => Variables.SharpHook[FUNC_KILLBGM].Execute();
    }
}
