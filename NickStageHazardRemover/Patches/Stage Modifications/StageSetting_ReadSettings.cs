using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;

namespace NickStageHazardRemover.Patches.Stage_Modifications
{
    [HarmonyPatch(typeof(GameSetup.StageSetting), "ReadSettings")]
    class StageSetting_ReadSettings
    {
        static void Prefix(ref StageMetaData smd)
        {
            Plugin.LogWarning($"smd.id:{smd.id}");
            if (smd.id.Equals("stage_mascot_blocks"))
            {
                Plugin.LogWarning($"smd.blastzoneDistDown:{smd.blastzoneDistDown}");
                smd.blastzoneDistDown = 15f;
            }
        }
    }
}
