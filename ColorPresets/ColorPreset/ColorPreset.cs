using System;
using UnityEngine;

namespace ColorPresets.ColorPreset
{
    [Serializable]
    public class ColorPreset
    {


        public Color leftSaber { get; set; } = new Color(168, 32, 32);
        public Color rightSaber { get; set; } = new Color(32, 100, 168);
        public Color lightOne { get; set; } = new Color(32, 100, 168);
        public Color lightTwo { get; set; } = new Color(48, 152, 255);
        public Color wall { get; set; } = new Color(168, 32, 32);
        public Color boostOne { get; set; } = new Color(192, 48, 48);
        public Color boostTwo { get; set; } = new Color(48, 152, 255);

        private string _name { get; set; }

        public ColorPreset(string name)
        {
            _name = name;
        }

        public ColorPreset(string name, Color leftSaber, Color rightSaber, Color lightOne, Color lightTwo, Color wall, Color boostOne, Color boostTwo)
        {
            _name = name;
            this.leftSaber = leftSaber;
            this.rightSaber = rightSaber;
            this.lightOne = lightOne;
            this.lightTwo = lightTwo;
            this.wall = wall;  
            this.boostOne = boostOne;
            this.boostTwo = boostTwo;
        }
        public override string ToString()
        {
            return _name;
        }

        public static string toJson(ColorPreset preset)
        {
            return JsonUtility.ToJson(preset);
        }
    }
}
