using Binarysharp.MSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2FML
{
    public static class Variables
    {
        public static MemorySharp SharpHook;

        public static bool IS_TITLE =>
           Hypervisor.Read<uint>(ADDR_Area) == 0x00FFFFFF
        || Hypervisor.Read<uint>(ADDR_Area) == 0x00000101
        || Hypervisor.Read<uint>(ADDR_Title) == 0x00000001
        || Hypervisor.Read<uint>(ADDR_Reset) == 0x00000001;

        public static bool IS_LOADED =>
            Hypervisor.Read<byte>(ADDR_LoadFlag) == 0x01;

        public static bool IS_CUTSCENE =>
            Hypervisor.Read<byte>(ADDR_CutsceneFlag) != 0x00 &&
            Hypervisor.Read<byte>(ADDR_LoadFlag) == 0x01;

        public static bool IS_MOVIE =>
            Hypervisor.Read<byte>(ADDR_MovieFlag) == 0x01;

        public static BUTTON CONFIRM_BUTTON =>
            Hypervisor.Read<byte>(ADDR_Confirm) == 0x01 ? BUTTON.CIRCLE : BUTTON.CROSS;

        public static BUTTON REJECT_BUTTON =>
            Hypervisor.Read<byte>(ADDR_Confirm) == 0x01 ? BUTTON.CROSS : BUTTON.CIRCLE;

        public static bool IS_PRESSED(BUTTON Input) => 
            (Hypervisor.Read<BUTTON>(ADDR_Input) & Input) == Input;

        public static BATTLE_TYPE BATTLE_MODE =>
            Hypervisor.Read<BATTLE_TYPE>(ADDR_BattleFlag);

        public static ulong ADDR_Area = 0x0717008;
        public static ulong ADDR_Reset = 0x0ABAC5A;
        public static ulong ADDR_Input = 0x0BF3270;
        public static ulong ADDR_Title = 0x07169B4;
        public static ulong ADDR_Config = 0x09ADA54;
        public static ulong ADDR_Confirm = 0x0715382;
        public static ulong ADDR_LoadFlag = 0x09BA8D0;
        public static ulong ADDR_MenuFlag = 0x0717418;
        public static ulong ADDR_PlayerHP = 0x2A23598;
        public static ulong ADDR_MenuType = 0x0900724;
        public static ulong ADDR_SaveData = 0x09A98B0;
        public static ulong ADDR_Framerate = 0x071536E;
        public static ulong ADDR_PauseFlag = 0x09006B0;
        public static ulong ADDR_ActionExe = 0x2A5C996;
        public static ulong ADDR_MovieFlag = 0x2B561E8;
        public static ulong ADDR_IntroMenu = 0x0820200;
        public static ulong ADDR_MenuSelect = 0x0902FA0;
        public static ulong ADDR_ReactionID = 0x2A11162;
        public static ulong ADDR_ConfigMenu = 0x0820000;
        public static ulong ADDR_BattleFlag = 0x2A11404;
        public static ulong ADDR_FinishFlag = 0x0ABC66C;
        public static ulong ADDR_Viewspace2D = 0x08A09B8;
        public static ulong ADDR_Viewspace3D = 0x08A0990;
        public static ulong ADDR_SubMenuType = 0x07435D4;
        public static ulong ADDR_TitleSelect = 0x0B1D5E4;
        public static ulong ADDR_CommandMenu = 0x05B16A8;
        public static ulong ADDR_CommandFlag = 0x071740C;
        public static ulong ADDR_DialogSelect = 0x0902521;
        public static ulong ADDR_CutsceneFlag = 0x0B65210;
        public static ulong ADDR_Framelimiter = 0x0ABAC08;
        public static ulong ADDR_IntroSelection = 0x0820500;
        public static ulong ADDR_ControllerMode = 0x2B44A88;
        public static ulong ADDR_RenderResolution = 0x08A0980;

        public static ulong PINT_GameOver = 0x0BEF4A8;
        public static ulong PINT_SystemMSG = 0x2A11678;
        public static ulong PINT_ChildMenu = 0x2A11118;
        public static ulong PINT_ConfigMenu = 0x0BF0150;
        public static ulong PINT_SaveInformation = 0x079CB10;
        public static ulong PINT_GameOverOptions = 0x2A11360;
        public static ulong PINT_SubMenuOptionSelect = 0x0BEECD8;

        public enum MENU : int
        {
            CAMP = 0x00,

        }

        public enum BATTLE_TYPE : byte
        {
            PEACEFUL = 0x00,
            FIELD = 0x01,
            BOSS = 0x02
        };

        public enum BUTTON : ushort
        {
            NONE = 0x0000,
            SELECT = 0x0001,
            START = 0x0008,
            TRIANGLE = 0x1000,
            CIRCLE = 0x2000,
            CROSS = 0x4000,
            SQUARE = 0x8000,
            L1 = 0x0400,
            R1 = 0x0800,
            L2 = 0x0100,
            R2 = 0x0200,
            L3 = 0x0002,
            R3 = 0x0004,
            UP = 0x0010,
            RIGHT = 0x0020,
            DOWN = 0x0040,
            LEFT = 0x0080
        }

        public enum CONFIG : ushort
        {
            OFF = 0x0000,
            VIBRATION = 0x0001,
            AUTOSAVE_SILENT = 0x0002,
            AUTOSAVE_INDICATOR = 0x0004,
            NAVI_MAP = 0x0008,
            FIELD_CAM = 0x0010,
            RIGHT_STICK = 0x0020,
            COMMAND_KH2 = 0x0040,
            CAMERA_H = 0x0080,
            CAMERA_V = 0x0100,
            SUMMON_PARTIAL = 0x0200,
            SUMMON_FULL = 0x0400,
            AUDIO_PRIMARY = 0x0800,
            AUDIO_SECONDARY = 0x1000,
            PROMPT_CONTROLLER = 0x2000,
            MUSIC_VANILLA = 0x4000,
            HEARTLESS_VANILLA = 0x8000
        }
    }
}
