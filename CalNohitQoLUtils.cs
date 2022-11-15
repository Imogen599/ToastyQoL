using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.NPCs;
using CalamityMod.CalPlayer;
using CalamityMod.World;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalNohitQoL.NPCs;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.Calamitas;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.GreatSandShark;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SulphurousSea;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using CalNohitQoL.UI;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria.DataStructures;
using CalNohitQoL.UI.QoLUI;
using Terraria.GameContent;
using ReLogic.Graphics;
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