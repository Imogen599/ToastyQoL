using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Items.ReforgeToaster
{
    public class ToasterItem : ModItem
    {
        public override string Texture => "CalNohitQoL/ExtraTextures/toaster";
        private readonly Color LightColor = new(209, 180, 128);
        private readonly Color DarkColor = new(209,167,96);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toaster");
            Tooltip.SetDefault("'Ding'");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.shoot = ModContent.ProjectileType<ToasterProj>(); // "Shoot" your pet projectile.
            Item.buffType = ModContent.BuffType<ToasterBuff>(); // Apply buff upon usage of the Item.
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (nameLine != null)
            {
                nameLine.OverrideColor = CalNohitQoLUtils.TwoColorPulse(LightColor,DarkColor, 2f);
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
