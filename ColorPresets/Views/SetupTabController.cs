using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Parser;
using ColorPresets.Configuration;
using ColorPresets.PresetConfig;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ColorPresets.Views
{
    [ViewDefinition("ColorPresets.Views.GameplaySetup.bsml")]
    public class SetupTabController
    {

        #region enableColorOverride

        [UIValue("enableColorOverride")]
        private bool enableColorOverride
        {
            get { return PluginConfig.Instance.enableColorOverride; }
            set { PluginConfig.Instance.enableColorOverride = value; }
        }

        #endregion enableColorOverride

        #region PresetSelector
        [UIComponent("colorPresetsDropDown")]
        private DropDownListSetting list = new DropDownListSetting();

        [UIValue("listOptions")]
        public List<object> listOptions = new List<object>(PresetSaveLoader.getListOfAllPresets());

        [UIValue("listChoice")]
        private object listChoice {
            get { 
                 return PluginConfig.Instance.selectedPreset;     
                
                }
            set {
                updateList();
                PluginConfig.Instance.selectedPreset = list.Value as string;
                updateTabValues();
            }
        }

        #endregion PresetSelector

        #region enablePresetEditingSwitch

        [UIValue("enablePresetEditing")]
        private bool enablePresetEditing
        {
            get { return PluginConfig.Instance.enablePresetEditing; }
            set
            {
                PluginConfig.Instance.enablePresetEditing = value;
                enableOrDisableEditing(value);
            }
        }

        #endregion enablePresetEditingSwitch

        #region SaberColorValues
        [UIComponent("leftSaberColorSelector")]
        private ColorSetting leftSaberColorSelector;


        [UIValue("leftSaberColorVal")]
        private UnityEngine.Color leftSaberColorVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).leftSaber.convertToUnityColor(); }

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

        #region nameEditingField

        [UIComponent("presetNameSetting")]
        private StringSetting presetNameSetting = new StringSetting();

        [UIValue("presetNameVal")]
        private string presetNameVal
        {
            get { 
                // you can say many things about putting the editing enabler and disabler in the getter for the preset name
                // you cannot say that it does not work
                enableOrDisableEditing(PluginConfig.Instance.enablePresetEditing);
                return PluginConfig.Instance.selectedPreset; 
            }
            set
            {

                if (!File.Exists($"{PresetSaveLoader.pathToFolder}{value}.json"))
                {
                    try
                    {
                        File.Move($"{PresetSaveLoader.pathToFolder}{PluginConfig.Instance.selectedPreset}.json", $"{PresetSaveLoader.pathToFolder}{value}.json");
                        PluginConfig.Instance.selectedPreset = value;

                        PresetSaveLoader.writeNameToPreset(value, value);

                        updateList();

                        list.dropdown.SelectCellWithIdx(list.values.IndexOf(PluginConfig.Instance.selectedPreset));

                        // updateColors();
                    }
                    catch (Exception e)
                    {
                        Plugin.Log.Error($"An error occured renaming this preset: {e}");
                    }
                }

                updateTabValues();
            }
        }

        #endregion nameEditingField

        #region NewPresetButton
        [UIAction("newPresetButtonClicked")]
        private void newPresetButton()
        {
            //create preset
            string nameOfSelected = $"NewPreset{OtherUtils.findNewPresetCount()}";
            PresetSaveLoader.writeToPreset(new ColorPreset.ColorPreset(nameOfSelected));
            PluginConfig.Instance.selectedPreset = nameOfSelected;
            // update values 
            updateList();

            list.dropdown.SelectCellWithIdx(list.values.IndexOf(nameOfSelected));

            updateTabValues();

        }
        #endregion NewPresetButton

        internal void updateList()
        {
            list.values = new List<object>(PresetSaveLoader.getListOfAllPresets());
            list.UpdateChoices();
        }  

        internal void updateTabValues()
        {
            leftSaberColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).leftSaber.convertToUnityColor();
            rightSaberColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).rightSaber.convertToUnityColor();
            lightOneColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).lightOne.convertToUnityColor();
            lightTwoColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).lightTwo.convertToUnityColor();
            wallColorSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).wall.convertToUnityColor();
            presetNameSetting.Text = PluginConfig.Instance.selectedPreset;
        }

        public void enableOrDisableEditing(bool enabled)
        {
            leftSaberColorSelector.editButton.enabled = enabled;
            rightSaberColorSelector.editButton.enabled= enabled;
            lightOneColorSelector.editButton.enabled = enabled;
            lightTwoColorSelector.editButton.enabled = enabled;
            wallColorSelector.editButton.enabled = enabled;

            presetNameSetting.gameObject.SetActive(enabled);
        }
    }
}