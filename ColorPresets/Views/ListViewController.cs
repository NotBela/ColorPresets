using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using ColorPresets.Configuration;
using ColorPresets.PresetConfig;
using System.Collections.Generic;
using UnityEngine;

namespace ColorPresets.Views
{
    [ViewDefinition("ColorPresets.Views.GameplaySetup.bsml")]
    public class ListViewController
    {
        #region PresetSelector
        [UIComponent("colorPresetsDropDown")]
        private DropDownListSetting list = new DropDownListSetting();

        [UIValue("listOptions")]
        public List<object> listOptions = new List<object>(PresetSaveLoader.getListOfAllPresets());

        [UIValue("listChoice")]
        private object listChoice { 
            get { return PluginConfig.Instance.selectedPreset; } 
            set { PluginConfig.Instance.selectedPreset = listOptions[list.dropdown.selectedIndex] as string; } 
        }

        #endregion PresetSelector

        #region SaberColorValues
        // [UIValue("leftSaberColorVal")]
        // private Color leftSaberColorVal = 

        #region NewPresetButton
        [UIAction("newPresetButtonClicked")]
        private void newPresetButton()
        {
            string nameOfSelected = PresetSaveLoader.writeToPreset(new ColorPreset.ColorPreset($"NewPreset{OtherUtils.findNewPresetCount()}"));
            updateList(nameOfSelected);
            list.dropdown.SelectCellWithIdx(list.values.IndexOf(nameOfSelected));

        }
        #endregion NewPresetButton

        internal void updateList(string nameOfSelected)
        {
            list.values = new List<object>(PresetSaveLoader.getListOfAllPresets());
            list.UpdateChoices();
        }
    }
}