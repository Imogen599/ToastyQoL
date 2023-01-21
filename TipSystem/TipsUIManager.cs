using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalNohitQoL.TipSystem
{
    public class TipsUIManager
    {
        public static bool IsDrawing { get; internal set; } = false;
        private float Opacity = 1;
        private int Timer;
        private float Scale = 1;
        private bool CurrentlyHoveringCloseButtonMain;
        private bool CurrentlyHoveringBossIcon;
        private Vector2 PreHardmodeBossIconScrollOffset = Vector2.Zero;

        public Player player => Main.LocalPlayer;

        private static Rectangle MouseHitbox => new(Main.mouseX, Main.mouseY, 2, 2);

        private readonly List<Rectangle> ActiveBossIconRects = new();

        public bool ShouldDraw
        {
            get
            {
                if (IsDrawing)
                {
                    return true;
                }
                if (Timer > 0)
                    Timer = 0;
                return false;
            }
        }

        public List<BossElement> BossList = new()
        {
            //new BossElement("King Slime", "Slimy Monarch", "50%", TipsList.KSTipsList, new Color(50,50,255) ,ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/kingSlime",(AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/kingSlimeGlow",(AssetRequestMode)1).Value),
            //new BossElement("Desert Scourge", "Dried Worm", "N/A", TipsList.DSTipsList, new Color(200,170,0) ,ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/desertScourge",(AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/desertScourgeGlow",(AssetRequestMode)1).Value, 0.75f),
            //new BossElement("Eye of Cthulhu", "Demonic Seer", "", TipsList.EoCTipsList, new Color(50,50,255) ,ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/eoc",(AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/eoc",(AssetRequestMode)1).Value),
        };

        public BossElement CurrentBossShowing;
        
        public BossElement BaseBossShowing = new BossElement("base", "base", "base", new List<string>() { "base" }, Color.White, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/kingSlime", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/kingSlime", (AssetRequestMode)1).Value, 1);
        
        public void DrawBase(SpriteBatch spriteBatch)
        {
            if (ShouldDraw)
            {
                if (Main.playerInventory)
                    Main.playerInventory = false;
                // Define a base drawing position using Main.screenWidth + Main.screenHeight to allow for scaling.
                Vector2 spawnPos = new(Main.screenWidth / 2, Main.screenHeight / 2);

                Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/TipSystem/Textures/mainBackground", (AssetRequestMode)2).Value;

                Rectangle backgroundRect = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size() * Scale);
                Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);

                if (Timer <= 10)
                {
                    Opacity = (float)Timer / 10;
                    Timer++;
                }
                if (mouseHitbox.Intersects(backgroundRect))
                    Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                spriteBatch.Draw(backgroundTexture, spawnPos, null, Color.White * Opacity, 0, backgroundTexture.Size() * 0.5f, Scale, 0, 0);

                #region close button
                // Draw close button.
                Vector2 closeButtonDrawPos = new Vector2(335, -153) + spawnPos;


                string text = "Close";
                Texture2D texture = ModContent.Request<Texture2D>("CalNohitQoL/TipSystem/Textures/closeButtonBackground", (AssetRequestMode)2).Value;
                Texture2D glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/TipSystem/Textures/closeButtonBackgroundGlow", (AssetRequestMode)2).Value;

                spriteBatch.Draw(texture, closeButtonDrawPos, null, Color.White, 0, texture.Size() * 0.5f, Scale, 0, 0);
                Rectangle closeRect = Utils.CenteredRectangle(closeButtonDrawPos, texture.Size() * Scale);
                DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, CalNohitQoL.DraedonFont, text, closeButtonDrawPos + new Vector2(-17, -8.5f), Color.Cyan * Opacity, 0, Vector2.Zero, 0.4f, 0, 0);
                //Rectangle closeRect = new((int)drawPos.X, (int)drawPos.Y, (int)(textureSize.X*textureScale.X)+10, (int)(textureSize.Y*textureScale.Y)+5);
                Color textColor = Color.Cyan;
                if (mouseHitbox.Intersects(closeRect))
                {
                    if (!CurrentlyHoveringCloseButtonMain)
                        SoundEngine.PlaySound(SoundID.MenuTick, player.Center);

                    CurrentlyHoveringCloseButtonMain = true;
                    spriteBatch.Draw(glowTexture, closeButtonDrawPos, null, Color.White, 0, glowTexture.Size() * 0.5f, Scale, 0, 0);
                    textColor = Color.LightCyan;
                    Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                    if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                    {
                        // ON CLICK AFFECT
                        IsDrawing = false;
                        SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    }
                }
                else
                    CurrentlyHoveringCloseButtonMain = false;
                DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, CalNohitQoL.DraedonFont, text, closeButtonDrawPos + new Vector2(-17, -8.5f), textColor * Opacity, 0, Vector2.Zero, 0.4f, 0, 0);
                #endregion
                DrawMainPageElements(spriteBatch, spawnPos);
            }
        }
        private void DrawMainPageElements(SpriteBatch spriteBatch, Vector2 spawnPos)
        {
            #region Headers
            // Main Title Text
            DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, CalNohitQoL.DraedonFont, "Boss Compendium", spawnPos + new Vector2(-333, -170), Color.White * Opacity, 0, Vector2.Zero, 0.8f, 0, 0);
            // Seperator under title
            Texture2D titleSeperatorTexture = ModContent.Request<Texture2D>("CalNohitQoL/TipSystem/Textures/blueLineSeperatorTitle", (AssetRequestMode)2).Value;
            spriteBatch.Draw(titleSeperatorTexture, spawnPos + new Vector2(-220, -120), null, Color.White * Opacity, 0, titleSeperatorTexture.Size() * 0.5f, Scale, 0, 0);
            #endregion
            #region Pre Hardmode Section
            // Background

            // Scroll Bar
            Texture2D scrollBarBaseTexture = ModContent.Request<Texture2D>("CalNohitQoL/TipSystem/Textures/draedonScrollBarBackground", (AssetRequestMode)2).Value;
            spriteBatch.Draw(scrollBarBaseTexture, spawnPos + new Vector2(0, -30), null, Color.White, 0, scrollBarBaseTexture.Size() * 0.5f, Scale, SpriteEffects.None, 0);
            // Boss Icons
            Vector2 baseIconDrawPosition = spawnPos + new Vector2(-300, -75);
            Vector2 horizontalIconOffset = new(50, 0);
            for (int i = 0; i < 10; i++)
            {
                if (i < BossList.Count)
                {
                    BossElement bossElement = BossList[i];
                    Vector2 finalDrawPos = baseIconDrawPosition + PreHardmodeBossIconScrollOffset + horizontalIconOffset * i;
                    Vector2 origin = bossElement.Texture.Size() * 0.5f;
                    Rectangle bossIconRectangle = Utils.CenteredRectangle(finalDrawPos, bossElement.Texture.Size() * bossElement.Scale);
                    ActiveBossIconRects.Add(bossIconRectangle);
                    if (MouseHitbox.Intersects(bossIconRectangle))
                    {
                        #region BackGlow Additive Drawing
                        spriteBatch.End();
                        spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
                        Texture2D bloomTex = ModContent.Request<Texture2D>("CalNohitQoL/ExtraTextures/Bloom", (AssetRequestMode)2).Value;
                        float opacity = 0.7f;
                        float scale2 = 0.3f + (float)Math.Sin((double)Main.GlobalTimeWrappedHourly) * 0.02f;
                        float rot = Main.GlobalTimeWrappedHourly * 0.5f;
                        spriteBatch.Draw(bloomTex, finalDrawPos, (Rectangle?)null, Color.Cyan * opacity, rot, new Vector2(123f, 124f), scale2, (SpriteEffects)0, 0f);
                        spriteBatch.End();
                        spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
                        #endregion
                    }
                    spriteBatch.Draw(bossElement.Texture, finalDrawPos, null, Color.White, 0, origin, bossElement.Scale*Scale, SpriteEffects.None, 0);
                }
            }
            bool isHovering = false;
            foreach (Rectangle rect in ActiveBossIconRects)
            {
                if (MouseHitbox.Intersects(rect))
                    isHovering = true;
            }
            if (isHovering && !CurrentlyHoveringBossIcon)
            {
                CurrentlyHoveringBossIcon = true;
                SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
            }
            else if(!isHovering)
                CurrentlyHoveringBossIcon = false;
            ActiveBossIconRects.Clear();
            #endregion
        }
        public static void DrawCustomMouseText(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Color baseColor, bool withShadow = true)
        {
            Vector2 position;
            Vector2 size = font.MeasureString(text)*(font == CalNohitQoL.DraedonFont ? 0.4f : 1);
            Vector2 textboxStart = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(14, 14);
            if (Main.ThickMouse)
                textboxStart += Vector2.One * 6f;
            if (!Main.mouseItem.IsAir)
                textboxStart.X += 34f;
            if (textboxStart.X + size.X + 4f > Main.screenWidth)
                textboxStart.X = Main.screenWidth - size.X - 4f;
            if (textboxStart.Y + size.Y + 4f > Main.screenHeight)
                textboxStart.Y = Main.screenHeight - size.Y - 4f;

            position = new Vector2(textboxStart.X, textboxStart.Y);
            float scale;
            if (font == CalNohitQoL.DraedonFont)
                scale = 0.4f;
            else
                scale = 1;
            DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, text, position, baseColor,0, Vector2.Zero, scale, 0,0);
        }
    }
}
