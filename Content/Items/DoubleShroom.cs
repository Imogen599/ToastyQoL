using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Content.Buffs;

namespace ToastyQoL.Content.Items
{
    public class DoubleShroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Double Shrooms");
            // Tooltip.SetDefault("Trippier (Hage)\nThis gets replaced.");
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 48;
            Item.useTurn = true;
            Item.maxStack = 30;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<DoubleTrippy>();
            Item.buffTime = 216000;
            Item.value = Item.buyPrice(1);
            Item.rare = -12;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine obj = tooltips.LastOrDefault((x) => x.Name == "Tooltip1" && x.Mod == "Terraria");
            obj.Text = "Creates 8 copies of things in various screen positions\nMay be hard on the eyes, be cautious with use.";
            obj.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);

            TooltipLine obj2 = tooltips.FirstOrDefault((x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            obj2.Text = "Are you worthy?";
            obj2.OverrideColor = new Color(244, 127, 255, 255);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
            for (int i = 0; i < 3; i++)
            {
                Color drawColor2 = Main.hslToRgb(i / 3, 1f, 0.5f);
                spriteBatch.Draw(texture, position + (Vector2.One * 3).RotatedBy(MathHelper.TwoPi * i / 3 + Main.GlobalTimeWrappedHourly), frame, drawColor2, 0, origin, scale, 0, 0);
            }
            spriteBatch.Draw(texture, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0);
            return false;
        }
    }
}