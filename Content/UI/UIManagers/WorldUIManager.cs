using CalNohitQoL.Core;
using CalNohitQoL.Core.ModPlayers;
using CalNohitQoL.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.UI.UIManagers
{
    public class WorldUIManager : BaseTogglesUIManager
    {
        public const string UIName = "WorldManager";

        public override string Name => UIName;

        public override void Initialize()
        {
            UIElements = new()
            {
                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Map", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/MapGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Reveal The Full Map",
                () => "Fills out all of your map, cannot be reversed",
                1f,
                () => { MapSystem.MapReveal = true; }),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Spawns", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/SpawnsGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Enemy Spawns",
                () => "Toggles blocking enemies from spawning",
                2f,
                () => { Toggles.NoSpawns = !Toggles.NoSpawns; },
                typeof(Toggles).GetField("NoSpawns", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/time", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/timeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Time Flow",
                () => "Toggles the flow of time",
                3f,
                () => { Toggles.FrozenTime = !Toggles.FrozenTime; },
                typeof(Toggles).GetField("FrozenTime", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/time", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/timeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Day Time",
                () => "Swaps between night and day",
                4f,
                () => 
                {
                     Main.dayTime = !Main.dayTime;
                     Main.time = 0.0;
                     if (Main.dayTime)
                     {
                        TogglesUIManager.TextToShow = "Time set to Day";
                        TogglesUIManager.ColorToUse = Color.Gold;
                     }
                     else
                     {
                        TogglesUIManager.TextToShow = "Time set to Night";
                        TogglesUIManager.ColorToUse = Color.DimGray;
                     }
                }),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/water", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/waterGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Rain",
                () => "Toggles a rainstorm",
                5f,
                () => { Toggles.ToggleRain = !Toggles.ToggleRain; },
                typeof(Toggles).GetField("ToggleRain", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Event", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/EventGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Disable Events",
                () => "Cancels/disables any active events",
                6f,
                () => { Toggles.DisableEvents = !Toggles.DisableEvents; }),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/biome", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/biomeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Biome Fountains",
                () => "Water fountains now force their biome post Queen Bee",
                7f,
                () => { Toggles.BiomeFountainsForceBiome = !Toggles.BiomeFountainsForceBiome; },
                typeof(Toggles).GetField("BiomeFountainsForceBiome", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/difficulty", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/difficultyGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle World Difficulty",
                () => 
                {
                    string difficulty = "";
                    string color = "";
                    switch (Main.GameMode)
                    {
                        case 0:
                            difficulty = "Expert";
                            color = "[c/af4bff:";
                            break;

                        case 1:
                            difficulty = "Master";
                            color = "[c/FF4444:";
                            break;

                        case 2:
                            difficulty = "Journey";
                            color = "[c/ffff66:";
                            break;

                        default:
                            difficulty = "Normal";
                            color = "[c/ffffff:";
                            break;
                    }
                    return $"Set the world difficulty to] {color}{difficulty}";
                },
                8f,
                () => 
                {
                    string text;
                    switch (Main.GameMode)
                    {
                        case 0:
                            Main.GameMode = 1;
                            Main.LocalPlayer.difficulty = 0;
                            text = "Expert enabled";
                            TogglesUIManager.ColorToUse = new Color(175, 75, 255);
                            break;

                        case 1:
                            Main.GameMode = 2;
                            Main.LocalPlayer.difficulty = 0;
                            text = "Master enabled";
                            TogglesUIManager.ColorToUse = new Color(255, 68, 68);
                            break;

                        case 2:
                            Main.GameMode = 3;
                            Main.LocalPlayer.difficulty = 3;
                            text = "Journey enabled";
                            TogglesUIManager.ColorToUse = new Color(255, 255, 102);
                            break;

                        default:
                            Main.GameMode = 0;
                            Main.LocalPlayer.difficulty = 0;
                            text = "Normal enabled";
                            TogglesUIManager.ColorToUse = Color.White;
                            break;
                    }
                    GenericUpdatesModPlayer.UIUpdateTextTimer = 120;
                    TogglesUIManager.TextToShow = text;
                }),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Skull", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/SkullGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Player Difficulty",
                () =>
                {
                    string difficulty;
                    string color;
                    switch (Main.LocalPlayer.difficulty)
                    {
                        case 0:
                            difficulty = "Mediumcore";
                            color = "[c/af4bff:";
                            break;
                        case 1:
                            difficulty = "Hardcore";
                            color = "[c/FF4444:";
                            break;
                        default:
                            difficulty = "Classic";
                            color = "[c/ffffff:";
                            break;
                    }
                    return $"Set the player difficulty to] {color}{difficulty}";
                },
                9f,
                () =>
                {
                    string text;
                    switch (Main.LocalPlayer.difficulty)
                    {
                        case 0:
                            Main.LocalPlayer.difficulty = 1;
                            text = "Mediumcore enabled";
                            TogglesUIManager.ColorToUse = new Color(175, 75, 255);
                            break;
                        case 1:
                            Main.LocalPlayer.difficulty = 2;
                            text = "Hardcore Enabled";
                            TogglesUIManager.ColorToUse = new Color(255, 68, 68);
                            break;
                        default:
                            Main.LocalPlayer.difficulty = 0;
                            text = "Classic enabled";
                            TogglesUIManager.ColorToUse = Color.White;
                            break;
                    }
                    if (Main.GameMode == 3 && Main.LocalPlayer.difficulty != 3)
                    {
                        // Stops you and the world desyncing being journey.
                        Main.GameMode = 1;
                    }
                    GenericUpdatesModPlayer.UIUpdateTextTimer = 120;
                    TogglesUIManager.TextToShow = text;
                }),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/water", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/waterGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Map Teleporting",
                () => "Toggles right clicking the fullscreen map to teleport",
                10f,
                () => { MapSystem.MapTeleport = !MapSystem.MapTeleport; },
                typeof(MapSystem).GetField("MapTeleport", CalNohitQoLUtils.UniversalBindingFlags)),
            };
        }
    }
}
