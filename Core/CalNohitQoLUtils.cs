using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using CalNohitQoL.Content.UI.UIManagers;
using ReLogic.Graphics;
using System.Reflection;

namespace CalNohitQoL
{
    public static class CalNohitQoLUtils
    {
        public static Vector2 ScreenCenter => new(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f);

        public static Rectangle MouseRectangle => new(Main.mouseX, Main.mouseY, 2, 2);

        public static bool CanAndHasClickedUIElement => (Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0;

        public static BindingFlags UniversalBindingFlags => BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        public static Color TwoColorPulse(Color color1, Color color2, float time)
        {
            float timeScale = (float)((Math.Sin((double)((float)Math.PI * 2f / time) * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
            return Color.Lerp(color1, color2, timeScale);
        }

        /// <summary>
        ///  Sends a chat message, accounting for MP.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void DisplayText(string text, Color? color = null)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(text, color ?? Color.White);
            else if (Main.netMode == NetmodeID.Server)
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color ?? Color.White);
        }

        /// <summary>
        /// Swaps to the provided render target and flushes the screen.
        /// </summary>
        /// <param name="renderTarget"></param>
        /// <param name="flushColor">The color to flush the screen with. Defaults to <see cref="Color.Transparent"/></param>
        public static void SwapToRenderTarget(this RenderTarget2D renderTarget, Color? flushColor = null)
        {
            // If we are in the menu, a server, or any of these are null, return.
            if (Main.gameMenu || Main.dedServ || renderTarget is null || Main.graphics.GraphicsDevice is null || Main.spriteBatch is null)
                return;

            // Otherwise set the render target.
            Main.graphics.GraphicsDevice.SetRenderTarget(renderTarget);

            flushColor ??= Color.Transparent;
            // "Flush" the screen, removing any previous things drawn to it.
            Main.graphics.GraphicsDevice.Clear(flushColor.Value);
        }

        public static void DrawBorderStringFourWayBetter(SpriteBatch sb, DynamicSpriteFont font, string text, float x, float y, Color textColor, Color borderColor, Vector2 origin, float scale = 1f, float layerDepth = 0f)
        {
            Color color = borderColor;
            Vector2 zero = Vector2.Zero;
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        zero.X = x - 2f;
                        zero.Y = y;
                        break;
                    case 1:
                        zero.X = x + 2f;
                        zero.Y = y;
                        break;
                    case 2:
                        zero.X = x;
                        zero.Y = y - 2f;
                        break;
                    case 3:
                        zero.X = x;
                        zero.Y = y + 2f;
                        break;
                    default:
                        zero.X = x;
                        zero.Y = y;
                        color = textColor;
                        break;
                }

                sb.DrawString(font, text, zero, color, 0f, origin, scale, SpriteEffects.None, layerDepth);
            }
        }
    }
}