using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using ToastyQoL.Content.UI;
using ToastyQoL.Content.UI.MiscUI;
using ToastyQoL.Content.UI.PotionUI;

namespace ToastyQoL.Core.Systems
{
    public class UIManagerSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int mouseIndex = layers.FindIndex((layer) => layer.Name == "Vanilla: Mouse Text");
            if (mouseIndex == -1)
                return;

            layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Special UIs", () =>
            {
                if (!Main.inFancyUI && Main.playerInventory)
                {
                    CheatIndicatorUIRenderer.Draw(Main.spriteBatch);
                    SummonSlotUIIcon.Draw(Main.spriteBatch);
                }
                if (!Main.inFancyUI)
                {
                    TogglesUIManager.Draw(Main.spriteBatch);
                    PotionUIManager.Draw(Main.spriteBatch);
                }
                return true;
            }, InterfaceScaleType.UI));
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
        }
    }
}
