using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.CalPlayer;

namespace CalNohitQoL.Items
{
    public class SacrificialDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Sacrificial Dagger");
            Tooltip.SetDefault("Reduces player health to 10\n" +
                "Reduces health to 1 if already at 10 health or less\n" +
                "Does not work while a boss is alive");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.rare = 1;
            Item.useAnimation = Item.useTime = 5;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
        }
        public override bool CanUseItem(Player player) => !CalamityPlayer.areThereAnyDamnBosses; //You're not supposed to use this while a boss is alive.

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            if (player.statLife > 10)
            {
                player.statLife = 10;
            }
            else
            {
                player.statLife = 1;
            }
			
            return true;
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