using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;

namespace NickStageHazardRemover.Patches
{
    [HarmonyPatch(typeof(OnlineGameRunner), "SetupInvites")]
    class OnlineGameRunner_SetupInvites
    {
        static void Prefix()
        {
            Plugin.isOnline = false;
        }
    }
}
