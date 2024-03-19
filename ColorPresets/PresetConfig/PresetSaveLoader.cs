using System.Collections.Generic;
using IPA.Utilities;
using System.IO;
using UnityEngine;

namespace ColorPresets.PresetConfig
{
    public static class PresetSaveLoader
    {
        private static readonly string pathToFolder = UnityGame.InstallPath + "\\UserData\\ColorPresets\\";

        public static void makeFolder()
        {
            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
                writeToPreset(new ColorPreset.ColorPreset("NewPreset0"));
            }

        }

        public static List<string> getListOfAllPresets()
        {
            string[] fileListWithPath = Directory.GetFiles(pathToFolder);

            List<string> list = new List<string>();

            foreach (string file in fileListWithPath)
            {
                list.Add(Path.GetFileNameWithoutExtension(file));
            }

            if (fileListWithPath.Length == 0) return new List<string>() { "You have no presets!" };

            return list;
        }

        public static string writeToPreset(ColorPreset.ColorPreset preset)
        {
            List<object> list = new List<object>() { preset };

            File.WriteAllText(pathToFolder + preset + ".json", JsonUtility.ToJson(list));

            return preset.ToString();
        }

        public static ColorPreset.ColorPreset readPreset(string jsonName)
        {
            ColorPreset.ColorPreset jsonDeserialized = JsonUtility.FromJson<ColorPreset.ColorPreset>(jsonName + ".json");

            return jsonDeserialized;
        }
    }
}
