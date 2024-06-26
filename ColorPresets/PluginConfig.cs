﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BeatSaberMarkupLanguage.GameplaySetup;
using ColorPresets.Views;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using Unity.Collections;
using IPA.Config.Stores.Converters;
using ColorPresets.PresetConfig;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ColorPresets.Configuration
{
    internal class PluginConfig
    {

        public static PluginConfig Instance { get; set; }

        public virtual bool enableColorOverride { get; set; } = true;

        public virtual bool enablePresetEditing { get; set; } = true;

        public virtual bool isEnabled { get; set; } = true;

        [NonNullable]
        public virtual string selectedPreset { get; set; } = "NewPreset0";

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            

        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            

            // Do stuff when the config is changed.
            if (!isEnabled)
            {
                GameplaySetup.instance.RemoveTab("ColorPresets");
            }
            else
            {
                SetupTabController setupTab = new SetupTabController();
                GameplaySetup.instance.AddTab("ColorPresets", "ColorPresets.Views.GameplaySetup.bsml", setupTab, MenuType.All);
                // setupTab.setPresetNameChangerActive(enablePresetEditing);
            }
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}