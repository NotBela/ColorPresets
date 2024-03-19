using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Macros;
using BeatSaberMarkupLanguage.ViewControllers;
using ColorPresets.ColorPreset;
using ColorPresets.Configuration;
using IPA.Logging;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;


namespace ColorPresets.Views
{
    [ViewDefinition("ColorPresets.Views.GameplaySetup.bsml")]
    public class ListViewController
    {
        [UIComponent("colorPresetsDropDown")]
        private DropDownListSetting list = new DropDownListSetting();

        [UIValue("listOptions")]
        public List<object> listOptions = new List<object>(PluginConfig.Instance.presets);

        [UIValue("listChoice")]
        private object listChoice = PluginConfig.Instance.selected; // PluginConfig.Instance.selected as object; //PluginConfig.Instance.selected;

        [UIAction("newPresetButtonClicked")]
        private void newPresetButton()
        {
            // PluginConfig.Instance.presets.Add(new ColorPreset.ColorPreset("NewPreset1"));
            Plugin.Log.Info("button pressed!!! wow!!!");
        }

        // [UIAction("#apply")]
        // public void OnApply() => Logger.log.Info($"list-choice value applied, now: {listChoice}");
    }

}