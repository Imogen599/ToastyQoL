using CalamityMod;
using CalamityMod.Buffs.Placeables;
using CalamityMod.Items;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Placeables.Furniture;
using CalNohitQoL.Core.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Core.Globals
{
    public class TogglesGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        #region Max Stack
        public override void SetDefaults(Item item)
        {
            // Make everything have a high stack that isnt a coin.
            if (item.maxStack > 10 && item.maxStack != 100 && !(item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin))
                item.maxStack = 9999;
        }
        #endregion

        #region Infinite Ammo
        public override bool CanBeConsumedAsAmmo(Item ammo, Item weapon, Player player)
        {
            if (Toggles.InfiniteAmmo)
                if (ammo.ammo != 0)
                    return false;
            return true;
        }
        #endregion

        #region Infinite Consumables
        public override bool ConsumeItem(Item item, Player player)
        {
            if (Toggles.InfiniteConsumables)
            {
                if (item.damage > 0 && item.ammo == 0)
                    return false;
                if ((item.buffType > 0 || item.type == ItemID.RecallPotion || item.type == ItemID.PotionOfReturn || item.type == ItemID.WormholePotion) && (item.stack >= 30 || player.inventory.Any(i => i.type == item.type && !i.IsAir && i.stack >= 30)))
                    return false;
            }
            return true;
        }
        #endregion

        #region Update Progression System
        public static readonly List<int> ProgressionItems = new()
        {
            ModContent.ItemType<BloodOrange>(),
            ModContent.ItemType<MiracleFruit>(),
            ModContent.ItemType<Elderberry>(),
            ModContent.ItemType<Dragonfruit>(),
            ModContent.ItemType<CometShard>(),
            ModContent.ItemType<EtherealCore>(),
            ModContent.ItemType<PhantomHeart>(),
            ModContent.ItemType<MushroomPlasmaRoot>(),
            ModContent.ItemType<InfernalBlood>(),
            ModContent.ItemType<RedLightningContainer>(),
            ModContent.ItemType<ElectrolyteGelPack>(),
            ModContent.ItemType<StarlightFuelCell>(),
            ModContent.ItemType<Ectoheart>(),
            ModContent.ItemType<CelestialOnion>(),
            ItemID.DemonHeart,
        };

        public override bool? UseItem(Item item, Player player)
        {
            // Update the progression 
            if (ProgressionItems.Contains(item.type))
                GenericUpdatesModPlayer.UpdateProgressionSystem = true;
            return null;
        }
        #endregion

        #region Infinite Buffs
        public override void UpdateInventory(Item item, Player player)
        {
            // I don't like this item.
            if (Toggles.ItemLock && item.type == ModContent.ItemType<NormalityRelocator>() && !NPC.downedMoonlord)
                player.Calamity().normalityRelocator = false;

            if (Toggles.InfinitePotions)
            {
                if (item.buffType != 0 && item.stack > 1)
                    player.AddBuff(item.buffType, 2);

                switch (item.type)
                {
                    case ItemID.SharpeningStation:
                        player.AddBuff(BuffID.Sharpened, 2);
                        break;
                    case ItemID.AmmoBox:
                        player.AddBuff(BuffID.AmmoBox, 2);
                        break;
                    case ItemID.CrystalBall:
                        player.AddBuff(BuffID.Clairvoyance, 2);
                        break;
                    case ItemID.BewitchingTable:
                        player.AddBuff(BuffID.Bewitched, 2);
                        break;
                    case ItemID.SliceOfCake:
                        player.AddBuff(BuffID.SugarRush, 2);
                        break;
                }

                if (item.type == ModContent.ItemType<VigorousCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusPinkCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<SpitefulCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusYellowCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<WeightlessCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusBlueCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<ResilientCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusPurpleCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<CrimsonEffigy>())
                    player.AddBuff(ModContent.BuffType<CrimsonEffigyBuff>(), 2);

                else if (item.type == ModContent.ItemType<CorruptionEffigy>())
                    player.AddBuff(ModContent.BuffType<CorruptionEffigyBuff>(), 2);
            }
        }
        #endregion
    }
}
