using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace ToastyQoL.Content.UI.PotionUI
{
    public class PotionMod
    {
        public readonly string Name;

        public readonly Texture2D SmallUIModIcon;

        public readonly Texture2D SmallUIModIconGlow;


        public PotionMod(string modInternalName, string smallUIModIconPath) 
        { 
            Name = modInternalName;
            SmallUIModIcon = ModContent.Request<Texture2D>(smallUIModIconPath, AssetRequestMode.ImmediateLoad).Value;
            SmallUIModIconGlow = ModContent.Request<Texture2D>(smallUIModIconPath + "Glow", AssetRequestMode.ImmediateLoad).Value;
        }
    }
}
