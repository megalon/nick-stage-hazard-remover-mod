using System;
using System.Collections.Generic;
using System.Text;
using Nick;
using HarmonyLib;

namespace NickStageHazardRemover.Patches
{
    [HarmonyPatch(typeof(GameModeSetup), "SetupRunner")]
    class GameModeSetup_SetupRunner
    {
        static void Prefix(GameModeSetup __instance)
        {
            if (__instance.modeCfg.mode == GameModeSetup.Mode.Arcade)
            {
                Plugin.isArcade = true;
                Plugin.LogDebug("Arcade mode detected. Disabling mod!");
            }
            else
            {
                Plugin.isArcade = false;
            }
        }
    }
}
