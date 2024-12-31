using Binarysharp.MSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH2FML
{
    public partial class Entry
    {
        public static void Initialize(Process Input)
        {
            Hypervisor.AttachProcess(Input);
            Variables.SharpHook = new MemorySharp(Hypervisor.Process);

            Effect.FUNC_PAXSTART = Hypervisor.FindSignature(Variables.SIGN_StartPAX);

            Popup.FUNC_SHOWPRIZE = Hypervisor.FindSignature(Variables.SIGN_ShowObatined);
            Popup.FUNC_STARTCAMP = Hypervisor.FindSignature(Variables.SIGN_ExecuteCampMenu);
            Popup.FUNC_SHOWINFORMATION = Hypervisor.FindSignature(Variables.SIGN_ShowInformation);

            Dialog.FUNC_SETMENUMODE = Hypervisor.FindSignature(Variables.SIGN_SetMenuType);
            Dialog.FUNC_SETCAMPWARNING = Hypervisor.FindSignature(Variables.SIGN_SetCampWarning);
            Dialog.FUNC_SHOWCAMPWARNING = Hypervisor.FindSignature(Variables.SIGN_ShowCampWarning);
            Dialog.FUNC_FADECAMPWARNING = Hypervisor.FindSignature(Variables.SIGN_FadeCampWarning);

            System.FUNC_MAPJUMP = Hypervisor.FindSignature(Variables.SIGN_MapJump);
            System.FUNC_ITEMTABLEGET = Hypervisor.FindSignature(Variables.SIGN_ItemTableGet);
            System.FUNC_ITEMPARAMGET = Hypervisor.FindSignature(Variables.SIGN_ItemParamGet);
            System.FUNC_GIVEBACKYARD = Hypervisor.FindSignature(Variables.SIGN_GiveBackyard);
            System.FUNC_REDUCEBACKYARD = Hypervisor.FindSignature(Variables.SIGN_ReduceBackyard);
            System.FUNC_GETNUMBACKYARD = Hypervisor.FindSignature(Variables.SIGN_GetNumberBackyard);

            IO.FUNC_FINDFILE = Hypervisor.FindSignature(Variables.SIGN_FindFile);
            IO.FUNC_GETFILESIZE = Hypervisor.FindSignature(Variables.SIGN_GetFileSize);
            IO.FUNC_OBJENTRYGET = Hypervisor.FindSignature(Variables.SIGN_ObjentryGet);
            IO.FUNC_FREETASKMGR = Hypervisor.FindSignature(Variables.SIGN_TaskManagerFree);
            IO.FUNC_CREATETASKMGR = Hypervisor.FindSignature(Variables.SIGN_TaskManagerCreate);

            Text.FUNC_MESSAGEGETDATA = Hypervisor.FindSignature(Variables.SIGN_MessageGetData);

            Sound.FUNC_PLAYSFX = Hypervisor.FindSignature(Variables.SIGN_PlaySFX);
            Sound.FUNC_PLAYVSB = Hypervisor.FindSignature(Variables.SIGN_SslVsbPlay);
            Sound.FUNC_KILLBGM = Hypervisor.FindSignature(Variables.SIGN_StopBGM);
        }
    }
}
