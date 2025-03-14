using System.Diagnostics;

using Binarysharp.MSharp;

namespace KH2FML
{
    public partial class Entry
    {
        public static void Initialize(Process Input)
        {
            Hypervisor.AttachProcess(Input);
            Variables.SharpHook = new MemorySharp(Hypervisor.Process);

            Terminal.Log("Locating all of the System Functions...", 1);
            var _taskSystem = Task.Run(() =>
            {
                Axa.FUNC_SUSPENDTASK = Hypervisor.FindSignature<IntPtr>("48 83 EC 28 E8 ?? ?? ?? ?? C6 80 08 03 00 00 01 48 83 C4 28 C3");
                Axa.FUNC_RESUMETASK = Hypervisor.FindSignature<IntPtr>("48 83 EC 28 E8 ?? ?? ?? ?? C6 80 08 03 00 00 00 48 83 C4 28 C3");
                Axa.FUNC_SOUNDPAUSE = Hypervisor.FindSignature<IntPtr>("40 56 57 41 56 48 83 EC 30 48 C7 44 24 20 FE FF FF FF 48 89 5C 24 50 48 89 6C 24 58 41 8B F0 8B EA 0F B6 F9 4C 8D 35 ?? ?? ?? ??");

                Shisutemu.FUNC_RECOVER = Hypervisor.FindSignature<IntPtr>("48 83 EC 28 85 C9 0F 84 88 00 00 00 FF C9 83 F9 0B 77 32 48 63 C1 48 8D 15 ?? ?? ?? ??");
                Shisutemu.FUNC_MAPJUMP = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 80 3D ?? ?? ?? ?? 00 41 0F B6 E9");
                Shisutemu.FUNC_OBJENTRYGET = Hypervisor.FindSignature<IntPtr>("40 53 48 83 EC 20 8B C1 8B D1 25 FF FF FF 0F 3D EE 03 00 00 0F 87 ?? ?? ?? ?? 0F 84 ?? ?? ?? ??");
                Shisutemu.FUNC_ITEMTABLEGET = Hypervisor.FindSignature<IntPtr>("48 8B 15 ?? ?? ?? ?? 45 33 C0 44 8B 4A 04 48 8D 42 08 45 85 C9 7E 13 0F B7 10 3B D1 74 0E");
                Shisutemu.FUNC_ITEMPARAMGET = Hypervisor.FindSignature<IntPtr>("0F B6 41 02 3C 02 72 1A 3C 0D 76 0D 3C 0F 77 12 0F B7 49 04 E9 ?? ?? ?? ??");
                Shisutemu.FUNC_GIVEBACKYARD = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 10 48 89 6C 24 18 56 57 41 56 48 83 EC 20 4C 89 7C 24 40 8B D9 44 8B FA E8 ?? ?? ?? ??");
                Shisutemu.FUNC_REDUCEBACKYARD = Hypervisor.FindSignature<IntPtr>("40 53 48 83 EC 20 8B DA E8 ?? ?? ?? ?? 48 8D 15 5C 4E 5E 00 F6 40 03 01 0F B7 48 12 0F B7 C9 74 22 8B C1 83 E1 1F 48 C1 E8 05 48 8D 14 82 B8 01 00 00 00 D3 E0 F7 D0 21 82 C0 36 00 00");
                Shisutemu.FUNC_GETNUMBACKYARD = Hypervisor.FindSignature<IntPtr>("48 83 EC 28 E8 ?? ?? ?? ?? F6 40 03 01 74 2A 0F B7 48 12 8B C1 8B D1 48 C1 E8 05 48 8D 0D ?? ?? ?? ?? 83 E2 1F 8B 8C 81 C0 36 00 00");
            });

            Terminal.Log("Locating all of the Visual Functions...", 1);
            var _taskVisual = Task.Run(() =>
            {
                Popup.FUNC_SHOWHELP = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 48 89 7C 24 20 41 56 48 83 EC 20 8B F1 8B DA B1 01 E8 ?? ?? ?? ??");
                Popup.FUNC_SHOWPRIZE = Hypervisor.FindSignature<IntPtr>("40 53 48 83 EC 20 48 8B 15 ?? ?? ?? ?? 48 8B D9 4C 63 82 ?? ?? ?? ??");
                Popup.FUNC_STARTCAMP = Hypervisor.FindSignature<IntPtr>("40 56 41 56 41 57 48 83 EC 20 45 32 FF 44 8B F2 44 38 3D ?? ?? ?? ??");
                Popup.FUNC_SHOWINFORMATION = Hypervisor.FindSignature<IntPtr>("40 53 48 83 EC 20 48 8B D9 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ?? 48 8B D3");

                Dialog.FUNC_SETMENUMODE = Hypervisor.FindSignature<IntPtr>("89 0D ?? ?? ?? ?? C7 05 ?? ?? ?? ?? FF FF FF FF 89 15 ?? ?? ?? ??");
                Dialog.FUNC_SETCAMPWARNING = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 08 57 48 83 EC 50 8B F9 8B DA");
                Dialog.FUNC_SHOWCAMPWARNING = Hypervisor.FindSignature<IntPtr>("40 55 48 83 EC 50 44 8B 0D ?? ?? ?? ??");
                Dialog.FUNC_FADECAMPWARNING = Hypervisor.FindSignature<IntPtr>("48 83 EC 28 85 C9 BA 0B 00 00 00 48 8B 0D ?? ?? ?? ?? B8 08 00 00 00 0F 44 D0 E8 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ??");
            });

            Terminal.Log("Locating all of the I/O Functions...", 1);
            var _taskIO = Task.Run(() =>
            {
                Effect.FUNC_PAXSTART = Hypervisor.FindSignature<IntPtr>("48 83 EC 38 4C 8D 91 80 00 00 00 49 83 3A 00 75 07 33 C0 48 83 C4 38 C3 48 8B 44 24 60");

                IO.FUNC_AREAALLOC = Hypervisor.FindSignature<IntPtr>("48 8B D1 45 33 C0 48 8B 0D ?? ?? ?? ?? 48 8B 01 48 FF 60 08 CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 08 57 48 83 EC 20 48 8B FA");
                IO.FUNC_CACHEREADREQ = Hypervisor.FindSignature<IntPtr>("40 55 56 57 48 81 EC 30 01 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 20 01 00 00 49 8B F0 8B EA 48 8B F9 E8 ?? ?? ?? ??");
                IO.FUNC_CACHEFIND = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 45 33 C0 4D 85 C0 75 09 4C 8B 05 ?? ?? ?? ??");
                IO.FUNC_FILEREAD = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 18 48 89 74 24 20 57 48 81 EC 30 01 00 00 48 8B 05 ?? ?? ?? ??");
                IO.FUNC_GETFILESIZE = Hypervisor.FindSignature<IntPtr>("40 53 48 81 EC 30 01 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 20 01 00 00 48 8D 15 ?? ?? ?? ??");
                IO.FUNC_FREETASKMGR = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 18 57 48 83 EC 20 48 8B 59 10 48 8B F9 48 85 DB 0F 84 ?? ?? ?? ?? 48 89 6C 24 30 33 ED 48 89 74 24 38 48 85 DB 75 0A 48 8B 77 10 48 8B 43 78 EB 07 48 8B 73 78 48 8B C6 48 3B 5F 40 75 11 48 85 DB 75 04 48 8B 47 10 48 89 47 40 48 8B 43 78 48 8B 8B 80 00 00 00 48 85 C9 75 06 48 89 47 10 EB 04 48 89 41 78 48 8B 4B 78 48 8B 83 80 00 00 00 48 85 C9 75 06 48 89 47 18 EB 07 48 89 81 80 00 00 00 48 8B 43 68 48 89 AB 80 00 00 00 48 89 6B 78 48 85 C0 74 05 48 8B CB FF D0 48 8B 53 70 48 85 D2 74 22 48 8B 0A 48 85 C9 74 10 E8 5B 51 FE FF 48 8B 43 70 48 89 28 48 8B 53 70 48 8B 4B 58 48 8B 01 FF 50 10 48 8B 4B 58 48 8B D3 48 8B 01 FF 50 10 48 8B DE 48 85 F6 0F 85 43 FF FF FF 48 8B 74 24 38 48 8B 6C 24 30 48 8B 5C 24 40 48 83 C4 20 5F");
                IO.FUNC_CREATETASKMGR = Hypervisor.FindSignature<IntPtr>("40 53 48 83 EC 20 E8 ?? ?? ?? ?? 45 33 C0 48 8B C8 48 8B D8 4C 8B 08 41 8D 50 48 41 FF 51 08 48 8D 0D ?? ?? ?? ??");

                Text.FUNC_MESSAGEGETDATA = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 41 56 41 57 48 83 EC 50 45 33 F6 48 63 E9 33 F6 48 8D 3D ?? ?? ?? ?? 4C 8D 3D ?? ?? ?? ??");

                Sound.FUNC_PLAYSFX = Hypervisor.FindSignature<IntPtr>("48 83 EC ?? 44 8B C2 C7 44 24 20 ?? ?? ?? ??");
                Sound.FUNC_PLAYVSB = Hypervisor.FindSignature<IntPtr>("48 83 EC 28 0F 57 D2 45 85 C0 74 10 66 41 0F 6E D0 0F 5B D2 F3 0F 59 15 ?? ?? ?? ?? 45 33 C0 4C 8D 0D ?? ?? ?? ?? 66 66 0F 1F 84 00 00 00 00 00");
                Sound.FUNC_KILLBGM = Hypervisor.FindSignature<IntPtr>("40 53 48 83 EC 20 48 83 3D ?? ?? ?? ?? 00 0F 84 ?? ?? ?? ?? 48 8B 1D ?? ?? ?? ??");

                MallocKH.FUNC_CREATEALLOC = Hypervisor.FindSignature<IntPtr>("48 89 5C 24 08 57 48 83 EC 20 48 8D 59 0F 33 FF 48 83 E3 F0 4C 8D 0C 11 4C 8D 83 80 00 00 00 49 8D 40 30 4C 3B C8 0F 86 8E 00 00 00 48 39 3D ?? ?? ?? ??");

                ShopFace.FIRST_OFFSET = Hypervisor.FindSignature<ulong>("40 56 48 81 EC C0 00 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 A0 00 00 00 48 8B 0D ?? ?? ?? ?? 33 F6 E8 ?? ?? ?? ??");
                ShopFace.SECOND_OFFSET = Hypervisor.FindSignature<ulong>("48 89 5C 24 08 57 48 83 EC 20 48 8B DA 8B F9 48 8B CB 48 8D 15 ?? ?? ?? ?? E8 ?? ?? ?? ??");
                ShopFace.THIRD_OFFSET = Hypervisor.FindSignature<ulong>("40 57 48 81 EC B0 00 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 A0 00 00 00 48 8B 0D ?? ?? ?? ?? 33 FF E8 ?? ?? ?? ??");

                Variables.HFIX_HelpimageOffsets.AddRange(
                [
                    Hypervisor.FindSignature<ulong>("40 53 57 41 57 48 83 EC 50 48 8B 0D ?? ?? ?? ?? 48 81 C1 40 05 00 00 E8 ?? ?? ?? ??"),
                    Hypervisor.FindSignature<ulong>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 48 89 7C 24 20 41 56 48 83 EC 20 8B F1 E8 ?? ?? ?? ?? 48 8B 15 ?? ?? ?? ?? 48 8D 0D ?? ?? ?? ?? 48 8B E8 4C 8D 35 E7 59 25 00 33 C0"),
                    Hypervisor.FindSignature<ulong>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 48 63 FA 48 8D 2D ?? ?? ?? ??"),
                    Hypervisor.FindSignature<ulong>("48 83 EC 28 4C 8B 05 ?? ?? ?? ?? 49 0F BE 50 0E 41 0F BE 48 0D 8D 42 01 3B C1 7C 23 48 8B 0D DD 8F 88 00 BA 14 00 00 00"),
                    Hypervisor.FindSignature<ulong>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 48 89 7C 24 20 41 56 48 83 EC 20 40 32 F6 E8 ?? ?? ?? ??"),
                    Hypervisor.FindSignature<ulong>("48 89 5C 24 10 48 89 6C 24 18 48 89 74 24 20 57 48 81 EC D0 00 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 C0 00 00 00 33 C9 E8 ?? ?? ?? ??"),
                    Hypervisor.FindSignature<ulong>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 48 89 7C 24 20 41 56 48 83 EC 20 E8 ?? ?? ?? ?? 0F B7 0D ?? ?? ?? ?? 4C 8D 35 ?? ?? ?? ?? 33 FF"),
                    Hypervisor.FindSignature<ulong>("48 89 5C 24 08 57 48 83 EC 20 8B F9 48 8D 1D ?? ?? ?? ?? 83 FF FF 74 08 0F B6 43 02 3B C7 75 0C 0F B7 0B E8 C8 34 03 00 84 C0 75 17"),
                    Hypervisor.FindSignature<ulong>("48 89 5C 24 08 57 48 83 EC 50 48 8B 0D ?? ?? ?? ?? 48 81 C1 20 03 00 00 E8 ?? ?? ?? ??")
                ]);
            });

            Terminal.Log("Locating all of the Menu Functions...", 1);
            var _taskMenu = Task.Run(() =>
            {
                Variables.HFIX_ConfigOffsets.AddRange(
                [
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_ConfigFirst),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_ConfigSecond),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_ConfigThird),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_ConfigFourth),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_ConfigFifth),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_ConfigSixth),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_ConfigSeventh)
                ]);

                Variables.HFIX_IntroOffsets.AddRange(
                [
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_IntroFirst),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_IntroSecond),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_IntroThird),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_IntroFourth),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_IntroFifth),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_IntroSixth),
                    Hypervisor.FindSignature<ulong>(Variables.HFIX_IntroSeventh)
                ]);
            });

            Task.WaitAll(_taskSystem, _taskVisual, _taskIO, _taskMenu);

            Terminal.Log("Creating an Allocator at the memory address 0x7B0000...", 1);
            Variables.MemoryKH = new MallocKH(0x7B0000);

            Terminal.Log("Initializing Shopface and Helpimage files...", 1);
            Variables.ShopFace = new ShopFace();
            Variables.HelpImage = new HelpImage();

            Terminal.Log("Kingdom Hearts II - Flexible Modding Library has been initialized!", 0);
        }
    }
}
