using BepInEx;
using System.Collections.Generic;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;
using BepInEx.Configuration;
using UnityEngine;

namespace NickStageHazardRemover
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static Plugin Instance;
        public static bool WaitingForUpdate = false;
        internal ConfigEntry<bool> isEnabled;

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

            if (!Plugin.Instance.isEnabled.Value)
            {
                Logger.LogWarning($"Plugin {PluginInfo.PLUGIN_NAME} is disabled! You must enable it in the config file for it to work!");
            }

            config.SettingChanged += OnConfigSettingChanged;

            // Harmony patches
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        static void OnConfigSettingChanged(object sender, EventArgs args)
        {
            LogDebug($"{PluginInfo.PLUGIN_NAME} OnConfigSettingChanged");
            LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} config value \"Enabled\" set to \"{Plugin.Instance.isEnabled.Value}\"!");
            Plugin.Instance?.Config?.Reload();
        }

        internal static void LogDebug(string message) => Instance.Log(message, LogLevel.Debug);
        internal static void LogInfo(string message) => Instance.Log(message, LogLevel.Info);
        internal static void LogWarning(string message) => Instance.Log(message, LogLevel.Warning);
        internal static void LogError(string message) => Instance.Log(message, LogLevel.Error);
        private void Log(string message, LogLevel logLevel) => Logger.Log(logLevel, message);
    }
}
