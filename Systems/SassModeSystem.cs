using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalamityMod.World;
using CalNohitQoL.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Systems
{
    internal class SassModeSystem : ModSystem
    {
        private static string SassToSay = null;
        public static readonly string[] GenericSassQuotesUnderLose = new string[]
        {
            "Use your noggin...",
            "This is going to be a looooong journey, isn't it?",
            "You can adjust the difficulty by clicking on the difficulty indicator in the top right corner!",
            "I hope the youtube views are worth it.",
            "Cheese belongs on crackers, not nohits.",
            "Fallgodding might help.",
            "Avoid projectiles, they hurt!",
            "It's okay, you can always blame the RNG.",
            "Did you get your loadout from CMT?",
            "You know you need to kill the boss for it to be a nohit, right?",
            "You literally died at the end. This isn't a nohit.",
            "I think Hello Kitty Online might be more your speed?",
            "The venn diagram of you and the boss' attacks is a perfect circle.",
            "Hello? you awake? haven't turned on your brain yet, have you?",
            "Unfortunately for you, difficulty is subjective.",
            "When was the last time you went outside?",
            "Skill issue detected. Solution: Touch Grass.",
            "You're supposed to nohit the boss, not the other way around...",
            "Were you trying to get hit?"
        };
        public static readonly string[] GenericSassQuotesWin = new string[]
        {
            "I hope you know your judge's favorite song.",
            "You should see a Doctor. Frequent, pre-mature kills can be a sign of a severe skill issue.",
            "Is okiiiii i guess...",
            "Come on you can do better than this... Maybe.",
            "Hey, your ego is showing.",
            "When was the last time you went outside?",
            "Skill issue detected. Solution: Touch Grass.",
            "Your mother must be so proud of you.",
        };
        public static readonly int[] SassSpecificBoss = new int[]
        {
            NPCID.KingSlime,
            ModContent.NPCType<Cryogen>(),
            ModContent.NPCType<Yharon>(),
            ModContent.NPCType<SupremeCalamitas>(),
            ModContent.NPCType<Apollo>(),
            ModContent.NPCType<Artemis>(),
            ModContent.NPCType<AresBody>(),
            ModContent.NPCType<ThanatosHead>(),
            NPCID.EyeofCthulhu,
            ModContent.NPCType<RavagerBody>(),
            ModContent.NPCType<DevourerofGodsHead>(),
            NPCID.Plantera,
            NPCID.WallofFlesh,
            NPCID.Retinazer,
            NPCID.Spazmatism,
            NPCID.QueenBee,
            ModContent.NPCType<HiveMind>(),
            ModContent.NPCType<PlaguebringerGoliath>(),
            ModContent.NPCType<Crabulon>(),
            NPCID.DukeFishron,
            ModContent.NPCType<OldDuke>(),
            ModContent.NPCType<AstrumDeusHead>(),
            ModContent.NPCType<StormWeaverHead>(),
            ModContent.NPCType<AquaticScourgeHead>(),
            ModContent.NPCType<Leviathan>(),
            ModContent.NPCType<Anahita>(),
        };
        /// <summary>
        ///  Handles the Sass Mode messages
        /// </summary>
        /// <param name="boss"></param>
        /// <param name="bossDead"></param>
        /// <param name="underMNL"></param>
        /// <param name="bossLifeRatio"></param>
        /// <param name="damageSource"></param>
        public static void SassModeHandler(NPC boss, bool bossDead, bool underMNL, float bossLifeRatio, float timerUnderMNL = 0, PlayerDeathReason damageSource = null)
        {
            if (bossDead)
                SassToSay = SassMode_BossDead(boss.type, bossLifeRatio, timerUnderMNL);
            else if (!bossDead && timerUnderMNL > 0)
                SassToSay = SassMode_BossAliveAndUnder(boss.type, bossLifeRatio, timerUnderMNL);

            if (SassToSay != null)
                CalNohitQoLUtils.DisplayText(SassToSay, Color.Orange);
            FightStatsModPlayer.currentBoss = null;
        }
        private static string SassMode_BossDead(int bossType, float bossLifeRatio, float timeUnderMNL)
        {
            string textToReturn = null;
            if (timeUnderMNL > 0)
            {

                int hi = Main.rand.Next(GenericSassQuotesWin.Length);

                textToReturn = GenericSassQuotesWin[hi];
            }
            else if (SassSpecificBoss.Contains(bossType) && Main.rand.NextBool())
            {
                if (bossType == ModContent.NPCType<Cryogen>())
                {
                    textToReturn = "You need to chill out.";
                }
                if (bossType == NPCID.Plantera)
                    textToReturn = "Well done, you killed a plant.";
                if (bossType == ModContent.NPCType<Crabulon>())
                    textToReturn = "I better not see this RRed 10 times...";
                if (bossType == ModContent.NPCType<SupremeCalamitas>())
                    textToReturn = "I bet you're going to go spam ping someone in #nohit-discussion now aren't you. Spoiler: No one cares.";
                if (bossType == NPCID.DukeFishron || bossType == ModContent.NPCType<Leviathan>() || bossType == ModContent.NPCType<Anahita>() || bossType == ModContent.NPCType<OldDuke>())
                    textToReturn = "Don't fish for compliments.";
            }
            return textToReturn;
        }
        private static string SassMode_BossAliveAndUnder(int bossType, float bossLifeRatio, float timeUnderMNL)
        {
            string textToReturn = null;
            #region Boss Specific
            if (SassSpecificBoss.Contains(bossType) && Main.rand.NextBool(5))
            {
                if (bossType == ModContent.NPCType<SupremeCalamitas>())
                {
                    if (bossLifeRatio == 1f)
                    {
                        textToReturn = "Another BH1 death?";
                    }
                }
                if (bossType == ModContent.NPCType<Cryogen>())
                {
                    textToReturn = "You need to chill out.";
                }
                if (bossType == NPCID.KingSlime)
                {
                    if (Main.rand.NextBool())
                        textToReturn = "Press 'A' and 'D' to move!";
                    else
                        textToReturn = "A Guinea Pig did better than you...";
                }
                if (bossType == NPCID.Plantera)
                    textToReturn = "Well done, you killed a plant.";
                if (bossType == ModContent.NPCType<Yharon>())
                {
                    float yharonHPPercent = 0;
                    if (CalamityWorld.death)
                        yharonHPPercent = 0.16f;
                    else if (CalamityWorld.revenge)
                        yharonHPPercent = 0.11f;
                    if (yharonHPPercent != 0)
                    {
                        if (bossLifeRatio <= yharonHPPercent)
                        {
                            textToReturn = "This is my MESSAGE TO MY MASTER, this is a FIGHT YOU DID NOT WIN!";
                        }
                    }
                }
                if (bossType == NPCID.WallofFlesh || bossType == NPCID.Deerclops)
                {
                    textToReturn = "Press 'Space' to jump!";
                }
                if (bossType == NPCID.EyeofCthulhu || bossType == NPCID.Retinazer || bossType == NPCID.Spazmatism)
                {
                    textToReturn = "This Boss has better eyesight than you...";
                }
                if (bossType == NPCID.QueenBee || bossType == ModContent.NPCType<PlaguebringerGoliath>() || bossType == ModContent.NPCType<HiveMind>())
                {
                    textToReturn = "'Hive' got a plan for you: Give up.";
                }
                if (bossType == ModContent.NPCType<Artemis>() || bossType == ModContent.NPCType<Apollo>() || bossType == ModContent.NPCType<AresBody>() || bossType == ModContent.NPCType<ThanatosHead>())
                {
                    textToReturn = "You fell right into his calculations...";
                }
                if (bossType == ModContent.NPCType<OldDuke>() || bossType == NPCID.DukeFishron)
                {
                    if (Main.rand.NextBool())
                        textToReturn = "Double tap to dash!";
                    else
                        textToReturn = "Excessive Salt can lead to high blood pressure.";
                }
                if (bossType == ModContent.NPCType<Leviathan>() || bossType == ModContent.NPCType<Anahita>() || bossType == ModContent.NPCType<AquaticScourgeHead>())
                    textToReturn = "Excessive Salt can lead to high blood pressure.";
            }
            #endregion
            else
                textToReturn = Main.rand.NextFromList(GenericSassQuotesUnderLose);
            if (textToReturn == null)
                textToReturn = Main.rand.NextFromList(GenericSassQuotesUnderLose);
            return textToReturn;
        }
    }
}
