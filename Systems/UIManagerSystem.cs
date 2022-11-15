using CalNohitQoL.UI.QoLUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalNohitQoL.Systems
{
    public class UIManagerSystem : ModSystem
    {
		public static SpriteBatch SpriteBatch => Main.spriteBatch;

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{

			int mouseIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Mouse Text");
			if (mouseIndex == -1)
			{
				return;
			}

			GameInterfaceDrawMethod val = delegate
			{
				//IL_0082: Unknown result type (might be due to invalid IL or missing references)
				//IL_008e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0098: Unknown result type (might be due to invalid IL or missing references)
				if (!Main.inFancyUI && Main.playerInventory)
				{
					CalNohitQoL.cheatIndicatorUIRenderer.Draw(SpriteBatch);
					CalNohitQoL.SummonSlotUIIcon2.Draw(SpriteBatch);

				}
				if (!Main.inFancyUI)
				{
					CalNohitQoL.TogglesUI.Draw(SpriteBatch);

					if (TogglesUIManager.UIOpen)
					{
						CalNohitQoL.upgradesUIManager.Draw(SpriteBatch);
						CalNohitQoL.locksUIManager.Draw(SpriteBatch);
						CalNohitQoL.powersUIManager.Draw(SpriteBatch);
						CalNohitQoL.worldUIManager.Draw(SpriteBatch);
						CalNohitQoL.miscUIManager.Draw(SpriteBatch);
						CalNohitQoL.bossTogglesUIManager.Draw(SpriteBatch);
					}
					CalNohitQoL.potionUIManager.Draw(SpriteBatch);
					//CalNohitQoL.tipsUIManager.DrawBase(SpriteBatch);
					//CalNohitQoL.tipMainButtonUIElement.Draw(SpriteBatch);
				}
				return true;
			};
			object obj = (object)val;


			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Special UIs", (GameInterfaceDrawMethod)obj, (InterfaceScaleType)1));
		}
		public override void UpdateUI(GameTime gameTime)
		{
			base.UpdateUI(gameTime);
		}
	}
}
