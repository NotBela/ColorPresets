using ColorPresets.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPresets.ColorPreset
{
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
        public override string ToString()
        {
            return name;
        }

        public static List<object> convertToStringList()
        {
            List<object> list = new List<object>();

            //foreach(var preset in PluginConfig.Instance.presets)
            //{
            //    // list.Add(preset.ToString());
            //}
            return list;
        }

        //public static List<object> namesOfPresets()
        //{
        //    List<object> list = new List<object>();
        //    foreach(var preset in PluginConfig.Instance.presets)
        //    {
        //        list.Add(preset.toString());
        //    }

        //    return list;
        //}
    }
}
