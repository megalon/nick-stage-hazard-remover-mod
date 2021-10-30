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
            if (smd.id.Equals("stage_mascot_blocks"))
            {
                smd.blastzoneDistDown = 15f;
            }
        }
    }
}
