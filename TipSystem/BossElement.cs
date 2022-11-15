using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CalNohitQoL.TipSystem
{
    public struct BossElement
    {
        public string Name;
        public string Description;
        public string AttackPattern;
        public List<string> TipsList;
        public Color ColorName;
        public Texture2D Texture;
        public Texture2D GlowTexture;
        public float Scale;

        public BossElement(string name, string description, string attackPattern, List<string> tipsList, Color colorName,Texture2D mapTexture, Texture2D glowTexture, float scale = 1)
        {
            Name = name;
            Description = description;
            AttackPattern = attackPattern;
            TipsList = tipsList;
            ColorName = colorName;
            Texture = mapTexture;
            GlowTexture = glowTexture;
            Scale = scale;
        }

    }
}