using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using Terraria.Chat;
using Terraria.Localization;

namespace CalNohitQoL
{
    public static class CalNohitQoLUtils
    {
        public static Color TwoColorPulse(Color color1, Color color2, float time)
        {
            float timeScale = (float)((Math.Sin((double)((float)Math.PI * 2f / time) * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
            return Color.Lerp(color1, color2, timeScale);
        }

        public static string TwoColorPulseHex(Color color1, Color color2, float time)
        {
            float timeScale = (float)((Math.Sin((double)((float)Math.PI * 2f / time) * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
            return Color.Lerp(color1, color2, timeScale).Hex3();
        }

        public static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static float Clamp(this float value, float min, float max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static int Clamp(this int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        /// <summary>
        ///  Returns true if bigger/equal to 0, false if less than.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool IsPositive(int value)
        {
            if (value >= 0)
                return false;
            else
                return true;
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
    }
}