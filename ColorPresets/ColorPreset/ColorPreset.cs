using ColorPresets.Configuration;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace ColorPresets.ColorPreset
{
    
    public class ColorPreset
    {
        public static readonly Color defaultLeftSaber = new Color(.6588f, .1254f, .1254f);

        public static readonly Color defaultRightSaber = new Color(.1254f, .3921f, .6588f);
        
        public static readonly Color defaultLightOne = new Color(.6588f, .1254f, .1254f);

        public static readonly Color defaultLightTwo = new Color(.1882f, .5960f, 1.0f);

        public static readonly Color defaultWall = new Color(.6588f, .1254f, .1254f);
        
        public static readonly Color defaultBoostOne = new Color(.6588f, .1254f, .1254f);

        public static readonly Color defaultBoostTwo = new Color(.1882f, .5960f, 1.0f);

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

        public ColorPreset(string name, ColorScheme scheme)
        {
            _name = name;
            leftSaber = Color.convertFromUnityColor(scheme.saberAColor) ?? defaultLeftSaber;
            rightSaber = Color.convertFromUnityColor(scheme.saberBColor) ?? defaultRightSaber;
            lightOne = Color.convertFromUnityColor(scheme.environmentColor0) ?? defaultLightOne;
            lightTwo = Color.convertFromUnityColor(scheme.environmentColor1) ?? defaultLightTwo;
            wall = Color.convertFromUnityColor(scheme.obstaclesColor) ?? defaultWall;
            boostOne = Color.convertFromUnityColor(scheme.environmentColor0Boost) ?? defaultBoostOne;
            boostTwo = Color.convertFromUnityColor(scheme.environmentColor1Boost) ?? defaultBoostTwo;
        }

        [JsonConstructor]
        public ColorPreset(string name, Color leftSaber, Color rightSaber, Color lightOne, Color lightTwo, Color wall, Color boostOne, Color boostTwo)
        {
            _name = name;
            this.leftSaber = leftSaber ?? defaultLeftSaber;
            this.rightSaber = rightSaber ?? defaultRightSaber;
            this.lightOne = lightOne ?? defaultLightOne;
            this.lightTwo = lightTwo ?? defaultLightTwo;
            this.wall = wall ?? defaultWall;  
            this.boostOne = boostOne ?? defaultBoostOne;
            this.boostTwo = boostTwo ?? defaultBoostTwo;
        }

        public override string ToString()
        {
            return _name;

            // the name field is basically just used to make toString work
        }

        public static ColorScheme convertToBaseGameScheme(ColorPreset preset)
        {
            return new ColorScheme(preset._name,
                "",
                true,
                preset._name,
                false,
                preset.leftSaber.convertToUnityColor(),
                preset.rightSaber.convertToUnityColor(),
                preset.lightOne.convertToUnityColor(),
                preset.lightTwo.convertToUnityColor(),
                true,
                preset.boostOne.convertToUnityColor(),
                preset.boostTwo.convertToUnityColor(),
                preset.wall.convertToUnityColor()
            );
        }
        


        //UNUSED
        
        /*
        public static ColorPreset convertToColorPreset(string name, ColorScheme scheme)
        {
            var returnPreset = new ColorPreset(
                name,
                Color.convertFromUnityColor(scheme.saberAColor),
                Color.convertFromUnityColor(scheme.saberBColor),
                Color.convertFromUnityColor(scheme.environmentColor0),
                Color.convertFromUnityColor(scheme.environmentColor1),
                Color.convertFromUnityColor(scheme.obstaclesColor),
                Color.convertFromUnityColor(scheme.environmentColor0Boost),
                Color.convertFromUnityColor(scheme.environmentColor1Boost)
            );

            return returnPreset.replaceNullWithColor(ColorPresets.PresetConfig.PresetSaveLoader.readPreset(PluginConfig.Instance.selectedPreset));
        }
        
        public ColorPreset replaceNullWithColor(ColorPreset other)
        {
            var returnScheme = this;

            this.leftSaber = this.leftSaber ?? other.leftSaber ?? throw new Exception("BothValsAreNull");
            this.rightSaber = this.rightSaber ?? other.rightSaber ?? throw new Exception("BothValsAreNull");
            this.lightOne = this.lightOne ?? other.lightOne ?? throw new Exception("BothValsAreNull");
            this.lightTwo = this.lightTwo ?? other.lightTwo ?? throw new Exception("BothValsAreNull");
            this.wall = this.wall ?? other.wall ?? throw new Exception("BothValsAreNull");
            this.boostOne = this.boostOne ?? other.boostOne ?? throw new Exception("BothValsAreNull");
            this.boostTwo = this.boostTwo ?? other.boostTwo ?? throw new Exception("BothValsAreNull");

            return returnScheme;
        }
        */
    }

}
