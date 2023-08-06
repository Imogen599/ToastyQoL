using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Core.Systems;

namespace ToastyQoL.Content.UI.BossUI
{
    public class BossTogglesUIManager
    {
        #region Fields/Properties
        internal static List<BossToggleElement> BossElements
        {
            get;
            private set;
        } = new();

        private static Vector2 ScrollbarOffset = new(115, 15);

        public const int HorizontalOffset = 70;

        public const int VerticalOffset = 50;
        #endregion

        #region Methods
        public static void InitializeBossElements()
        {
            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/kingSlime", "King Slime",
                typeof(NPC).GetField("downedSlimeKing", ToastyQoLUtils.UniversalBindingFlags), Weights.PostKingSlime).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/eoc", "Eye of Cthulhu",
                typeof(NPC).GetField("downedBoss1", ToastyQoLUtils.UniversalBindingFlags), Weights.PostEyeOfCthulhu).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/eow", "Eater of World",
                typeof(SavingSystem).GetField("_downedEater", ToastyQoLUtils.UniversalBindingFlags), Weights.PostEaterOfWorlds).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/boc", "Brain of Cthulhu",
                typeof(SavingSystem).GetField("_downedBrain", ToastyQoLUtils.UniversalBindingFlags), Weights.PostBrainOfCthulhu).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/queenBee", "Queen Bee",
                typeof(NPC).GetField("downedQueenBee", ToastyQoLUtils.UniversalBindingFlags), Weights.PostQueenBee).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/deerclops", "Deerclops",
                typeof(NPC).GetField("downedDeerclops", ToastyQoLUtils.UniversalBindingFlags), Weights.PostDeerclops, 0.85f).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/skeletron", "Skeletron",
                typeof(NPC).GetField("downedBoss3", ToastyQoLUtils.UniversalBindingFlags), Weights.PostSkeletron).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/wof", "Wall of Flesh",
                typeof(Main).GetField("hardMode", ToastyQoLUtils.UniversalBindingFlags), Weights.PostWallOfFlesh).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/queenSlime", "Queen Slime",
                typeof(NPC).GetField("downedQueenSlime", ToastyQoLUtils.UniversalBindingFlags), Weights.PostQueenSlime).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/twins", "The Twin",
                typeof(NPC).GetField("downedMechBoss2", ToastyQoLUtils.UniversalBindingFlags), Weights.PostTwins).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/destroyer", "The Destroyer",
                typeof(NPC).GetField("downedMechBoss1", ToastyQoLUtils.UniversalBindingFlags), Weights.PostDestroyer).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/prime", "Skeletron Prime",
                typeof(NPC).GetField("downedMechBoss3", ToastyQoLUtils.UniversalBindingFlags), Weights.PostSkeletronPrime).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/plantera", "Plantera",
                typeof(NPC).GetField("downedPlantBoss", ToastyQoLUtils.UniversalBindingFlags), Weights.PostPlantera).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/golem", "Golem",
                typeof(NPC).GetField("downedGolemBoss", ToastyQoLUtils.UniversalBindingFlags), Weights.PostGolem).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/empress", "Empress of Light",
                typeof(NPC).GetField("downedEmpressOfLight", ToastyQoLUtils.UniversalBindingFlags), Weights.PostEmpressOfLight).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/fishron", "Duke Fishron",
                typeof(NPC).GetField("downedFishron", ToastyQoLUtils.UniversalBindingFlags), Weights.PostDukeFishron).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/cultist", "Lunatic Cultist",
                typeof(NPC).GetField("downedAncientCultist", ToastyQoLUtils.UniversalBindingFlags), Weights.PostLunaticCultist).Register();

            new BossToggleElement("ToastyQoL/Content/UI/Textures/BossIcons/moonlord", "Moon Lord",
                typeof(NPC).GetField("downedMoonlord", ToastyQoLUtils.UniversalBindingFlags), Weights.PostMoonlord).Register();          
        }

        public static void AddBossElement(BossToggleElement element)
        {
            BossElements.Add(element);
            BossElements = BossElements.OrderBy(e => e.Weight).ToList();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Draw the background.
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/baseSettingsUIBackgroundWide", (AssetRequestMode)2).Value;
            Vector2 drawCenter;
            drawCenter.X = Main.screenWidth / 2;
            drawCenter.Y = Main.screenHeight / 2;
            // This spawn pos is very important. As it is affected by Main.screenWidth/Height, it will scale properly. Every single thing you draw needs to use
            // this vector, unless they are a completely new one and use Main.screenWidth.Height themselves for the VERY BASE of their definition.
            Vector2 spawnPos = drawCenter + new Vector2(300, 0);

            spriteBatch.Draw(backgroundTexture, spawnPos, null, Color.White, 0, backgroundTexture.Size() * 0.5f, 1f, 0, 0);

            // Block the mouse if we are hovering over it.
            Rectangle hoverArea = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size());
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(hoverArea);
            if (isHovering)
            {
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
            }

            // Go to the next things to draw, passing in spawnPos for convienience.
            DrawElements(spriteBatch, spawnPos);
        }

        public static void DrawElements(SpriteBatch spriteBatch, Vector2 spawnPos)
        {
            #region KillAllBosses
            // ALL THE TEXTURES
            Texture2D crossTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D crossGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            Texture2D tickTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value;
            Texture2D tickGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value;
            Texture2D whiteGlowSmall = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/SmallerWhiteRect", (AssetRequestMode)2).Value;
            // Get the mouse hitbox
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);

            // Scrollable icons

            // The method I use here to make things disapear when scrolling so they dont "break out" of their container is.. scuffed? honestly i dont know how else
            // you would do this, and it works very well. The way i wrote it is probably not that great, but its fully functional so uh, ye.
            // The below sets up the two zones after drawing the boss icons, so that they draw over the icons, allowing the icons to stop drawing when fully under.
            Texture2D deleteIconTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/thingForScrolling", (AssetRequestMode)2).Value;
            Vector2 deleteIconCenter = spawnPos + new Vector2(0, -144);
            Vector2 deleteIconCenter2 = spawnPos + new Vector2(0, 159);

            // We want to draw these first so they go under the kill zones.
            DrawBossIcons(spriteBatch, spawnPos, deleteIconCenter, deleteIconCenter2);

            // Draw the kill zones. This is for detecting if it scrolls, if the center of it is above or below these, it will stop drawing to keep it in bounds.

            spriteBatch.Draw(deleteIconTexture, deleteIconCenter, null, Color.White, 0, deleteIconTexture.Size() * 0.5f, 1f, 0, 0);
            spriteBatch.Draw(deleteIconTexture, deleteIconCenter2, null, Color.White, 0, deleteIconTexture.Size() * 0.5f, 1f, 0, 0);

            // Mark all as alive button stuff.
            Vector2 tickPos = new(-80f, -154f);
            Rectangle tickHoverRect = Utils.CenteredRectangle(spawnPos + tickPos, whiteGlowSmall.Size());
            if (mouseHitbox.Intersects(tickHoverRect))
            {
                spriteBatch.Draw(whiteGlowSmall, spawnPos + tickPos, null, Color.White * 0.3f, 0, whiteGlowSmall.Size() * 0.5f, 1f, 0, 0);
                Rectangle tickGlowRect = Utils.CenteredRectangle(spawnPos + tickPos, tickTexture.Size());
                if (mouseHitbox.Intersects(tickGlowRect))
                {
                    spriteBatch.Draw(tickGlowTexture, spawnPos + tickPos, null, Color.White, 0, tickGlowTexture.Size() * 0.5f, 1f, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Mark every boss as alive]";
                    if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                    {
                        // On click stuff
                        MarkAllBossesAsX(false);
                        TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                        SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                    }
                }
            }
            spriteBatch.Draw(tickTexture, spawnPos + tickPos, null, Color.White, 0, tickTexture.Size() * 0.5f, 1f, 0, 0);

            // Mark all as dead button.
            Vector2 crossPos = new(60f, -154f);
            Rectangle crossHoverRect = Utils.CenteredRectangle(spawnPos + crossPos, whiteGlowSmall.Size());
            if (mouseHitbox.Intersects(crossHoverRect))
            {
                spriteBatch.Draw(whiteGlowSmall, spawnPos + crossPos, null, Color.White * 0.3f, 0, whiteGlowSmall.Size() * 0.5f, 1f, 0, 0);
                Rectangle crossGlowRect = Utils.CenteredRectangle(spawnPos + crossPos, tickTexture.Size());
                if (mouseHitbox.Intersects(crossGlowRect))
                {
                    spriteBatch.Draw(crossGlowTexture, spawnPos + crossPos, null, Color.White, 0, crossGlowTexture.Size() * 0.5f, 1f, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Mark every boss as dead]";
                    if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                    {
                        MarkAllBossesAsX(true);
                        TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                        SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                    }
                }
            }
            spriteBatch.Draw(crossTexture, spawnPos + crossPos, null, Color.White, 0, crossTexture.Size() * 0.5f, 1f, 0, 0);
            #endregion

            #region Info
            Texture2D infoTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Info").Value;
            Texture2D infoGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/InfoGlow").Value;

            Vector2 infoPos = new(-10f, -154f);
            Rectangle infoHoverRect = Utils.CenteredRectangle(spawnPos + infoPos, whiteGlowSmall.Size());

            if (mouseHitbox.Intersects(infoHoverRect))
            {
                Rectangle infoGlowHoverRect = Utils.CenteredRectangle(spawnPos + infoPos, infoTexture.Size());

                spriteBatch.Draw(whiteGlowSmall, spawnPos + infoPos, null, Color.White * 0.3f, 0, whiteGlowSmall.Size() * 0.5f, 1f, 0, 0);
                if (mouseHitbox.Intersects(infoGlowHoverRect))
                    spriteBatch.Draw(infoGlowTexture, spawnPos + infoPos, null, Color.White, 0, infoGlowTexture.Size() * 0.5f, 1.2f, 0, 0);

                Main.hoverItemName = "[c/659cff:Hold L-SHIFT to toggle every boss up to the selected boss']\n[c/659cff:progression to the selected boss' state when clicking]";

            }
            spriteBatch.Draw(infoTexture, spawnPos + infoPos, null, Color.White, 0, infoTexture.Size() * 0.5f, 1.2f, 0, 0);
            #endregion

            #region Scrollbar
            Texture2D scrollbarBackgroundTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/scrollbarBackground", (AssetRequestMode)2).Value;
            Texture2D scrollbarTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/fullScrollbar", (AssetRequestMode)2).Value;
            Vector2 scrollbarBackgroundOffset = new(115, 8);
            spriteBatch.Draw(scrollbarBackgroundTexture, spawnPos + scrollbarBackgroundOffset, null, Color.White, 0, scrollbarBackgroundTexture.Size() * 0.5f, 1, 0, 0);

            // I didnt want to pass this Rectangle through as a parameter so i re-get the background rectangle here. This is to allow for scrolling with the mouse wheel.
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/baseSettingsUIBackgroundWide", (AssetRequestMode)2).Value;
            Rectangle backRect = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size());

            float maxScrollDistance = -50f * (((BossElements.Count / 3) > 5) ? (BossElements.Count / 3) - 5f : 0f);

            if (mouseHitbox.Intersects(backRect))
            {
                PlayerInput.LockVanillaMouseScroll("BossUI");
                // This means the mouse wheel has moved up.
                if (PlayerInput.MouseInfo.ScrollWheelValue - PlayerInput.MouseInfoOld.ScrollWheelValue > 0)
                {
                    // This need it minused not added, I have no fucking idea why lmao.
                    ScrollbarOffset.Y -= 50 * 0.47f;
                    ScrollbarOffset.Y = MathHelper.Clamp(ScrollbarOffset.Y, 8f, -maxScrollDistance);
                }
                // This means the wheel moved down. The same thing happens inside here as above, just in the other direction.
                else if (PlayerInput.MouseInfo.ScrollWheelValue - PlayerInput.MouseInfoOld.ScrollWheelValue < 0)
                {

                    ScrollbarOffset.Y += 50 * 0.47f;
                    ScrollbarOffset.Y = MathHelper.Clamp(ScrollbarOffset.Y, 8f, -maxScrollDistance);
                }
            }

            Vector2 scrollOffset = ScrollbarOffset;
            //if (-maxScrollDistance > 235f)
                scrollOffset.Y = Utils.Remap(scrollOffset.Y, 8f, -maxScrollDistance, 8f, 235f);

            spriteBatch.Draw(scrollbarTexture, spawnPos + scrollOffset, null, Color.White, 0, scrollbarTexture.Size() * 0.5f, 1f, 0, 0);
            #endregion
        }

        public static void DrawBossIcons(SpriteBatch spriteBatch, Vector2 spawnPos, Vector2 killIfAbove, Vector2 killIfBelow)
        {
            // Get all of the base textures.
            Texture2D deleteIconTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/thingForScrolling", (AssetRequestMode)2).Value;
            Texture2D crossTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D crossGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            Texture2D tickTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value;
            Texture2D tickGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value;

            // And the initial draw position.
            Vector2 drawPosition = spawnPos + new Vector2(-80, -90);

            for (int i = 0; i < BossElements.Count; i++)
            {
                if (i > 0)
                {
                    if (i % 3 == 0)
                    {
                        drawPosition.X -= HorizontalOffset * 2f;
                        drawPosition.Y += VerticalOffset;
                    }
                    else
                        drawPosition.X += HorizontalOffset;
                }

                Vector2 drawPositionFinal = drawPosition - Vector2.UnitY * ScrollbarOffset.Y;
                BossToggleElement element = BossElements[i];

                // Only draw it if we are inside the bounds.
                if (drawPositionFinal.Y !> killIfAbove.Y && drawPositionFinal.Y !< killIfBelow.Y)
                {
                    bool dead = element.GetStatus();
                    Texture2D tickOrCross = dead ? crossTexture : tickTexture;
                    Texture2D tickOrCrossGlow = dead ? crossGlowTexture : tickGlowTexture;

                    // Get a bunch of Rectangles.
                    Rectangle hitbox = Utils.CenteredRectangle(drawPositionFinal, element.Texture.Size() * element.Scale);
                    Rectangle indicatorHitbox = Utils.CenteredRectangle(drawPositionFinal + new Vector2(10, 10), tickOrCross.Size());
                    Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);

                    // nono and nono2 are the rects of the kill zones. You dont want to be able to interact with the icons through it, so
                    // ensure you arent hovering over it. No, I do not know why I deemed this a fitting name.
                    Rectangle nono = Utils.CenteredRectangle(killIfAbove, deleteIconTexture.Size());
                    Rectangle nono2 = Utils.CenteredRectangle(killIfBelow, deleteIconTexture.Size());
                    bool dontDraw = mouseHitbox.Intersects(nono) || mouseHitbox.Intersects(nono2);

                    Texture2D whiteTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/BossIcons/bossWhiteRect", (AssetRequestMode)2).Value;
                    Rectangle whiteRect = Utils.CenteredRectangle(drawPositionFinal, whiteTexture.Size());

                    if (mouseHitbox.Intersects(whiteRect))
                    {
                        // Draw white texture
                        spriteBatch.Draw(whiteTexture, drawPositionFinal, null, Color.White * 0.15f, 0, whiteTexture.Size() * 0.5f, 1, 0, 0);

                        // If we are hovering over the icon and can draw
                        if (mouseHitbox.Intersects(hitbox) && !dontDraw)
                        {
                            // Draw it and set the mouse text.
                            spriteBatch.Draw(element.GlowTexture, drawPositionFinal, null, Color.White, 0, element.GlowTexture.Size() * 0.5f, element.Scale, 0, 0);
                            Main.hoverItemName = $"[c/ffcc44:Toggle {element.Name}'s Death]";

                        }
                        if (ToastyQoLUtils.CanAndHasClickedUIElement)
                        {
                            if (Main.keyState.IsKeyDown(Keys.LeftShift))
                            {
                                float maxLayer = element.Weight;

                                bool statusToSetTo = element.GetStatus();

                                bool playSound = false;

                                foreach (var element2 in BossElements)
                                {
                                    if (element2.Weight >= maxLayer)
                                        continue;

                                    if (element2.GetStatus() == statusToSetTo)
                                        continue;

                                    element2.MarkAsStatus(statusToSetTo);
                                    playSound = true;
                                }
                                if (playSound)
                                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                            }
                            else
                            {
                                element.ToggleValue();
                                SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                            }
                        }
                    }
                    // Draw the base texture.
                    spriteBatch.Draw(element.Texture, drawPositionFinal, null, Color.White, 0, element.Texture.Size() * 0.5f, element.Scale, 0, 0);

                    // Draw the indicator.
                    string status = dead ? "[c/f92a07:Dead]" : "[c/19a028:Alive]";
                    spriteBatch.Draw(tickOrCross, drawPositionFinal + new Vector2(10, 10), null, Color.White, 0, tickOrCross.Size() * 0.5f, 1, 0, 0);

                    // If we are hovering over it, and can draw
                    if (mouseHitbox.Intersects(indicatorHitbox) && !dontDraw)
                    {
                        // Draw it and set the mouse text.
                        spriteBatch.Draw(tickOrCrossGlow, drawPositionFinal + new Vector2(10, 10), null, Color.White, 0, tickOrCrossGlow.Size() * 0.5f, 1, 0, 0);
                        Main.hoverItemName = $"[c/ffcc44:Toggle {element.Name}'s Death]" + "\n" + status;
                    }
                }
            }
        }

        public static void MarkAllBossesAsX(bool value)
        {
            foreach (var element in BossElements)
                element.MarkAsStatus(value);
        }
        #endregion
    }
}