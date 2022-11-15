using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.NPCs;
using CalamityMod.CalPlayer;
using CalamityMod.World;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalNohitQoL.NPCs;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.Calamitas;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.GreatSandShark;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SulphurousSea;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalNohitQoL.UI;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria.DataStructures;
using CalNohitQoL.UI.QoLUI;
using Terraria.GameContent;
using ReLogic.Graphics;

namespace CalNohitQoL
{
    public static class CalNohitQoLUtils
    {
        private readonly static int fireBlastProj = ModContent.ProjectileType<SCalBrimstoneFireblast>();
        private readonly static int gigaBlastProj = ModContent.ProjectileType<SCalBrimstoneGigablast>();
        public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
        {
            if (condition)
            {
                list.Add(type);
            }
        }
        #region SCAL BH SIMS
        public static void SCalBH1Sim(this NPC NPC, Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset)
        {
            #region FirstAttack
            //if (BHType <= 1 && !endBH)
            //{
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int BH1FrequencyAtPlayer = SpawnFrequency * 6;

                if (overallTimer % SpawnFrequency == (SpawnFrequency - 1))
                {
                    if (overallTimer % BH1FrequencyAtPlayer == (BH1FrequencyAtPlayer - 1)) //Aimed projectile
                    {
                        float distance = Main.rand.NextBool() ? -1000f : 1000f;
                        float velocity = (distance == -1000f ? 4f : -4f) * offset;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    if (bhStageCounter == 1f) // Blasts from above
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from above, left, and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 3f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
            //}
            #endregion
        }
        public static void SCalBH2Sim(this NPC NPC, Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (bhStageCounter == 1f) // Fireblasts
                {
                    if (overallTimer % 180 == 179) // Blasts from top
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);
                }
                else if (bhStageCounter == 2f)
                {
                    if (overallTimer % 180 == 179) // Blasts from right
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -5f * offset, 0f, fireBlastProj, 1, 0f, Main.myPlayer);
                }
                else if (bhStageCounter == 3f)
                {
                    if (overallTimer % 180 == 179) // Blasts from top
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);
                }
                int BH2Frequency = SpawnFrequency + 1;
                int BH2FrequencyAtPlayer = BH2Frequency * 6;

                if (overallTimer % BH2Frequency == (BH2Frequency - 1)) // Hellblasts
                {
                    // This BH does not have aimed projectiles. May make this toggleable for  the funsies.
                    //
                    //if (npc.ai[1] % BH2FrequencyAtPlayer == (BH2FrequencyAtPlayer - 1)) // Aimed projectile
                    //{
                    //	float distance = Main.rand.NextBool() ? -1000f : 1000f;
                    //	float velocity = (distance == -1000f ? 4f : -4f) * offset;
                    //	Projectile.NewProjectile(player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    //}
                    if (bhStageCounter == 1f) // Blasts from below
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y + 1000f, 0f, -4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        public static void SCalBH3Sim(this NPC NPC, Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) // Gigablasts and Fireblasts
            {
                if (overallTimer < 900)
                    overallTimer += 1800f;
                if (overallTimer % 180 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 240 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * offset, gigaBlastProj, 1, 0f, Main.myPlayer);

                int BH3Frequency = SpawnFrequency + 4;
                int BH3FrequencyAtPlayer = BH3Frequency * 6;

                if (overallTimer % BH3Frequency == (BH3Frequency - 1))
                {
                    // No aimed hellblasts
                    //
                    //if (npc.ai[1] % BH3FrequencyAtPlayer == (BH3FrequencyAtPlayer - 1)) // Aimed projectile
                    //{
                    //	float distance = Main.rand.NextBool() ? -1000f : 1000f;
                    //	float velocity = (distance == -1000f ? 4f : -4f) * offset;
                    //	Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    //}
                    if (bhStageCounter == 1f) // Blasts from above
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        public static void SCalBH4Sim(this NPC NPC, Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset, bool RevengeanceMode, float moonAI)
        {

            if (Main.netMode != NetmodeID.MultiplayerClient) // Gigablasts and Fireblasts
            {
                if (overallTimer < 900)
                    overallTimer += 2700f;
                if (overallTimer % 180 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 240 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * offset, gigaBlastProj, 1, 0f, Main.myPlayer);

                int divisor = (RevengeanceMode ? 225 : (Main.expertMode ? 450 : 675));
                if (overallTimer % divisor == 0 && Main.expertMode) // Moons
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, moonAI);

                }

                int BH4Frequency = SpawnFrequency + 6;
                int BH4FrequencyAtPlayer = BH4Frequency * 6;

                if (overallTimer % BH4Frequency == (BH4Frequency - 1))
                {
                    // You get the idea, no aimed hellblasts.
                    //
                    //if (npc.ai[1] % BH4FrequencyAtPlayer == (BH4FrequencyAtPlayer - 1)) // Aimed projectile
                    //{
                    //	float distance = Main.rand.NextBool() ? -1000f : 1000f;
                    //	float velocity = (distance == -1000f ? 4f : -4f) * offset;
                    //	Projectile.NewProjectile(player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    //}
                    if (bhStageCounter == 1f) // Blasts from below
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y + 1000f, 0f, -4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        public static void SCalBH5Sim(this NPC NPC, Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset, bool RevengeanceMode, float moonAI, bool spawnMoons)
        {
            // TODO: Make this be better, add a delay before the moons spawn or smth
            if (Main.netMode != NetmodeID.MultiplayerClient) // Gigablasts, Fireblasts, Skulls  
            {

                if (overallTimer < 900)
                {

                    overallTimer += 3600;
                }
                if (overallTimer == 3605)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 0);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 1);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 2);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 3);
                }
                if (overallTimer % 240 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 360 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * offset, gigaBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 30 == 0)
                {
                    int random = Main.rand.Next(-500, 501);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + random, -5f * offset, 0f, ModContent.ProjectileType<BrimstoneWave>(), 1, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y - random, 5f * offset, 0f, ModContent.ProjectileType<BrimstoneWave>(), 1, 0f, Main.myPlayer);
                }
                int BH5Frequency = SpawnFrequency + 8;
                int BH5FrequencyAtPlayer = BH5Frequency * 6;

                if (overallTimer % BH5Frequency == (BH5Frequency - 1))
                {
                    if (overallTimer % BH5FrequencyAtPlayer == (BH5FrequencyAtPlayer - 1)) // Aimed projectile
                    {
                        float distance = Main.rand.NextBool() ? -1000f : 1000f;
                        float velocity = (distance == -1000f ? 4f : -4f) * offset;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    if (bhStageCounter == 1f) // Blasts from above
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from above, left, and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 3f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }

       
        #endregion
        #region MISC UTILS
        public static Color TwoColorPulse(Color color1, Color color2, float time)
        {
            float timeScale = (float)((Math.Sin((double)((float)Math.PI * 2f / time) * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
            return Color.Lerp(color1, color2, timeScale);
            //return Color.Lerp(firstColor, secondColor, colorMePurple);
        }
        public static string TwoColorPulseHex(Color color1, Color color2, float time)
        {
            float timeScale = (float)((Math.Sin((double)((float)Math.PI * 2f / time) * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
            return Color.Lerp(color1, color2, timeScale).Hex3();
            //return Color.Lerp(firstColor, secondColor, colorMePurple);
        }
        public static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
        #endregion
        #region PROGRESSION SYSTEM
        public enum PUpgradeBossProgressionOrder
        {
            Crabulon,
            SlimeGod,
            WallOfFlesh,
            PostAllMechs,
            AstrumAureus,
            Golem,
            Ravager,
            Deus,
            MoonLord,
            Dragonfolly,
            Providence,
            Polterghast,
            Yharon,
            NA
        }
        private static readonly string eater = "[c/745e61:Eater]";
        private static readonly string perforators = "[c/cc5151:Perforators]";
        public static readonly string[] CommunityBossProgression = new string[43]{
            "[c/5a9aff:King Slime]", //0
            "[c/835f39:Desert Scourge]", //1
            "[c/fd9999:Eye of Cthulhu]", // 2
            "[c/b3bd9b:Crabulon]", // 3
            "[c/c87578:Brain]"+"/"+eater, // 4
            "[c/42356d:Hive Mind]"+"/"+perforators, // 5
            "[c/baaa16:Queen Bee]", // 6
            "[c/cccc9f:Skeletron]", // 7 
            "[c/d3ccc4:Deerclops]", // 8
            "[c/e34f4f:Slime God]", // 9
            "[c/b84e71:Wall of Flesh]", // 10
            "[c/f776e3:Queen Slime]", // 11
            "[c/6c8ff3:Cryogen]", // 12
            "[c/a0a0a0:The Twins]", // 13
            "[c/49a677:Aquatic Scourge]", // 14
            "[c/a0a0a0:The Destroyer]", // 15
            "[c/8a2030:Brimstone Elemental]", // 16
            "[c/a0a0a0:Skeletron Prime]", // 17
            "[c/ad3446:Calamitas]", // 18
            "[c/cb5498:Plantera]", // 19
            "[c/33a68e:Leviathan and Anahita]", // 20
            "[c/80809e:Astrum Aureus]", // 21
            "[c/8d3800:Golem]", // 22
            "[c/33634b:Plaguebringer Goliath]", // 23
            "[c/fff93b:Empress of Light]", // 24
            "[c/1dcf85:Duke Fishron]", // 25
            "[c/a1756d:Ravager]", // 26
            "[c/3454be:Lunatic Cultist]", // 27
            "[c/7b6382:Astrum Deus]", // 28
            "[c/a28859:Moon Lord]", // 29
            "[c/ffbf49:Profaned Guardians]", // 30
            "[c/c29151:Dragonfolly]", // 31
            "[c/ffbf49:Providence, the Profaned Goddess]", // 32
            "[c/726b85:Ceaseless Void]", // 33
            "[c/ba94b4:Storm Weaver]", // 34
            "[c/964478:Signus]", // 35
            "[c/23c8fe:Polterghast]", // 36
            "[c/8d794d:Old Duke]", // 37
            "[c/ff00ff:The Devourer of Gods]", // 38
            "[c/ffa500:Jungle Dragon, Yharon]", // 39
            "[c/505967:Exo Mechs]", // 40
            "[c/bd0000:Supreme Calamitas]", // 41
            "[c/00FF00:Pre Boss]" // 42
        };

        public static void CheckProgressionBossStatus()
        {
            if (CalNohitQoL.Instance.AutomateProgressionUpgrades)
            {
                if (DownedBossSystem.downedYharon)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Yharon);
                else if (DownedBossSystem.downedPolterghast)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Polterghast);
                else if (DownedBossSystem.downedProvidence)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Providence);
                else if (DownedBossSystem.downedDragonfolly)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Dragonfolly);
                else if (NPC.downedMoonlord)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.MoonLord);
                else if (DownedBossSystem.downedAstrumDeus)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Deus);
                else if (DownedBossSystem.downedRavager)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Ravager);
                else if (NPC.downedGolemBoss)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Golem);
                else if (DownedBossSystem.downedAstrumAureus)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.AstrumAureus);
                else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.PostAllMechs);
                else if (Main.hardMode)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.WallOfFlesh);
                else if (DownedBossSystem.downedSlimeGod)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.SlimeGod);
                else if (DownedBossSystem.downedCrabulon)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Crabulon);
                else
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.NA);
            }
        }
        private static void HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder bossJustKilled)
        {
            switch (bossJustKilled)
            {
                case PUpgradeBossProgressionOrder.NA:
                    HandleMassUpgradeSetting(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
                case PUpgradeBossProgressionOrder.Crabulon:
                    HandleMassUpgradeSetting(false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
                    break;
                case PUpgradeBossProgressionOrder.SlimeGod:
                    HandleMassUpgradeSetting(false, false, false, false, false, false, false, true, false, false, true, false, false, false, false);
                    break;
                case PUpgradeBossProgressionOrder.WallOfFlesh:
                    HandleMassUpgradeSetting(false, false, false, false, true, false, false, true, false, false, true, false, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.PostAllMechs:
                    HandleMassUpgradeSetting(true, false, false, false, true, false, false, true, false, false, true, false, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.AstrumAureus:
                    HandleMassUpgradeSetting(true, false, false, false, true, false, false, true, false, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.Golem:
                    HandleMassUpgradeSetting(true, true, false, false, true, false, false, true, false, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.Ravager:
                    HandleMassUpgradeSetting(true, true, false, false, true, false, false, true, true, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.Deus:
                    HandleMassUpgradeSetting(true, true, false, false, true, true, false, true, true, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.MoonLord:
                    HandleMassUpgradeSetting(true, true, false, false, true, true, false, true, true, false, true, true, false, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Dragonfolly:
                    HandleMassUpgradeSetting(true, true, false, false, true, true, false, true, true, true, true, true, false, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Providence:
                    HandleMassUpgradeSetting(true, true, true, false, true, true, false, true, true, true, true, true, false, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Polterghast:
                    HandleMassUpgradeSetting(true, true, true, false, true, true, true, true, true, true, true, true, true, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Yharon:
                    HandleMassUpgradeSetting(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    break;
            }
        }

        // This makes my life a lot easier, this progression thing is annoyin lo
        private static void HandleMassUpgradeSetting(bool bOrange, bool mFruit, bool eBerry, bool dFruit, bool cShard, bool eCore, bool pHeart, bool rageOne, bool rageTwo, bool rageThree, bool adrenOne, bool adrenTwo, bool adrenThree, bool accOne, bool accTwo)
        {
            Main.LocalPlayer.Calamity().bOrange = bOrange;
            Main.LocalPlayer.Calamity().mFruit = mFruit;
            Main.LocalPlayer.Calamity().eBerry = eBerry;
            Main.LocalPlayer.Calamity().dFruit = dFruit;
            Main.LocalPlayer.Calamity().cShard = cShard;
            Main.LocalPlayer.Calamity().eCore = eCore;
            Main.LocalPlayer.Calamity().pHeart = pHeart;
            Main.LocalPlayer.Calamity().rageBoostOne = rageOne;
            Main.LocalPlayer.Calamity().rageBoostTwo = rageTwo;
            Main.LocalPlayer.Calamity().rageBoostThree = rageThree;
            Main.LocalPlayer.Calamity().adrenalineBoostOne = adrenOne;
            Main.LocalPlayer.Calamity().adrenalineBoostTwo = adrenTwo;
            Main.LocalPlayer.Calamity().adrenalineBoostThree = adrenThree;
            Main.LocalPlayer.extraAccessory = accOne;
            Main.LocalPlayer.Calamity().extraAccessoryML = accTwo;
        }
        public static string GetLatestBossKilled()
        {
            string bossToReturn;
            // the pain..
            if (DownedBossSystem.downedSCal)
                bossToReturn = CommunityBossProgression[41];

            else if (DownedBossSystem.downedExoMechs)
                bossToReturn = CommunityBossProgression[40];

            else if (DownedBossSystem.downedYharon)
                bossToReturn = CommunityBossProgression[39];

            else if (DownedBossSystem.downedDoG)
                bossToReturn = CommunityBossProgression[38];

            else if (DownedBossSystem.downedBoomerDuke)
                bossToReturn = CommunityBossProgression[37];

            else if (DownedBossSystem.downedPolterghast)
                bossToReturn = CommunityBossProgression[36];

            else if (DownedBossSystem.downedSignus)
                bossToReturn = CommunityBossProgression[35];

            else if (DownedBossSystem.downedStormWeaver)
                bossToReturn = CommunityBossProgression[34];

            else if (DownedBossSystem.downedCeaselessVoid)
                bossToReturn = CommunityBossProgression[33];

            else if (DownedBossSystem.downedProvidence)
                bossToReturn = CommunityBossProgression[32];

            else if (DownedBossSystem.downedDragonfolly)
                bossToReturn = CommunityBossProgression[31];

            else if (DownedBossSystem.downedGuardians)
                bossToReturn = CommunityBossProgression[30];

            else if (NPC.downedMoonlord)
                bossToReturn = CommunityBossProgression[29];

            else if (DownedBossSystem.downedAstrumDeus)
                bossToReturn = CommunityBossProgression[28];

            else if (NPC.downedAncientCultist)
                bossToReturn = CommunityBossProgression[27];

            else if (DownedBossSystem.downedRavager)
                bossToReturn = CommunityBossProgression[26];

            else if (NPC.downedFishron)
                bossToReturn = CommunityBossProgression[25];

            else if (NPC.downedEmpressOfLight)
                bossToReturn = CommunityBossProgression[24];

            else if (DownedBossSystem.downedPlaguebringer)
                bossToReturn = CommunityBossProgression[23];

            else if (NPC.downedGolemBoss)
                bossToReturn = CommunityBossProgression[22];

            else if (DownedBossSystem.downedAstrumAureus)
                bossToReturn = CommunityBossProgression[21];

            else if (DownedBossSystem.downedLeviathan)
                bossToReturn = CommunityBossProgression[20];

            else if (NPC.downedPlantBoss)
                bossToReturn = CommunityBossProgression[19];

            else if (DownedBossSystem.downedCalamitas)
                bossToReturn = CommunityBossProgression[18];

            else if (NPC.downedMechBoss3)
                bossToReturn = CommunityBossProgression[17];

            else if (DownedBossSystem.downedBrimstoneElemental)
                bossToReturn = CommunityBossProgression[16];

            else if (NPC.downedMechBoss1)
                bossToReturn = CommunityBossProgression[15];

            else if (DownedBossSystem.downedAquaticScourge)
                bossToReturn = CommunityBossProgression[14];

            else if (NPC.downedMechBoss2)
                bossToReturn = CommunityBossProgression[13];

            else if (DownedBossSystem.downedCryogen)
                bossToReturn = CommunityBossProgression[12];

            else if (NPC.downedQueenSlime)
                bossToReturn = CommunityBossProgression[11];

            else if (Main.hardMode)
                bossToReturn = CommunityBossProgression[10];

            else if (DownedBossSystem.downedSlimeGod)
                bossToReturn = CommunityBossProgression[9];

            else if (NPC.downedDeerclops)
                bossToReturn = CommunityBossProgression[8];

            else if (NPC.downedBoss3)
                bossToReturn = CommunityBossProgression[7];

            else if (NPC.downedQueenBee)
                bossToReturn = CommunityBossProgression[6];

            else if (DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator)
                bossToReturn = CommunityBossProgression[5];

            else if (NPC.downedBoss2)
                bossToReturn = CommunityBossProgression[4];

            else if (DownedBossSystem.downedCrabulon)
                bossToReturn = CommunityBossProgression[3];

            else if (NPC.downedBoss1)
                bossToReturn = CommunityBossProgression[2];

            else if (DownedBossSystem.downedDesertScourge)
                bossToReturn = CommunityBossProgression[1];

            else if (NPC.downedSlimeKing)
                bossToReturn = CommunityBossProgression[0];
            else
                bossToReturn = CommunityBossProgression[42];
            return bossToReturn;
        }
        #endregion
        #region MNL SYSTEM
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

        public static void DisplayMNLMessage(ref float BossAliveFrames, ref NPC Boss, bool bossDied, PlayerDeathReason deathReason)
        {
            if (BossAliveFrames >= 3 && Main.netMode != NetmodeID.Server)
            {
                if (!BossMNLS.TryGetValue(Boss.type, out float MNL))
                    return;
                // Under MNL Message
                if (BossAliveFrames < MNL)
                {
                    float secondsMNL = MNL / 60;
                    float secondsTimer = BossAliveFrames / 60;
                    float timerUnder = secondsMNL - secondsTimer;
                    timerUnder = (float)Math.Truncate((double)timerUnder * 100f) / 100f;
                    //if (Boss.type == ModContent.NPCType<SupremeCalamitas>() && !DownedBossSystem.downedSCal)
                    //{
                    //    timerUnder += 12f;
                    //}
                    CalNohitQoLPlayer.startTextDelay = true;
                    CalNohitQoLPlayer.timeUnderOrOverMNL = timerUnder;
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
                    if (CalNohitQoLPlayer.startTextDelay)
                    {
                        CalNohitQoLPlayer.startTextDelay = false;
                        CalNohitQoLPlayer.timeUnderOrOverMNL = 0;
                    }
                    CalNohitQoLPlayer.startOnTextDelay = true;
                }
            }
            if (CalNohitQoL.Instance.SassMode && bossDied)
            {
                CalNohitQoLPlayer.bossIsDead = 2;
            }
            else if (CalNohitQoL.Instance.SassMode && !bossDied)
            {
                CalNohitQoLPlayer.bossIsDead = 1;
            }
            else
                CalNohitQoLPlayer.bossIsDead = 0;
        }
        #endregion
        #region Sass Mode
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
        public static void SassModeHandler(NPC boss, bool bossDead, bool underMNL, float bossLifeRatio,float timerUnderMNL = 0, PlayerDeathReason damageSource = null)
        {
            if (bossDead)
                SassToSay = SassMode_BossDead(boss.type, bossLifeRatio, timerUnderMNL);
            else if (!bossDead && timerUnderMNL > 0)
                SassToSay = SassMode_BossAliveAndUnder(boss.type, bossLifeRatio,timerUnderMNL);

            if(SassToSay != null)
                Main.NewText(SassToSay, Color.Orange);
            CalNohitQoLPlayer.currentBoss = null;
        }
        private static string SassMode_BossDead(int bossType, float bossLifeRatio, float timeUnderMNL)
        {
            string textToReturn = null;
            if(timeUnderMNL>0)
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
                    float yharonHPPErcent = 0;
                    if (CalamityWorld.death)
                        yharonHPPErcent = 0.16f;
                    else if (CalamityWorld.revenge)
                        yharonHPPErcent = 0.11f;
                    if (yharonHPPErcent != 0)
                    {
                        if (bossLifeRatio <= yharonHPPErcent)
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
                if(bossType == ModContent.NPCType<Leviathan>() || bossType == ModContent.NPCType<Anahita>() || bossType == ModContent.NPCType<AquaticScourgeHead>())
                    textToReturn = "Excessive Salt can lead to high blood pressure.";
            }
            #endregion
            else
            {
                textToReturn = Main.rand.NextFromList(GenericSassQuotesUnderLose);
            }
            if (textToReturn == null)
            {
                textToReturn = Main.rand.NextFromList(GenericSassQuotesUnderLose);
            }
            return textToReturn;
        }
        #endregion
        public static float Clamp(this float value, float min, float max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static int Clamp(this int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }
        /// <summary>
        ///  Returns true if bigger/equal to 0, false if less than.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool IsPositive(int value)
        {
            if (value >= 0)
                return false;
            else
                return true;
        }

        public static Vector2 CalculatePredictiveAimToTarget(Vector2 startingPosition, Vector2 targetPosition, Vector2 targetVelocity, float shootSpeed, int iterations = 4)
        {
            float accuracyFloat = 0f;
            Vector2 finalPosition = targetPosition;
            for (int i = 0; i < iterations; i++)
            {
                float velocityToFinalPosition = Vector2.Distance(startingPosition, finalPosition) / shootSpeed;
                finalPosition += targetVelocity * (velocityToFinalPosition - accuracyFloat);
                accuracyFloat = velocityToFinalPosition;
            }

            return (finalPosition - startingPosition).SafeNormalize(Vector2.UnitY) * shootSpeed;
        }

    }
}