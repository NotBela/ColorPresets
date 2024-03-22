using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using ColorPresets.Configuration;
using ColorPresets.PresetConfig;
using System;
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
            get { 
                try
                {

                    return PluginConfig.Instance.selectedPreset;

                }
                catch (Exception e)
                {
                    // this should work maybe
                    Plugin.Log.Info("An error occured attempting to find the selected preset (did you delete it?)");
                    Plugin.Log.Info("Attempting to select another preset...");
                    return listOptions[0];
                }
                
                }
            set {
                updateList();
                PluginConfig.Instance.selectedPreset = listOptions[list.dropdown.selectedIndex] as string;
                updateColors();
            }
        }

        #endregion PresetSelector

        #region SaberColorValues
        [UIComponent("leftSaberColorSelector")]
        private ColorSetting leftSaberColorSelector;


        [UIValue("leftSaberColorVal")]
        private UnityEngine.Color leftSaberColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).leftSaber.convertToUnityColor(); } //  }

            // set not needed
            // set WAS needed actually idk what i was talking about
            set { PresetSaveLoader.writeColorToPreset("leftSaber", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(leftSaberColorSelector.CurrentColor)); }
        }

        [UIComponent("rightSaberColorSelector")]
        private ColorSetting rightSaberColorSelector;

        [UIValue("rightSaberColorVal")]
        private UnityEngine.Color rightSaberColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).rightSaber.convertToUnityColor(); }
            set { PresetSaveLoader.writeColorToPreset("rightSaber", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(rightSaberColorSelector.CurrentColor)); }
        }

        [UIComponent("lightOneColorSelector")]
        private ColorSetting lightOneColorSelector;

        [UIValue("lightOneColorVal")]
        private UnityEngine.Color lightOneColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).lightOne.convertToUnityColor(); }
            set { PresetSaveLoader.writeColorToPreset("lightOne", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(lightOneColorSelector.CurrentColor)); }
        }

        [UIComponent("lightTwoColorSelector")]
        private ColorSetting lightTwoColorSelector;

        [UIValue("lightTwoColorVal")]
        private UnityEngine.Color lightTwoColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).lightTwo.convertToUnityColor(); }
            set { PresetSaveLoader.writeColorToPreset("lightTwo", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(lightTwoColorSelector.CurrentColor)); }
        }

        [UIComponent("wallColorSelector")]
        private ColorSetting wallColorSelector;

        [UIValue("wallColorVal")]
        private UnityEngine.Color wallColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).wall.convertToUnityColor(); }
            set { PresetSaveLoader.writeColorToPreset("wall", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(wallColorSelector.CurrentColor)); }
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

        internal void updateColors()
        {
            //ADD ALL COLORS HERE WHEN IMPLEMENTED
            leftSaberColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).leftSaber.convertToUnityColor();
            rightSaberColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).rightSaber.convertToUnityColor();
            lightOneColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).lightOne.convertToUnityColor();
            lightTwoColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).lightTwo.convertToUnityColor();
            wallColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).wall.convertToUnityColor();
        }
    }
}