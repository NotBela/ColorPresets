using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

namespace ColorPresets.ColorPreset
{
    
    public class ColorPreset
    {
        private static readonly Color defaultLeftSaber = new Color(.6588f, .1254f, .1254f);

        private static readonly Color defaultRightSaber = new Color(.1254f, .3921f, .6588f);
        
        private static readonly Color defaultLightOne = new Color(.6588f, .1254f, .1254f);

        private static readonly Color defaultLightTwo = new Color(.1882f, .5960f, 1.0f);

        private static readonly Color defaultWall = new Color(.6588f, .1254f, .1254f);
        
        private static readonly Color defaultBoostOne = new Color(.6588f, .1254f, .1254f);
        
        private static readonly Color defaultBoostTwo = new Color(.6588f, .1254f, .1254f);

        public Color leftSaber { get; set; }
        
        public Color rightSaber { get; set; }
        
        public Color lightOne { get; set; }
        
        public Color lightTwo { get; set; }
        
        public Color wall { get; set; }
        
        public Color boostOne { get; set; }
        
        public Color boostTwo { get; set; }

        public string _name { get; set; }

        public ColorPreset(string name)
        {
            _name = name;
            leftSaber = defaultLeftSaber;
            rightSaber = defaultRightSaber;
            lightOne = defaultLightOne;
            lightTwo = defaultLightTwo;
            wall = defaultWall;
            boostOne = defaultBoostOne;
            boostTwo = defaultBoostTwo;
        }

        [JsonConstructor]
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

            // the name field is basically just used to make toString work
        }
    }

}
