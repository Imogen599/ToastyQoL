using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using ToastyQoL.Content.UI;

namespace ToastyQoL
{
    public static class ToastyQoLUtils
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

        public static float EaseInOutSine(float value) => -(MathF.Cos(MathF.PI * value) - 1) / 2;

        public static float EaseInCirc(float value) => 1 - MathF.Sqrt(1 - MathF.Pow(value, 2));

        public static NPC ClosestNPCAt(this Vector2 origin, float maxDistanceToCheck)
        {
            NPC result = null;

            float distance = 0f;
            bool bossFound = false;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                float currentDistance = Vector2.Distance(origin, Main.npc[i].Center);

                if (currentDistance < distance && currentDistance < maxDistanceToCheck && (!bossFound || Main.npc[i].boss))
                {
                    distance = Vector2.Distance(origin, Main.npc[i].Center);
                    result = Main.npc[i];
                    if (Main.npc[i].boss)
                        bossFound = true;
                }
            }

            return result;
        }
    }
}