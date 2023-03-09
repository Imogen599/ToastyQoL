using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalNohitQoL.Core.Systems
{
    public class MapSystem : ModSystem
    {
        internal static bool MapReveal = false;
        internal static bool MapTeleport = true;
        private static int RevealingMapPart = 1;

        public static void RevealTheEntireMap()
        {
            // Splitting this over 4 frames reduced the lag from 1.07 seconds to just 0.27 :) (different worlds so maybe
            // not quite that much, but it should be faster I hope).
            switch (RevealingMapPart)
            {
                case 1:
                    for (int i = 0; i < Main.maxTilesX / 4; i++)
                    {
                        for (int j = 0; j < Main.maxTilesY; j++)
                        {
                            if (WorldGen.InWorld(i, j, 0))
                            {
                                Main.Map.Update(i, j, byte.MaxValue);
                            }
                        }
                    }
                    Main.refreshMap = true;
                    RevealingMapPart++;
                    break;
                case 2:
                    for (int i = Main.maxTilesX / 4; i < Main.maxTilesX / 2; i++)
                    {
                        for (int j = 0; j < Main.maxTilesY; j++)
                        {
                            if (WorldGen.InWorld(i, j, 0))
                            {
                                Main.Map.Update(i, j, byte.MaxValue);
                            }
                        }
                    }
                    Main.refreshMap = true;
                    RevealingMapPart++;
                    break;
                case 3:
                    for (int i = Main.maxTilesX / 2; i < 3 * (Main.maxTilesX / 4); i++)
                    {
                        for (int j = 0; j < Main.maxTilesY; j++)
                        {
                            if (WorldGen.InWorld(i, j, 0))
                            {
                                Main.Map.Update(i, j, byte.MaxValue);
                            }
                        }
                    }
                    Main.refreshMap = true;
                    RevealingMapPart++;
                    break;
                case 4:
                    for (int i = 3 * (Main.maxTilesX / 4); i < Main.maxTilesX; i++)
                    {
                        for (int j = 0; j < Main.maxTilesY; j++)
                        {
                            if (WorldGen.InWorld(i, j, 0))
                            {
                                Main.Map.Update(i, j, byte.MaxValue);
                            }
                        }
                    }
                    Main.refreshMap = true;
                    RevealingMapPart = 1;
                    MapReveal = false;
                    break;
            }

        }
        public static void TryToTeleportPlayerOnMap()
        {
            if (Main.mouseRight && Main.keyState.IsKeyUp((Keys)162))
            {
                int mapWidth = Main.maxTilesX * 16;
                int mapHeight = Main.maxTilesY * 16;
                Vector2 cursorPosition;
                cursorPosition = new Vector2(Main.mouseX, Main.mouseY);
                cursorPosition.X -= Main.screenWidth / 2;
                cursorPosition.Y -= Main.screenHeight / 2;
                Vector2 mapPosition = Main.mapFullscreenPos;
                Vector2 cursorWorldPosition = mapPosition;
                cursorPosition /= 16f;
                cursorPosition *= 16f / Main.mapFullscreenScale;
                cursorWorldPosition += cursorPosition;
                cursorWorldPosition *= 16f;
                cursorWorldPosition.Y -= Main.LocalPlayer.height;
                if (cursorWorldPosition.X < 0f)
                    cursorWorldPosition.X = 0f;
                else if (cursorWorldPosition.X + Main.LocalPlayer.width > mapWidth)
                    cursorWorldPosition.X = mapWidth - Main.LocalPlayer.width;
                if (cursorWorldPosition.Y < 0f)
                    cursorWorldPosition.Y = 0f;
                else if (cursorWorldPosition.Y + Main.LocalPlayer.height > mapHeight)
                    cursorWorldPosition.Y = mapHeight - Main.LocalPlayer.height;
                if (Main.LocalPlayer.position != cursorWorldPosition)
                {
                    Main.LocalPlayer.Teleport(cursorWorldPosition, 1, 0);
                    Main.LocalPlayer.position = cursorWorldPosition;
                    Main.LocalPlayer.velocity = Vector2.Zero;
                    Main.LocalPlayer.fallStart = (int)(Main.LocalPlayer.position.Y / 16f);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Main.myPlayer, cursorWorldPosition.X, cursorWorldPosition.Y, 1, 0, 0);
                }
            }
        }
        public override void PostUpdateWorld()
        {
            if (MapReveal)
                RevealTheEntireMap();
        }

        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            if (MapTeleport)
                TryToTeleportPlayerOnMap();
        }
    }
}