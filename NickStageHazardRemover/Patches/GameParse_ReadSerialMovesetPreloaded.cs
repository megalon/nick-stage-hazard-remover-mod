using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Nick;
using UnityEngine;

namespace NickStageHazardRemover.Patches
{
	[HarmonyPatch(typeof(GameParse), "ReadSerialMovesetPreloaded")]
	class GameParse_ReadSerialMovesetPreloaded
	{
		private static bool Prefix(ref TextAsset[] movesetLayers)
		{
			if (!Plugin.Instance.isEnabled.Value) return true;

			for (int i = 0; i < movesetLayers.Length; i++)
			{
				// Look for modified file in text assets
				string text = Properties.Resources.ResourceManager.GetString(movesetLayers[i].name + "_new");
				if (text != null)
                {
					Plugin.LogInfo($"Swapping TextAsset for \"{movesetLayers[i].name}\" to modified file");
					movesetLayers[i] = new TextAsset(text);
					continue;
				}

				// Swap out stages for generic stage
				// Jellyfish Fields is technically a legal stage, even though it has a moving platform, so ignore it
				if (movesetLayers[i].name.StartsWith("stage_") && !movesetLayers[i].name.Equals("stage_apple_fields"))
                {
					Plugin.LogInfo($"Swapping stage TextAsset for \"{movesetLayers[i].name}\" to generic static stage");
					movesetLayers[i] = new TextAsset(Properties.Resources.stage_static);
				}
			}

			return true;
		}
	}
}
