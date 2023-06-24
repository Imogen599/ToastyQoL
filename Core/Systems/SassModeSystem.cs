using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ToastyQoL.Core.Systems
{
    internal class SassModeSystem : ModSystem
    {
        private static string SassToSay = null;

        internal static List<string> GenericSassQuotesLose
        {
            get;
            set;
        } = new();

        internal static List<string> GenericSassQuotesWin
        {
            get;
            set;
        } = new();

        internal static Dictionary<int, List<string>> SassSpecificBossQuotes
        {
            get;
            set;
        } = new();

        public override void Load()
        {
            GenericSassQuotesLose = new()
            {
                "Use your noggin...",
                "This is going to be a looooong journey, isn't it?",
                "I hope the youtube views are worth it.",
                "Cheese belongs on crackers, not nohits.",
                "Fallgodding might help.",
                "Avoid projectiles, they hurt!",
                "It's okay, you can always blame the RNG.",
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

            GenericSassQuotesWin = new()
            {
                "I hope you know your judge's favorite song.",
                "You should see a Doctor. Frequent, premature kills can be a sign of a severe skill issue.",
                "Is okiiiii i guess...",
                "Come on you can do better than this... Maybe.",
                "Hey, your ego is showing.",
                "When was the last time you went outside?",
                "Skill issue detected. Solution: Touch Grass.",
                "Your mother must be so proud of you.",
            };

            SassSpecificBossQuotes = new()
            {
                [NPCID.KingSlime] = new() { "Press 'A' and 'D' to move!", "A Guinea Pig did better than you..." },
                [NPCID.EyeofCthulhu] = new() { "This Boss has better eyesight than you..." },
                [NPCID.Plantera] = new () { "Well done, you killed a plant." },
                [NPCID.WallofFlesh] = new() { "Press 'Space' to jump!" },
                [NPCID.Deerclops] = new() { "Press 'Space' to jump!" },
                [NPCID.Retinazer] = new() { "This Boss has better eyesight than you..." },
                [NPCID.Spazmatism] = new() { "This Boss has better eyesight than you..." },
                [NPCID.QueenBee] = new() { "'Hive' got a plan for you: Give up." },
                [NPCID.DukeFishron] = new() { "Don't fish for compliments.", "Double tap to dash!" }
            };
        }

        public override void Unload()
        {
            GenericSassQuotesLose = null;
            GenericSassQuotesWin = null;
            SassSpecificBossQuotes = null;
        }

        public static void SassModeHandler(NPC boss, bool bossDead)
        {
            if (bossDead)
                SassToSay = SassMode_BossDead(boss.type);
            else
                SassToSay = SassMode_BossAlive(boss.type);

            if (SassToSay != null)
                ToastyQoLUtils.DisplayText(SassToSay, Color.Orange);
        }

        private static string SassMode_BossDead(int bossType)
        {
            string textToReturn;

            int index = Main.rand.Next(GenericSassQuotesWin.Count);

            textToReturn = GenericSassQuotesWin[index];
            if (SassSpecificBossQuotes.TryGetValue(bossType, out var texts) && Main.rand.NextBool())
                textToReturn = texts[Main.rand.Next(0, texts.Count)];

            return textToReturn;
        }

        private static string SassMode_BossAlive(int bossType)
        {
            string textToReturn;
            if (SassSpecificBossQuotes.TryGetValue(bossType, out var texts) && Main.rand.NextBool(5))
                textToReturn = texts[Main.rand.Next(0, texts.Count)];
            else
                textToReturn = Main.rand.NextFromList(GenericSassQuotesLose.ToArray());

            return textToReturn;
        }
    }
}
