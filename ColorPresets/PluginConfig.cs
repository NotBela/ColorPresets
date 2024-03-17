using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BeatSaberMarkupLanguage.GameplaySetup;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ColorPresets.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual bool isEnabled { get; set; } = true;

        public virtual int colorPresetsCreated { get; set; } = 0;

        public virtual string selected { get; set; } = "NewPreset0";

        [NonNullable]
        public virtual List<ColorPreset.ColorPreset> colorsList { get; set; } = new List<ColorPreset.ColorPreset>() {new ColorPreset.ColorPreset("NewPreset0")}.ToList();

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            
            
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
            if (!isEnabled)
            {
                GameplaySetup.instance.RemoveTab("ColorPresets");
            }
            else GameplaySetup.instance.AddTab("ColorPresets", "ColorPresets.Views.GameplaySetup.bsml", PluginConfig.Instance, MenuType.All);
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}