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
            if (!Plugin.Instance.isEnabled.Value || Plugin.isOnline) return;

            Plugin.WaitingForUpdate = true;
        }
    }
}
