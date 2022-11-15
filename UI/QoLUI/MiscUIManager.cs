using CalNohitQoL.ModPlayers;
using CalNohitQoL.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.UI.QoLUI
{
    public class MiscUIManager
    {
        internal static bool IsDrawing;
        internal static int PageNumber = 1;
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
                        if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.ClickCooldownTimer == 0)
                        {
                            // ON CLICK AFFECT
                            TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
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
                        if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.ClickCooldownTimer == 0)
                        {
                            // ON CLICK AFFECT
                            TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
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
            ref bool thingToSend = ref MapSystem.MapReveal;
            TogglesUIManager.SpecialToggleOnClick toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
            switch (pageNumber)
            {
                case 1:
                    // Go down in intervals of around 120
                    for (int i = 0; i < 5; i++)
                    {
                        
                        switch (i)
                        {
                            case 0: // Gravestones
                                baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/gravestone", (AssetRequestMode)2).Value;
                                glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/gravestoneGlow", (AssetRequestMode)2).Value;
                                textToSend = "Toggle Gravestones";
                                textToSendFormat = "[c/ffcc44:Enable Gravestones dropping]";
                                thingToSend = ref Toggles.GravestonesEnabled;
                                toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                break;
                            case 1: // Light Hack      
                                // this isnt going to use this due to complexity.
                                break;
                            case 2: // Shrooms Extra Damage
                                baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/shroom", (AssetRequestMode)2).Value;
                                glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/shroomGlow", (AssetRequestMode)2).Value;
                                textToSend = "Toggle Shrooms Damage";
                                textToSendFormat = "[c/ffcc44:Toggles the bonus damage given by the Odd Mushroom family]";
                                thingToSend = ref Toggles.ShroomsExtraDamage;
                                toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                break;
                            case 3: // Shrooms fixed projectiles
                                baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/shroom", (AssetRequestMode)2).Value;
                                glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/shroomGlow", (AssetRequestMode)2).Value;
                                textToSend = "Toggle Shrooms Fix";
                                textToSendFormat = "[c/ffcc44:Toggles making certain broken projectiles visible when]\n[c/ffcc44:using any shroom item]";
                                thingToSend = ref Toggles.ShroomsInvisProjectilesVisible;
                                toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                break;
                            case 4: // Draedon Weapons Charge
                                baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/charge", (AssetRequestMode)2).Value;
                                glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/chargeGlow", (AssetRequestMode)2).Value;
                                textToSend = "Toggle Arsenal Recharge";
                                textToSendFormat = "[c/ffcc44:Toggles fully charging Arsenal Weapons on respawn]";
                                thingToSend = ref Toggles.AutoChargeDraedonWeapons;
                                toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                break;
                        }
                            
                    }
                    #region Light Hack
                    Texture2D autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/lightHack", (AssetRequestMode)2).Value;
                    Texture2D statusTexture = Toggles.LightHack>0 ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
                    Texture2D statusTextureGlow = Toggles.LightHack > 0 ? ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
                    // Position of the Icon
                    Vector2 backgroundDrawCenter2;
                    backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
                    backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset+baseVerticalInterval) / 2;
                    Vector2 drawPos2 = backgroundDrawCenter2;

                    Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset+baseVerticalInterval) / 2);
                    // Rectangle area of the icon and mouse to check for hovering.
                    Rectangle IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, Utils.Size(fancyTexture));
                    //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
                    Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
                    bool isHovering = mouseHitbox.Intersects(IconRectangeArea2);
                    string text1 = Toggles.LightHack > 0 ? "Set Light Hack to " + (Toggles.LightHack * 100f).ToString() + "%" : "Turn Light Hack off";
                    if (isHovering)
                    {

                        spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                        Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, Utils.Size(autoTexture));

                        if (mouseHitbox.Intersects(hoverArea))
                        {
                            Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/lightHackGlow", (AssetRequestMode)2).Value;
                            spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
                            
                            Main.hoverItemName = $"{text1}";
                        }
                        Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
                        if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.ClickCooldownTimer == 0)
                        {
                            // ON CLICK AFFECT
                            TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                            SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                            string text = "";
                            if (Toggles.LightHack == 0f)
                            {

                                Toggles.LightHack = 0.25f;
                                text = "Set to 25%";
                            }
                            else if (Toggles.LightHack == 0.25f)
                            {
                                Toggles.LightHack = 0.5f;
                                text = "Set to 50%";
                            }
                            else if (Toggles.LightHack == 0.5f)
                            {
                                Toggles.LightHack = 0.75f;
                                text = "Set to 75%";
                            }
                            else if (Toggles.LightHack == 0.75f)
                            {
                                Toggles.LightHack = 1f;
                                text = "Set to 100%";
                            }
                            else
                            {
                                Toggles.LightHack = 0f;
                                text = "Turned Off";
                            }
                            GenericUpdatesModPlayer.UIUpdateTextTimer = 120;
                            TogglesUIManager.TextToShow = text;
                            TogglesUIManager.ColorToUse = Color.LightSkyBlue;
                        }
                    }



                    spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);


                    if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
                    {
                        spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
                        Main.hoverItemName = $"[c/ffcc44:{text1}]\n" + (Toggles.LightHack > 0 ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
                    }
                    spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

                    string textToShow2 = "Toggle Light Hack";
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
                    #endregion                                                       
                    break;
                case 2:
                    for (int i = 0; i < 5; i++)
                    {

                        switch (i)
                        {
                            case 0: // MNL Indicator.
                                baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/time", (AssetRequestMode)2).Value;
                                glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/timeGlow", (AssetRequestMode)2).Value;
                                textToSend = "Toggle MNL Indicator";
                                textToSendFormat = "[c/ffcc44:Shows a chat message informing you how close you]\n[c/ffcc44:were to a bosses MNL according to nohit rules]";
                                thingToSend = ref Toggles.MNLIndicator;
                                toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.None;
                                TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                break;
                            case 1: // Sass Mode.
                                baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/sass", (AssetRequestMode)2).Value;
                                glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/sassGlow", (AssetRequestMode)2).Value;
                                textToSend = "Toggle Sass Mode";
                                textToSendFormat = "[c/ffcc44:Shows a chat message when you die that]\n[c/ffcc44:insults you.]";
                                thingToSend = ref Toggles.SassMode;
                                toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.SassMode;
                                TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                break;
                            case 2: // DPS
                                baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/dps", (AssetRequestMode)2).Value;
                                glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/dpsGlow", (AssetRequestMode)2).Value;
                                textToSend = "Toggle DPS stats";
                                textToSendFormat = "[c/ffcc44:Shows a chat message that tells you the average dps you had]\n[c/ffcc44:on a boss.]";
                                thingToSend = ref Toggles.BossDPS;
                                toggleOnClickExtra = TogglesUIManager.SpecialToggleOnClick.BossDPS;
                                TogglesUIManager.DrawElementWithBasicToggle(spriteBatch, baseTexture, glowTexture, player, i, baseVerticalOffset, baseVerticalInterval, textToSend, textToSendFormat, ref thingToSend, toggleOnClickExtra);
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
