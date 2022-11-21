using CalamityMod;
using CalNohitQoL.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalNohitQoL.UI.QoLUI
{
    
    public class BossTogglesUIManager
    {
        public static bool IsDrawing;
        private bool ShouldDraw
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
        private float BossIconY;
        private Vector2 ScrollbarOffset = new Vector2(115, 15);
        private bool dragging;
        private Vector2 mouseBasePos = default;
        private static Vector2 PreHardmodeXOffset = new(-70,0);
        private static Vector2 PostMoonlordXOffset = new(70, 0);

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!ShouldDraw)
                return;
            // Draw the background.
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/baseSettingsUIBackgroundWide", (AssetRequestMode)2).Value;
            Vector2 drawCenter;
            drawCenter.X = Main.screenWidth / 2;
            drawCenter.Y = Main.screenHeight / 2;
            // This spawn pos is very important. As it is affected by Main.screenWidth/Height, it will scale properly. Every single thing you draw needs to use
            // this vector, unless they are a completely new one and use Main.screenWidth.Height themselves for the VERY BASE of their definition.
            Vector2 spawnPos = drawCenter + new Vector2(300, 0);

            spriteBatch.Draw(backgroundTexture, spawnPos, null, Color.White, 0, backgroundTexture.Size() * 0.5f, 1f, 0, 0);

            // Block the mouse if we are hovering over it.
            Rectangle hoverArea = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size());
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(hoverArea);
            if (isHovering)
            {
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
            }
            
            // Go to the next things to draw, passing in spawnPos for convienience.
            DrawElements(spriteBatch, spawnPos);
        }
        public void DrawElements(SpriteBatch spriteBatch, Vector2 spawnPos)
        {
            #region KillAllBosses
            // ALL THE TEXTURES
            Texture2D crossTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D crossGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            Texture2D tickTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Tick", (AssetRequestMode)2).Value;
            Texture2D tickGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value;
            Texture2D whiteGlow = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/whiteTangle", (AssetRequestMode)2).Value;
            Texture2D whiteGlowSmall = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/SmallerWhiteRect", (AssetRequestMode)2).Value;
            // Get the mouse hitbox
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);

            // Scrollable icons

            // The method I use here to make things disapear when scrolling so they dont "break out" of their container is.. scuffed? honestly i dont know how else
            // you would do this, and it works very well. The way i wrote it is probably not that great, but its fully functional so uh, ye.
            // The below sets up the two zones after drawing the boss icons, so that they draw over the icons, allowing the icons to stop drawing when fully under.
            Texture2D deleteIconTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/thingForScrolling", (AssetRequestMode)2).Value;
            Vector2 deleteIconCenter = spawnPos + new Vector2(0, -144);
            Vector2 deleteIconCenter2 = spawnPos + new Vector2(0, 159);

            // We want to draw these first so they go under the kill zones.
            DrawBossIcons(spriteBatch, spawnPos, deleteIconCenter, deleteIconCenter2);

            // Draw the kill zones. This is for detecting if it scrolls, if the center of it is above or below these, it will stop drawing to keep it in bounds.
            
            spriteBatch.Draw(deleteIconTexture, deleteIconCenter, null, Color.White, 0, deleteIconTexture.Size() * 0.5f, 1f, 0, 0);
            spriteBatch.Draw(deleteIconTexture, deleteIconCenter2, null, Color.White, 0, deleteIconTexture.Size() * 0.5f, 1f, 0, 0);

            // Mark all as alive button stuff.
            Vector2 tickPos = new Vector2(-80, -154);
            Rectangle tickHoverRect = Utils.CenteredRectangle(spawnPos + tickPos, whiteGlowSmall.Size());
            if(mouseHitbox.Intersects(tickHoverRect))
            {
                spriteBatch.Draw(whiteGlowSmall, spawnPos + tickPos, null, Color.White * 0.3f, 0, whiteGlowSmall.Size()*0.5f, 1f, 0, 0);
                Rectangle tickGlowRect = Utils.CenteredRectangle(spawnPos + tickPos, tickTexture.Size());
                if (mouseHitbox.Intersects(tickGlowRect))
                {
                    spriteBatch.Draw(tickGlowTexture, spawnPos + tickPos, null, Color.White, 0, tickGlowTexture.Size() * 0.5f, 1f, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Mark every boss as alive]";
                    if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.ClickCooldownTimer == 0)
                    {
                        // On click stuff
                        MarkAllBossesAsX(false);
                        TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                        SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                    }
                }
            }
            spriteBatch.Draw(tickTexture, spawnPos + tickPos, null, Color.White, 0,tickTexture.Size() * 0.5f, 1f,0,0);
            // Mark all as dead button.
            Vector2 crossPos = new Vector2(60, -154);
            Rectangle crossHoverRect = Utils.CenteredRectangle(spawnPos + crossPos, whiteGlowSmall.Size());
            if (mouseHitbox.Intersects(crossHoverRect))
            {
                spriteBatch.Draw(whiteGlowSmall, spawnPos + crossPos, null, Color.White * 0.3f, 0, whiteGlowSmall.Size() * 0.5f, 1f, 0, 0);
                Rectangle crossGlowRect = Utils.CenteredRectangle(spawnPos + crossPos, tickTexture.Size());
                if (mouseHitbox.Intersects(crossGlowRect))
                {
                    spriteBatch.Draw(crossGlowTexture, spawnPos + crossPos, null, Color.White, 0, crossGlowTexture.Size() * 0.5f, 1f, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Mark every boss as dead]";
                    if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.ClickCooldownTimer == 0)
                    {
                        MarkAllBossesAsX(true);
                        TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                        SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                    }
                }
            }
            spriteBatch.Draw(crossTexture, spawnPos + crossPos, null, Color.White, 0, crossTexture.Size() * 0.5f, 1f, 0, 0);
            #endregion
            #region Scrollbar
            // This is the forbidden scroll bar. This was made from literal scratch by me and honestly im just happy it works. I will do my best to describe it
            // so i dont come back to use it in the future and get completely lost. If you want to improve this further, make it snap to the mousePos.Y when you first
            // click, i couldnt get it to work.
            // Set the textures, the offset to the spawn pos, and draw the background. Both the background and the bar have the spawn pos added in the draw itself, and
            // the change in Y is done directly to the offset. It works just fine.
            Texture2D scrollbarBackgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/scrollbarBackground", (AssetRequestMode)2).Value;
            Texture2D scrollbarTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/fullScrollbar", (AssetRequestMode)2).Value;
            Vector2 scrollbarBackgroundOffset = new Vector2(115, 8);
            spriteBatch.Draw(scrollbarBackgroundTexture, spawnPos + scrollbarBackgroundOffset, null, Color.White,0,scrollbarBackgroundTexture.Size()*0.5f,1,0,0);

            // Get the rectangle of the scroll bar.
            Rectangle scrollBarRect = Utils.CenteredRectangle(spawnPos + scrollbarBackgroundOffset, scrollbarTexture.Size());
            // I didnt want to pass this Rectangle through as a parameter so i re-get the background rectangle here. This is to allow for scrolling with the mouse wheel.
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/baseSettingsUIBackgroundWide", (AssetRequestMode)2).Value;
            Rectangle backRect = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size());
            if (mouseHitbox.Intersects(backRect))
            {
                // This means the mouse wheel has moved up.
                if (PlayerInput.MouseInfo.ScrollWheelValue - PlayerInput.MouseInfoOld.ScrollWheelValue > 0)
                {
                    // We increase the Y by a set amount, and then clamp it. You could probably do that in a single line but this is nice to read ig.
                    // The clamping keeps it all within the given bounds, 0 is the top, and -500 is the lowest point it scrolls to have em all show up.
                    BossIconY += 50;
                    BossIconY = BossIconY.Clamp(-500,0);
                    // This need it minused not added, I have no fucking idea why lmao.
                    ScrollbarOffset.Y -= 50 * 0.47f;
                    ScrollbarOffset.Y = ScrollbarOffset.Y.Clamp(8, 235);
                }
                // This means the wheel moved down. The same thing happens inside here as above, just in the other direction.
                else if(PlayerInput.MouseInfo.ScrollWheelValue - PlayerInput.MouseInfoOld.ScrollWheelValue < 0)
                {
                    BossIconY -= 50;
                    BossIconY = BossIconY.Clamp(-500, 0);
                    ScrollbarOffset.Y += 50 * 0.47f;
                    ScrollbarOffset.Y = ScrollbarOffset.Y.Clamp(8, 235);
                }
            }
            // This is where the fun begins. I need to go over this because i don't actually understand it.
            // if we are hovering over the thing, or already dragging.
            if (mouseHitbox.Intersects(scrollBarRect)||(dragging))
            {
                // 285 - size of the scrollbar area where it can scroll.

                // These two statements go back and forth every frame. This is how it updates while you drag it as opposed to only
                // when you let go.

                // If we are holding left mouse
                if (Main.mouseLeft)
                {
                    // This is default by, well default. This runs on the first pass.
                    if (mouseBasePos == default)
                    {
                        // We set the base mouse point to the current mouse location.
                        mouseBasePos = Main.MouseScreen;
                        // Another check before setting dragging to true, i dont think this is needed
                        //if (mouseHitbox.Intersects(scrollBarRect))
                            dragging = true;
                    }
                    // Next frame, this will run due to mouseBasePos being the last frame mouse location instead of default.
                    else
                    {
                        // ~~This check is compeltely redundant, it will never be default else this wouldnt be running.~~
                        // This originally checked if it wasnt default. Now, it ensures the mouse has actually moved from the last location before
                        // running this, meaning it wont run if it doesnt need to. Minor optimization, but is good.
                        if (mouseBasePos != Main.MouseScreen)
                        {
                            // get the disatance the mouse moved by subtracting the current mouse location from the original position, and add it to the
                            // two scrolling positions.
                            float mouseDistanceY = mouseBasePos.Y - Main.MouseScreen.Y;
                            // Reset the base position.
                            mouseBasePos = default;
                            BossIconY += 4.5f * mouseDistanceY;
                            BossIconY = BossIconY.Clamp(-500, 0);
                            ScrollbarOffset.Y -= 4.5f * (mouseDistanceY * 0.455f);
                            ScrollbarOffset.Y = ScrollbarOffset.Y.Clamp(8, 235);
                        }
                    }
                    // As we are holding m1, set the texture to a lighter one.
                    scrollbarTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/fullScrollbarClicking", (AssetRequestMode)2).Value;
                }
                // If we have stopped holding left mouse.
                else
                {
                    // Riz wanted to be here for some reason.
                    // We are no longer dragging the panel.
                    //dragging = false;
                    // If this has a value, as it will ~50% of the time, we need to set it. Not doing this makes it behave all fucky when you try to scroll it
                    // again.
                    if (mouseBasePos != default)
                    {
                        // The same as above, just get the distance moved and update it.
                        Vector2 mouseDistance = mouseBasePos - Main.MouseScreen;
                        // Make sure to reset this.
                        mouseBasePos = default;
                        BossIconY += 4.5f*mouseDistance.Y;
                        BossIconY = BossIconY.Clamp(-500, 0);
                        dragging = false;
                        ScrollbarOffset.Y -= 4.5f * (mouseDistance.Y * 0.455f);
                        ScrollbarOffset.Y = ScrollbarOffset.Y.Clamp(8, 235);
                    }
                }
            }
            else
            {
                // You know the drill by now, the same as the one above.
                if (mouseBasePos != default)
                {
                    Vector2 mouseDistance = mouseBasePos - Main.MouseScreen;
                    mouseBasePos = default;
                    BossIconY += 4.5f*mouseDistance.Y;
                    BossIconY = BossIconY.Clamp(-500, 0);                  
                    ScrollbarOffset.Y -= 4.5f * (mouseDistance.Y * 0.455f);
                    ScrollbarOffset.Y = ScrollbarOffset.Y.Clamp(8, 235);
                }

            }
            // Actually draw the texture with all of the changes.
            spriteBatch.Draw(scrollbarTexture, spawnPos + ScrollbarOffset, null, Color.White, 0, scrollbarTexture.Size() * 0.5f, 1, 0, 0);
            #endregion
        }
        public void DrawBossIcons(SpriteBatch spriteBatch, Vector2 spawnPos, Vector2 killIfAbove, Vector2 killIfBelow)
        {
            // Get the spawn pos for the first boss icon. Make sure you use the passed spawnPos.
            Vector2 iconSpawnPos = spawnPos + new Vector2(-10, -90);
            // Set this, so we can reset the spawn pos to the top when we want to go to the next column.
            Vector2 iconBaseSpawnPos = iconSpawnPos;

            // Get all of the base textures.
            Texture2D deleteIconTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/thingForScrolling", (AssetRequestMode)2).Value;
            Texture2D baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/kingSlime", (AssetRequestMode)2).Value;
            Texture2D glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/kingSlimeGlow", (AssetRequestMode)2).Value;
            Texture2D crossTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D crossGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            Texture2D tickTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Tick", (AssetRequestMode)2).Value;
            Texture2D tickGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value;

            // THIS NEEDS TO EXIST:
            //
            // ref bool bossStatusToChange = ;
            //
            // THESE NEED TO BE CHANGED TO USE REF DOWNED BOOLS.
            Texture2D tickOrCross = tickTexture;
            Texture2D tickOrCrossGlow = tickGlowTexture;
            string status = "Alive";
            
            // Create the scroll offset with the field that we change in the scroll bar section.
            Vector2 scrollOffset = new(0, BossIconY);
            // This is the start of the slog. Ensure you add the correct, if any, horizontal offsets and MAKE SURE TO INCLUDE THE SCROLL OFFSET. That
            // is what makes it scroll.
            Vector2 finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            string bossName = "[c/ffcc44:Toggle King Slime's Death]";
            bool bossDeathValue;
            #region Pre HM
            // KS
            // Actually draw the boss icon. This is an embeded function? smth like that. Theyre cool but i dont see why you would use them in most cases,
            // I just want to have an example of one that i wrote myself.
            bossDeathValue = NPC.downedSlimeKing;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.KingSlime);
            
            // DS
            // Add 50 vertically each time to the spawn pos, this automatically makes them go underneath at the correct difference.
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/desertScourge", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/desertScourgeGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Desert Scourge's Death]";
            // bossStatusToChange = ref etcblahblahblah;
            bossDeathValue = DownedBossSystem.downedDesertScourge;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.DesertScourge ,true);

            // EoC
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/eoc", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/eocGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Eye of Cthulhu's Death]";
            bossDeathValue = NPC.downedBoss1;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.EyeOfCthulhu);
            
            // Crabulon
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/crabulon", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/crabulonGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Crabulon's Death]";
            bossDeathValue = DownedBossSystem.downedCrabulon;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Crabulon);

            // EoW
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/eow", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/eowGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Eater of Worlds' Death]";
            bossDeathValue = CalNohitQoL.DownedEater;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.EaterOfWorlds);

            // BoC
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/boc", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/bocGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Brain of Cthulhu's Death]";
            bossDeathValue = CalNohitQoL.DownedBrain;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue,Boss.BrainOfCthulhu);

            // Hive Mind
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/hiveMind", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/hiveMindGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Hive Mind's Death]";
            bossDeathValue = DownedBossSystem.downedHiveMind;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.HiveMind);

            // Perfs
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/perforators", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/perforatorsGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Perforator Hive's Death]";
            bossDeathValue = DownedBossSystem.downedPerforator;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Perforators);

            // QB
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/queenBee", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/queenBeeGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Queen Bee's Death]";
            bossDeathValue = NPC.downedQueenBee;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.QueenBee);

            // Deerclops
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/deerclops", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/deerclopsGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Deerclops' Death]";
            bossDeathValue = NPC.downedDeerclops;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Deerclops, true);

            // Skeletron
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/skeletron", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/skeletronGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Skeletron's Death]";
            bossDeathValue = NPC.downedBoss3;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Skeletron);

            // Slime God
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/slimeGod", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/slimeGodGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Slime God's Death]";
            bossDeathValue = DownedBossSystem.downedSlimeGod;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.SlimeGod);

            // WoF
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/wof", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/wofGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Wall of Flesh's Death]";
            bossDeathValue = Main.hardMode;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.WallOfFlesh);

            // EoL
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/empress", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/empressGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Empress of Light's Death]";
            bossDeathValue = NPC.downedEmpressOfLight;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.EmpressOfLight);

            // Cultist
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PreHardmodeXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/cultist", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/cultistGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Cultist's Death]";
            bossDeathValue = NPC.downedAncientCultist;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.LunaticCultist);
            #endregion
            #region HM
            // Queen Slime
            // Reset the spawn pos.
            iconSpawnPos = iconBaseSpawnPos;
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/queenSlime", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/queenSlimeGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Queen Slime's Death]";
            bossDeathValue = NPC.downedQueenSlime;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.QueenSlime);

            // Cryogen
            // Resume going down.
            iconSpawnPos += new Vector2(0,50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/cryogen", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/cryogenGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Cryogen's Death]";
            bossDeathValue = DownedBossSystem.downedCryogen;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Cryogen, true);

            // Twins
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/twins", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/twinsGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle The Twin's Death]";
            bossDeathValue = NPC.downedMechBoss2;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.TheTwins);

            // Brimmy
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/brimmy", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/brimmyGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Brimstone Elemental's Death]";
            bossDeathValue = DownedBossSystem.downedBrimstoneElemental;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.BrimstoneElemental, true);

            // Destroyer
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/destroyer", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/destroyerGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle The Destroyer's Death]";
            bossDeathValue = NPC.downedMechBoss1;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.TheDestroyer);

            // Aquatic Scourge
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/aquaticScourge", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/aquaticScourgeGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Aquatic Scourge's Death]";
            bossDeathValue = DownedBossSystem.downedAquaticScourge;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.TheAquaticScourge, true);

            // Prime
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/prime", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/primeGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Skeletron Prime's Death]";
            bossDeathValue = NPC.downedMechBoss3;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue,Boss.SkeletronPrime);

            // Calamitas
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/clone", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/cloneGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Calamitas' Death]";
            bossDeathValue = DownedBossSystem.downedCalamitas;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Cloneamitas, true);

            // Plantera
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/plantera", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/planteraGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Plantera's Death]";
            bossDeathValue = NPC.downedPlantBoss;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Plantera);

            // Levi
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/levi", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/leviGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Leviathan's Death]";
            bossDeathValue = DownedBossSystem.downedLeviathan;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Leviathan);

            // Aureus
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/aureus", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/aureusGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Astrum Aureus' Death]";
            bossDeathValue = DownedBossSystem.downedAstrumAureus;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.AstrumAureus);

            // Golem
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/golem", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/golemGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Golem's Death]";
            bossDeathValue = NPC.downedGolemBoss;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Golem);

            // PBG
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/pbg", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/pbgGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Plaguebringer Goliath's Death]";
            bossDeathValue = DownedBossSystem.downedPlaguebringer;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.PlaguebringerGoliath);        

            // Fishron
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/fishron", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/fishronGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Duke Fishron's Death]";
            bossDeathValue = NPC.downedFishron;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.DukeFishron);

            // Deus
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/deus", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/deusGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Astrum Deus' Death]";
            bossDeathValue = DownedBossSystem.downedAstrumDeus;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.AstrumDeus, true);
            #endregion
            #region PML
            // Guardians
            // Reset the spawn pos.
            iconSpawnPos = iconBaseSpawnPos;
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/guardians", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/guardiansGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Profaned Guardian's Death]";
            bossDeathValue = DownedBossSystem.downedGuardians;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.ProfanedGuardians);

            // Folly
            // Resume heading down.
            iconSpawnPos += new Vector2(0,50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/folly", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/follyGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Dragonfolly's Death]";
            bossDeathValue = DownedBossSystem.downedDragonfolly;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Dragonfolly);

            // Provi
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/provi", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/proviGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Providence's Death]";
            bossDeathValue = DownedBossSystem.downedProvidence;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Providence, true);

            // CV
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/void", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/voidGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Ceaseless Void's Death]";
            bossDeathValue = DownedBossSystem.downedCeaselessVoid;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.CeaselessVoid, true);

            // Weaver
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/weaver", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/weaverGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Storm Weaver's Death]";
            bossDeathValue = DownedBossSystem.downedStormWeaver;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.StormWeaver, true);

            // Signus
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/signus", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/signusGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Signus' Death]";
            bossDeathValue = DownedBossSystem.downedSignus;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Signus, true);

            // Polter
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/polter", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/polterGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Polterghast's Death]";
            bossDeathValue = DownedBossSystem.downedPolterghast;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Polterghast);

            // Old Duke
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/oldDuke", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/oldDukeGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Old Duke's Death]";
            bossDeathValue = DownedBossSystem.downedBoomerDuke;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.OldDuke);

            // DoG
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/dog", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/dogGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Devourer of Gods' Death]";
            bossDeathValue = DownedBossSystem.downedDoG;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.DevourerOfGods);

            // Yharon
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/yharon", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/yharonGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Yharon's Death]";
            bossDeathValue = DownedBossSystem.downedYharon;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Yharon, true);

            // Exo Mechs
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/chin", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/chinGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Exo Mech's Death]";
            bossDeathValue = DownedBossSystem.downedExoMechs;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Draedon);

            // SCal
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/scal", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/scalGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Supreme Calamitas' Death]";
            bossDeathValue = DownedBossSystem.downedSCal;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.SupremeCalamitas);

            // AEW
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/aew", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/aewGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Adult Eidolon Worm's Death]";
            bossDeathValue = DownedBossSystem.downedAdultEidolonWyrm;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.AEW);

            // Ravager
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos + PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/ravager", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/ravagerGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Ravager's Death]";
            bossDeathValue = DownedBossSystem.downedRavager;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.Ravager, true);

            // Moonlord
            iconSpawnPos += new Vector2(0, 50);
            finalDrawPos = iconSpawnPos +PostMoonlordXOffset + scrollOffset;
            baseTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/moonlord", (AssetRequestMode)2).Value;
            glowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/moonlordGlow", (AssetRequestMode)2).Value;
            bossName = "[c/ffcc44:Toggle Moonlord's Death]";
            bossDeathValue = NPC.downedMoonlord;
            ActuallyDrawBossIcons(finalDrawPos.Y, bossDeathValue, Boss.MoonLord);
            #endregion
            // This is where the drawing happens.
            void ActuallyDrawBossIcons(float killHere, bool bossDeathValue,Boss boss, bool shrinkIcon = false)
            {
                // Only draw it if we are inside the bounds.
                if (killHere !> killIfAbove.Y&& killHere !< killIfBelow.Y)
                {
                    // if the optional parameter shrinkIcon is true, shrink the scale. I did this due to several map icons being
                    // stupidly large.
                    float scale = 1;
                    if (shrinkIcon)
                        scale = 0.8f;

                    // Get a bunch of Rectangles.
                    Rectangle hitbox = Utils.CenteredRectangle(finalDrawPos, baseTexture.Size()*scale);
                    Rectangle indicatorHitbox = Utils.CenteredRectangle(finalDrawPos + new Vector2(10, 10), tickOrCross.Size());
                    Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);

                    // nono and nono2 are the rects of the kill zones. You dont want to be able to interact with the icons through it, so
                    // Ensure you arent hovering over it.
                    Rectangle nono = Utils.CenteredRectangle(killIfAbove, deleteIconTexture.Size());
                    Rectangle nono2 = Utils.CenteredRectangle(killIfBelow, deleteIconTexture.Size());
                    bool dontDraw = mouseHitbox.Intersects(nono)|| mouseHitbox.Intersects(nono2);

                    Texture2D whiteTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/BossIcons/bossWhiteRect", (AssetRequestMode)2).Value;
                    Rectangle whiteRect = Utils.CenteredRectangle(finalDrawPos, whiteTexture.Size());

                    if (mouseHitbox.Intersects(whiteRect))
                    {
                        // Draw white texture
                        spriteBatch.Draw(whiteTexture, finalDrawPos, null, Color.White * 0.15f, 0, whiteTexture.Size() * 0.5f, 1, 0, 0);
                        // If we are hovering over the icon and can draw
                        if (mouseHitbox.Intersects(hitbox) && !dontDraw)
                        {
                            // Draw it and set the mouse text.
                            spriteBatch.Draw(glowTexture, finalDrawPos, null, Color.White, 0, baseTexture.Size() * 0.5f, scale, 0, 0);
                            Main.hoverItemName = bossName;
                            
                        }            
                        if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                        {
                            // ON CLICK AFFECT
                            GenericUpdatesModPlayer.UpdateUpgradesTextFlag = true;
                            Hatred(boss);
                            SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);


                        }
                    }
                    // Draw the base texture.
                    spriteBatch.Draw(baseTexture, finalDrawPos, null, Color.White, 0, baseTexture.Size() * 0.5f, scale, 0, 0);
             
                    // Draw the indicator.
                    tickOrCross = bossDeathValue ? crossTexture : tickTexture;
                    tickOrCrossGlow = bossDeathValue ? crossGlowTexture : tickGlowTexture;
                    status = bossDeathValue ? "[c/f92a07:Dead]" : "[c/19a028:Alive]";
                    spriteBatch.Draw(tickOrCross, finalDrawPos + new Vector2(10, 10), null, Color.White, 0, tickOrCross.Size() * 0.5f, 1, 0, 0);

                    // If we are hovering over it, and can draw
                    if (mouseHitbox.Intersects(indicatorHitbox) && !dontDraw)
                    {
                        // Draw it and set the mouse text.
                        spriteBatch.Draw(tickOrCrossGlow, finalDrawPos + new Vector2(10, 10), null, Color.White, 0, tickOrCrossGlow.Size() * 0.5f, 1, 0, 0);
                        Main.hoverItemName = bossName + "\n" + status;
                    }
                    
                }
                
            }
        }

        public static void MarkAllBossesAsX(bool value)
        {
            // This is chunky.
            NPC.downedSlimeKing = value;
            DownedBossSystem.downedDesertScourge = value;
            NPC.downedBoss1 = value;
            DownedBossSystem.downedCrabulon = value;
            NPC.downedBoss2 = value;
            CalNohitQoL.DownedEater = value;
            CalNohitQoL.DownedBrain = value;
            DownedBossSystem.downedHiveMind = value;
            DownedBossSystem.downedPerforator = value;
            NPC.downedQueenBee = value;
            NPC.downedBoss3 = value;
            NPC.downedDeerclops = value;
            DownedBossSystem.downedSlimeGod = value;
            Main.hardMode = value;
            NPC.downedQueenSlime = value;
            DownedBossSystem.downedCryogen = value;
            NPC.downedMechBoss2 = value;
            DownedBossSystem.downedAquaticScourge = value;
            NPC.downedMechBoss1 = value;
            DownedBossSystem.downedBrimstoneElemental = value;
            NPC.downedMechBoss3 = value;
            DownedBossSystem.downedCalamitas = value;
            NPC.downedPlantBoss = value;
            DownedBossSystem.downedLeviathan = value;
            DownedBossSystem.downedAstrumAureus = value;
            NPC.downedGolemBoss = value;
            DownedBossSystem.downedPlaguebringer = value;
            NPC.downedEmpressOfLight = value;
            NPC.downedFishron = value;
            DownedBossSystem.downedRavager = value;
            NPC.downedAncientCultist = value;
            DownedBossSystem.downedAstrumDeus = value;
            NPC.downedMoonlord = value;
            DownedBossSystem.downedGuardians = value;
            DownedBossSystem.downedDragonfolly = value;
            DownedBossSystem.downedProvidence = value;
            DownedBossSystem.downedCeaselessVoid = value;
            DownedBossSystem.downedStormWeaver = value;
            DownedBossSystem.downedSignus = value;
            DownedBossSystem.downedPolterghast = value;
            DownedBossSystem.downedBoomerDuke = value;
            DownedBossSystem.downedDoG = value;
            DownedBossSystem.downedYharon = value;
            DownedBossSystem.downedExoMechs = value;
            DownedBossSystem.downedSCal = value;
            DownedBossSystem.downedAdultEidolonWyrm = value;
        }
        
        public static void Hatred(Boss boss)
        {
            switch (boss)
            {
                case Boss.KingSlime:
                    NPC.downedSlimeKing = !NPC.downedSlimeKing;
                    break;
                case Boss.DesertScourge:
                    DownedBossSystem.downedDesertScourge = !DownedBossSystem.downedDesertScourge;
                    break;
                case Boss.EyeOfCthulhu:
                    NPC.downedBoss1 = !NPC.downedBoss1;
                    break;
                case Boss.Crabulon:
                    DownedBossSystem.downedCrabulon = !DownedBossSystem.downedCrabulon;
                    break;
                case Boss.EaterOfWorlds:
                    CalNohitQoL.DownedEater = !CalNohitQoL.DownedEater;
                    break;
                case Boss.BrainOfCthulhu:
                    CalNohitQoL.DownedBrain = !CalNohitQoL.DownedBrain;
                    break;
                case Boss.HiveMind:
                    DownedBossSystem.downedHiveMind = !DownedBossSystem.downedHiveMind;
                    break;
                case Boss.Perforators:
                    DownedBossSystem.downedPerforator = !DownedBossSystem.downedPerforator;
                    break;
                case Boss.QueenBee:
                    NPC.downedQueenBee = !NPC.downedQueenBee;
                    break;
                case Boss.Skeletron:
                    NPC.downedBoss3 = !NPC.downedBoss3;
                    break;
                case Boss.Deerclops:
                    NPC.downedDeerclops = !NPC.downedDeerclops;
                    break;
                case Boss.SlimeGod:
                    DownedBossSystem.downedSlimeGod = !DownedBossSystem.downedSlimeGod;
                    break;
                case Boss.WallOfFlesh:
                    Main.hardMode = !Main.hardMode;
                    break;
                case Boss.QueenSlime:
                    NPC.downedQueenSlime = !NPC.downedQueenSlime;
                    break;
                case Boss.Cryogen:
                    DownedBossSystem.downedCryogen = !DownedBossSystem.downedCryogen;
                    break;
                case Boss.TheTwins:
                    NPC.downedMechBoss2 = !NPC.downedMechBoss2;
                    break;
                case Boss.BrimstoneElemental:
                    DownedBossSystem.downedBrimstoneElemental = !DownedBossSystem.downedBrimstoneElemental;
                    break;
                case Boss.TheDestroyer:
                    NPC.downedMechBoss1 = !NPC.downedMechBoss1;
                    break;
                case Boss.TheAquaticScourge:
                    DownedBossSystem.downedAquaticScourge = !DownedBossSystem.downedAquaticScourge;
                    break;
                case Boss.SkeletronPrime:
                    NPC.downedMechBoss3 = !NPC.downedMechBoss3;
                    break;
                case Boss.Cloneamitas:
                    DownedBossSystem.downedCalamitas = !DownedBossSystem.downedCalamitas;
                    break;
                case Boss.Plantera:
                    NPC.downedPlantBoss = !NPC.downedPlantBoss;
                    break;
                case Boss.AstrumAureus:
                    DownedBossSystem.downedAstrumAureus = !DownedBossSystem.downedAstrumAureus;
                    break;
                case Boss.Leviathan:
                    DownedBossSystem.downedLeviathan = !DownedBossSystem.downedLeviathan;
                    break;
                case Boss.Golem:
                    NPC.downedGolemBoss = !NPC.downedGolemBoss;
                    break;
                case Boss.PlaguebringerGoliath:
                    DownedBossSystem.downedPlaguebringer = !DownedBossSystem.downedPlaguebringer;
                    break;
                case Boss.EmpressOfLight:
                    NPC.downedEmpressOfLight = !NPC.downedEmpressOfLight;
                    break;
                case Boss.DukeFishron:
                    NPC.downedFishron = !NPC.downedFishron;
                    break;
                case Boss.Ravager:
                    DownedBossSystem.downedRavager = !DownedBossSystem.downedRavager;
                    break;
                case Boss.LunaticCultist:
                    NPC.downedAncientCultist = !NPC.downedAncientCultist;
                    break;
                case Boss.AstrumDeus:
                    DownedBossSystem.downedAstrumDeus = !DownedBossSystem.downedAstrumDeus;
                    break;
                case Boss.MoonLord:
                    NPC.downedMoonlord = !NPC.downedMoonlord;
                    break;
                case Boss.ProfanedGuardians:
                    DownedBossSystem.downedGuardians = !DownedBossSystem.downedGuardians;
                    break;
                case Boss.Dragonfolly:
                    DownedBossSystem.downedDragonfolly = !DownedBossSystem.downedDragonfolly;
                    break;
                case Boss.Providence:
                    DownedBossSystem.downedProvidence = !DownedBossSystem.downedProvidence;
                    break;
                case Boss.CeaselessVoid:
                    DownedBossSystem.downedCeaselessVoid = !DownedBossSystem.downedCeaselessVoid;
                    break;
                case Boss.StormWeaver:
                    DownedBossSystem.downedStormWeaver = !DownedBossSystem.downedStormWeaver;
                    break;
                case Boss.Signus:
                    DownedBossSystem.downedSignus = !DownedBossSystem.downedSignus;
                    break;
                case Boss.Polterghast:
                    DownedBossSystem.downedPolterghast = !DownedBossSystem.downedPolterghast;
                    break;
                case Boss.OldDuke:
                    DownedBossSystem.downedBoomerDuke = !DownedBossSystem.downedBoomerDuke;
                    break;
                case Boss.DevourerOfGods:
                    DownedBossSystem.downedDoG = !DownedBossSystem.downedDoG;
                    break;
                case Boss.Yharon:
                    DownedBossSystem.downedYharon = !DownedBossSystem.downedYharon;
                    break;
                case Boss.Draedon:
                    DownedBossSystem.downedExoMechs = !DownedBossSystem.downedExoMechs;
                    break;
                case Boss.SupremeCalamitas:
                    DownedBossSystem.downedSCal = !DownedBossSystem.downedSCal;
                    break;
                case Boss.AEW:
                    DownedBossSystem.downedAdultEidolonWyrm = !DownedBossSystem.downedAdultEidolonWyrm;
                    break;
            }
        }
    }
}