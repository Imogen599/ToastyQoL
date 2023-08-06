using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.ModLoader;
using ToastyQoL.Content.UI.Pages;
using ToastyQoL.Core;

namespace ToastyQoL.Content.UI.UIManagers
{
    public static partial class UIManagerAutoloader
    {
        public const string PowerUIName = "PowersManager";

        public static void InitializePower()
        {
            List<PageUIElement> uIElements = new()
            {
                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Godmode", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/GodmodeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Godmode",
                () => "Prevents you from taking damage",
                1f,
                () => { Toggles.GodmodeEnabled = !Toggles.GodmodeEnabled; },
                typeof(Toggles).GetField("GodmodeEnabled", ToastyQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/InstantDeath", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/InstantDeathGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Instant Death",
                () => "Makes you die upon taking damage",
                2f,
                () => { Toggles.InstantDeath = !Toggles.InstantDeath; },
                typeof(Toggles).GetField("InstantDeath", ToastyQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Wings", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/WingsGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Flight",
                () => "You never run out of flight time",
                3f,
                () => { Toggles.InfiniteFlightTime = !Toggles.InfiniteFlightTime; },
                typeof(Toggles).GetField("InfiniteFlightTime", ToastyQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Potion", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/PotionGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Potions",
                () => "Potion durations are infinite",
                4f,
                () => { Toggles.InfinitePotions = !Toggles.InfinitePotions; },
                typeof(Toggles).GetField("InfinitePotions", ToastyQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Ammo", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/AmmoGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Ammo",
                () => "Ammo is not consumed",
                5f,
                () => { Toggles.InfiniteAmmo = !Toggles.InfiniteAmmo; },
                typeof(Toggles).GetField("InfiniteAmmo", ToastyQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Cons", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/ConsGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Cons.",
                () => "Consumables are not consumed",
                6f,
                () => { Toggles.InfiniteConsumables = !Toggles.InfiniteConsumables; },
                typeof(Toggles).GetField("InfiniteConsumables", ToastyQoLUtils.UniversalBindingFlags)
                )
            };

            TogglesPage uIManager = new(uIElements, PowerUIName, "Powers Toggles", ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/playerUIIcon", AssetRequestMode.ImmediateLoad).Value, 6f);
            uIManager.TryRegister();
        }
    }
}