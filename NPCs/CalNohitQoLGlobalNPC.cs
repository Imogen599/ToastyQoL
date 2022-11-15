using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace CalNohitQoL.NPCs
{
    public class CalNohitQoLGlobalNPC : GlobalNPC
    {
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

        internal bool PillarSpawn = true;
        internal bool NoLoot = false;

        internal static int eaterBoss = -1;
        internal static int brainBoss = -1;
        internal static int plantBoss = -1;

        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (npc.boss)
            {
                boss = npc.whoAmI;
            }

            switch (npc.type)
            {
                case NPCID.EaterofWorldsHead:
                    eaterBoss = npc.whoAmI;
                    break;

                case NPCID.BrainofCthulhu:
                    brainBoss = npc.whoAmI;
                    break;

                case NPCID.Plantera:
                    plantBoss = npc.whoAmI;
                    break;
                default:
                    break;
            }

            return true;
        }
        public static bool SpecificBossIsAlive(ref int bossID, int bossType)
        {
            if (bossID != -1)
            {
                if (Main.npc[bossID].active && Main.npc[bossID].type == bossType)
                {
                    return true;
                }
                else
                {
                    bossID = -1;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        internal static int boss = -1;

        public static bool AnyBossAlive()
        {
            if (boss == -1)
                return false;

            NPC npc = Main.npc[boss];

            if (npc.active && npc.type != NPCID.MartianSaucerCore && (npc.boss || npc.type == NPCID.EaterofWorldsHead))
                return true;
            boss = -1;
            return false;
        }
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
            if (CalNohitQoLWorld.NoSpawns)
            {
                spawnRate = 0;
                maxSpawns = 0;
            }
        }
       
    }
}