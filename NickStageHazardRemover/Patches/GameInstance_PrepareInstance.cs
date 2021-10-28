using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;

namespace NickStageModifier.Patches
{
    [HarmonyPatch(typeof(GameInstance), "PrepareInstance")]
    class GameInstance_PrepareInstance
    {
        static void Postfix()
        {
            Plugin.WaitingForUpdate = true;
        }
    }
}
