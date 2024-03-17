using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using ColorPresets.ColorPreset;
using ColorPresets.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ColorPresets.Views
{
    public class ExampleViewController : BSMLResourceViewController
    {
        public override string ResourceName => "ColorPresets.Views.GameplaySetup.bsml";

        [UIValue("colorSelected")]
        private string selected = PluginConfig.Instance.selected;

        [UIValue("colorsList")]
        private List<ColorPreset.ColorPreset> colorList = PluginConfig.Instance.colorsList;


    }

}
