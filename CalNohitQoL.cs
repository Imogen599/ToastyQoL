using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Events;
using ReLogic.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using CalNohitQoL.Content.UI.UIManagers;
using CalNohitQoL.Content.UI;
using CalNohitQoL.Core;
using CalNohitQoL.Content.UI.PotionUI;
using CalamityMod.Projectiles.Boss;

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
        internal static PowersUIManager powersUIManager = new();
        internal static WorldUIManager worldUIManager = new();
        internal static BossTogglesUIManager bossTogglesUIManager = new();

        internal static PotionUIManager potionUIManager = new();

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
            //TipsList.LoadLists();
            PotionUIManager.Load();
            BaseTogglesUIManager.InitializeUIManagers(this);

            if (ModLoader.TryGetMod("InfernumMode", out Mod infernumMod))
            {
                InfernumMod = infernumMod;
                InfernumModeEnabled = true;
            }

            LoadShaders();
            LoadFonts();
        }

        public override void Unload()
		{
			Instance = null;
            CalNohitQoLLists.UnloadLists();
            //TipsList.UnloadLists();
            PotionUIManager.Unload();
            InfernumMod = null;
            InfernumModeEnabled = false;
            UnloadFonts();

        }
        private void LoadShaders()
        {
            if (Main.netMode is not NetmodeID.Server)
            {
                Ref<Effect> shrooms = new(Assets.Request<Effect>("Assets/Effects/ShroomShader", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["CalNohitQoL:Shrooms"] = new MiscShaderData(shrooms, "ShroomsPass");

                Ref<Effect> hologram = new(Assets.Request<Effect>("Assets/Effects/HologramShader", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["CalNohitQoL:Hologram"] = new MiscShaderData(hologram, "HologramPass");
            }
        }

        private static void LoadFonts()
        {
            if (!Main.dedServ)
            {
                // This was crashing on Linux and such in Calamity, I am unable to check if it will here so I am playing it safe and only allowing custom fonts to work on Windows.
                if ((int)Environment.OSVersion.Platform == 2)
                    DraedonFont = ModContent.Request<DynamicSpriteFont>("CalNohitQoL/Assets/Fonts/DraedonFont", AssetRequestMode.ImmediateLoad).Value;
                else
                    // If not the correct OS, we need to make it the default Terraria Font, Andy.
                    DraedonFont = FontAssets.MouseText.Value;
            }
        }

        private static void UnloadFonts() => DraedonFont = null;
        
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