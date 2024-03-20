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

        public static void writeToPreset(ColorPreset.ColorPreset presetToReadFrom, string presetToWriteTo)
        {
            File.WriteAllText($"{pathToFolder}{presetToWriteTo.ToString()}.json", JsonConvert.SerializeObject(presetToReadFrom));
        }

        public static void writeToPreset(ColorPreset.ColorPreset preset) {
            File.WriteAllText(pathToFolder + preset.ToString() + ".json", JsonConvert.SerializeObject(preset, Formatting.Indented)); // new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        public static ColorPreset.ColorPreset readPreset(string presetName)
        {
            ColorPreset.ColorPreset jsonDeserialized = JsonConvert.DeserializeObject<ColorPreset.ColorPreset>(File.ReadAllText($"{pathToFolder}{presetName}.json"));

            return jsonDeserialized;
        }
    }
}
