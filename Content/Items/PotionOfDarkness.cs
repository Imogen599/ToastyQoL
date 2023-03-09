using CalamityMod;
using CalamityMod.CalPlayer;
using CalNohitQoL.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace CalNohitQoL.Content.Items
{
    public class PotionOfDarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potion of Rage");
            Tooltip.SetDefault("Fills your rage bar, and keeps it from draining for 2 seconds after using\nDoes not work while a boss is alive");
            SacrificeTotal = 20;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.useTurn = true;
            Item.maxStack = 30;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 2);
        }
        public override bool? UseItem(Player player)
        {
            if (!CalamityPlayer.areThereAnyDamnBosses)
            {
                player.Calamity().rage = player.Calamity().rageMax + 5;
                GenericUpdatesModPlayer.KeepRageMaxedTimer = 120;
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Mushroom, 3).AddTile(TileID.Bottles).Register();
        }
    }
}
