namespace ColorPresets.ColorPreset
{

    // this exists because newtonsoft is dumb and cant handle unity colors or the game crashes
    // yippee!!!!

    public class Color
    {
        public float r;
        public float g;
        public float b;

        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public UnityEngine.Color convertToUnityColor()
        {
            return new UnityEngine.Color(r, g, b, 1f);
        }

        public static Color convertFromUnityColor(float r, float g, float b)
        {
            return new Color(r, g, b);
        }
        public static Color convertFromUnityColor(UnityEngine.Color color)
        {
            return new Color(color.r, color.g, color.b);
        }
    }
}
