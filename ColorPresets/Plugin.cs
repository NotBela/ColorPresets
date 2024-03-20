using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using System.Reflection;
using ColorPresets.Configuration;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using ColorPresets.Views;
using ColorPresets.PresetConfig;


namespace ColorPresets
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }

        internal static Harmony harmony { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, IPA.Config.Config conf)
        {
            Instance = this;
            Log = logger;
            // Log.Info("ColorPresets initialized.");
            harmony = new Harmony("Bela.BeatSaber.ColorPresets");
            PresetSaveLoader.makeFolder();
            PluginConfig.Instance = conf.Generated<PluginConfig>();
        }
        
        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            // Log.Debug("OnApplicationStart");
            new GameObject("ColorPresetsController").AddComponent<ColorPresetsController>();
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            BSMLSettings.instance.AddSettingsMenu("ColorPresets", "ColorPresets.Views.Settings.bsml", PluginConfig.Instance);
            ListViewController listViewController = new ListViewController();

            if (PluginConfig.Instance.isEnabled) GameplaySetup.instance.AddTab("ColorPresets", "ColorPresets.Views.GameplaySetup.bsml", listViewController, MenuType.All);

            Log.Info("ColorPresets loaded successfully");
        }

        [OnExit]
        public void OnApplicationStop()
        {
            // Log.Debug("OnApplicationQuit");
            harmony.UnpatchSelf();
            // BSMLSettings.instance.RemoveSettingsMenu(PluginConfig.Instance);
            // GameplaySetup.instance.RemoveTab("ColorPresets");
        }
    }
}
