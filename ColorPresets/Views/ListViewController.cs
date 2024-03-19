using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Macros;
using BeatSaberMarkupLanguage.ViewControllers;
using ColorPresets.ColorPreset;
using ColorPresets.Configuration;
using ColorPresets.PresetConfig;
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
        public List<object> listOptions = new List<object>(PresetSaveLoader.getListOfAllPresets());

        [UIValue("listChoice")]
        private object listChoice = PluginConfig.Instance.selected;

        

        [UIAction("newPresetButtonClicked")]
        private void newPresetButton()
        {
            // PluginConfig.Instance.presets.Add(new ColorPreset.ColorPreset("NewPreset1"));
            PresetSaveLoader.writeToPreset(new ColorPreset.ColorPreset($"NewPreset{OtherUtils.findNewPresetCount()}"));
            list.values = new List<object> (PresetSaveLoader.getListOfAllPresets());
            list.UpdateChoices();
        }
    }
}