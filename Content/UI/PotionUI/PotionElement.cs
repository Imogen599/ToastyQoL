using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ToastyQoL.Content.UI.PotionUI
{
    public class PotionElement
    {
        public string PotionName;

        public string PotionDescription;

        public Texture2D PotionTexture;

        public Texture2D PotionGlowTexture;

        public int PotionBuffID;

        public Func<bool> IsAvailable;

        public float Weight;

        public float Scale;

        public bool Selected;

        public PotionElement(string potionName, string potionDescription, string potionTexturePath, int potionBuffID, Func<bool> isAvailable, float weight, float scale = 1)
        {
            PotionName = potionName;
            PotionDescription = potionDescription;
            PotionTexture = ModContent.Request<Texture2D>(potionTexturePath, AssetRequestMode.ImmediateLoad).Value;
            PotionGlowTexture = ModContent.Request<Texture2D>(potionTexturePath + "Glow", AssetRequestMode.ImmediateLoad).Value;
            PotionBuffID = potionBuffID;
            IsAvailable = isAvailable;
            Weight = weight;

            Scale = scale;
        }

        public void Save(TagCompound tag)
        {
            tag["Selected"] = Selected;
        }

        public void Load(TagCompound tag)
        {
            Selected = tag.GetBool("Selected");
        }
    }
}
