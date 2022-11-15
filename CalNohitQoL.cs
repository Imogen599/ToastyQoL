using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalNohitQoL.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI.Chat;
using Terraria.GameContent.Events;
using CalNohitQoL.NPCs;
using CalNohitQoL.UI.QoLUI;
using CalNohitQoL.UI.QoLUI.PotionUI;
using CalNohitQoL.Items.ReforgeToaster;
using CalNohitQoL.TipSystem;
using ReLogic.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace CalNohitQoL
{
    public class CalNohitQoL : Mod
	{
        private readonly string message = "This isnt really very good but oh well. This mod has been in development since i properly started coding, and theres a lot of early done things i would change now, but there isnt much point at this point.";

        // UI TOGGLES
        internal static CalNohitQoL Instance;
        internal bool PotionTooltips = true;
        internal bool ItemTooltips = true;
        internal bool PotionLock = false;
        internal bool ItemLock = false;
        internal bool AccLock = false;
        internal bool InstantDeath = false;
        internal bool GodmodeEnabled = false;
        internal bool GravestonesEnabled = true;
        internal bool InfiniteFlightTime = false;
        internal bool DisableRain = false;
        internal bool EnableRain = false;
        internal bool InfiniteAmmo = true;
        internal bool InfiniteConsumables = true;
        internal bool InfinitePotions = true;
        internal bool BiomeFountainsForceBiome = true;
        internal bool DisableEvents = false;
        internal float LightHack = 0;
        internal bool playerShouldBeJourney = false;
        internal bool InfiniteMana = false;
        internal bool AutoChargeDraedonWeapons = true;
        internal bool AutomateProgressionUpgrades = true;
        internal bool ShroomsInvisProjectilesVisible = true;
        internal bool MNLIndicator = true;
        internal bool SassMode = false;
        internal bool ShroomsExtraDamage = false;
        internal bool ToggleRain = false;
        internal bool BossDPS = true;

        // OTHER UI VARIABLES
        internal static List<TogglesUIElement> OtherUIElements = new();
        internal static CheatIndicatorUIRenderer UltimateUI2 = new();
        internal static SummonSlotUIIcon SummonSlotUIIcon2 = new();
        internal static TogglesUIManager TogglesUI = new();

        internal static UpgradesUIManager upgradesUIManager = new();
        internal static LocksUIManager locksUIManager = new();
        internal static PowersUIManager powersUIManager = new();
        internal static WorldUIManager worldUIManager = new();
        internal static MiscUIManager miscUIManager = new();
        internal static BossTogglesUIManager bossTogglesUIManager = new();

        internal static PotionUIManager potionUIManager = new();
        internal static TipsUIManager tipsUIManager = new();
        internal static TipMainButtonUIElement tipMainButtonUIElement = new();
        // OTHER VARIABLES
        public static int BHTYPE { get; internal set; } = 1;
        public static int CLONEBHTYPE { get; internal set; } = 1;
        public static bool DownedBrain { get; internal set; } = false;
        public static bool DownedEater { get; internal set; } = false;

        public static Mod InfernumMod { get; private set; }
        public static bool InfernumModeEnabled { get; internal set; } = false;

        public static DynamicSpriteFont DraedonFont { get; private set; }
		public override void Load()
		{
			Instance = this;
            CalNohitQoLLists.LoadLists();
            TipsList.LoadLists();
            PotionUIManager.Load();

            if (ModLoader.TryGetMod("InfernumMode", out Mod infernumMod))
            {
                InfernumMod = infernumMod;
                InfernumModeEnabled = true;
            }

            LoadFonts();
        }

        public override void Unload()
		{
			Instance = null;
            CalNohitQoLLists.UnloadLists();
            TipsList.UnloadLists();
            PotionUIManager.Unload();
            InfernumMod = null;
            InfernumModeEnabled = false;
            UnloadFonts();

        }
        private static void LoadFonts()
        {
            if (!Main.dedServ)
            {
                // This was crashing on Linux and such in Calamity, i am unable to check if it will here so I am playing it safe and only allowing custom fonts to work on Windows.
                if ((int)Environment.OSVersion.Platform == 2)
                    DraedonFont = ModContent.Request<DynamicSpriteFont>("CalNohitQoL/Fonts/DraedonFont", (AssetRequestMode)1).Value;
                else
                    // If not the correct OS, we need to make it the default Terraria Font, Andy.
                    DraedonFont = FontAssets.MouseText.Value;
            }
        }
        private static void UnloadFonts()
        {
            DraedonFont = null;
        }
        internal static bool TryClearEvents()
        {

           
                if (Main.invasionType != 0)
                {
                    Main.invasionType = 0;
                }

                if (Main.pumpkinMoon)
                {
                    Main.pumpkinMoon = false;
                }

                if (Main.snowMoon)
                {
                    Main.snowMoon = false;
                }

                if (Main.eclipse)
                {
                    Main.eclipse = false;
                }

                if (Main.bloodMoon)
                {
                    Main.bloodMoon = false;
                }

                if (Main.WindyEnoughForKiteDrops)
                {
                    Main.windSpeedTarget = 0;
                    Main.windSpeedCurrent = 0;
                }

                if (Main.slimeRain)
                {
                    Main.StopSlimeRain();
                    Main.slimeWarningDelay = 1;
                    Main.slimeWarningTime = 1;
                }

                if (BirthdayParty.PartyIsUp)
                    BirthdayParty.CheckNight();

                if (DD2Event.Ongoing && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    DD2Event.StopInvasion();
                }

                if (Sandstorm.Happening)
                {
                    Sandstorm.Happening = false;
                    Sandstorm.TimeLeft = 0;
                    Sandstorm.IntendedSeverity = 0;
                }

                if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
                {
                    NPC.LunarApocalypseIsUp = false;
                    NPC.ShieldStrengthTowerNebula = 0;
                    NPC.ShieldStrengthTowerSolar = 0;
                    NPC.ShieldStrengthTowerStardust = 0;
                    NPC.ShieldStrengthTowerVortex = 0;

                    // Purge all towers
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (Main.npc[i].active
                            && (Main.npc[i].type == NPCID.LunarTowerNebula || Main.npc[i].type == NPCID.LunarTowerSolar
                            || Main.npc[i].type == NPCID.LunarTowerStardust || Main.npc[i].type == NPCID.LunarTowerVortex))
                        {
                            Main.npc[i].dontTakeDamage = false;
                            Main.npc[i].GetGlobalNPC<CalNohitQoLGlobalNPC>().NoLoot = true;
                            Main.npc[i].StrikeNPCNoInteraction(int.MaxValue, 0f, 0);
                        }
                    }                 
                }
                if (Main.IsItRaining || Main.IsItStorming)
                {
                    Main.StopRain();
                    Main.cloudAlpha = 0;
                    if (Main.netMode == NetmodeID.Server)
                        Main.SyncRain();                
                } 
            return true;
        }
    }
	public class CalNohitQoLSystem : ModSystem
    {
        public override void PreUpdateWorld()
        {
            if (CalNohitQoL.Instance.ToggleRain)
            {
                if (Main.raining == true)
                {
                    Main.StopRain();
                    Sandstorm.Happening = false;
                    Sandstorm.StopSandstorm();
                    Sandstorm.WorldClear();
                    Main.SyncRain();
                }
                else
                {
                    Main.StartRain();
                    Main.rainTime = 12000;
                    Sandstorm.StartSandstorm();
                    Sandstorm.Happening = true;
                    Main.SyncRain();
                }
                CalNohitQoL.Instance.ToggleRain = false;
            }
        }
    }
}