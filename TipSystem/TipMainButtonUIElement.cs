using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalNohitQoL.TipSystem
{
    public class TipMainButtonUIElement
    {
        public static bool IsDrawing;
        public float ScaleBoost = 0;
        public bool CurrentlyHovering;
        public float scale = 1;
        public string currentString = "";
        public bool ShouldDraw
        {
            get
            {
                if (!Main.playerInventory)
                {
                    return false;
                }
                return true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!ShouldDraw)
                return;
            Texture2D iconTexture = ModContent.Request<Texture2D>("CalNohitQoL/TipSystem/Textures/mainButton", (AssetRequestMode)2).Value;
            Vector2 iconCenter = new Vector2((float)Main.screenWidth - 330f, 155f);
            Rectangle iconRectangle = Utils.CenteredRectangle(iconCenter, iconTexture.Size());
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(iconRectangle);
            
            if (isHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!CurrentlyHovering)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
                CurrentlyHovering = true;
                Texture2D iconGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/TipSystem/Textures/mainButtonGlow", (AssetRequestMode)2).Value;

                if (scale < 1.1f)
                    scale += 0.03f;

                #region BackGlow Additive Drawing
                spriteBatch.End();
                spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
                Texture2D bloomTex = ModContent.Request<Texture2D>("CalNohitQoL/ExtraTextures/Bloom", (AssetRequestMode)2).Value;
                float opacity = 0.7f;
                float scale2 = 0.3f + (float)Math.Sin((double)Main.GlobalTimeWrappedHourly) * 0.02f;
                float rot = Main.GlobalTimeWrappedHourly * 0.5f;
                spriteBatch.Draw(bloomTex, iconCenter, (Rectangle?)null, Color.Cyan * opacity, rot, new Vector2(123f, 124f), scale2, (SpriteEffects)0, 0f);
                spriteBatch.End();
                spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
                #endregion

                spriteBatch.Draw(iconGlowTexture, iconCenter, null, Color.White, 0, iconGlowTexture.Size() * 0.5f, scale, SpriteEffects.None, 0);
            }
            else
            {
                CurrentlyHovering = false;
                if (scale > 1)
                    scale -= 0.03f;
                
            }
            if (scale < 1)
                scale = 1;
            spriteBatch.Draw(iconTexture, iconCenter, null, Color.White, 0, iconTexture.Size()*0.5f, scale, SpriteEffects.None, 0);
            if (isHovering)
            {
                // Today we are going to learn how to draw a box. Why? Snazzy.
                string mouseTextString = "Open Boss Compendium";

                Vector2 boxSize = FontAssets.MouseText.Value.MeasureString(mouseTextString);
                Vector2 textboxStart = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(14,14);
                if (Main.ThickMouse)
                {
                    textboxStart += Vector2.One * 6f;
                }
                if (!Main.mouseItem.IsAir)
                {
                    textboxStart.X += 34f;
                }
                if (textboxStart.X + boxSize.X + 4f > Main.screenWidth)
                {
                    textboxStart.X = Main.screenWidth - boxSize.X - 4f;
                }
                if (textboxStart.Y + boxSize.Y + 4f > Main.screenHeight)
                {
                    textboxStart.Y = Main.screenHeight - boxSize.Y - 4f;
                }
                Utils.DrawInvBG(spriteBatch, new Rectangle((int)textboxStart.X - 10, (int)textboxStart.Y-6, (int)boxSize.X-22, (int)boxSize.Y+5), new Color(21, 37, 46));
                //Main.instance.MouseText(mouseTextString);
                // Custom Mouse Text
                TipsUIManager.DrawCustomMouseText(spriteBatch, CalNohitQoL.DraedonFont, mouseTextString, Color.Cyan, true);
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    //Player player = Main.LocalPlayer;
                    //player.KillMe(new Terraria.DataStructures.PlayerDeathReason(), 1000, 1);
                    //Main.NewText(player.name+" got no bitches", new Color(225, 25, 25));

                    
                    TipsUIManager.IsDrawing = !TipsUIManager.IsDrawing;
                    SoundEngine.PlaySound(TipsUIManager.IsDrawing ? SoundID.MenuOpen : SoundID.MenuClose, Main.LocalPlayer.Center);
                    if (TipsUIManager.IsDrawing)
                    {
                        Main.playerInventory = false;
                    }
                }
            }
        }
    }
}
