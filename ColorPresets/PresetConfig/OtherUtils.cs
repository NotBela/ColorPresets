using System.Collections.Generic;

namespace ColorPresets.PresetConfig
{
    public static class OtherUtils
    {
        public static int findNewPresetCount()
        {
            int count = 0;

            List<string> list = PresetSaveLoader.getListOfAllPresets();
            
            while(true)
            {
                if (!list.Contains($"NewPreset{count}"))
                {
                    return count;
                }
                count++;
            }
        }

        public static string checkForNameInUse(string name)
        {
            int count = 2;

            List<string> list = PresetSaveLoader.getListOfAllPresets();
            if (!list.Contains(name))
            {
                return name;
            }

            while (true)
            {
                if (!list.Contains($"{name}{count}")) return $"{name}{count}";
                count++;
            }
        }
    }
}
