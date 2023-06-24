using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ToastyQoL.Core.Systems.TieringSystem;

namespace ToastyQoL.Core.Globals
{
    public class TieringGlobalItem : GlobalItem
    {
        public TooltipLine CreateProgressionTooltip(int itemType, BossLockInformation bossLockInformation)
        {
            string text = $"This is post {bossLockInformation.BossName}, you should probably not be using it.";
            return new TooltipLine(Mod, $"ItemLock{itemType}", text)
            {
                OverrideColor = Color.Red
            };
        }

        public override bool InstancePerEntity => true;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (Toggles.ItemTooltips)
                if (ItemShouldBeMarked(item.type, out var lockInformation))
                    tooltips.Add(CreateProgressionTooltip(item.type, lockInformation));

            if (Toggles.PotionTooltips)
                if (PotionShouldBeMarked(item.type, out var lockInformation))
                    tooltips.Add(CreateProgressionTooltip(item.type, lockInformation));
        }

        public override bool CanUseItem(Item item, Player player) => LockItemOrPotionFromUse(item.type);

        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if (Toggles.AccLock)
                return !ItemShouldBeMarked(item.type, out var _);

            return true;
        }
    }
}
