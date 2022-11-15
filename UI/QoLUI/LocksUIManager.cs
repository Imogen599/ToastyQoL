using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.UI.QoLUI
{
    public class LocksUIManager
    {
        internal static bool IsDrawing;
        private static bool ShouldDraw
        {
            get
            {
                if (!TogglesUIManager.UIOpen)
                {
                    IsDrawing = false;
                    return false;
                }
                if (IsDrawing)
                {
                    return true;
                }
                return false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!ShouldDraw)
                return;
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/baseSettingsUIBackgroundSmall", (AssetRequestMode)2).Value;
            Player player = Main.LocalPlayer;
            Vector2 drawCenter;
            drawCenter.X = Main.screenWidth / 2;
            drawCenter.Y = Main.screenHeight / 2;
            Vector2 spawnPos = drawCenter + new Vector2(300, 0);

            spriteBatch.Draw(backgroundTexture, spawnPos, null, Color.White, 0, backgroundTexture.Size() * 0.5f, 1f, 0, 0);
            Rectangle hoverArea = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size());
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(hoverArea);
            if (isHovering)
            {
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
            }
            DrawElements(spriteBatch);
        }
        public void DrawElements(SpriteBatch spriteBatch)
        {
            // 138 103 +35
            float baseVerticalOffset = -175;
            Texture2D fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/whiteTangle", (AssetRequestMode)2).Value;
            Player player = Main.LocalPlayer;
            // Go down in intervals of around 120
            float baseVerticalInterval = 120;
            Texture2D texture;
            Texture2D hoverTexture;
            #region Potion Tooltips
;
            texture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/Potion", (AssetRequestMode)2).Value;
            hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/PotionGlow", (AssetRequestMode)2).Value;

            // Position of the Icon
            Vector2 backgroundDrawCenter2;
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
            Vector2 drawPos2 = backgroundDrawCenter2;

            Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
            // Rectangle area of the icon and mouse to check for hovering.
            Rectangle IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, Utils.Size(fancyTexture));
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {

                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, Utils.Size(texture));

                if (mouseHitbox.Intersects(hoverArea))
                {
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Adds a tooltip to potions past your progression point]";
                }
                else
                {
                    // Seeing as our glow draws the inside of the texture, AND has glow
                    spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
                }
                Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
                if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.clickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    CalNohitQoL.Instance.PotionTooltips = !CalNohitQoL.Instance.PotionTooltips;
                    TogglesUIManager.clickCooldownTimer = TogglesUIManager.clickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }
            else
            {
                // Seeing as our glow draws the inside of the texture, AND has glow
                spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
            }

            Texture2D statusTexture = CalNohitQoL.Instance.PotionTooltips ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D statusTextureGlow = CalNohitQoL.Instance.PotionTooltips ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
            {
                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
                Main.hoverItemName = "[c/ffcc44:Adds a tooltip to potions past your progression point]\n" + (CalNohitQoL.Instance.PotionTooltips ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
            }
            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

            string textToShow2 = "Toggle Potion Tooltips";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion
            #region Potion Locks

            texture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/Potion", (AssetRequestMode)2).Value;
            hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/PotionGlow", (AssetRequestMode)2).Value;

            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval) / 2);
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, Utils.Size(fancyTexture));
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {

                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, Utils.Size(texture));

                if (mouseHitbox.Intersects(hoverArea))
                {
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Prevents drinking potions past your progression point]";
                }
                else
                    spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
                Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
                if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.clickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    CalNohitQoL.Instance.PotionLock = !CalNohitQoL.Instance.PotionLock;
                    TogglesUIManager.clickCooldownTimer = TogglesUIManager.clickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }
            else
            {
                // Seeing as our glow draws the inside of the texture, AND has glow
                spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
            }
            statusTexture = CalNohitQoL.Instance.PotionLock ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            statusTextureGlow = CalNohitQoL.Instance.PotionLock ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
            {
                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
                Main.hoverItemName = "[c/ffcc44:Prevents drinking potions past your progression point]\n" + (CalNohitQoL.Instance.PotionLock ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
            }
            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);
            textToShow2 = "Toggle Potion Locks";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);

            #endregion
            #region Item Tooltips

            texture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/Item", (AssetRequestMode)2).Value;
            hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/ItemGlow", (AssetRequestMode)2).Value;

            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval*2) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval*2) / 2);
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, Utils.Size(fancyTexture));
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {

                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, Utils.Size(texture));

                if (mouseHitbox.Intersects(hoverArea))
                {
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Adds a tooltip to Calamity items past your progression point]";
                }
                else
                    spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
                Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
                if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.clickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    CalNohitQoL.Instance.ItemTooltips = !CalNohitQoL.Instance.ItemTooltips;
                    TogglesUIManager.clickCooldownTimer = TogglesUIManager.clickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }
            else
            {
                // Seeing as our glow draws the inside of the texture, AND has glow
                spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
            }
            statusTexture = CalNohitQoL.Instance.ItemTooltips ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            statusTextureGlow = CalNohitQoL.Instance.ItemTooltips ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
            {
                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
                Main.hoverItemName = "[c/ffcc44:Adds a tooltip to Calamity items past your progression point]\n" + (CalNohitQoL.Instance.ItemTooltips ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
            }
            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);
            textToShow2 = "Toggle Item Tooltips";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion
            #region Item Locks
            
            texture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/Item", (AssetRequestMode)2).Value;
            hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Settings/ItemGlow", (AssetRequestMode)2).Value;
            
            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 3) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 3) / 2);
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, Utils.Size(fancyTexture));
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {

                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, Utils.Size(texture));

                if (mouseHitbox.Intersects(hoverArea))
                {
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Prevents using Calamity items past your progression point]";
                }
                else
                    spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
                Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
                if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.clickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    CalNohitQoL.Instance.ItemLock = !CalNohitQoL.Instance.ItemLock;
                    CalNohitQoL.Instance.AccLock = !CalNohitQoL.Instance.AccLock;
                    TogglesUIManager.clickCooldownTimer = TogglesUIManager.clickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }
            else
            {
                // Seeing as our glow draws the inside of the texture, AND has glow
                spriteBatch.Draw(texture, drawPos2, null, Color.White, 0, texture.Size() * 0.5f, 1f, 0, 0);
            }
            statusTexture = CalNohitQoL.Instance.ItemLock ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            statusTextureGlow = CalNohitQoL.Instance.ItemLock ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
            {
                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
                Main.hoverItemName = "[c/ffcc44:Prevents using Calamity items past your progression point]\n" + (CalNohitQoL.Instance.ItemLock ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
            }
            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);
            textToShow2 = "Toggle Item Locks";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion
        }
    }
}
