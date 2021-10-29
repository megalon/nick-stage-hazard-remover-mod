using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;

namespace NickStageHazardRemover.Patches
{
    [HarmonyPatch(typeof(StageSelectScreen), "MenuOpen")]
    class StageSelectScreen_MenuOpen
    {
        static void Prefix(StageSelectScreen __instance)
        {
            var title = __instance.gameObject.transform.Find("Canvas/MainContainer/Title");

            if (title != null)
            {
                Plugin.LogInfo("Found Canvas/MainContainer/Title!");
                Plugin.stageSelectTextContent = title.GetComponent<MenuTextContent>();
                if (!Plugin.stageSelectTextContent)
                {
                    Plugin.LogError("\"Plugin.stageSelectTextContent\" is undefined, even though it should exist?");
                } else
                {
                    Plugin.updateStageSelectText();
                }
            }
            else
            {
                Plugin.LogError("Could not find \"Canvas/MainContainer/Title!\"");
            }
        }
    }
}
