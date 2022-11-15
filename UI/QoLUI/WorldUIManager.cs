using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalNohitQoL.UI.QoLUI
{
    public class WorldUIManager
    {
        public static bool IsDrawing { get; internal set; }
        public static int PageNumber { get; internal set; } = 1;
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
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/baseSettingsUIBackground", (AssetRequestMode)2).Value;
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
            float baseVerticalOffset = -308;
            Texture2D fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/whiteTangle", (AssetRequestMode)2).Value;
            Texture2D fancyTextureSmall = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/SmallerWhiteRect", (AssetRequestMode)2).Value;
            Player player = Main.LocalPlayer;
            #region Page Numbers
            switch (PageNumber)
            {
                // First Page
                case 1:
                    Texture2D arrowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Arrow", (AssetRequestMode)2).Value;
                    Texture2D arrowGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/ArrowGlow", (AssetRequestMode)2).Value;
                    Vector2 backgroundDrawCenter2;
                    backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
                    backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
                    Vector2 drawPos2 = backgroundDrawCenter2;
                    Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

                    drawPos2.X += 130;
                    drawPos2.Y -= 2f;
                    Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 762) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
                    Rectangle whiteHitbox = Utils.CenteredRectangle(whiteDrawPos, fancyTextureSmall.Size());

                    if (mouseHitbox.Intersects(whiteHitbox))
                    {
                        spriteBatch.Draw(fancyTextureSmall, whiteDrawPos, null, Color.White * 0.3f, 0, fancyTextureSmall.Size() * 0.5f, 1f, 0, 0);
                        Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
                        if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.clickCooldownTimer == 0)
                        {
                            // ON CLICK AFFECT
                            TogglesUIManager.clickCooldownTimer = TogglesUIManager.clickCooldownLength;
                            SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                            PageNumber = 2;
                        }
                    }
                    Rectangle arrowRect = Utils.CenteredRectangle(whiteDrawPos, arrowTexture.Size());
                    float scale = 1;
                    if (mouseHitbox.Intersects(arrowRect))
                    {
                        scale = 1.15f;
                    }
                    spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.White, 0, arrowTexture.Size() * 0.5f, scale, 0, 0);
                    if (mouseHitbox.Intersects(arrowRect))
                    {
                        spriteBatch.Draw(arrowGlowTexture, whiteDrawPos, null, Color.White, 0, arrowGlowTexture.Size() * 0.5f, scale, 0, 0);

                    }
                    backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
                    backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
                    drawPos2 = backgroundDrawCenter2;
                    mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

                    drawPos2.X += 130;
                    drawPos2.Y -= 2f;
                    whiteDrawPos = new Vector2((Main.screenWidth + 436) / 2, (Main.screenHeight + baseVerticalOffset) / 2);

                    spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.Black * 0.8f, 0, arrowTexture.Size() * 0.5f, 1f, SpriteEffects.FlipHorizontally, 0);
                    DrawPage(PageNumber, spriteBatch);
                    break;
                case 2:
                    arrowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Arrow", (AssetRequestMode)2).Value;
                    arrowGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/ArrowGlow", (AssetRequestMode)2).Value;
                    backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
                    backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
                    drawPos2 = backgroundDrawCenter2;
                    mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

                    drawPos2.X += 130;
                    drawPos2.Y -= 2f;
                    whiteDrawPos = new Vector2((Main.screenWidth + 436) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
                    whiteHitbox = Utils.CenteredRectangle(whiteDrawPos, fancyTextureSmall.Size());

                    if (mouseHitbox.Intersects(whiteHitbox))
                    {
                        spriteBatch.Draw(fancyTextureSmall, whiteDrawPos, null, Color.White * 0.3f, 0, fancyTextureSmall.Size() * 0.5f, 1f, 0, 0);
                        Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
                        if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.clickCooldownTimer == 0)
                        {
                            // ON CLICK AFFECT
                            TogglesUIManager.clickCooldownTimer = TogglesUIManager.clickCooldownLength;
                            SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                            PageNumber = 1;
                        }
                    }
                    arrowRect = Utils.CenteredRectangle(whiteDrawPos, arrowTexture.Size());
                    scale = 1;
                    if (mouseHitbox.Intersects(arrowRect))
                    {
                        scale = 1.15f;
                    }
                    spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.White, 0, arrowTexture.Size() * 0.5f, scale, SpriteEffects.FlipHorizontally, 0);
                    if (mouseHitbox.Intersects(arrowRect))
                    {
                        spriteBatch.Draw(arrowGlowTexture, whiteDrawPos, null, Color.White, 0, arrowGlowTexture.Size() * 0.5f, scale, SpriteEffects.FlipHorizontally, 0);
                    }

                    backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
                    backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
                    drawPos2 = backgroundDrawCenter2;
                    mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

                    drawPos2.X += 130;
                    drawPos2.Y -= 2f;
                    whiteDrawPos = new Vector2((Main.screenWidth + 762) / 2, (Main.screenHeight + baseVerticalOffset) / 2);

                    spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.Black * 0.8f, 0, arrowTexture.Size() * 0.5f, 1f, 0, 0);
                    DrawPage(PageNumber, spriteBatch);
                    break;
            }
            #endregion
        }
        public void DrawPage(int pageNumber, SpriteBatch spriteBatch)
        {
            float baseVerticalOffset = -210;
            float baseVerticalInterval = 120;
            Texture2D fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/whiteTangle", (AssetRequestMode)2).Value;
            Player player = Main.LocalPlayer;

            Texture2D baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Map", (AssetRequestMode)2).Value;
            Texture2D glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/MapGlow", (AssetRequestMode)2).Value;
            string textToSend = "Reveal The Full Map";
            string textToSendFormat = "[c/ffcc44:Fills out all of your map, cannot be reversed]";
            ref bool thingToSend = ref CalNohitQoLWorld.MapReveal;
            TogglesUIManager.SpecialToggleOnClick toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;

            switch (pageNumber)
            {
                case 1:
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 3)
                        {
                            switch (i)
                            {
                                case 0: // Map
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.MapReveal;
                                    TogglesUIManager.DrawElementWithOneJob(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, toggleOnClickExtra);
                                    break;
                                case 1: // Spawns
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Spawns", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/SpawnsGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle Enemy Spawns";
                                    textToSendFormat = "[c/ffcc44:Toggles allowing enemies to spawn]";
                                    thingToSend = ref CalNohitQoLWorld.NoSpawns;
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                    TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                    break;
                                case 2: // Time Flow
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/time", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/timeGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle Time Flow";
                                    textToSendFormat = "[c/ffcc44:Pauses & resumes time flowing]";
                                    thingToSend = ref CalNohitQoLWorld.FrozenTime;
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                    TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                    break;
                            }
                            
                        }
                        else
                        {
                            switch (i)
                            {
                                case 3: // Rain On
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/time", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/timeGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle time";
                                    textToSendFormat = "[c/ffcc44:Swaps between night and day]";
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.ToggleTime;
                                    break;
                                case 4: // Rain off
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/water", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/waterGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle rain";
                                    textToSendFormat = "[c/ffcc44:Toggles a rainstorm]";
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.ToggleRain;
                                    break;
                            }
                            TogglesUIManager.DrawElementWithOneJob(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, toggleOnClickExtra);
                        }
                    }                   
                    break;
                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        
                            switch (i)
                            {
                                case 0: // Events
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Event", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/EventGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Disable Events";
                                    textToSendFormat = "[c/ffcc44:Cancels/disables any active events]";
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.DisableEvents;
                                    TogglesUIManager.DrawElementWithOneJob(spriteBatch,baseTexture,glowTexture,player,i,baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, toggleOnClickExtra);
                                    break;
                                case 1: // Biome Fountains
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/biome", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/biomeGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle Biome Fountains";
                                    textToSendFormat = "[c/ffcc44:Water fountains now force their biome post Queen Bee]";
                                    thingToSend = ref CalNohitQoL.Instance.BiomeFountainsForceBiome;
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                    TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                    break;
                                case 2: // World Difficulty
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/difficulty", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/difficultyGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle World Difficulty";
                                    string difficulty = "";
                                    string color = "";
                                    switch (Main.GameMode)
                                    {
                                        case 0:
                                            difficulty = "Expert";
                                            color = "[c/af4bff:";
                                            break;
                                        case 1:
                                            difficulty = "Master";
                                            color = "[c/FF4444:";
                                            break;
                                        case 2:
                                            difficulty = "Journey";
                                            color = "[c/ffff66:";
                                            break;
                                        default:
                                            difficulty = "Normal";
                                            color = "[c/ffffff:";
                                            break;
                                    }
                                    textToSendFormat = $"[c/ffcc44:Set the world difficulty to] {color}{difficulty}]";
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.ToggleWorldDifficulty;
                                    TogglesUIManager.DrawElementWithOneJob(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, toggleOnClickExtra);
                                    break;
                                case 3:
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Skull", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/SkullGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle Player Difficulty";
                                    difficulty = "";
                                    color = "";
                                    switch (player.difficulty)
                                    {
                                        case 0:
                                            difficulty = "Mediumcore";
                                            color = "[c/af4bff:";
                                            break;
                                        case 1:
                                            difficulty = "Hardcore";
                                            color = "[c/FF4444:";
                                            break;
                                        default:
                                            difficulty = "Classic";
                                            color = "[c/ffffff:";
                                            break;
                                    }
                                    textToSendFormat = $"[c/ffcc44:Set the player difficulty to] {color}{difficulty}]";
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.TogglePlayerDificulty;
                                    TogglesUIManager.DrawElementWithOneJob(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, toggleOnClickExtra);
                                    break;
                                case 4:
                                    baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/teleport", (AssetRequestMode)2).Value;
                                    glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/teleportGlow", (AssetRequestMode)2).Value;
                                    textToSend = "Toggle Map Teleporting";
                                    textToSendFormat = "[c/ffcc44:Right click the fullscreen map to teleport]";
                                    thingToSend = ref CalNohitQoLWorld.MapTeleport;
                                    toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                    TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                    break;
                            }
                            
                        
                    }                                                                                   
                    break;                  
            }
        }
    }
}
