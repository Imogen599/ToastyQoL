using CalNohitQoL.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.UI.UIManagers
{
    public class PowersUIManager : BaseTogglesUIManager
    {
        public const string UIName = "PowersManager";

        public override string Name => UIName;

        public override void Initialize()
        {
            UIElements = new()
            {
                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Godmode", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/GodmodeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Godmode",
                () => "Prevents you from taking damage",
                1f,
                () => { Toggles.GodmodeEnabled = !Toggles.GodmodeEnabled; },
                typeof(Toggles).GetField("GodmodeEnabled", CalNohitQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/InstantDeath", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/InstantDeathGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Instant Death",
                () => "Makes you die upon taking damage",
                2f,
                () => { Toggles.InstantDeath = !Toggles.InstantDeath; },
                typeof(Toggles).GetField("InstantDeath", CalNohitQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Wings", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/WingsGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Flight",
                () => "You never run out of flight time",
                3f,
                () => { Toggles.InfiniteFlightTime = !Toggles.InfiniteFlightTime; },
                typeof(Toggles).GetField("InfiniteFlightTime", CalNohitQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Potion", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/PotionGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Potions",
                () => "Potion durations are infinite",
                4f,
                () => { Toggles.InfinitePotions = !Toggles.InfinitePotions; },
                typeof(Toggles).GetField("InfinitePotions", CalNohitQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Ammo", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/AmmoGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Ammo",
                () => "Ammo is not consumed",
                5f,
                () => { Toggles.InfiniteAmmo = !Toggles.InfiniteAmmo; },
                typeof(Toggles).GetField("InfiniteAmmo", CalNohitQoLUtils.UniversalBindingFlags)
                ),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cons", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/ConsGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Infinite Cons.",
                () => "Consumables are not consumed",
                6f,
                () => { Toggles.InfiniteConsumables = !Toggles.InfiniteConsumables; },
                typeof(Toggles).GetField("InfiniteConsumables", CalNohitQoLUtils.UniversalBindingFlags)
                )
            };
        }

        //internal static int PageNumber = 1;

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    if (!ShouldDraw)
        //        return;
        //    Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/baseSettingsUIBackground", (AssetRequestMode)2).Value;
        //    Player player = Main.LocalPlayer;
        //    Vector2 drawCenter;
        //    drawCenter.X = Main.screenWidth / 2;
        //    drawCenter.Y = Main.screenHeight / 2;
        //    Vector2 spawnPos = drawCenter + new Vector2(300, 0);

        //    spriteBatch.Draw(backgroundTexture, spawnPos, null, Color.White, 0, backgroundTexture.Size() * 0.5f, 1f, 0, 0);
        //    Rectangle hoverArea = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size());
        //    Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //    bool isHovering = mouseHitbox.Intersects(hoverArea);
        //    if (isHovering)
        //    {
        //        Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //    }
        //    DrawElements(spriteBatch);
        //}
        //public void DrawElements(SpriteBatch spriteBatch)
        //{
        //    float baseVerticalOffset;
        //    baseVerticalOffset = -308;
        //    Texture2D fancyTexture;
        //    fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/whiteTangle", (AssetRequestMode)2).Value;
        //    Texture2D fancyTextureSmall;
        //    fancyTextureSmall = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/SmallerWhiteRect", (AssetRequestMode)2).Value;
        //    Player player;
        //    player = Main.LocalPlayer;
        //    #region Page Numbers
        //    switch (PageNumber)
        //    {
        //        // First Page
        //        case 1:
        //            Texture2D arrowTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Arrow", (AssetRequestMode)2).Value;
        //            Texture2D arrowGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/ArrowGlow", (AssetRequestMode)2).Value;
        //            Vector2 backgroundDrawCenter2;
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
        //            Vector2 drawPos2 = backgroundDrawCenter2;
        //            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

        //            drawPos2.X += 130;
        //            drawPos2.Y -= 2f;
        //            Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 762) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
        //            Rectangle whiteHitbox = Utils.CenteredRectangle(whiteDrawPos, fancyTextureSmall.Size());

        //            if (mouseHitbox.Intersects(whiteHitbox))
        //            {
        //                spriteBatch.Draw(fancyTextureSmall, whiteDrawPos, null, Color.White * 0.3f, 0, fancyTextureSmall.Size() * 0.5f, 1f, 0, 0);
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    PageNumber = 2;
        //                }
        //            }
        //            Rectangle arrowRect = Utils.CenteredRectangle(whiteDrawPos, arrowTexture.Size());
        //            float scale = 1;
        //            if (mouseHitbox.Intersects(arrowRect))
        //            {
        //                scale = 1.15f;
        //            }
        //            spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.White, 0, arrowTexture.Size() * 0.5f, scale, 0, 0);
        //            if (mouseHitbox.Intersects(arrowRect))
        //            {
        //                spriteBatch.Draw(arrowGlowTexture, whiteDrawPos, null, Color.White, 0, arrowGlowTexture.Size() * 0.5f, scale, 0, 0);

        //            }
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
        //            drawPos2 = backgroundDrawCenter2;
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

        //            drawPos2.X += 130;
        //            drawPos2.Y -= 2f;
        //            whiteDrawPos = new Vector2((Main.screenWidth + 436) / 2, (Main.screenHeight + baseVerticalOffset) / 2);

        //            spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.Black * 0.8f, 0, arrowTexture.Size() * 0.5f, 1f, SpriteEffects.FlipHorizontally, 0);
        //            DrawPage(PageNumber, spriteBatch);
        //            break;
        //        case 2:
        //            arrowTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Arrow", (AssetRequestMode)2).Value;
        //            arrowGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/ArrowGlow", (AssetRequestMode)2).Value;
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
        //            drawPos2 = backgroundDrawCenter2;
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

        //            drawPos2.X += 130;
        //            drawPos2.Y -= 2f;
        //            whiteDrawPos = new Vector2((Main.screenWidth + 436) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
        //            whiteHitbox = Utils.CenteredRectangle(whiteDrawPos, fancyTextureSmall.Size());

        //            if (mouseHitbox.Intersects(whiteHitbox))
        //            {
        //                spriteBatch.Draw(fancyTextureSmall, whiteDrawPos, null, Color.White * 0.3f, 0, fancyTextureSmall.Size() * 0.5f, 1f, 0, 0);
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    PageNumber = 1;
        //                }
        //            }
        //            arrowRect = Utils.CenteredRectangle(whiteDrawPos, arrowTexture.Size());
        //            scale = 1;
        //            if (mouseHitbox.Intersects(arrowRect))
        //            {
        //                scale = 1.15f;
        //            }
        //            spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.White, 0, arrowTexture.Size() * 0.5f, scale, SpriteEffects.FlipHorizontally, 0);
        //            if (mouseHitbox.Intersects(arrowRect))
        //            {
        //                spriteBatch.Draw(arrowGlowTexture, whiteDrawPos, null, Color.White, 0, arrowGlowTexture.Size() * 0.5f, scale, SpriteEffects.FlipHorizontally, 0);
        //            }

        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
        //            drawPos2 = backgroundDrawCenter2;
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);

        //            drawPos2.X += 130;
        //            drawPos2.Y -= 2f;
        //            whiteDrawPos = new Vector2((Main.screenWidth + 762) / 2, (Main.screenHeight + baseVerticalOffset) / 2);

        //            spriteBatch.Draw(arrowTexture, whiteDrawPos, null, Color.Black * 0.8f, 0, arrowTexture.Size() * 0.5f, 1f, 0, 0);
        //            DrawPage(PageNumber, spriteBatch);
        //            break;
        //    }
        //    #endregion
        //}
        //private void DrawPage(int pageNumber, SpriteBatch spriteBatch)
        //{
        //    float baseVerticalOffset = -210;
        //    float baseVerticalInterval = 120;
        //    Texture2D fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/whiteTangle", (AssetRequestMode)2).Value;

        //    Player player = Main.LocalPlayer;

        //    switch (pageNumber)
        //    {
        //        case 1:
        //            // Go down in intervals of around 120
        //            #region Godmode

        //            Texture2D autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Godmode", (AssetRequestMode)2).Value;
        //            Texture2D statusTexture = Toggles.GodmodeEnabled ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
        //            Texture2D statusTextureGlow = Toggles.GodmodeEnabled ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
        //            // Position of the Icon
        //            Vector2 backgroundDrawCenter2;
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
        //            Vector2 drawPos2 = backgroundDrawCenter2;

        //            Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
        //            // Rectangle area of the icon and mouse to check for hovering.
        //            Rectangle IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
        //            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
        //            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //            bool isHovering = mouseHitbox.Intersects(IconRectangeArea2);

        //            if (isHovering)
        //            {

        //                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

        //                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

        //                if (mouseHitbox.Intersects(hoverArea))
        //                {
        //                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/GodmodeGlow", (AssetRequestMode)2).Value;
        //                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);

        //                    Main.hoverItemName = "[c/ffcc44:Prevents you from taking damage]";
        //                }
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    Toggles.GodmodeEnabled = !Toggles.GodmodeEnabled;
        //                }
        //            }



        //            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);


        //            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
        //            {
        //                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
        //                Main.hoverItemName = "[c/ffcc44:Prevents you from taking damage]\n" + (Toggles.GodmodeEnabled ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
        //            }
        //            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

        //            string textToShow2 = "Toggle Godmode";
        //            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        //            #endregion
        //            #region Instant Death
        //            autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/InstantDeath", (AssetRequestMode)2).Value;
        //            statusTexture = Toggles.InstantDeath ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
        //            statusTextureGlow = Toggles.InstantDeath ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
        //            // Position of the Icon
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval) / 2;
        //            drawPos2 = backgroundDrawCenter2;

        //            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval) / 2);
        //            // Rectangle area of the icon and mouse to check for hovering.
        //            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
        //            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
        //            if (isHovering)
        //            {

        //                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

        //                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

        //                if (mouseHitbox.Intersects(hoverArea))
        //                {
        //                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/InstantDeathGlow", (AssetRequestMode)2).Value;
        //                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
        //                    Main.hoverItemName = "[c/ffcc44:Makes you die upon taking damage]";
        //                }
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    Toggles.InstantDeath = !Toggles.InstantDeath;
        //                }
        //            }

        //            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);

        //            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
        //            {
        //                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
        //                Main.hoverItemName = "[c/ffcc44:Makes you die upon taking damage]\n" + (Toggles.InstantDeath ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
        //            }
        //            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

        //            textToShow2 = "Toggle Instant Death";
        //            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        //            #endregion
        //            #region Infinite Flight
        //            autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Wings", (AssetRequestMode)2).Value;
        //            statusTexture = Toggles.InfiniteFlightTime ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
        //            statusTextureGlow = Toggles.InfiniteFlightTime ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
        //            // Position of the Icon
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 2) / 2;
        //            drawPos2 = backgroundDrawCenter2;

        //            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 2) / 2);
        //            // Rectangle area of the icon and mouse to check for hovering.
        //            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
        //            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
        //            if (isHovering)
        //            {

        //                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

        //                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

        //                if (mouseHitbox.Intersects(hoverArea))
        //                {
        //                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/WingsGlow", (AssetRequestMode)2).Value;
        //                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
        //                    Main.hoverItemName = "[c/ffcc44:You never run out of flight time]";
        //                }
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    Toggles.InfiniteFlightTime = !Toggles.InfiniteFlightTime;
        //                }
        //            }

        //            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);

        //            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
        //            {
        //                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
        //                Main.hoverItemName = "[c/ffcc44:You never run out of flight time]\n" + (Toggles.InfiniteFlightTime ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
        //            }
        //            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

        //            textToShow2 = "Toggle Infinite Flight";
        //            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        //            #endregion
        //            #region Infinite Mana
        //            autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Mana", (AssetRequestMode)2).Value;
        //            statusTexture = Toggles.InfiniteMana ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
        //            statusTextureGlow = Toggles.InfiniteMana ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
        //            // Position of the Icon
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 3) / 2;
        //            drawPos2 = backgroundDrawCenter2;

        //            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 3) / 2);
        //            // Rectangle area of the icon and mouse to check for hovering.
        //            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
        //            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
        //            if (isHovering)
        //            {

        //                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

        //                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

        //                if (mouseHitbox.Intersects(hoverArea))
        //                {
        //                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/ManaGlow", (AssetRequestMode)2).Value;
        //                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
        //                    Main.hoverItemName = "[c/ffcc44:You never run out of Mana]";
        //                }
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    Toggles.InfiniteMana = !Toggles.InfiniteMana;
        //                }
        //            }

        //            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);

        //            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
        //            {
        //                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
        //                Main.hoverItemName = "[c/ffcc44:You never run out of Mana]\n" + (Toggles.InfiniteMana ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
        //            }
        //            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

        //            textToShow2 = "Toggle Infinite Mana";
        //            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        //            #endregion
        //            #region Infinite Potions
        //            autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Potion", (AssetRequestMode)2).Value;
        //            statusTexture = Toggles.InfinitePotions ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
        //            statusTextureGlow = Toggles.InfinitePotions ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
        //            // Position of the Icon
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 4) / 2;
        //            drawPos2 = backgroundDrawCenter2;

        //            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval * 4) / 2);
        //            // Rectangle area of the icon and mouse to check for hovering.
        //            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
        //            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
        //            if (isHovering)
        //            {

        //                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

        //                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

        //                if (mouseHitbox.Intersects(hoverArea))
        //                {
        //                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/PotionGlow", (AssetRequestMode)2).Value;
        //                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
        //                    Main.hoverItemName = "[c/ffcc44:Potion durations are infinite]";
        //                }
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    Toggles.InfinitePotions = !Toggles.InfinitePotions;
        //                }
        //            }

        //            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);

        //            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
        //            {
        //                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
        //                Main.hoverItemName = "[c/ffcc44:Potion durations are infinite]\n" + (Toggles.InfinitePotions ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
        //            }
        //            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

        //            textToShow2 = "Toggle Infinite Potions";
        //            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        //            #endregion
        //            break;
        //        case 2:
        //            #region Infinite Ammo
        //            autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Ammo", (AssetRequestMode)2).Value;
        //            statusTexture = Toggles.InfiniteAmmo ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
        //            statusTextureGlow = Toggles.InfiniteAmmo ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
        //            // Position of the Icon
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
        //            drawPos2 = backgroundDrawCenter2;

        //            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
        //            // Rectangle area of the icon and mouse to check for hovering.
        //            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
        //            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
        //            if (isHovering)
        //            {

        //                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

        //                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

        //                if (mouseHitbox.Intersects(hoverArea))
        //                {
        //                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/AmmoGlow", (AssetRequestMode)2).Value;
        //                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
        //                    Main.hoverItemName = "[c/ffcc44:Ammo is not consumed]";
        //                }
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    Toggles.InfiniteAmmo = !Toggles.InfiniteAmmo;
        //                }
        //            }

        //            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);

        //            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
        //            {
        //                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
        //                Main.hoverItemName = "[c/ffcc44:Ammo is not consumed]\n" + (Toggles.InfiniteAmmo ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
        //            }
        //            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);


        //            textToShow2 = "Toggle Infinite Ammo";
        //            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        //            #endregion
        //            #region Infinite Consumables
        //            autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cons", (AssetRequestMode)2).Value;
        //            statusTexture = Toggles.InfiniteConsumables ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
        //            statusTextureGlow = Toggles.InfiniteConsumables ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
        //            // Position of the Icon
        //            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
        //            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseVerticalInterval) / 2;
        //            drawPos2 = backgroundDrawCenter2;

        //            whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseVerticalInterval) / 2);
        //            // Rectangle area of the icon and mouse to check for hovering.
        //            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
        //            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
        //            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
        //            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
        //            if (isHovering)
        //            {

        //                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

        //                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

        //                if (mouseHitbox.Intersects(hoverArea))
        //                {
        //                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/ConsGlow", (AssetRequestMode)2).Value;
        //                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
        //                    Main.hoverItemName = "[c/ffcc44:Consumables are not consumed]";
        //                }
        //                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
        //                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
        //                {
        //                    // ON CLICK AFFECT
        //                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
        //                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
        //                    Toggles.InfiniteConsumables = !Toggles.InfiniteConsumables;
        //                }
        //            }

        //            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);

        //            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
        //            {
        //                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
        //                Main.hoverItemName = "[c/ffcc44:Consumables are not consumed]\n" + (Toggles.InfiniteConsumables ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
        //            }
        //            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);

        //            textToShow2 = "Toggle Infinite Cons.";
        //            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        //            #endregion
        //            break;
        //    }
        //}
    }
}