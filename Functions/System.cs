using BSharpConvention = Binarysharp.MSharp.Assembly.CallingConvention.CallingConventions;


namespace KH2FML
{
    public class System
    {
        public static nint FUNC_MAPJUMP;
        public static nint FUNC_GIVEBACKYARD;
        public static nint FUNC_ITEMTABLEGET;
        public static nint FUNC_ITEMPARAMGET;
        public static nint FUNC_REDUCEBACKYARD;
        public static nint FUNC_GETNUMBACKYARD;

        /// <summary>
        /// Gets the absolute memory location of a item in "03system.bin/ITEM"!
        /// Extremely slow when used repeatedly! Cache your stuff when using this!
        /// </summary>
        /// <param name="ItemID">The ID of the Item to fetch.</param>
        /// <returns>The absolute memory location of the item information, "0x00" if not found.</returns>
        public static ulong FetchItem(short ItemID)
        {
            var _fetchItem = Variables.SharpHook[FUNC_ITEMTABLEGET].Execute(ItemID);

            if (_fetchItem == IntPtr.Zero)
                return 0x00;

            else
                return Hypervisor.MemoryOffset + (ulong)_fetchItem;
        }

        /// <summary>
        /// Gets the absolute memory location of the item parameters of the given item in "03system.bin/ITEM"!
        /// Extremely slow when used repeatedly! Cache your stuff when using this!
        /// </summary>
        /// <param name="ItemID">The ID of the Item to fetch the parameters of.</param>
        /// <returns>The absolute memory location of the item parameters, "0x00" if not found.</returns>
        public static ulong FetchItemParams(short ItemID)
        {
            var _fetchItem = FetchItem(ItemID);
            var _fetchParams = Variables.SharpHook[FUNC_ITEMPARAMGET].Execute((long)_fetchItem);

            if (_fetchParams == IntPtr.Zero)
                return 0x00;

            else
                return Hypervisor.MemoryOffset + (ulong)_fetchParams;
        }

        /// <summary>
        /// Gets the count of items with the given ID from the inventory.
        /// </summary>
        /// <param name="ItemID">Item ID to survey.</param>
        /// <returns>Amount of items in the inventory.</returns>
        public static int FetchInventory(short ItemID) => Variables.SharpHook[FUNC_GETNUMBACKYARD].Execute<int>(ItemID);

        /// <summary>
        /// Sets the amount of items with the given ID in the inventory.
        /// </summary>
        /// <param name="ItemID">Item ID to edit.</param>
        /// <param name="Amount">Amount to set.</param>
        public static void SetInventory(short ItemID, int Amount)
        {
            var _itemCount = FetchInventory(ItemID);

            if (Amount < _itemCount)
                Variables.SharpHook[FUNC_REDUCEBACKYARD].Execute(BSharpConvention.MicrosoftX64, ItemID, _itemCount - Amount);

            else
                Variables.SharpHook[FUNC_GIVEBACKYARD].Execute(BSharpConvention.MicrosoftX64, ItemID, Amount - _itemCount);
        }

        /// <summary>
        /// Warps to a given Area, whether it be an event, cutscene, or a room.
        /// Yes, this can indeed be used on ADDR_Area after writing to it.
        /// </summary>
        /// <param name="Area">The absolute memory location of Area Data.</param>
        /// <param name="Fade">The fade to use when warping.</param>
        public static void ExecuteWarp(long Area, FADE_TYPE Fade) => Variables.SharpHook[FUNC_MAPJUMP].Execute(BSharpConvention.MicrosoftX64, Area, Fade, 0, 0, 0);

        public enum FADE_TYPE : int
        {
            ROOM_FADE = 0x00,
            BLACKOUT = 0x01,
            WHITEOUT = 0x02
        }
    }
}
