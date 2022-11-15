using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.UI.QoLUI.PotionUI
{
    public struct PotionElement
    {
        public string PotionName;
        public string PotionPositives;
        public Texture2D PotionTexture;
        public Texture2D PotionGlowTexture;
        public int PotionBuffID;
        public Boss BossLocked;
        public float Scale;

        public PotionElement(string potionName, string potionPositives, Texture2D potionTexture, Texture2D potionGlowTexture, int potionBuffID, Boss bossLocked, float scale = 1)
        {
            PotionName = potionName;
            PotionPositives = potionPositives;
            PotionTexture = potionTexture;
            PotionGlowTexture = potionGlowTexture;
            PotionBuffID = potionBuffID;
            BossLocked = bossLocked;
            Scale = scale;
        }
    }
}
