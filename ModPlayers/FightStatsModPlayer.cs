using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Events;
using CalNohitQoL.Globals;
using CalNohitQoL.Systems;
using CalNohitQoL.UI.QoLUI;
using CalNohitQoL.UI.QoLUI.PotionUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalNohitQoL.ModPlayers
{
    public class FightStatsModPlayer : ModPlayer
    {
        // Boss Rush Stats
        internal static bool IsBossRushActive = false;
        internal static bool WasBossRushJustDisabled = false;
        internal static int BossRushActiveFrames = 0;
        public const int BossRushMNL = 34200;
        internal static int BRDelayTimer = 0;
        internal static MNLStats FightStats = new();
        public Dictionary<string, int> BRAttempts = new()
        {
            ["Boss Rush"] = 0
        };
        // DPS
        private static readonly List<int> BossDPS = new();

        internal static int bossIsDead = 0;
        private static float currentBossLifeRatio;
        internal static NPC currentBoss = null;
        public bool bossDead;
        internal static bool startTextDelay;
        internal static bool startOnTextDelay;
        internal static float timeUnderOrOverMNL;

        public override void PreUpdate()
        {
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
                    TimeSpan mnlTime = TimeSpan.FromSeconds(amountUnder / 60);

                    if (mnlTime.Hours < 1 && mnlTime.Days < 1)
                        hours = "";
                    else
                        hours = (mnlTime.Days * 24 + mnlTime.Hours).ToString() + ":";

                    if (hours != "" || mnlTime.Minutes >= 10)
                        minutes = mnlTime.Minutes.ToString() + ":";
                    else
                        minutes = "0" + mnlTime.Minutes.ToString() + ":";

                    if (mnlTime.Seconds >= 10)
                        seconds = mnlTime.Seconds.ToString();
                    else
                        seconds = "0" + mnlTime.Seconds.ToString();
                    string line2 = hours + minutes + seconds;

                    string underOrOverString = overMNL ? "over" : "under";
                    CalNohitQoLUtils.DisplayText($"[c/e9341f:Boss Rush Attempt] {BRAttempts["Boss Rush"]} [c/e9341f:Stats:]");
                    CalNohitQoLUtils.DisplayText($"[c/e7684b:Total Length:] [c/fccccf:{line}]");
                    CalNohitQoLUtils.DisplayText($"[c/e7684b:Amount {underOrOverString} MNL:] [c/fccccf:{line2}]");
                }
                else
                    BRDelayTimer--;
            }

            if (BossRushEvent.BossRushActive && !IsBossRushActive && Toggles.MNLIndicator)
                IsBossRushActive = true;

            if (!BossRushEvent.BossRushActive && IsBossRushActive && !WasBossRushJustDisabled)
            {
                BRAttempts["Boss Rush"]++;
                WasBossRushJustDisabled = true;
                BRDelayTimer = Main.LocalPlayer.respawnTimer + 7;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (IsBossRushActive && !WasBossRushJustDisabled)
            {
                BossRushActiveFrames++;
            }
            if (Main.netMode == NetmodeID.Server || Player.whoAmI != Main.myPlayer)
            {
                return;
            }
            if (Main.npc.Any((n) => (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail) && n.active) && (BossRushActiveFrames! > 0 || !IsBossRushActive || BRDelayTimer! > 0 || !WasBossRushJustDisabled))
            {
                FightStats.BossDied = false;
                if (FightStats.BossAliveFrames == 0)
                {
                    FightStats.Start = DateTime.Now;
                }
                FightStats.End = DateTime.Now;
                FightStats.Boss = Main.npc.First((n) => (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail) && n.active);
                currentBoss = FightStats.Boss;
                FightStats.BossLife = FightStats.Boss.life / (float)FightStats.Boss.lifeMax;
                currentBossLifeRatio = FightStats.BossLife;
                if (MNLSystem.ActiveFightLength.TryGetValue(FightStats.Boss.type, out float mnl))
                {
                    FightStats.BossAliveFrames++;
                }
                if (Main.LocalPlayer.dpsDamage != BossDPS.LastOrDefault())
                {
                    BossDPS.Add(Main.LocalPlayer.dpsDamage);
                }
            }
            if (!Main.npc.Any((n) => (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail) && n.active))
            {
                string lengthType;
                if (Toggles.TesterTimes)
                    lengthType = "the average kill time";
                else
                    lengthType = "the minimum nohit length";
                if (startOnTextDelay)
                {
                    FightStats.BossAliveFrames = 0;
                    FightStats.Boss = null;
                    startOnTextDelay = false;
                    CalNohitQoLUtils.DisplayText($"[c/2fff2f:You were above {lengthType}!]");
                    if (Toggles.BossDPS && BossDPS.Count > 0)
                    {
                        CalNohitQoLUtils.DisplayText($"[c/e7684b:Average DPS:] [c/fccccf:{(int)BossDPS.Average()}]");
                    }
                    CalNohitQoLGlobalNPC.bossActive = false;
                    CalNohitQoLGlobalNPC.currentTimer = 0;
                    bossIsDead = 0;
                    BossDPS.Clear();
                    if (Toggles.SassMode)
                    {
                        SassModeSystem.SassModeHandler(currentBoss, bossIsDead == 2 ? true : false, false, currentBossLifeRatio, timeUnderOrOverMNL);
                        bossIsDead = 0;

                    }
                }
                else if (startTextDelay)
                {
                    FightStats.BossAliveFrames = 0;
                    FightStats.Boss = null;
                    startTextDelay = false;
                    CalNohitQoLUtils.DisplayText($"[c/ff2f2f:You were under {lengthType} by ]" + timeUnderOrOverMNL + "[c/ff2f2f: seconds!]");
                    if (Toggles.BossDPS && BossDPS.Count > 0)
                    {
                        CalNohitQoLUtils.DisplayText($"[c/e7684b:Average DPS:] [c/fccccf:{(int)BossDPS.Average()}]");
                    }
                    CalNohitQoLGlobalNPC.bossActive = false;
                    CalNohitQoLGlobalNPC.currentTimer = 0;
                    bossIsDead = 0;
                    BossDPS.Clear();
                    if (Toggles.SassMode)
                    {
                        SassModeSystem.SassModeHandler(currentBoss, bossIsDead == 2 ? true : false, false, currentBossLifeRatio, timeUnderOrOverMNL);
                        bossIsDead = 0;

                    }
                }
            }
        }

        public override void OnRespawn(Player player)
        {
            if (FightStats.BossAliveFrames > 0 && Toggles.MNLIndicator && !(BossRushActiveFrames > 0))
            {
                bossIsDead = 1; // Boss is not dead.
                MNLSystem.DisplayMNLMessage(ref FightStats.BossAliveFrames, ref FightStats.Boss, false);
            }
        }

        public override void LoadData(TagCompound tag)
        {
            if (Main.netMode != NetmodeID.Server && Player.whoAmI == Main.myPlayer)
                BRAttempts["Boss Rush"] = tag.GetInt("Attempts");
        }
        public override void SaveData(TagCompound tag)
        {
            if (Main.netMode != NetmodeID.Server && Player.whoAmI == Main.myPlayer)
                tag["Attempts"] = BRAttempts.GetValueOrDefault("Boss Rush");
        }
    }
}
