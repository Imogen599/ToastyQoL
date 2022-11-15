using CalNohitQoL.Items;
using CalNohitQoL.ModPlayers;
using CalNohitQoL.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Globals
{
    public class CalNohitQoLGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        internal static bool bossActive = false;
        internal static NPC currentBoss;
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
            if (FightStatsModPlayer.FightStats.BossAliveFrames > 0 && MNLSystem.BossMNLS.ContainsKey(npc.type) && Toggles.MNLIndicator && !(FightStatsModPlayer.BossRushActiveFrames > 0))
            {
                MNLSystem.DisplayMNLMessage(ref FightStatsModPlayer.FightStats.BossAliveFrames, ref FightStatsModPlayer.FightStats.Boss, true, null);
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
        }

        internal static int[] Bosses = {
            NPCID.KingSlime,
            NPCID.EyeofCthulhu,
            //NPCID.EaterofWorldsHead,
            NPCID.BrainofCthulhu,
            NPCID.QueenBee,
            NPCID.SkeletronHead,
            NPCID.QueenSlimeBoss,
            NPCID.TheDestroyer,
            NPCID.SkeletronPrime,
            NPCID.Retinazer,
            NPCID.Spazmatism,
            NPCID.Plantera,
            NPCID.Golem,
            NPCID.DukeFishron,
            NPCID.HallowBoss,
            NPCID.CultistBoss,
            NPCID.MoonLordCore,
            NPCID.MartianSaucerCore,
            NPCID.Pumpking,
            NPCID.IceQueen,
            NPCID.DD2Betsy,
            NPCID.DD2OgreT3,
            NPCID.IceGolem,
            NPCID.SandElemental,
            NPCID.Paladin,
            NPCID.Everscream,
            NPCID.MourningWood,
            NPCID.SantaNK1,
            NPCID.HeadlessHorseman,
            NPCID.PirateShip
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