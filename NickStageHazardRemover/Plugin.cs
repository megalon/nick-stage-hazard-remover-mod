using BepInEx;
using System.Collections.Generic;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;
using BepInEx.Configuration;
using UnityEngine;
using Nick;

namespace NickStageHazardRemover
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static Plugin Instance;
        public static bool WaitingForUpdate = false;
        public static bool isOnline = false;
        internal ConfigEntry<bool> isEnabled;
        internal ConfigEntry<bool> hazardsOn;
        internal static MenuTextContent stageSelectTextContent;

        private void Awake()
        {
            Logger.LogDebug($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");

            if (Instance)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;

            var config = this.Config;

            isEnabled = Config.Bind<bool>("Options", "Enabled", true);
            hazardsOn = Config.Bind<bool>("Options", "Hazards On", true);

            if (!Plugin.Instance.isEnabled.Value)
            {
                Logger.LogWarning($"Plugin {PluginInfo.PLUGIN_NAME} is disabled! You must enable it in the config file for it to work!");
            }

            config.SettingChanged += OnConfigSettingChanged;

            // Harmony patches
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void Update()
        {
            if (!stageSelectTextContent)
            {
                return;
            }

            if ((bool)MenuSystem.MainInput?.IsButtonPress(MenuAction.ActionButton.Opt2))
            {
                Plugin.Instance.hazardsOn.Value = !Plugin.Instance.hazardsOn.Value;
                Plugin.LogInfo($"Detected button press! Set hazardsOn to {Plugin.Instance.hazardsOn.Value}");
                updateStageSelectText();
            }
        }

        internal static void updateStageSelectText()
        {
            if (!stageSelectTextContent) return;

            var hazardsText = (Plugin.Instance.hazardsOn.Value ? "<color=yellow>On</color>" : "<color=red>Off</color>");
            stageSelectTextContent.SetString($"{Localization.stage_select_header} | Hazards: {hazardsText}\nLeft bumper to toggle");
        }

        static void OnConfigSettingChanged(object sender, EventArgs args)
        {
            LogDebug($"{PluginInfo.PLUGIN_NAME} OnConfigSettingChanged");
            Plugin.Instance?.Config?.Reload();
        }

        internal static void LogDebug(string message) => Instance.Log(message, LogLevel.Debug);
        internal static void LogInfo(string message) => Instance.Log(message, LogLevel.Info);
        internal static void LogWarning(string message) => Instance.Log(message, LogLevel.Warning);
        internal static void LogError(string message) => Instance.Log(message, LogLevel.Error);
        private void Log(string message, LogLevel logLevel) => Logger.Log(logLevel, message);
    }
}
