using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.Yharon;
using CalNohitQoL.Content.Items;
using CalNohitQoL.Core.ModPlayers;
using CalNohitQoL.Core.Systems;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Core.Globals
{
    public class CalNohitQoLGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        internal static bool bossActive = false;
        internal static float currentTimer;

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == ModContent.NPCType<CalamityMod.NPCs.TownNPCs.FAP>())
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<NostShroom>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1, 0, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DoubleShroom>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1, 0, 0, 0);
                nextSlot++;
            }
        }
        public override void OnKill(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.BrainofCthulhu:
                    CalNohitQoL.DownedBrain = true;
                    break;
                case NPCID.EaterofWorldsHead:
                    if (npc.boss == true)
                        CalNohitQoL.DownedEater = true;
                    break;
            }
            if (FightStatsModPlayer.FightStats.BossAliveFrames > 0 && MNLSystem.ActiveFightLength.ContainsKey(npc.type) && Toggles.MNLIndicator && !(FightStatsModPlayer.BossRushActiveFrames > 0))
            {
                MNLSystem.DisplayMNLMessage(ref FightStatsModPlayer.FightStats.BossAliveFrames, ref FightStatsModPlayer.FightStats.Boss, true);
            }
            if (FightStatsModPlayer.IsBossRushActive && Toggles.MNLIndicator && npc.boss == true)
            {
                TimeSpan time = TimeSpan.FromSeconds(FightStatsModPlayer.BossRushActiveFrames / 60);

                string hours;
                if (time.Hours < 1 && time.Days < 1)
                    hours = "";
                else
                    hours = (time.Days * 24 + time.Hours).ToString() + ":";

                string minutes;
                if (hours != "" || time.Minutes >= 10)
                    minutes = time.Minutes.ToString() + ":";
                else
                    minutes = "0" + time.Minutes.ToString() + ":";

                string seconds;
                if (time.Seconds >= 10)
                    seconds = time.Seconds.ToString();
                else
                    seconds = "0" + time.Seconds.ToString();

                string line = hours + minutes + seconds;
                CalNohitQoLUtils.DisplayText($"[c/e7684b:Current Time:] [c/fccccf:{line}]");
            }
            if (ProgressionBosses.Contains(npc.type))
                GenericUpdatesModPlayer.UpdateProgressionSystem = true;
        }

        internal readonly List<int> ProgressionBosses = new()
        {
            ModContent.NPCType<Crabulon>(),
            ModContent.NPCType<SlimeGodCore>(),
            NPCID.WallofFlesh,
            NPCID.TheDestroyer,
            NPCID.Retinazer,
            NPCID.Spazmatism,
            NPCID.SkeletronPrime,
            ModContent.NPCType<AstrumAureus>(),
            NPCID.Golem,
            ModContent.NPCType<RavagerBody>(),
            ModContent.NPCType<AstrumDeusHead>(),
            NPCID.MoonLordCore,
            ModContent.NPCType<Bumblefuck>(),
            ModContent.NPCType<Providence>(),
            ModContent.NPCType<Polterghast>(),
            ModContent.NPCType<Yharon>()
        };

        internal bool NoLoot = false;

        public override bool PreKill(NPC npc)
        {
            if (NoLoot)
            {
                return false;
            }
            return true;
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (Toggles.NoSpawns)
            {
                spawnRate = 0;
                maxSpawns = 0;
            }
        }

    }
}