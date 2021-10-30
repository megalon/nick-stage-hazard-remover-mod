using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;
using UnityEngine;
using SMU.Reflection;

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
                Plugin.stageSelectTextContent = title.GetComponent<MenuTextContent>();
                if (!Plugin.stageSelectTextContent)
                {
                    Plugin.LogError("\"Plugin.stageSelectTextContent\" is undefined, even though it should exist?");
                } else
                {
                    Plugin.updateStageSelectText();
                }

                Plugin.hazardButton.transform.position = Vector3.zero;
                Plugin.hazardButton.transform.SetParent(title.Find("Text"));
                Plugin.hazardButton.transform.localPosition = new Vector3(60, -65, 0);
            }
            else
            {
                Plugin.LogError("Could not find \"Canvas/MainContainer/Title!\"");
            }
        }
    }
}
