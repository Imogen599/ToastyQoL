using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ToastyQoL.Content.Items
{
    public class SacrificialDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Sacrificial Dagger");
            Tooltip.SetDefault("Reduces player health to 10\n" +
                "Reduces health to 1 if already at 10 health or less");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = Item.useTime = 5;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
        }

        public override bool? UseItem(Player player)
        {
            if (player.statLife > 10)
                player.statLife = 10;
            else
                player.statLife = 1;

            return null;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
            AddIngredient(ItemID.Hellstone, 4).
            AddTile(TileID.Anvils).
            Register();
        }
    }
}