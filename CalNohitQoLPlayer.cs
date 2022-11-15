using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Events;
using CalamityMod.Items;
using CalNohitQoL.NPCs;
using CalNohitQoL.UI.QoLUI;
using CalNohitQoL.UI.QoLUI.PotionUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalNohitQoL
{
    public class CalNohitQoLPlayer : ModPlayer
    {

        public bool Trippy = false;
        public bool DoubleTrippy = false;
        public bool bossDead;
        public float luckPotionBoost;
        internal static bool startTextDelay;
        internal static bool startOnTextDelay;
        internal static float timeUnderOrOverMNL;
        // Timers
        internal static int delayTimer = 0;
        public static int UIUpdateTextTimer { get; internal set; } = 0;
        public static int ToggleUICooldownTimer { get; internal set; }
        public static int PotionUICooldownTimer { get; internal set; }
        public const int UICooldownTimerLength = 15;
        // Other stuff
        internal static int bossIsDead = 0;
        private static float currentBossLifeRatio;
        internal static NPC currentBoss = null;
        // Boss Rush Stats
        internal static bool IsBossRushActive = false;
        internal static bool WasBossRushJustDisabled = false;
        internal static int BossRushActiveFrames = 0;
        public const int BossRushMNL = 34200;
        internal static int BRDelayTimer = 0;
        internal static MNLStats FightStats = new();
        private static int LocalTimer = 0;
        public Dictionary<string, int> BRAttempts = new()
        {
            ["Boss Rush"] = 0
        };
        // DPS
        private static readonly List<int> BossDPS = new List<int>();  
        // Godmode Hit thing
        public const int HitCooldownFrames = 40;
        public static int GMHitCooldownTimer = 0;

        public override void ResetEffects()
        {
            Trippy = false;
            DoubleTrippy = false;

        }
        public override void UpdateDead()
        {
            Trippy = false;
            DoubleTrippy = false;
            if (CalNohitQoL.Instance.InstantDeath)
            {
                Despawn();
            }
            //if (FightStats.BossAliveFrames > 0)
            //{
            //    bossIsDead = 1; // Boss is not dead.
            //    CalNohitQoLUtils.DisplayMNLMessage(ref FightStats.BossAliveFrames, ref FightStats.Boss, false, null);
            //}
            CalNohitQoLNPC.currentTimer = 0;
        }

        public override void Initialize()
        {
        }

        public override void LoadData(TagCompound tag)
        {
            if (Main.netMode != NetmodeID.Server && Player.whoAmI == Main.myPlayer)
            {
                BRAttempts["Boss Rush"] = tag.GetInt("Attempts");
            }
        }
        public override void SaveData(TagCompound tag)
        {
            if (Main.netMode != NetmodeID.Server && Player.whoAmI == Main.myPlayer)
            {
                tag["Attempts"] = BRAttempts.GetValueOrDefault("Boss Rush");
            }
        }

        public override void PreUpdate()
        {
            LocalTimer++;
            if (LocalTimer > 60)
                LocalTimer = 0;
            if (GMHitCooldownTimer > 0)
                GMHitCooldownTimer--;
            if (PotionUICooldownTimer > 0)
                PotionUICooldownTimer--;
            if (ToggleUICooldownTimer > 0)
                ToggleUICooldownTimer--;
            UpgradesUIManager.SortOutTextures();
            CalNohitQoLUtils.CheckProgressionBossStatus();
            CalNohitQoL.potionUIManager.GiveBuffs();
            if (CalNohitQoL.Instance.GodmodeEnabled)
                Player.statLife = Player.statLifeMax2;
            if (CalNohitQoL.Instance.InfiniteFlightTime)
                Player.wingTime = Player.wingTimeMax;
            if (CalNohitQoL.Instance.InfiniteMana)
                Player.statMana = Player.statManaMax2;
            
            if (CalNohitQoL.InfernumMod != null)
            {
                CalNohitQoL.InfernumModeEnabled = (bool)CalNohitQoL.InfernumMod.Call("GetInfernumActive");
            }
           
            if (!CalNohitQoL.Instance.MNLIndicator && CalNohitQoL.Instance.SassMode)
            {
                CalNohitQoL.Instance.SassMode = false;
            }
            if (TogglesUIManager.clickCooldownTimer > 0)
                TogglesUIManager.clickCooldownTimer--;
            if (UIUpdateTextTimer > 0)
                UIUpdateTextTimer--;

            if (WasBossRushJustDisabled)
            {
                if (BRDelayTimer == 0)
                {
                    WasBossRushJustDisabled = false;
                    IsBossRushActive = false;
                    int finalBRTimeFrames = BossRushActiveFrames;            
                    int amountUnder = BossRushMNL - BossRushActiveFrames;
                    BossRushActiveFrames = 0;
                    bool overMNL = CalNohitQoLUtils.IsPositive(amountUnder);
                    TimeSpan time = TimeSpan.FromSeconds(finalBRTimeFrames / 60);
                    string hours = "";
                    if (time.Hours < 1 && time.Days < 1)
                    {
                        hours = "";
                    }
                    else
                    {
                        hours = (time.Days * 24 + time.Hours).ToString() + ":";
                    }
                    string minutes = "";
                    if (hours != "" || time.Minutes >= 10)
                    {
                        minutes = time.Minutes.ToString() + ":";
                    }
                    else
                    {
                        minutes = "0" + time.Minutes.ToString() + ":";
                    }
                    string seconds = "";
                    if (time.Seconds >= 10)
                    {
                        seconds = time.Seconds.ToString();
                    }
                    else
                    {
                        seconds = "0" + time.Seconds.ToString();
                    }
                    string line = hours + minutes + seconds;

                    TimeSpan mnlTime = TimeSpan.FromSeconds(amountUnder / 60);
                    hours = "";
                    if(mnlTime.Hours < 1 && mnlTime.Days < 1)
                    {
                        hours = "";
                    }
                    else
                    {
                        hours = (mnlTime.Days * 24 + mnlTime.Hours).ToString() + ":";
                    }
                    minutes = "";
                    if (hours != "" || mnlTime.Minutes >= 10)
                    {
                        minutes = mnlTime.Minutes.ToString() + ":";
                    }
                    else
                    {
                        minutes = "0" + mnlTime.Minutes.ToString() + ":";
                    }
                    seconds = "";
                    if (mnlTime.Seconds >= 10)
                    {
                        seconds = mnlTime.Seconds.ToString();
                    }
                    else
                    {
                        seconds = "0" + mnlTime.Seconds.ToString();
                    }
                    string line2 = hours + minutes + seconds;

                    string underOrOverString = overMNL ? "over" : "under";
                    Main.NewText($"[c/e9341f:Boss Rush Attempt] {BRAttempts["Boss Rush"]} [c/e9341f:Stats:]");
                    Main.NewText($"[c/e7684b:Total Length:] [c/fccccf:{line}]");
                    Main.NewText($"[c/e7684b:Amount {underOrOverString} MNL:] [c/fccccf:{line2}]");
                }
                else
                { 
                    BRDelayTimer--;
                }    
            }
            if (BossRushEvent.BossRushActive && !IsBossRushActive&&CalNohitQoL.Instance.MNLIndicator)
            {
                IsBossRushActive = true;
            }
            if (!BossRushEvent.BossRushActive && IsBossRushActive&&!WasBossRushJustDisabled)
            {
                BRAttempts["Boss Rush"]++;
                WasBossRushJustDisabled = true;
                BRDelayTimer = Main.LocalPlayer.respawnTimer + 7 ;
            }
            if (CalNohitQoL.Instance.InstantDeath)
            {
                if (Player.FindBuffIndex(ModContent.BuffType<HolyInferno>()) > -1)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name +" was burnt by the Holy Inferno"), 1000.0, 0, false);
                }
                if(Player.HasBuff(BuffID.BrainOfConfusionBuff))
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s brain is not as big as they think"), 1000.0, 0, false);
                Player.buffImmune[59] = true;
                Player.onHitDodge = false;
                Player.buffImmune[ModContent.BuffType<TarragonImmunity>()] = true;
                Player.Calamity().tarragonImmunity = false;
            }
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (Player.whoAmI != Main.myPlayer)
                return false;

            else if (CalNohitQoL.Instance.GodmodeEnabled)
            {
                if (GMHitCooldownTimer == 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("CalNohitQoL/Sounds/Custom/godmodeHitSFX"), Main.player[Main.myPlayer].Center);
                    GMHitCooldownTimer = HitCooldownFrames;
                }
                return false;
            }
            else if (CalNohitQoL.Instance.InstantDeath)
            {
                if (Main.netMode == NetmodeID.Server || Player.whoAmI != Main.myPlayer)
                {
                    return PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
                }
                Player.mount.Dismount(Player);
                Player.dead = true;
                Player.respawnTimer = 120;
                Player.immuneAlpha = 0;
                Player.palladiumRegen = false;
                Player.iceBarrier = false;
                Player.crystalLeaf = false;
                NetworkText deathText = damageSource.GetDeathText(Player.name);
                if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(deathText, new Color(225, 25, 25), -1);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(deathText.ToString(), (Color?)new Color(225, 25, 25));
                }
                if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
                {
                    NetMessage.SendPlayerDeath(Player.whoAmI, damageSource, damage, hitDirection, pvp, -1, -1);
                }

            }          
            return true;
        }

       
        private void ForceBiomes()
        {
            if (CalNohitQoL.Instance.BiomeFountainsForceBiome)
            {
                if (CalNohitQoLGlobalNPC.SpecificBossIsAlive(ref CalNohitQoLGlobalNPC.plantBoss, NPCID.Plantera) && Player.Distance(Main.npc[CalNohitQoLGlobalNPC.plantBoss].Center) < 3000)
                {
                    Player.ZoneJungle = true;
                }
                if (NPC.downedQueenBee)//if skeletron is dead
                {
                    if (CalNohitQoLGlobalNPC.SpecificBossIsAlive(ref CalNohitQoLGlobalNPC.eaterBoss, NPCID.EaterofWorldsHead)
                    && Player.Distance(Main.npc[CalNohitQoLGlobalNPC.eaterBoss].Center) < 3000)
                    {
                        Player.ZoneCorrupt = true;
                    }

                    if (CalNohitQoLGlobalNPC.SpecificBossIsAlive(ref CalNohitQoLGlobalNPC.brainBoss, NPCID.BrainofCthulhu) && Player.Distance(Main.npc[CalNohitQoLGlobalNPC.brainBoss].Center) < 3000)
                    {
                        Player.ZoneCrimson = true;
                    }

                    switch (Main.SceneMetrics.ActiveFountainColor)
                    {
                        case -1: //no fountain active
                            goto default;
                        case 0: //pure water, ocean
                            Player.ZoneBeach = true;
                            break;
                        case 2: //corrupt
                            Player.ZoneCorrupt = true;
                            break;
                        case 3: //jungle
                            Player.ZoneJungle = true;
                            break;
                        case 4: //hallow
                            if (Main.hardMode)
                                Player.ZoneHallow = true;
                            break;
                        case 5: //ice
                            Player.ZoneSnow = true;
                            break;
                        case 6: //oasis
                            goto case 12;
                        case 8: //cavern
                            goto default;
                        case 9: //blood fountain
                            goto default;
                        case 10: //crimson
                            Player.ZoneCrimson = true;
                            break;
                        case 12: //desert fountain
                            Player.ZoneDesert = true;
                            if (Player.Center.Y > 3200f)
                                Player.ZoneUndergroundDesert = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public override void PostUpdateEquips()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                if (CalNohitQoL.Instance.InstantDeath)
                {
                    Player.shadowDodgeTimer = 2;
                    Player.blackBelt = false;
                    
                }
            }
        }
        public override void OnEnterWorld(Player player)
        {
            CalNohitQoLModSystem.CalamityCallQueued = false;
        }
        public override void PostUpdateMiscEffects()
        {
            ForceBiomes();
            Player.unlockedBiomeTorches = true;

            if (IsBossRushActive && !WasBossRushJustDisabled)
            {
                BossRushActiveFrames++;
            }
            if (CalNohitQoL.Instance.ShroomsExtraDamage&&DoubleTrippy)
            {
                Player.GetDamage<GenericDamageClass>() += 0.5f;
            }
            else if(CalNohitQoL.Instance.ShroomsExtraDamage&&Trippy)
            {
                Player.GetDamage<GenericDamageClass>() += 0.5f;
            }
            else if(Player.Calamity().trippy && CalNohitQoL.Instance.ShroomsExtraDamage&&Main.hardMode)
                Player.GetDamage<GenericDamageClass>() += 0.5f;
            if(Player.Calamity().trippy && !CalNohitQoL.Instance.ShroomsExtraDamage)
            {
                Player.GetDamage<GenericDamageClass>() -= 0.5f;
            }
            if (Main.netMode == NetmodeID.Server || Player.whoAmI != Main.myPlayer)
            {
                return;
            }
            if (Main.npc.Any((NPC n) => (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail) && n.active) && (BossRushActiveFrames!>0||!IsBossRushActive||BRDelayTimer!>0||!WasBossRushJustDisabled))
            {
                FightStats.BossDied = false;
                if (FightStats.BossAliveFrames == 0)
                {
                    FightStats.Start = DateTime.Now;
                }
                FightStats.End = DateTime.Now;
                FightStats.Boss = Main.npc.First((NPC n) => (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail) && n.active);
                currentBoss = FightStats.Boss;
                FightStats.BossLife = (float)FightStats.Boss.life/(float)FightStats.Boss.lifeMax;
                currentBossLifeRatio = FightStats.BossLife;
                if (CalNohitQoLUtils.BossMNLS.TryGetValue(FightStats.Boss.type, out float mnl))
                {
                    FightStats.BossAliveFrames++;
                }
                if(Main.LocalPlayer.dpsDamage != BossDPS.LastOrDefault())
                {
                    BossDPS.Add(Main.LocalPlayer.dpsDamage);
                }
            }
            if (!Main.npc.Any((NPC n) => (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail) && n.active))
            {
                if (startOnTextDelay)
                {
                    FightStats.BossAliveFrames = 0;
                    FightStats.Boss = null;
                    startOnTextDelay = false;
                    Main.NewText("[c/2fff2f:You were above MNL!]");
                    if (CalNohitQoL.Instance.BossDPS && BossDPS.Count > 0)
                    {
                        Main.NewText($"[c/e7684b:Average DPS:] [c/fccccf:{(int)BossDPS.Average()}]");
                    }
                    CalNohitQoLNPC.bossActive = false;
                    CalNohitQoLNPC.currentTimer = 0;
                    bossIsDead = 0;
                    BossDPS.Clear();
                    if (CalNohitQoL.Instance.SassMode)
                    {
                        CalNohitQoLUtils.SassModeHandler(currentBoss, bossIsDead == 2? true: false, false, currentBossLifeRatio, timeUnderOrOverMNL);
                        bossIsDead = 0;

                    }
                }
                else if (startTextDelay)
                {
                    FightStats.BossAliveFrames = 0;
                    FightStats.Boss = null;
                    startTextDelay = false;
                    Main.NewText("[c/ff2f2f:You were under MNL by ]" + timeUnderOrOverMNL + "[c/ff2f2f: seconds!]");
                    if (CalNohitQoL.Instance.BossDPS && BossDPS.Count > 0)
                    {
                        Main.NewText($"[c/e7684b:Average DPS:] [c/fccccf:{(int)BossDPS.Average()}]");
                    }
                    CalNohitQoLNPC.bossActive = false;
                    CalNohitQoLNPC.currentTimer = 0;
                    bossIsDead = 0;
                    BossDPS.Clear();
                    if (CalNohitQoL.Instance.SassMode)
                    {
                        CalNohitQoLUtils.SassModeHandler(currentBoss, bossIsDead == 2 ? true : false, false, currentBossLifeRatio, timeUnderOrOverMNL);
                        bossIsDead = 0;

                    }
                }
            }
        }
        public static void Despawn()
        {
            for (int i2 = 0; i2 < 1000; i2++)
            {
                if (Main.projectile[i2].hostile && ((Entity)Main.projectile[i2]).active)
                {
                    ((Entity)Main.projectile[i2]).active = false;
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, (NetworkText)null, i2, 0f, 0f, 0f, 0, 0, 0);
                }
            }

            for (int l = 0; l < 200; l++)
            {
                if (!Main.npc[l].friendly && !Main.npc[l].boss && ((Entity)Main.npc[l]).active)
                {
                    Main.npc[l].life = 0;
                    ((Entity)Main.npc[l]).active = false;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, (NetworkText)null, l, 0f, 0f, 0f, 0, 0, 0);

                }
            }
            for (int m = 0; m < 200; m++)
            {
                if (Main.npc[m].boss && ((Entity)Main.npc[m]).active)
                {
                    Main.npc[m].life = 0;
                    ((Entity)Main.npc[m]).active = false;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, (NetworkText)null, m, 0f, 0f, 0f, 0, 0, 0);
                }
            }

        }
        public override void OnRespawn(Player player)
        {
            Player.immuneTime = 60;
            if (CalNohitQoL.Instance.AutoChargeDraedonWeapons)
            {
                for (int i = 0; i < player.inventory.Length; i++)
                {
                    Item item = player.inventory[i];
                    if (item.type >= 5125)
                    {
                        CalamityGlobalItem modItem = item.Calamity();
                        if (modItem != null && modItem.UsesCharge)
                        {
                            modItem.Charge = modItem.MaxCharge;
                        }
                    }
                }
            }
            if (FightStats.BossAliveFrames > 0 && CalNohitQoL.Instance.MNLIndicator && (!(CalNohitQoLPlayer.BossRushActiveFrames > 0)))
            {
                bossIsDead = 1; // Boss is not dead.
                CalNohitQoLUtils.DisplayMNLMessage(ref FightStats.BossAliveFrames, ref FightStats.Boss, false, null);
            }
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (CalNohitQoLWorld.OpenTogglesUI.JustPressed)
            {
                if(PotionUIManager.IsDrawing == true)
                {
                    TogglesUIManager.CloseAllUI(true);
                    ToggleUICooldownTimer = UICooldownTimerLength;
                    PotionUICooldownTimer = UICooldownTimerLength;
                }
                else if (ToggleUICooldownTimer == 0)
                    TogglesUIManager.UIOpen = !TogglesUIManager.UIOpen;
                if (TogglesUIManager.UIOpen && ToggleUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.Center);
                    TogglesUIManager.CloseAllUI(false);
                    ToggleUICooldownTimer = UICooldownTimerLength;
                }
                else if (ToggleUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    TogglesUIManager.CloseAllUI(true);
                    ToggleUICooldownTimer = UICooldownTimerLength;
                }
            }
            if (CalNohitQoLWorld.OpenPotionsUI.JustPressed)
            {
                if (PotionUIManager.IsDrawing && PotionUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    PotionUIManager.IsDrawing = false;
                    PotionUICooldownTimer = UICooldownTimerLength;
                    PotionUIManager.Timer = 0;
                }
                else if(PotionUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.Center);
                    PotionUIManager.IsDrawing = true;
                    PotionUICooldownTimer = UICooldownTimerLength;
                    PotionUIManager.Timer = 0;
                }
                
            }
            /*if (CalNohitQoLWorld.OpenTipsUI.JustPressed)
            {
                TipsUIManager.IsDrawing = !TipsUIManager.IsDrawing;
            }*/
        }

    }
}