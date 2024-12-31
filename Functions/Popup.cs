using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;

namespace KH2FML
{
    public static class Popup
    {
        public static nint FUNC_STARTCAMP;
        public static nint FUNC_SHOWPRIZE;
        public static nint FUNC_SHOWINFORMATION;

        /// <summary>
        /// Activates a menu popup. Could be any of the "CAMP" style menus.
        /// Should not be used when a menu is already up, fucks shit up.
        /// </summary>
        /// <param name="Menu">The menu to popup.</param>
        /// <param name="Type">The type of the menu.</param>
        public static void PopupMenu(MENU Menu, int Type) => Variables.SharpHook[FUNC_STARTCAMP].Execute(BSharpConvention.MicrosoftX64, Menu, Type);

        /// <summary>
        /// Activates an information prompt with the given string ID.
        /// If called when a prompt is already present, causes the prompt to reset.
        /// In any conflicting scenario: "SYS.bar" has priority over "[WORLD].bar"!
        /// </summary>
        /// <param name="StringID">The ID of the String to display.</param>
        public static void PopupInformation(short StringID)
        {
            if (!Variables.IS_TITLE && !Variables.IS_LOADED && !Variables.IS_CUTSCENE)
            {
                long _pointString = (long)Text.GetStringPointer(StringID);
                Variables.SharpHook[FUNC_SHOWINFORMATION].Execute(_pointString);
            }
        }

        /// <summary>
        /// Activates an prize prompt with the given string ID.
        /// If called when a prompt is already present, it will be queued for display.
        /// In any conflicting scenario: "SYS.bar" has priority over "[WORLD].bar"!
        /// </summary>
        /// <param name="StringID">The ID of the String to display.</param>
        public static void PopupPrize(short StringID)
        {
            if (!Variables.IS_TITLE && !Variables.IS_LOADED && !Variables.IS_CUTSCENE)
            {
                long _pointString = (long)Text.GetStringPointer(StringID);
                Variables.SharpHook[FUNC_SHOWPRIZE].Execute(_pointString);
            }
        }

        public enum MENU : int
        {
            CAMP = 0x00
        }
    }
}
