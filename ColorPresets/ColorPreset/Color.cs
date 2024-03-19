using ColorPresets.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityEngine;

namespace ColorPresets.ColorPreset
{
    [Serializable]
    public class Color
    {
        public int red { get; set; } = 255;
        public int green { get; set; } = 255;
        public int blue { get; set; } = 255;

        public Color(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }
}
