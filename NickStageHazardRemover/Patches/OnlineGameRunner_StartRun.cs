using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;

namespace NickStageHazardRemover.Patches
{

    [HarmonyPatch(typeof(OnlineGameRunner), "StartRun")]
    class OnlineGameRunner_StartRun
    {
        static void Prefix()
        {
            Plugin.isOnline = true;
        }
    }
}
