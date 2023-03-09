using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using CalNohitQoL.Content.Buffs;

namespace CalNohitQoL.Content.Items
{
    public class DoubleShroom : ModItem
    {
        public int timer = 0;
        public int timer2 = 120;
        public int timer3 = 240;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Double Shrooms");
            Tooltip.SetDefault("Trippier (Hage)\nThis gets replaced.");
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
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>("CalNohitQoL/Items/DoubleShroom", (AssetRequestMode)2).Value;
            spriteBatch.End();
            spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, null, null, null, null, Main.UIScaleMatrix);
            spriteBatch.Draw(texture, position + (Vector2.One * 3).RotatedBy(MathHelper.ToRadians(timer)), frame, Color.Red * 1000, 0, origin, scale, 0, 0);
            spriteBatch.Draw(texture, position + (Vector2.One * 3).RotatedBy(MathHelper.ToRadians(timer2)), frame, Color.Green * 1000, 0, origin, scale, 0, 0);
            spriteBatch.Draw(texture, position + (Vector2.One * 3).RotatedBy(MathHelper.ToRadians(timer3)), frame, Color.Cyan * 1000, 0, origin, scale, 0, 0);

            spriteBatch.End();
            spriteBatch.Begin(0, null, null, null, null, null, Main.UIScaleMatrix);
            spriteBatch.Draw(texture, position, frame, drawColor, 0, origin, scale, 0, 0);
            timer++;
            timer2++;
            timer3++;
            if (timer > 360)
                timer = 0;
            if (timer2 > 360)
                timer2 = 0;
            if (timer3 > 360)
                timer3 = 0;
            return false;
        }
    }
}