using System.Collections.Generic;
using IPA.Utilities;
using System.IO;
using Newtonsoft.Json;

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
            List<ColorPreset.ColorPreset> list = new List<ColorPreset.ColorPreset>();

            list.Add(new ColorPreset.ColorPreset("testName")
            {
                leftSaber = preset.leftSaber,
                rightSaber = preset.rightSaber,
                lightOne = preset.lightOne,
                lightTwo = preset.lightTwo,
                wall = preset.wall,
                boostOne = preset.boostOne,
                boostTwo = preset.boostTwo,
            });

            File.WriteAllText(pathToFolder + preset.ToString() + ".json", JsonConvert.SerializeObject(list));

            return preset.ToString();
        }

        public static ColorPreset.ColorPreset readPreset(string presetName)
        {
            ColorPreset.ColorPreset jsonDeserialized = JsonConvert.DeserializeObject<ColorPreset.ColorPreset>(File.ReadAllText($"{pathToFolder}{presetName}.json"));

            return jsonDeserialized;
        }
    }
}
