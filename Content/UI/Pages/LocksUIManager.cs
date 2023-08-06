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
        public const string LocksUIName = "LocksManager";
    
        public static void InitializeLocks()
        {
            Texture2D potion = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Settings/Potion", AssetRequestMode.ImmediateLoad).Value;
            Texture2D potionGlow = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Settings/PotionGlow", AssetRequestMode.ImmediateLoad).Value;
            Texture2D item = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Settings/Item", AssetRequestMode.ImmediateLoad).Value;
            Texture2D itemGlow = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Settings/ItemGlow", AssetRequestMode.ImmediateLoad).Value;

            List<PageUIElement> uIElements = new()
            {
                new PageUIElement(potion,
                    potionGlow,
                    () => "Toggle Potion Tooltips",
                    () => "Adds a tooltip to potions past your progression point",
                    1f,
                    () => { Toggles.PotionTooltips = !Toggles.PotionTooltips; },
                    typeof(Toggles).GetField("PotionTooltips", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(potion,
                    potionGlow,
                    () => "Toggle Potion Locks",
                    () => "Prevents drinking potions past your progression point",
                    2f,
                    () => { Toggles.PotionLock = !Toggles.PotionLock; },
                    typeof(Toggles).GetField("PotionLock", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(item,
                    itemGlow,
                    () => "Toggle Item Tooltips",
                    () => "Adds a tooltip to some items past your progression point",
                    3f,
                    () => { Toggles.ItemTooltips = !Toggles.ItemTooltips; },
                    typeof(Toggles).GetField("ItemTooltips", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(item,
                    itemGlow,
                    () => "Toggle Item Locks",
                    () => "Prevents using some items past your progression point",
                    4f,
                    () => { Toggles.ItemLock = !Toggles.ItemLock; },
                    typeof(Toggles).GetField("ItemLock", ToastyQoLUtils.UniversalBindingFlags)),
            };
            TogglesPage uiManager = new(uIElements, LocksUIName, "Progression Locks", ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/locksUIIcon", AssetRequestMode.ImmediateLoad).Value, 4f, true);
            uiManager.TryRegister();
        }
    }      
}
