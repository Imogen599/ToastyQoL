using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace CalNohitQoL
{
    public static class CalNohitQoLUtils
    {
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
    }
}