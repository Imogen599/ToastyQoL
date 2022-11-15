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
using CalNohitQoL.Globals;

namespace CalNohitQoL
{
    public class CalNohitQoL : Mod
	{
        internal static CalNohitQoL Instance;

        // OTHER UI VARIABLES
        internal static List<TogglesUIElement> OtherUIElements = new();
        internal static CheatIndicatorUIRenderer cheatIndicatorUIRenderer = new();
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
        
    }
	public class CalNohitQoLSystem : ModSystem
    {
        public override void PreUpdateWorld()
        {
            if (Toggles.ToggleRain)
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
                Toggles.ToggleRain = false;
            }
        }
    }
}