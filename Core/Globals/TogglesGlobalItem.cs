using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ToastyQoL.Core.Globals
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

        #region Infinite Buffs
        public override void UpdateInventory(Item item, Player player)
        {
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
            }
        }
        #endregion
    }
}
