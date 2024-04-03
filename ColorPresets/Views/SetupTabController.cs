using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using ColorPresets.Configuration;
using ColorPresets.PresetConfig;
using JetBrains.Annotations;
using SongCore.Data;
using SongCore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using BS_Utils;
using BS_Utils.Utilities;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

namespace ColorPresets.Views
{
    [ViewDefinition("ColorPresets.Views.GameplaySetup.bsml")]
    public class SetupTabController
    {

        private IPreviewBeatmapLevel levelPreview;
        
        private int topDiffIndex;

        public SetupTabController()
        {
            BSEvents.levelSelected += BSEvents_levelSelected;
        }

        #region enableColorOverride

        [UIValue("enableColorOverride")]
        private bool enableColorOverride
        {
            get {
                
                return PluginConfig.Instance.enableColorOverride; 
            }
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

        [UIComponent("boostColorOneSelector")]
        private ColorSetting boostColorOneSelector;

        [UIValue("boostColorOneVal")]
        private UnityEngine.Color boostColorOneVal {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).boostOne.convertToUnityColor(); }
            set { PresetSaveLoader.writeColorToPreset("boostOne", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(boostColorOneSelector.CurrentColor)); }
        }

        [UIComponent("boostColorTwoSelector")]
        private ColorSetting boostColorTwoSelector;

        [UIValue("boostColorTwoVal")]
        private UnityEngine.Color boostColorTwoVal
        {
            get { return PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).boostTwo.convertToUnityColor(); }
            set { PresetSaveLoader.writeColorToPreset("boostTwo", PluginConfig.Instance.selectedPreset, ColorPreset.Color.convertFromUnityColor(boostColorTwoSelector.CurrentColor)); }
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

        #region importPresetButton

        [UIComponent("importPresetFromBeatmapButton")]
        public Button importPresetFromBeatmapButton;

        [UIAction("importPresetFromBeatmapClicked")]
        public void importPresetFromBeatmap()
        {
            ExtraSongData extraLevelData = SongCore.Collections.RetrieveExtraSongData(SongCore.Collections.hashForLevelID(this.levelPreview.levelID));
            // top diff selected because it is most likely to have custom colors
            // all diffs should have custom colors anyway but like idk

            ExtraSongData.DifficultyData topDiff = extraLevelData._difficulties[extraLevelData._difficulties.Length - 1];

            var leftSaber = ColorPreset.Color.convertFromSongCore(topDiff._colorLeft) ?? ColorPreset.ColorPreset.defaultLeftSaber;
            var rightSaber = ColorPreset.Color.convertFromSongCore(topDiff._colorRight) ?? ColorPreset.ColorPreset.defaultRightSaber;
            var leftLight = ColorPreset.Color.convertFromSongCore(topDiff._envColorLeft) ?? ColorPreset.ColorPreset.defaultLightOne;
            var rightLight = ColorPreset.Color.convertFromSongCore(topDiff._envColorLeft) ?? ColorPreset.ColorPreset.defaultLightTwo;
            var obstacle = ColorPreset.Color.convertFromSongCore(topDiff._obstacleColor) ?? ColorPreset.ColorPreset.defaultWall;
            var boostLeft = ColorPreset.Color.convertFromSongCore(topDiff._envColorLeftBoost) ?? ColorPreset.ColorPreset.defaultBoostOne;
            var boostRight = ColorPreset.Color.convertFromSongCore(topDiff._envColorRightBoost) ?? ColorPreset.ColorPreset.defaultBoostTwo;

            ColorPreset.ColorPreset songPreset = new ColorPreset.ColorPreset(
                OtherUtils.checkForNameInUse(levelPreview.songName),
                leftSaber,
                rightSaber, 
                leftLight,
                rightLight,
                obstacle,
                boostLeft,
                boostRight
            );

            PresetSaveLoader.writeToPreset(songPreset);
            updateList();
            PluginConfig.Instance.selectedPreset = songPreset._name;
            list.dropdown.SelectCellWithIdx(list.values.IndexOf(PluginConfig.Instance.selectedPreset));
            updateTabValues();

        }

        #endregion importPresetButton

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
            boostColorOneSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).boostOne.convertToUnityColor();
            boostColorTwoSelector.CurrentColor = PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset).boostTwo.convertToUnityColor();
            presetNameSetting.Text = PluginConfig.Instance.selectedPreset;
        }

        public void enableOrDisableEditing(bool enabled)
        {
            leftSaberColorSelector.editButton.enabled = enabled;
            rightSaberColorSelector.editButton.enabled= enabled;
            lightOneColorSelector.editButton.enabled = enabled;
            lightTwoColorSelector.editButton.enabled = enabled;
            wallColorSelector.editButton.enabled = enabled;
            boostColorOneSelector.editButton.enabled = enabled;
            boostColorTwoSelector.editButton.enabled = enabled;

            presetNameSetting.gameObject.SetActive(enabled);
        }

        internal void setImportButtonEnabled()
        {
            ExtraSongData extraLevelData = SongCore.Collections.RetrieveExtraSongData(SongCore.Collections.hashForLevelID(levelPreview.levelID));
            topDiffIndex = extraLevelData._difficulties.Length - 1;

            bool beatMapHasColors = SongCore.Utilities.Utils.DiffHasColors(extraLevelData._difficulties[topDiffIndex]); //extraLevelData._colorSchemes == null;

            if (importPresetFromBeatmapButton != null) importPresetFromBeatmapButton.interactable = beatMapHasColors;
        }

        private void BSEvents_levelSelected(LevelCollectionViewController arg1, IPreviewBeatmapLevel levelPreview)
        {
            this.levelPreview = levelPreview;
                
            setImportButtonEnabled();
        }
    }
}