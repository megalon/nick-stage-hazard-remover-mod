using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;
using SMU.Reflection;
using UnityEngine;

namespace NickStageModifier.Patches
{
    [HarmonyPatch(typeof(GameInstance), "DoFrame")]
    class GameInstance_DoFrame
    {
        static void PrintAllParents(Transform transform)
        {
            Plugin.LogDebug($"PrintAllParents:\"{transform.name}\"");
            while (transform.parent)
            {
                transform = transform.parent;
                Plugin.LogDebug($"parent:\"{transform.name}\"");
            }
        }

        static void Postfix(ref GameAgent[] ___updagents, ref GameInstance __instance, ref int ___agentsAdded)
        {
            if (Plugin.WaitingForUpdate)
            {
                Plugin.LogWarning($"Got to \"GameInstance_DoFrame\"!");

                GameAgent stageAgent = null;

                for (int i = 0; i < ___updagents.Length; i++)
                {
                    if (___updagents[i].GameUniqueIdentifier.StartsWith("stage_"))
                    {
                        stageAgent = ___updagents[i];
                        break;
                    }
                }

                if (!stageAgent)
                {
                    Plugin.LogError($"Could not find info in \"___updagents\" array!");
                    return;
                }

                Plugin.LogInfo($"Stage is \"{stageAgent.GameUniqueIdentifier}\"!");

                if (stageAgent.GameUniqueIdentifier.Equals("stage_rival_bus"))
                {
                    // Stop background movement
                    GameObject.Destroy(__instance.GetComponentInChildren<CityBackgroundMovement>());
                    
                    // Disable conveyer belt road
                    EdgeStageSource[] edgeStageSources = __instance.GetComponentsInChildren<EdgeStageSource>();
                    foreach(EdgeStageSource edgeStageSource in edgeStageSources)
                    {
                        StageLine[] stageLines = edgeStageSource.ecss.GetField<EdgeColliderStageSource, StageLine[]>("mStage");
                        stageLines[0].SetConveyorSpeed(0);
                    }

                    // Stop wind lines
                    var windTransform = __instance.transform.Find("stage_rival_bus(Clone)/FX/WindTrailsEffect");
                    windTransform.gameObject.SetActive(false);

                    // Get the bus object
                    var busTransform = __instance.transform.Find("stage_rival_bus(Clone)/stage/Bus");
                    if (busTransform)
                    {
                        // Stop bus shaking
                        var bus02Transform = busTransform.Find("RivalBus_Bus_02");
                        bus02Transform.GetComponent<UnityEngine.Animation>().enabled = false;

                        // Stop glare lines on bus windows                    
                        var windowsTransform = bus02Transform.Find("Windows");
                        windowsTransform.gameObject.SetActive(false);

                        // Stop bus wheels
                        var busWheel = busTransform.Find("RivalBus_Bus_02_Wheel");
                        var busWheel1 = busTransform.Find("RivalBus_Bus_02_Wheel_1");
                        busWheel.GetComponent<UnityEngine.Animation>().enabled = false;
                        busWheel1.GetComponent<UnityEngine.Animation>().enabled = false;
                    } else
                    {
                        Plugin.LogError("Could not find Bus!");
                    }
                }

                Plugin.WaitingForUpdate = false;
            }
        }
    }
}
