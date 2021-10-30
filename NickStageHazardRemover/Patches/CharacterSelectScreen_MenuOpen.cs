using System;
using System.Collections.Generic;
using System.Text;
using Nick;
using HarmonyLib;
using UnityEngine;
using SMU.Reflection;

namespace NickStageHazardRemover.Patches
{
    [HarmonyPatch(typeof(CharacterSelectScreen), "MenuOpen")]
    class CharacterSelectScreen_MenuOpen
    {
        static void Prefix(StageSelectScreen __instance)
        {
            if (Plugin.hazardButton != null) return;

            // Grab the ControlButton's ButtonImage so we can copy it
            GameObject controlsButtonImage = __instance.transform.Find("Canvas/MainContainer/PlayerSlots/Player1Slot/NavigationButtonsMenu/NavigationButtons/ControlsButton/ButtonImage").gameObject;
            Plugin.hazardButton = GameObject.Instantiate(controlsButtonImage);

            var hazardButtonImage = Plugin.hazardButton.GetComponent<ButtonImage>();
            hazardButtonImage.controller = controlsButtonImage.GetComponent<ButtonImage>().controller;
            hazardButtonImage.SetField("usesFirstController", true);
            hazardButtonImage.SetField("buttonType", MenuButtonGraphics.RequestButton.opt2);
        }
    }
}
