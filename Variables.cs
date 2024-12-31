#define STEAM

using Binarysharp.MSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

        public static bool IS_EVENT =>
            Hypervisor.Read<ulong>(PINT_EventInfo) != 0x00 &&
           (Hypervisor.Read<uint>(Hypervisor.GetPointer64(PINT_EventInfo, [0x04]), true) == 0xCAFEEFAC ||
            Hypervisor.Read<uint>(Hypervisor.GetPointer64(PINT_EventInfo, [0x04]), true) == 0xEFACCAFE);

        public static bool IS_CUTSCENE =>
            Hypervisor.Read<ulong>(PINT_EventInfo) != 0x00 &&
            Hypervisor.Read<uint>(Hypervisor.GetPointer64(PINT_EventInfo, [0x04]), true) != 0xCAFEEFAC &&
            Hypervisor.Read<uint>(Hypervisor.GetPointer64(PINT_EventInfo, [0x04]), true) != 0xEFACCAFE;

        public static bool IS_PAUSED =>
            Hypervisor.Read<byte>(ADDR_PauseFlag) == 0x00;

        public static bool IS_MOVIE =>
            Hypervisor.Read<byte>(ADDR_MovieFlag) == 0x01;

        #if STEAM
        public static BUTTON CONFIRM_BUTTON =>
                Hypervisor.Read<byte>(ADDR_Confirm) == 0x01 ? BUTTON.CIRCLE : BUTTON.CROSS;

            public static BUTTON REJECT_BUTTON =>
                Hypervisor.Read<byte>(ADDR_Confirm) == 0x01 ? BUTTON.CROSS : BUTTON.CIRCLE;
        #elif EPIC     
            public static BUTTON CONFIRM_BUTTON =>
                Hypervisor.Read<byte>(ADDR_Confirm) == 0x00 ? BUTTON.CIRCLE : BUTTON.CROSS;

            public static BUTTON REJECT_BUTTON =>
                Hypervisor.Read<byte>(ADDR_Confirm) == 0x00 ? BUTTON.CROSS : BUTTON.CIRCLE;
        #endif

        public static bool IS_PRESSED(BUTTON Input) => 
            (Hypervisor.Read<BUTTON>(ADDR_Input) & Input) == Input;

        public static BATTLE_TYPE BATTLE_MODE =>
            Hypervisor.Read<BATTLE_TYPE>(ADDR_BattleFlag);

        #if STEAM
        public static ulong ADDR_Area = 0x0717008;
        public static ulong ADDR_Mare = 0x0000000;
        public static ulong ADDR_Reset = 0x0ABAC5A;
        public static ulong ADDR_Input = 0x0BF3270;
        public static ulong ADDR_Title = 0x07169B4;
        public static ulong ADDR_Config = 0x09ADA54;
        public static ulong ADDR_Confirm = 0x0715382;
        public static ulong ADDR_LoadFlag = 0x09BA8D0;
        public static ulong ADDR_MenuFlag = 0x09006B0;
        public static ulong ADDR_MenuType = 0x0900724;
        public static ulong ADDR_SaveData = 0x09A98B0;
        public static ulong ADDR_MagicLV1 = 0x09ACE44;
        public static ulong ADDR_MagicLV2 = 0x09ACE7F;
        public static ulong ADDR_ContData = 0x07A0000;
        public static ulong ADDR_FadeValue = 0x0ABB3C7;
        public static ulong ADDR_Framerate = 0x071536E;
        public static ulong ADDR_PauseFlag = 0x0717418;
        public static ulong ADDR_MovieFlag = 0x2B561E8;
        public static ulong ADDR_IntroMenu = 0x0820200;
        public static ulong ADDR_VendorMem = 0x2A25378;
        public static ulong ADDR_PromptType = 0x0715380;
        public static ulong ADDR_MenuSelect = 0x0902FA0;
        public static ulong ADDR_ReactionID = 0x2A11162;
        public static ulong ADDR_ConfigMenu = 0x0820000;
        public static ulong ADDR_BattleFlag = 0x2A11404;
        public static ulong ADDR_FinishFlag = 0x0ABC66C;
        public static ulong ADDR_MagicIndex = 0x2A1073C;
        public static ulong ADDR_PlayerStats = 0x2A23598;
        public static ulong ADDR_TaskManager = 0x0716A38;
        public static ulong ADDR_CampBitwise = 0x0BEEC20;
        public static ulong ADDR_Viewspace2D = 0x08A09B8;
        public static ulong ADDR_Viewspace3D = 0x08A0990;
        public static ulong ADDR_SubMenuType = 0x07435D4;
        public static ulong ADDR_TitleSelect = 0x0B1D5E4;
        public static ulong ADDR_CommandMenu = 0x05B16A8;
        public static ulong ADDR_CommandFlag = 0x071740C;
        public static ulong ADDR_DialogSelect = 0x0902521;
        public static ulong ADDR_CutsceneMode = 0x0B65210;
        public static ulong ADDR_Framelimiter = 0x0ABAC08;
        public static ulong ADDR_ObjentryBase = 0x2A254D0;
        public static ulong ADDR_LoadedPicture = 0x0743E94;
        public static ulong ADDR_LimitShortcut = 0x05C9678;
        public static ulong ADDR_MagicCommands = 0x2A11188;
        public static ulong ADDR_IntroSelection = 0x0820500;
        public static ulong ADDR_ControllerMode = 0x2B44A88;
        public static ulong ADDR_RenderResolution = 0x08A0980;

        public static ulong DATA_BGMPath = 0x05B4C74;
        public static ulong DATA_PAXPath = 0x05C8590;
        public static ulong DATA_ANBPath = 0x05B8FB0;
        public static ulong DATA_EVTPath = 0x05B9020;
        public static ulong DATA_BTLPath = 0x05C5E48;
        public static ulong DATA_GMIPath = 0x05B5818;
        public static ulong PINT_Camp2LD = 0x09076D0;
        public static ulong PINT_GameOver = 0x0BEF4A8;
        public static ulong PINT_SystemMSG = 0x2A11678;
        public static ulong PINT_ChildMenu = 0x2A11118;
        public static ulong PINT_EnemyInfo = 0x2A0CD70;
        public static ulong PINT_EventInfo = 0x2A11478;
        public static ulong PINT_ActionEXE = 0x2A161E8;
        public static ulong PINT_PartyLimit = 0x2A24CC0;
        public static ulong PINT_ConfigMenu = 0x0BF0150;
        public static ulong PINT_SaveInformation = 0x079CAD0;
        public static ulong PINT_GameOverOptions = 0x2A11360;
        public static ulong PINT_SubMenuOptionSelect = 0x0BEECD8;

#elif EPIC
        public static ulong ADDR_Area = 0x0716DF8;
        public static ulong ADDR_Mare = 0x079C7FC;
        public static ulong ADDR_Reset = 0x0ABA6DA;
        public static ulong ADDR_Input = 0x29FAE40;
        public static ulong ADDR_Title = 0x07167A4;
        public static ulong ADDR_Config = 0x09AD4D4;
        public static ulong ADDR_Confirm = 0x0714E02;
        public static ulong ADDR_LoadFlag = 0x09BA350;
        public static ulong ADDR_MenuFlag = 0x0900150;
        public static ulong ADDR_PlayerHP = 0x2A23018;
        public static ulong ADDR_MenuType = 0x09001C4;
        public static ulong ADDR_SaveData = 0x09A9330;
        public static ulong ADDR_MagicLV1 = 0x09AC8C4;
        public static ulong ADDR_MagicLV2 = 0x09AC8FF;
        public static ulong ADDR_ContData = 0x07A0000;
        public static ulong ADDR_FadeValue = 0x0ABAE47;
        public static ulong ADDR_Framerate = 0x08CBD0A;
        public static ulong ADDR_PauseFlag = 0x0717208;
        public static ulong ADDR_MovieFlag = 0x2B56028;
        public static ulong ADDR_IntroMenu = 0x0820200;
        public static ulong ADDR_VendorMem = 0x2A24DF8;
        public static ulong ADDR_PromptType = 0x0715380;
        public static ulong ADDR_MenuSelect = 0x0902A40;
        public static ulong ADDR_ReactionID = 0x2A10BE2;
        public static ulong ADDR_ConfigMenu = 0x0820000;
        public static ulong ADDR_BattleFlag = 0x2A10E84;
        public static ulong ADDR_FinishFlag = 0x0ABC0EC;
        public static ulong ADDR_MagicIndex = 0x2A101BC;
        public static ulong ADDR_CampBitwise = 0x0BEE6A0;
        public static ulong ADDR_Viewspace2D = 0x08A0BE8;
        public static ulong ADDR_Viewspace3D = 0x08A0BC0;
        public static ulong ADDR_SubMenuType = 0x0743354;
        public static ulong ADDR_TitleSelect = 0x0B1D064;
        public static ulong ADDR_CommandMenu = 0x05B1868;
        public static ulong ADDR_CommandFlag = 0x07171FC;
        public static ulong ADDR_DialogSelect = 0x0902480;
        public static ulong ADDR_CutsceneMode = 0x0B64C90;
        public static ulong ADDR_CutsceneFlag = 0x07281C0;
        public static ulong ADDR_Framelimiter = 0x0ABA688;
        public static ulong ADDR_ObjentryBase = 0x2A24F50;
        public static ulong ADDR_LoadedPicture = 0x0743C14;
        public static ulong ADDR_LimitShortcut = 0x05C97E8;
        public static ulong ADDR_MagicCommands = 0x2A10C08;
        public static ulong ADDR_IntroSelection = 0x0820500;
        public static ulong ADDR_ControllerMode = 0x2B448C8;
        public static ulong ADDR_RenderResolution = 0x08A0BB0;

        public static ulong DATA_BGMPath = 0x05B4E34;
        public static ulong DATA_PAXPath = 0x05C8700;
        public static ulong DATA_ANBPath = 0x05B9140;
        public static ulong DATA_EVTPath = 0x05B91B0;
        public static ulong DATA_BTLPath = 0x05C5FB8;
        public static ulong DATA_GMIPath = 0x05B59D0;

        public static ulong PINT_Camp2LD = 0x0907170;
        public static ulong PINT_GameOver = 0x0BEEF28;
        public static ulong PINT_SystemMSG = 0x2A110F8;
        public static ulong PINT_ChildMenu = 0x2A10B98;
        public static ulong PINT_EnemyInfo = 0x2A0C7F0;
        public static ulong PINT_EventInfo = 0x2A10EF8;
        public static ulong PINT_ActionEXE = 0x2A15C68;
        public static ulong PINT_PartyLimit = 0x2A24740;
        public static ulong PINT_ConfigMenu = 0x0BEFBD0;
        public static ulong PINT_SaveInformation = 0x2B0C240;
        public static ulong PINT_GameOverOptions = 0x2A10DE0;
        public static ulong PINT_SubMenuOptionSelect = 0x0BEE758;
#endif

        public static string SIGN_ShowInformation = "40 53 48 83 EC 20 48 8B D9 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ?? 48 8B D3";
        public static string SIGN_ShowObatined = "40 53 48 83 EC 20 48 8B 15 ?? ?? ?? ?? 48 8B D9 4C 63 82 ?? ?? ?? ??";
        public static string SIGN_PlaySFX = "48 83 EC ?? 44 8B C2 C7 44 24 20 ?? ?? ?? ??";
        public static string SIGN_SetMenuType = "89 0D ?? ?? ?? ?? C7 05 ?? ?? ?? ?? FF FF FF FF 89 15 ?? ?? ?? ??";
        public static string SIGN_SetSLWarning = "40 57 48 83 EC 50 8B F9";
        public static string SIGN_ShowSLWarning = "48 89 5C 24 08 57 48 83 EC 40 8B F9 48 8B 0D ?? ?? ?? ??";
        public static string SIGN_SetCampWarning = "48 89 5C 24 08 57 48 83 EC 50 8B F9 8B DA";
        public static string SIGN_ShowCampWarning = "40 55 48 83 EC 50 44 8B 0D ?? ?? ?? ??";
        public static string SIGN_ExecuteCampMenu = "40 56 41 56 41 57 48 83 EC 20 45 32 FF 44 8B F2 44 38 3D ?? ?? ?? ??";
        public static string SIGN_StopBGM = "40 53 48 83 EC 20 48 83 3D ?? ?? ?? ?? 00 0F 84 ?? ?? ?? ?? 48 8B 1D ?? ?? ?? ??";
        public static string SIGN_MapJump = "48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 80 3D ?? ?? ?? ?? 00 41 0F B6 E9";
        public static string SIGN_SetFadeOff = "48 83 EC 28 85 C9 79 0F 0F BA F1 1F 89 0D ?? ?? ?? ?? 48 83 C4 28 C3 89 0D ?? ?? ?? ?? 81 E1 FF FF FF 3F 0F 84 ?? ?? ?? ?? 83 E9 01";
        public static string SIGN_FindFile = "48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 45 33 C0 4D 85 C0 75 09 4C 8B 05 ?? ?? ?? ??";
        public static string SIGN_GetFileSize = "40 53 48 81 EC 30 01 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 20 01 00 00 48 8D 15 ?? ?? ?? ??";
        public static string SIGN_ShortcutUpdate = "48 83 EC 28 E8 97 F9 FF FF 48 83 C4 28 E9 DE 02 00 00";
        public static string SIGN_ConfigUpdate = "40 53 55 56 57 41 54 41 55 41 56 41 57 48 83 EC 58 E8 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ?? 4C 8B F8 E8 ?? ?? ?? ??";
        public static string SIGN_SelectUpdate = "48 83 EC 28 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 63 D0 48 8B 05 ?? ?? ?? ?? 48 0F BE 0C 02";
        public static string SIGN_FadeCampWarning = "48 83 EC 28 85 C9 BA 0B 00 00 00 48 8B 0D ?? ?? ?? ?? B8 08 00 00 00 0F 44 D0 E8 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ??";
        public static string SIGN_ResetCommandMenu = "40 56 48 83 EC 30 8B 35 ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0 0F 85 ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0";
        public static string SIGN_ObjentryGet = "40 53 48 83 EC 20 8B C1 8B D1 25 FF FF FF 0F 3D EE 03 00 00 0F 87 ?? ?? ?? ?? 0F 84 ?? ?? ?? ??";
        public static string SIGN_MessageGetData = "48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 41 56 41 57 48 83 EC 50 45 33 F6 48 63 E9 33 F6 48 8D 3D ?? ?? ?? ?? 4C 8D 3D ?? ?? ?? ??";
        public static string SIGN_ItemTableGet = "48 8B 15 ?? ?? ?? ?? 45 33 C0 44 8B 4A 04 48 8D 42 08 45 85 C9 7E 13 0F B7 10 3B D1 74 0E";
        public static string SIGN_ItemParamGet = "0F B6 41 02 3C 02 72 1A 3C 0D 76 0D 3C 0F 77 12 0F B7 49 04 E9 ?? ?? ?? ??";
        public static string SIGN_ItemUpdate = "48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 41 54 41 55 41 56 41 57 48 83 EC 40 45 32 E4 E8 ?? ?? ?? ??";
        public static string SIGN_TaskManagerCreate = "40 53 48 83 EC 20 E8 ?? ?? ?? ?? 45 33 C0 48 8B C8 48 8B D8 4C 8B 08 41 8D 50 48 41 FF 51 08 48 8D 0D ?? ?? ?? ??";
        public static string SIGN_TaskManagerFree = "48 89 5C 24 18 57 48 83 EC 20 48 8B 59 10 48 8B F9 48 85 DB 0F 84 ?? ?? ?? ?? 48 89 6C 24 30 33 ED 48 89 74 24 38 48 85 DB 75 0A 48 8B 77 10 48 8B 43 78 EB 07 48 8B 73 78 48 8B C6 48 3B 5F 40 75 11 48 85 DB 75 04 48 8B 47 10 48 89 47 40 48 8B 43 78 48 8B 8B 80 00 00 00 48 85 C9 75 06 48 89 47 10 EB 04 48 89 41 78 48 8B 4B 78 48 8B 83 80 00 00 00 48 85 C9 75 06 48 89 47 18 EB 07 48 89 81 80 00 00 00 48 8B 43 68 48 89 AB 80 00 00 00 48 89 6B 78 48 85 C0 74 05 48 8B CB FF D0 48 8B 53 70 48 85 D2 74 22 48 8B 0A 48 85 C9 74 10 E8 5B 51 FE FF 48 8B 43 70 48 89 28 48 8B 53 70 48 8B 4B 58 48 8B 01 FF 50 10 48 8B 4B 58 48 8B D3 48 8B 01 FF 50 10 48 8B DE 48 85 F6 0F 85 43 FF FF FF 48 8B 74 24 38 48 8B 6C 24 30 48 8B 5C 24 40 48 83 C4 20 5F";
        public static string SIGN_GetNumberBackyard = "48 83 EC 28 E8 ?? ?? ?? ?? F6 40 03 01 74 2A 0F B7 48 12 8B C1 8B D1 48 C1 E8 05 48 8D 0D ?? ?? ?? ?? 83 E2 1F 8B 8C 81 C0 36 00 00";
        public static string SIGN_ReduceBackyard = "40 53 48 83 EC 20 8B DA E8 ?? ?? ?? ?? 48 8D 15 5C 4E 5E 00 F6 40 03 01 0F B7 48 12 0F B7 C9 74 22 8B C1 83 E1 1F 48 C1 E8 05 48 8D 14 82 B8 01 00 00 00 D3 E0 F7 D0 21 82 C0 36 00 00";
        public static string SIGN_GiveBackyard = "48 89 5C 24 10 48 89 6C 24 18 56 57 41 56 48 83 EC 20 4C 89 7C 24 40 8B D9 44 8B FA E8 ?? ?? ?? ??";
        public static string SIGN_StartPAX = "48 83 EC 38 4C 8D 91 80 00 00 00 49 83 3A 00 75 07 33 C0 48 83 C4 38 C3 48 8B 44 24 60";
        public static string SIGN_SslVsbPlay = "48 83 EC 28 0F 57 D2 45 85 C0 74 10 66 41 0F 6E D0 0F 5B D2 F3 0F 59 15 ?? ?? ?? ?? 45 33 C0 4C 8D 0D ?? ?? ?? ?? 66 66 0F 1F 84 00 00 00 00 00";
        public static string HFIX_ConfigFirst = "40 53 48 83 EC 20 0F B6 D9 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8B 1D ?? ?? ?? ??";
        public static string HFIX_ConfigSecond = "48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 41 54 41 55 41 56 41 57 48 81 EC 80 00 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 70 E8 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ??";
        public static string HFIX_ConfigThird = "48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 30 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B F8 E8 ?? ?? ?? ?? 8B F0 83 F8 02 0F 85 ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B D8 E8 ?? ?? ?? ?? 84 C0 75 59 48 8B 0D ?? ?? ?? ??";
        public static string HFIX_ConfigFourth = "48 89 5C 24 08 57 48 83 EC 20 8B FA 8B D9 E8 ?? ?? ?? ?? 8D 0C 3B 44 8D 04 9D 00 00 00 00";
        public static string HFIX_ConfigFifth = "48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 30 E8 ?? ?? ?? ?? 45 33 C0 33 C9 41 8D 50 FF E8 ?? ?? ?? ??";
        public static string HFIX_ConfigSixth = "40 53 55 56 57 41 54 41 55 41 56 41 57 48 83 EC 58 E8 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ?? 4C 8B F8 E8 ?? ?? ?? ?? 41 BD ?? ?? ?? ??";
        public static string HFIX_ConfigSeventh = "48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 20 33 DB 48 8D 35 ?? ?? ?? ?? 33 FF 66 0F 1F 44 00 00 83 FB 06 0F 87 B5 00 00 00";

        public static string HFIX_IntroFirst = "48 89 5C 24 18 55 56 57 41 54 41 55 41 56 41 57 48 83 EC 50 48 8B 05 ?? ?? ?? ??";
        public static string HFIX_IntroSecond = "48 89 5C 24 20 55 56 57 41 54 41 55 41 56 41 57 48 83 EC 50 4C 8B 3D ?? ?? ?? ??";
        public static string HFIX_IntroThird = "40 53 48 83 EC 20 48 8B 0D ?? ?? ?? ?? 48 81 C1 40 04 00 00 E8 ?? ?? ?? ??";
        public static string HFIX_IntroFourth = "48 83 EC 38 48 8B 0D ?? ?? ?? ?? 48 89 5C 24 40 48 85 C9 74 27 E8 ?? ?? ?? ??";
        public static string HFIX_IntroFifth = "48 89 5C 24 10 57 48 83 EC 40 48 8B 05 ?? ?? ?? ?? 80 78 0C 00 74 18 E8 ?? ?? ?? ??";
        public static string HFIX_IntroSixth = "48 89 5C 24 20 56 57 41 56 48 83 EC 30 E8 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ??";
        public static string HFIX_IntroSeventh = "48 83 EC 28 E8 ?? ?? ?? ?? 8B 15 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ??";

        public static List<ulong> HFIX_ConfigOffsets = new List<ulong>();
        public static List<ulong> HFIX_IntroOffsets = new List<ulong>();

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
            NAVI_MAP = 0x0008,
            FIELD_CAM = 0x0010,
            RIGHT_STICK = 0x0020,
            COMMAND_KH2 = 0x0040,
            CAMERA_H = 0x0080,
            CAMERA_V = 0x0100,
            SUMMON_PARTIAL = 0x0200,
            SUMMON_FULL = 0x0400
        }
    }
}
