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
            set {
                updateList();
                PluginConfig.Instance.selectedPreset = listOptions[list.dropdown.selectedIndex] as string; 
            } 
        }

        #endregion PresetSelector

        #region SaberColorValues
        [UIValue("leftSaberColorVal")]
        private Color leftSaberColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).leftSaber.convertToUnityColor(); }
            set {
                
                ColorPreset.ColorPreset tempPreset = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset);
                tempPreset.leftSaber = ColorPreset.Color.convertFromUnityColor(leftSaberColorVal.r, leftSaberColorVal.g, leftSaberColorVal.b);
                PresetSaveLoader.writeToPreset(tempPreset, PluginConfig.Instance.selectedPreset); 
            }
        }

        #endregion SaberColorValues

        #region NewPresetButton
        [UIAction("newPresetButtonClicked")]
        private void newPresetButton()
        {
            //create preset
            string nameOfSelected = $"NewPreset{OtherUtils.findNewPresetCount()}";
            PresetSaveLoader.writeToPreset(new ColorPreset.ColorPreset(nameOfSelected));

            // update values 
            updateList();

            list.dropdown.SelectCellWithIdx(list.values.IndexOf(nameOfSelected));

        }
        #endregion NewPresetButton

        internal void updateList()
        {
            list.values = new List<object>(PresetSaveLoader.getListOfAllPresets());
            list.UpdateChoices();
        }
    }
}