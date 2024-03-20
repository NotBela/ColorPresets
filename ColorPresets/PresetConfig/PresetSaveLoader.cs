using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPA;
using IPA.Utilities;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace ColorPresets.PresetConfig
{
    public static class PresetSaveLoader
    {
        private static readonly string pathToFolder = UnityGame.InstallPath + "\\UserData\\ColorPresets\\";

        public static void makeFolder()
        {
            if (Directory.Exists(pathToFolder)) { }
            try
            {
                Directory.CreateDirectory(pathToFolder);
                writeToPreset(new ColorPreset.ColorPreset("NewPreset0"));
            }
            catch
            {
                throw new Exception("CouldNotCreateDirException");
                
            }
        }

        public static List<string> getListOfAllPresets()
        {
            List<string> list = new List<string>(Directory.GetFiles(pathToFolder));

            return list;
        }

        public static void writeToPreset(ColorPreset.ColorPreset preset)
        {
            string json = JsonConvert.SerializeObject(preset);

            File.WriteAllText(Path.Combine(pathToFolder, preset.ToString()), json + ".json");
        }

        public static ColorPreset.ColorPreset readPreset(string presetName)
        {
            ColorPreset.ColorPreset jsonDeserialized = JsonConvert.DeserializeObject<ColorPreset.ColorPreset>(File.ReadAllText($"{pathToFolder}{presetName}.json"));

            return jsonDeserialized;
        }
    }
}
