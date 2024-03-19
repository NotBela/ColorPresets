using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
