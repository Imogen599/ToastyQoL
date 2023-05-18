using CalNohitQoL.Content.UI.UIManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalNohitQoL.Core.Systems
{
    public class UIManagerSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int mouseIndex = layers.FindIndex((layer) => layer.Name == "Vanilla: Mouse Text");
            if (mouseIndex == -1)
            {
                return;
            }

            static bool val()
            {
                if (!Main.inFancyUI && Main.playerInventory)
                {
                    CalNohitQoL.cheatIndicatorUIRenderer.Draw(Main.spriteBatch);
                    CalNohitQoL.SummonSlotUIIcon2.Draw(Main.spriteBatch);

                }
                if (!Main.inFancyUI)
                {
                    CalNohitQoL.TogglesUI.Draw(Main.spriteBatch);

                    if (TogglesUIManager.UIOpen)
                    {
                        BaseTogglesUIManager.DrawUIManagers(Main.spriteBatch);
                        CalNohitQoL.bossTogglesUIManager.Draw(Main.spriteBatch);
                    }
                    CalNohitQoL.potionUIManager.Draw(Main.spriteBatch);
                }
                return true;
            }

            layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Special UIs", val, InterfaceScaleType.UI));
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
        }
    }
}
