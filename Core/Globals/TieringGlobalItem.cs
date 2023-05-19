using CalamityMod.Items.Accessories;
using CalNohitQoL.Core.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using static CalNohitQoL.Core.Systems.TieringSystem;

namespace CalNohitQoL.Core.Globals
{
    public class TieringGlobalItem : GlobalItem
    {
        #region Statics
        public TooltipLine CreateProgressionTooltip(int itemType, BossLockInformation bossLockInformation)
        {
            string text = $"This is post {bossLockInformation.BossName}, you should probably not be using it.";
            return new TooltipLine(Mod, $"ItemLock{itemType}", text)
            {
                OverrideColor = Color.Red
            };
        }
        #endregion
        #region Overrides
        public override bool InstancePerEntity => true;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Set the name color for the items that require it.
            TooltipLine nameLine = tooltips.FirstOrDefault((x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (nameLine != null && CalNohitQoLLists.SCalTooltips.Contains(item.type))
                nameLine.OverrideColor = CalNohitQoLUtils.TwoColorPulse(new Color(255, 132, 22), new Color(221, 85, 7), 4f);

            // Communitys tooltip.
            if (item.type == ModContent.ItemType<TheCommunity>())
            {
                string bossName = ProgressionSystem.GetLatestBossKilled();
                TooltipLine obj2 = new(Mod, "ProgressionTooltip", "Current power level: " + bossName);
                tooltips.Add(obj2);
            }

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
        #endregion
    }
}
