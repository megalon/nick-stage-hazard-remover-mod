using System;
using System.Collections.Generic;
using System.Text;
using Nick;
using HarmonyLib;
using UnityEngine;

namespace NickStageHazardRemover.Patches
{
    [HarmonyPatch(typeof(PolygonStageSource), "Prepare")]
    class PolygonStageSource_Prepare
    {
        private static bool Prefix(ref GameStage stage, PolygonStageSource __instance)
        {
            if (Plugin.Instance.hazardsOn.Value || Plugin.isOnline || Plugin.isArcade) return true;

            // Continue to regular function if this isn't the kitchen stage
            if (!__instance) return true;
            if (!__instance.transform) return true;
            if (!__instance.transform.parent) return true;
            if (!__instance.transform.parent.parent) return true;

            // Update the PolygonCollider2D for stage_duo_kitchen
            // This is so that we can walk on the plate over the frying pan
            // Using "StartsWith" because string might be "...(Clone)"
            if (__instance.transform.parent.parent.name.StartsWith("stage_duo_kitchen"))
            {
                if (__instance.name.StartsWith("RightCluster_Collision"))
                {
                    // Add new points to edgePoints array
                    Vector2[] edgePoints = __instance.pcss.edge.points;
                    Vector2[] updatedPoints = new Vector2[edgePoints.Length + 4];
                    edgePoints.CopyTo(updatedPoints, 0);

                    // Add new points to gap in array
                    updatedPoints[0].x = 56f;
                    updatedPoints[edgePoints.Length] = new Vector2(15.93f, 5.84f);
                    updatedPoints[edgePoints.Length + 1] = new Vector2(29.4f, 7.5f);
                    updatedPoints[edgePoints.Length + 2] = new Vector2(32.24f, 9.4f);
                    updatedPoints[edgePoints.Length + 3] = new Vector2(56f, 9.4f);

                    // Update points
                    __instance.pcss.edge.points = updatedPoints;
                }
            }

            return true;
        }
    }
}
