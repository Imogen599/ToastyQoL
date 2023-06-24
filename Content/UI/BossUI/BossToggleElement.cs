using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Reflection;
using Terraria.ModLoader;

namespace ToastyQoL.Content.UI.BossUI
{
    public class BossToggleElement
    {
        public Texture2D Texture
        {
            get;
            private set;
        }

        public Texture2D GlowTexture
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public FieldInfo DownedBoolean
        {
            get;
            private set;
        }

        public float Weight
        {
            get;
            private set;
        }

        public float Scale
        {
            get;
            private set;
        }

        public BossToggleElement(string texturePath, string nameSingular, FieldInfo downedBoolean, float weight, float scale = 1f)
        {
            Texture = ModContent.Request<Texture2D>(texturePath, AssetRequestMode.ImmediateLoad).Value;
            GlowTexture = ModContent.Request<Texture2D>(texturePath + "Glow", AssetRequestMode.ImmediateLoad).Value;
            Name = nameSingular;
            if (downedBoolean.FieldType != typeof(bool) || !downedBoolean.IsStatic)
                throw new ArgumentException("The provided field info MUST be a static boolean.");
            DownedBoolean = downedBoolean;
            Weight = weight;
            Scale = scale;
        }

        public BossToggleElement Register()
        {
            BossTogglesUIManager.AddBossElement(this);
            return this;
        }

        public bool GetStatus() => (bool)DownedBoolean.GetValue(null);

        public void MarkAsStatus(bool status) => DownedBoolean.SetValue(null, status);

        public void ToggleValue() => DownedBoolean.SetValue(null, !GetStatus());
    }
}
