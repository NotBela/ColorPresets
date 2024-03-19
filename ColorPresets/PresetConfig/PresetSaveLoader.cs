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
using UnityEngine;
using static UnityEngine.JsonUtility;

namespace ColorPresets.PresetConfig
{
    public static class PresetSaveLoader
    {
        private static readonly string pathToFolder = UnityGame.InstallPath + "\\UserData\\ColorPresets\\";

        public static void makeFolder()
        {
            if (Directory.Exists(pathToFolder))
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

            return list;
        }

        public static void writeToPreset(ColorPreset.ColorPreset preset)
        {


            string json = JsonUtility.ToJson(preset);

            File.WriteAllText(pathToFolder + preset + ".json", json);
        }

        public static ColorPreset.ColorPreset readPreset(string jsonName)
        {
            ColorPreset.ColorPreset jsonDeserialized = JsonUtility.FromJson<ColorPreset.ColorPreset>(jsonName + ".json");

            return jsonDeserialized;
        }
    }
}
