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

    public class ColorPreset
    {
        private Color leftSaber { get; set; } = new Color(168, 32, 32);
        private Color rightSaber { get; set; } = new Color(32, 100, 168);
        private Color lightOne { get; set; } = new Color(32, 100, 168);
        private Color lightTwo { get; set; } = new Color(48, 152, 255);
        private Color wall { get; set; } = new Color(168, 32, 32);
        private Color boostOne { get; set; } = new Color(192, 48, 48);
        private Color boostTwo { get; set; } = new Color(48, 152, 255);

        private string name { get; set; } 

        public ColorPreset(string name)
        {
            this.name = name;
        }

        public static ColorPreset findInstanceFromName(String name)
        {
            IEnumerable<ColorPreset> iterator = 
                from scheme in PluginConfig.Instance.colorsList 
                where scheme.name == name 
                select scheme;

            return iterator.First();
        }

        public static bool containsInstanceFromName(String name)
        {
            IEnumerable<ColorPreset> iterator =
                from scheme in PluginConfig.Instance.colorsList
                where scheme.name == name
                select scheme;

            if (iterator.Count() > 0) return true;

            return false;
        }

    }
}
