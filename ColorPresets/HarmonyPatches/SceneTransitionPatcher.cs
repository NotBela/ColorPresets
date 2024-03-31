using ColorPresets.Configuration;
using ColorPresets.PresetConfig;
using HarmonyLib;
using System;

namespace ColorPresets.HarmonyPatches
{
    [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO), nameof(StandardLevelScenesTransitionSetupDataSO.Init))]
    static class SceneTransitionPatcher
    {
        
        private static void Prefix(ref IDifficultyBeatmap difficultyBeatmap, ref ColorScheme overrideColorScheme)
        {

            if (PluginConfig.Instance.isEnabled && PluginConfig.Instance.enableColorOverride)
            {
                try
                {
                    overrideColorScheme = ColorPreset.ColorPreset.convertToBaseGameScheme(PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset));
                }
                catch (Exception)
                {
                    overrideColorScheme = new ColorScheme(difficultyBeatmap.GetEnvironmentInfo().colorScheme);
                    Plugin.Log.Warn("Could not find preset, using fallback default");

                }
            }
                        
        }
    }
}