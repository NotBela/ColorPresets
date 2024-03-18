using ColorPresets.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPresets.ColorPreset
{
    public class Color
    {
        private int red { get; set; } = 255;
        private int green { get; set; } = 255;
        private int blue { get; set; } = 255;

        public Color(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }
}
