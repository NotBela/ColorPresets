using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using ColorPresets.Configuration;
using ColorPresets.PresetConfig;
using System.Collections.Generic;

namespace ColorPresets.Views
{
    [ViewDefinition("ColorPresets.Views.GameplaySetup.bsml")]
    public class SetupTabController
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
        private UnityEngine.Color leftSaberColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).leftSaber.convertToUnityColor(); }

            // set not needed
            // set WAS needed actually idk what i was talking about
            set { PresetSaveLoader.writeColorToPreset("leftSaber", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(leftSaberColorVal)); }
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