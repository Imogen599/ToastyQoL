using CalamityMod;
using CalamityMod.Events;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.NPCs.AdultEidolonWyrm;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace CalNohitQoL.Items
{
    public class EidolicRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Eidolic Rod");
            Tooltip.SetDefault("Summons the Adult Eidolon Wyrm\n" +
                "Grants Amidias' Blessing when summoned\n" +
                "Can be used regardless of biome");
        }

        public override void SetDefaults()
        {
            Item.width = 88;
            Item.height = 88;
            Item.maxStack = 1;
            Item.rare = 10;
            Item.useAnimation = Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool CanUseItem(Player player)
        {
            return (!NPC.AnyNPCs(ModContent.NPCType<AdultEidolonWyrmHead>()) && !BossRushEvent.BossRushActive);
        }

        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            player.AddBuff(ModContent.BuffType<AmidiasBlessing>(), 36000);
			SoundEngine.PlaySound(AdultEidolonWyrmHead.SpawnSound, player.Center);
			if (Main.netMode != NetmodeID.MultiplayerClient)
				NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AdultEidolonWyrmHead>());
			else
				NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, ModContent.NPCType<AdultEidolonWyrmHead>());

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
            AddIngredient(ItemID.RodofDiscord).
            AddIngredient<Lumenyl>(20).
            AddTile(TileID.Anvils).
            Register();
        }
    }
}