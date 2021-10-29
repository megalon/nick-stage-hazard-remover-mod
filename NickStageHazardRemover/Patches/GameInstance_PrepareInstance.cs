using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;

namespace NickStageHazardRemover.Patches
{
    [HarmonyPatch(typeof(GameInstance), "PrepareInstance")]
    class GameInstance_PrepareInstance
    {
        static void Postfix()
        {
            Plugin.WaitingForUpdate = true;

            if (Plugin.Instance.hazardsOn.Value || Plugin.isOnline) return;
        }
    }
}
