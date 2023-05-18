using CalNohitQoL.Core;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.UI.UIManagers
{
    public class LocksUIManager : BaseTogglesUIManager
    {
        public const string UIName = "LocksManager";

        public override string Name => UIName;

        public override bool UseSmallerBackground => true;

        public override void Initialize()
        {
            Texture2D potion = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Settings/Potion", AssetRequestMode.ImmediateLoad).Value;
            Texture2D potionGlow = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Settings/PotionGlow", AssetRequestMode.ImmediateLoad).Value;
            Texture2D item = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Settings/Item", AssetRequestMode.ImmediateLoad).Value;
            Texture2D itemGlow = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Settings/ItemGlow", AssetRequestMode.ImmediateLoad).Value;

            UIElements = new()
            {
                new PageUIElement(potion,
                    potionGlow,
                    () => "Toggle Potion Tooltips",
                    () => "Adds a tooltip to potions past your progression point",
                    1f,
                    () => { Toggles.PotionTooltips = !Toggles.PotionTooltips; },
                    typeof(Toggles).GetField("PotionTooltips", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(potion,
                    potionGlow,
                    () => "Toggle Potion Locks",
                    () => "Prevents drinking potions past your progression point",
                    2f,
                    () => { Toggles.PotionLock = !Toggles.PotionLock; },
                    typeof(Toggles).GetField("PotionLock", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(item,
                    itemGlow,
                    () => "Toggle Item Tooltips",
                    () => "Adds a tooltip to Calamity items past your progression point",
                    3f,
                    () => { Toggles.ItemTooltips = !Toggles.ItemTooltips; },
                    typeof(Toggles).GetField("ItemTooltips", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(item,
                    itemGlow,
                    () => "Toggle Item Locks",
                    () => "Prevents using Calamity items past your progression point",
                    4f,
                    () => { Toggles.ItemLock = !Toggles.ItemLock; },
                    typeof(Toggles).GetField("ItemLock", CalNohitQoLUtils.UniversalBindingFlags)),
            };
        }
    }      
}
