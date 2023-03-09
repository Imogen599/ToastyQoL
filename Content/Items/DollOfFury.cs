using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Buffs;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalNohitQoL.Content.NPCs;
using CalNohitQoL.Content.Projectiles;

namespace CalNohitQoL.Content.Items
{
    public class DollOfFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Doll of Fury");
            Tooltip.SetDefault("Completes the stack level of the Shattered Community\n" +
                "Cannot be used if the Rage Mode buff is active");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 34;
            Item.maxStack = 1;
            Item.rare = 10;
            Item.useAnimation = Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player) => !(player.FindBuffIndex(ModContent.BuffType<RageMode>()) > -1);

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            player.Calamity().rage += 10f; //Give a little rage to let the item work
            player.AddBuff(ModContent.BuffType<RageMode>(), 75); //1.25 second
            NPC.NewNPC(new EntitySource_ItemUse(player, Item), (int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<DollDummy>(), 1);
            Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, Vector2.Zero, ModContent.ProjectileType<DollStrike>(), 11111111, 0, player.whoAmI);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
            AddIngredient<HeartofDarkness>().
            AddIngredient(ItemID.Silk, 10).
            AddIngredient<AshesofAnnihilation>().
            AddTile<CosmicAnvil>().
            Register();
        }
    }
}