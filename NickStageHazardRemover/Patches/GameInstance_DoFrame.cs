using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;
using SMU.Reflection;
using UnityEngine;

namespace NickStageHazardRemover.Patches
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
            if (!Plugin.Instance.isEnabled.Value || Plugin.isOnline) return;

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


                if (stageAgent.GameUniqueIdentifier.Equals("stage_duo_kitchen"))
                {
                    // Move a plate to cover up the frying pan
                    var originalPlateObj = __instance.transform.Find("stage_duo_kitchen(Clone)/KitchenAssets/Plate_Duo");
                    GameObject newPlate = GameObject.Instantiate(originalPlateObj.gameObject);
                    newPlate.transform.position = new Vector3(91.381f, 1.6465f, 6.4497f);

                    Quaternion rot = new Quaternion();
                    rot.eulerAngles = new Vector3(3.454f, -137.947f, -5.169f);
                    newPlate.transform.rotation = rot;

                    float plateScale = 3.922204f;
                    newPlate.transform.localScale = new Vector3(plateScale, plateScale, plateScale);

                    // Turn off extra VFX
                    var smellRise = __instance.transform.Find("stage_duo_kitchen(Clone)/VFX/DuoKitchen_SmellRise");
                    var smellRise1 = __instance.transform.Find("stage_duo_kitchen(Clone)/VFX/DuoKitchen_SmellRise_1");
                    var waterBoiling = __instance.transform.Find("stage_duo_kitchen(Clone)/VFX/WaterBoiling");
                    smellRise.gameObject.SetActive(false);
                    smellRise1.gameObject.SetActive(false);
                    waterBoiling.gameObject.SetActive(false);

                    // Turn off frying SFX
                    var frySFX = __instance.transform.Find("stage_duo_kitchen(Clone)/stage/environment_sounds/sfx_pan_fry");
                    frySFX.gameObject.SetActive(false);
                }

                Plugin.WaitingForUpdate = false;
            }
        }
    }
}
