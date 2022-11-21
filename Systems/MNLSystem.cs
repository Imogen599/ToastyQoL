using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.Calamitas;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalNohitQoL.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Systems
{
    public class MNLSystem : ModSystem
    {
        public static Dictionary<int, float> ActiveFightLength { get; private set; }
        public static Dictionary<int, float> BossMNLS => new()
        {
            // PreHardmode
            [NPCID.KingSlime] = 2100,
            [ModContent.NPCType<DesertScourgeHead>()] = 2100,
            [NPCID.EyeofCthulhu] = 2700,
            [ModContent.NPCType<Crabulon>()] = 2700,
            [NPCID.EaterofWorldsHead] = 2700,
            [NPCID.BrainofCthulhu] = 2700,
            [ModContent.NPCType<HiveMind>()] = 3600,
            [ModContent.NPCType<PerforatorHive>()] = 3600,
            [NPCID.QueenBee] = 2400,
            [NPCID.SkeletronHead] = 3600,
            [ModContent.NPCType<SlimeGodCore>()] = 4800,
            [NPCID.WallofFlesh] = 2400,
            // Hardmode
            [NPCID.QueenSlimeBoss] = 4500,
            [ModContent.NPCType<Cryogen>()] = 4200,
            [NPCID.Retinazer] = 4800,
            [NPCID.Spazmatism] = 4800,
            [ModContent.NPCType<BrimstoneElemental>()] = 4500,
            [NPCID.TheDestroyer] = 3900,
            [ModContent.NPCType<AquaticScourgeHead>()] = 3300,
            [NPCID.SkeletronPrime] = 4500,
            [ModContent.NPCType<CalamitasClone>()] = 6600,
            [NPCID.Plantera] = 4200,
            [ModContent.NPCType<Leviathan>()] = 6000,
            [ModContent.NPCType<Anahita>()] = 6000,
            [ModContent.NPCType<AstrumAureus>()] = 4200,
            [NPCID.Golem] = 3000,
            [ModContent.NPCType<PlaguebringerGoliath>()] = 3300,
            [NPCID.HallowBoss] = 5400,
            [NPCID.DukeFishron] = 3300,
            [ModContent.NPCType<RavagerBody>()] = 4200,
            [NPCID.CultistBoss] = 3300,
            [ModContent.NPCType<AstrumDeusHead>()] = 5400,
            [NPCID.MoonLordCore] = 7200,
            // PostMoonlord
            [ModContent.NPCType<ProfanedGuardianCommander>()] = 2400,
            [ModContent.NPCType<Bumblefuck>()] = 3600,
            [ModContent.NPCType<Providence>()] = 7200,
            [ModContent.NPCType<StormWeaverHead>()] = 2700,
            [ModContent.NPCType<CeaselessVoid>()] = 5100,
            [ModContent.NPCType<Signus>()] = 3300,
            [ModContent.NPCType<Polterghast>()] = 5100,
            [ModContent.NPCType<OldDuke>()] = 4200,
            [ModContent.NPCType<DevourerofGodsHead>()] = 7800,
            [ModContent.NPCType<Yharon>()] = 7200,
            [ModContent.NPCType<SupremeCalamitas>()] = 12000,
            [ModContent.NPCType<Artemis>()] = 9000,
            [ModContent.NPCType<Apollo>()] = 9000,
            [ModContent.NPCType<AresBody>()] = 9000,
            [ModContent.NPCType<ThanatosHead>()] = 9000,
        };
        public static Dictionary<int, float> TesterKilltimes => new()
        {
            // Pre Hardmode
            [NPCID.KingSlime] = 3600,
            [ModContent.NPCType<DesertScourgeHead>()] = 3600,
            [NPCID.EyeofCthulhu] = 5400,
            [ModContent.NPCType<Crabulon>()] = 5400,
            [NPCID.BrainofCthulhu] = 5400,
            [NPCID.EaterofWorldsHead] = 7200,
            [ModContent.NPCType<PerforatorHive>()] = 7200,
            [ModContent.NPCType<HiveMind>()] = 7200,
            [NPCID.QueenBee] = 7200,
            [NPCID.Deerclops] = 5400,
            [NPCID.SkeletronHead] = 7200,
            [ModContent.NPCType<SlimeGodCore>()] = 9000,
            [NPCID.WallofFlesh] = 7200,

            // Hardmode
            [NPCID.QueenSlimeBoss] = 7200,
            [ModContent.NPCType<Cryogen>()] = 10800,
            [ModContent.NPCType<BrimstoneElemental>()] = 10800,
            [ModContent.NPCType<AquaticScourgeHead>()] = 7200,
            [NPCID.TheDestroyer] = 10800,
            [NPCID.Retinazer] = 10800,
            [NPCID.Spazmatism] = 10800,
            [NPCID.SkeletronPrime] = 10800,
            [ModContent.NPCType<CalamitasClone>()] = 14400,
            [NPCID.Plantera] = 10800,
            [ModContent.NPCType<Leviathan>()] = 10800,
            [ModContent.NPCType<Anahita>()] = 10800,
            [ModContent.NPCType<AstrumAureus>()] = 10800,
            [NPCID.Golem] = 9000,
            [ModContent.NPCType<PlaguebringerGoliath>()] = 10800,
            [NPCID.HallowBoss] = 10800,
            [NPCID.DukeFishron] = 9000,
            [ModContent.NPCType<RavagerBody>()] = 10800,
            [NPCID.CultistBoss] = 9000,
            [ModContent.NPCType<AstrumDeusHead>()] = 7200,
            [NPCID.MoonLordCore] = 14400,

            // Post Moonlord
            [ModContent.NPCType<ProfanedGuardianCommander>()] = 5400,
            [ModContent.NPCType<Bumblefuck>()] = 7200,
            [ModContent.NPCType<Providence>()] = 14400,
            [ModContent.NPCType<StormWeaverHead>()] = 8100,
            [ModContent.NPCType<CeaselessVoid>()] = 10800,
            [ModContent.NPCType<Signus>()] = 7200,
            [ModContent.NPCType<Polterghast>()] = 10800,
            [ModContent.NPCType<OldDuke>()] = 10800,
            [ModContent.NPCType<DevourerofGodsHead>()] = 14400,
            [ModContent.NPCType<Yharon>()] = 14700,
            [ModContent.NPCType<SupremeCalamitas>()] = 18000,
            [ModContent.NPCType<Artemis>()] = 21600,
            [ModContent.NPCType<Apollo>()] = 21600,
            [ModContent.NPCType<AresBody>()] = 21600,
            [ModContent.NPCType<ThanatosHead>()] = 21600,
        };

        public static void DisplayMNLMessage(ref float BossAliveFrames, ref NPC Boss, bool bossDied)
        {
            if (BossAliveFrames >= 3 && Main.netMode != NetmodeID.Server)
            {
                if (!ActiveFightLength.TryGetValue(Boss.type, out float length))
                    return;
                // Under MNL Message
                if (BossAliveFrames < length)
                {
                    float secondsMNL = length / 60;
                    float secondsTimer = BossAliveFrames / 60;
                    float timerUnder = secondsMNL - secondsTimer;
                    timerUnder = (float)Math.Truncate((double)timerUnder * 100f) / 100f;
                    //if (Boss.type == ModContent.NPCType<SupremeCalamitas>() && !DownedBossSystem.downedSCal)
                    //{
                    //    timerUnder += 12f;
                    //}
                    FightStatsModPlayer.startTextDelay = true;
                    FightStatsModPlayer.timeUnderOrOverMNL = timerUnder;
                }
                // Over MNL Message
                else
                {
                    // This is a shitty fix for a bug where it "queues" an under mnl message for a when a boss dies but one is still alive.
                    // Checking if there are any bosses alive in OnKill stops it from showing at all as that runs before the boss even dies.
                    // I hate this. There isnt a post kill or anything.
                    // This doesnt stop that from being queued, it simply clears it if it has been already queued and we should be showing
                    // the above mnl message.

                    // ACTUALLY i think its because i wasnt "advancing the queue" unless a boss *was* alive, but im too lazy to go change it
                    // to see if thats the case when this works.
                    if (FightStatsModPlayer.startTextDelay)
                    {
                        FightStatsModPlayer.startTextDelay = false;
                        FightStatsModPlayer.timeUnderOrOverMNL = 0;
                    }
                    FightStatsModPlayer.startOnTextDelay = true;
                }
            }
            if (Toggles.SassMode && bossDied)
            {
                FightStatsModPlayer.bossIsDead = 2;
            }
            else if (Toggles.SassMode && !bossDied)
            {
                FightStatsModPlayer.bossIsDead = 1;
            }
            else
                FightStatsModPlayer.bossIsDead = 0;
        }

        public static bool UpdateActiveDictonary()
        {
            if (Toggles.TesterTimes)
                ActiveFightLength = TesterKilltimes;
            else
                ActiveFightLength = BossMNLS;
            return false;
        }
        public override void Load()
        {
            ActiveFightLength = new Dictionary<int, float>();
        }
    }
}
