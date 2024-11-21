using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public static class Popups
    {
        static nint FUNC_STARTCAMP;
        static nint FUNC_SHOWPRIZE;
        static nint FUNC_SHOWINFORMATION;

        public static void PopupMenu() => Variables.SharpHook[FUNC_STARTCAMP].Execute(BSharpConvention.MicrosoftX64, 0, 0);

        public static void PopupInformation(ushort StringID)
        {
            if (!Variables.IS_TITLE && !Variables.IS_LOADED && !Variables.IS_CUTSCENE)
            {
                long _pointString = (long)Operators.FetchPointerMSG(Variables.PINT_SystemMSG, StringID);
                Variables.SharpHook[FUNC_SHOWINFORMATION].Execute(_pointString);
            }
        }

        public static void PopupPrize(ushort StringID)
        {
            if (!Variables.IS_TITLE && !Variables.IS_LOADED && !Variables.IS_CUTSCENE)
            {
                long _pointString = (long)Operators.FetchPointerMSG(Variables.PINT_SystemMSG, StringID);
                Variables.SharpHook[FUNC_SHOWPRIZE].Execute(_pointString);
            }
        }
    }
}
