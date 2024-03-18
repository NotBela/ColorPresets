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


namespace ColorPresets.Views
{
    [ViewDefinition("ColorPresets.Views.GameplaySetup.bsml")]
    public class ListViewController
    {
        [UIComponent("colorPresetsDropDown")]
        private DropDownListSetting list = new DropDownListSetting();

        [UIValue("listOptions")]
        private List<object> listOptions = ColorPreset.ColorPreset.namesOfPresets();

        [UIValue("listChoice")]
        private string listChoice = PluginConfig.Instance.selected.toString();

        [UIAction("newPresetButtonClicked")]
        private void newPresetButton()
        {
            PluginConfig.Instance.presets.Add(new ColorPreset.ColorPreset("NewPreset1"));
        }

        // [UIAction("#apply")]
        // public void OnApply() => Logger.log.Info($"list-choice value applied, now: {listChoice}");
    }

}