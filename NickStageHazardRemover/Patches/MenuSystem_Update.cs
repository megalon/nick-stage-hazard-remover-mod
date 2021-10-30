using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;

namespace NickStageHazardRemover.Patches
{
    [HarmonyPatch(typeof(MenuSystem), "Update")]
    class MenuSystem_Update
    {
        static void Prefix(ref List<string> ___screenHistory)
        {
            // Most reliable way I've found to detect in menu if we're online
            if (___screenHistory.Contains("onlinemenu"))
            {
                Plugin.isOnline = true;
            } else
            {
                Plugin.isOnline = false;
            }
            Plugin.LogInfo($"MenuSystem_Update Plugin.isOnline:{Plugin.isOnline}");
        }
    }
}
