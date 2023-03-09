using CalNohitQoL.Core;
using CalNohitQoL.Core.ModPlayers;
using CalNohitQoL.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.UI.UIManagers
{
    public class TogglesUIManager
    {
        public static bool UIOpen { get; internal set; } = true;
        public static string TextToShow = "";
        public static int ClickCooldownTimer = 0;
        public static int ClickCooldownLength = 15;
        public Color OnColor = new Color(98, 255, 71);
        public Color OffColor = new Color(255, 48, 43);
        public static Color ColorToUse;
        public static int IntroTimer = 0;
        public static int OutroTimer = 60;
        public static int Timer;
        public int Smoltimer;
        public static int Frame = -1;

        private bool ShouldDraw
        {
            get
            {
                if (UIOpen)
                {
                    return true;
                }
                return false;
            }
        }

        public virtual List<TogglesUIElement> UIElementList
        {
            get
            {
                List<TogglesUIElement> list = new List<TogglesUIElement>();

                // Add Base Ones Here

                list.Add(new TogglesUIElement("Set Spawn", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/setSpawnUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    Main.spawnTileX = (int)(Main.LocalPlayer.position.X - 8f + Main.LocalPlayer.width / 2) / 16;
                    Main.spawnTileY = (int)(Main.LocalPlayer.position.Y + Main.LocalPlayer.height) / 16;
                    GenericUpdatesModPlayer.UIUpdateTextTimer = 120;
                    TextToShow = "Spawn Set";
                    ColorToUse = Color.White;
                }
                ));
                //list.Add(new TogglesUIElement("Pause Time", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/pauseTimeUIIcon", (AssetRequestMode)2).Value, delegate
                //{
                //    GenericModSystem.FrozenTime = !GenericModSystem.FrozenTime;
                //    CalNohitQoLPlayer.UIUpdateTextTimer = 60;
                //    textToShow = GenericModSystem.FrozenTime ? "Time Paused" : "Time Unpaused";
                //    ColorToUse = GenericModSystem.FrozenTime ? OffColor : OnColor;
                //}
                //));
                list.Add(new TogglesUIElement("Toggle Potions", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/potionUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    bool smily = PotionUI.PotionUIManager.IsDrawing;
                    CloseAllUI(false);
                    PotionUI.PotionUIManager.IsDrawing = !smily;
                    SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.Center);
                    Main.playerInventory = false;
                    PotionUI.PotionUIManager.Timer = 0;
                    GenericUpdatesModPlayer.PotionUICooldownTimer = GenericUpdatesModPlayer.UICooldownTimerLength;
                }
                ));
                list.Add(new TogglesUIElement("Set Upgrades", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/upgradesUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    bool smily = UpgradesUIManager.IsDrawing;
                    CloseAllUI(false);
                    UpgradesUIManager.IsDrawing = !smily;
                }
                ));
                list.Add(new TogglesUIElement("Progression Locks", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/locksUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    bool smily = LocksUIManager.IsDrawing;
                    CloseAllUI(false);
                    LocksUIManager.IsDrawing = !smily;
                }
                ));
                list.Add(new TogglesUIElement("Misc Toggles", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/settingsUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    bool smily = MiscUIManager.IsDrawing;
                    CloseAllUI(false);
                    MiscUIManager.IsDrawing = !smily;
                }
                ));
                list.Add(new TogglesUIElement("Set Powers", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/playerUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    bool smily = PowersUIManager.IsDrawing;
                    CloseAllUI(false);
                    PowersUIManager.IsDrawing = !smily;
                }
                ));
                list.Add(new TogglesUIElement("World Toggles", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/worldUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    bool smily = WorldUIManager.IsDrawing;
                    CloseAllUI(false);
                    WorldUIManager.IsDrawing = !smily;
                }
                ));
                list.Add(new TogglesUIElement("Boss Toggles", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/bossDeathsUIIcon", (AssetRequestMode)2).Value, delegate
                {
                    bool smily = BossTogglesUIManager.IsDrawing;
                    CloseAllUI(false);
                    BossTogglesUIManager.IsDrawing = !smily;
                }
                ));
                //list.Add(new TogglesUIElement("Toggle Spawns", ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/toggleSpawnsUIIcon", (AssetRequestMode)2).Value, delegate
                //{
                //    GenericModSystem.NoSpawns = !GenericModSystem.NoSpawns;
                //    if (GenericModSystem.NoSpawns)
                //    {
                //        for (int i = 0; i < 200; i++)
                //        {
                //            if (Main.npc[i].type != NPCID.LunarTowerNebula && Main.npc[i].type != NPCID.LunarTowerSolar && Main.npc[i].type != NPCID.LunarTowerStardust && Main.npc[i].type != NPCID.LunarTowerVortex && Main.npc[i] != null && !Main.npc[i].townNPC)
                //            {
                //                Main.npc[i].life = 0;
                //                if (Main.netMode == NetmodeID.Server)
                //                {
                //                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
                //                }
                //            }
                //        }
                //    }
                //    CalNohitQoLPlayer.UIUpdateTextTimer = 60;
                //    textToShow = GenericModSystem.NoSpawns ? "Spawns Disabled" : "Spawns Enabled";
                //    ColorToUse = GenericModSystem.NoSpawns ? OffColor : OnColor;
                //}
                //));

                List<TogglesUIElement> list2 = list;
                list2.AddRange(CalNohitQoL.OtherUIElements);
                return list2;
            }
        }
        public void DrawElements(SpriteBatch spriteBatch)
        {
            OutroTimer = 45;
            int currentHover = -1;
            for (int i = 0; i < UIElementList.Count; i++)
            {
                // Size of the icon
                TogglesUIElement uiElement = UIElementList[i];
                Player player = Main.LocalPlayer;
                Vector2 drawCenter;
                drawCenter.X = Main.screenWidth / 2;
                drawCenter.Y = Main.screenHeight / 2;
                Vector2 drawAngle = (MathHelper.TwoPi * i / UIElementList.Count + MathHelper.Pi + MathHelper.PiOver2).ToRotationVector2();
                float distance = 100f;
                float opacity = 1f;

                Texture2D texture;
                if (uiElement.AltTexture != default)
                    texture = uiElement.AltTexture;
                else
                    texture = uiElement.IconTexture;

                if (IntroTimer <= 60)
                {
                    distance = (float)IntroTimer / 60 * 100;
                    opacity = (float)IntroTimer / 60 / 2;
                    IntroTimer++;
                }

                Vector2 spawnPos = drawCenter + drawAngle * distance;
                float scale = 1;
                spriteBatch.End();
                spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, null, null, null, null, Main.UIScaleMatrix);
                Texture2D bloomTexture = ModContent.Request<Texture2D>("CalNohitQoL/Assets/ExtraTextures/Bloom", (AssetRequestMode)2).Value;
                float rot = Main.GlobalTimeWrappedHourly * 0.5f;
                spriteBatch.Draw(bloomTexture, spawnPos, null, new Color(59, 50, 77) * (0.9f * opacity), rot, new Vector2(123f, 124f), 0.4f, 0, 0f);
                spriteBatch.End();
                spriteBatch.Begin(0, null, null, null, null, null, Main.UIScaleMatrix);

                // Rectangle area of the icon to check for hovering.
                Rectangle iconRectangeArea = Utils.CenteredRectangle(spawnPos, texture.Size() * 1f);
                Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
                bool isHovering = mouseHitbox.Intersects(iconRectangeArea);
                bool animationActive = Frame >= 0 && Frame < 12;
                if (isHovering && IntroTimer >= 60)
                {
                    if (currentHover == -1)
                        currentHover = i;

                    scale = 1.1f;
                    spriteBatch.Draw(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/UIIconOutline", (AssetRequestMode)2).Value, spawnPos, null, Color.White * opacity, 0f, texture.Size() * 0.5f, scale, 0, 0f);
                    Vector2 size = FontAssets.MouseText.Value.MeasureString(uiElement.Description);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, uiElement.Description, drawCenter.X - size.X / 2, drawCenter.Y + 25f, Color.White, Color.Black, default, 1f);

                    if (currentHover == i)
                    {
                        if (Frame < 0)
                            Frame = 0;

                        if (animationActive)
                        {
                            Smoltimer++;
                            if (Frame >= 12)
                            {
                                Frame = 0;
                            }
                            if (Smoltimer >= 6)
                            {
                                Smoltimer = 0;
                                Frame++;

                            }

                        }
                        else
                        {
                            Frame = 0;
                        }
                    }

                }

                else if (currentHover == i)
                {
                    Frame = -1;
                }
                spriteBatch.Draw(texture, spawnPos, null, Color.White * opacity, 0f, texture.Size() * 0.5f, scale, 0, 0f);
                if (GenericUpdatesModPlayer.UIUpdateTextTimer > 0)
                {
                    Vector2 size = FontAssets.MouseText.Value.MeasureString(TextToShow);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, TextToShow, drawCenter.X - size.X / 2, drawCenter.Y - 40f, ColorToUse * opacity, Color.Black, default, 1f);
                }
                if (uiElement.OnClick != null)
                {
                    if (mouseHitbox.Intersects(iconRectangeArea))
                    {
                        Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                        if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && ClickCooldownTimer == 0)
                        {
                            uiElement.OnClick();
                            ClickCooldownTimer = ClickCooldownLength;
                            SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                        }
                    }
                }
                Texture2D animationTex = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/animatedGlow", (AssetRequestMode)2).Value;
                if (animationActive && currentHover == i)
                {
                    int frameHeight = animationTex.Height / 12 - 1;
                    Rectangle animCropRect = new Rectangle(0, (frameHeight + 1) * Frame, animationTex.Width, frameHeight);
                    float xOffset = (animationTex.Width - animationTex.Width) / 2f;
                    float yOffset = (animationTex.Height - frameHeight) / 2f;
                    Vector2 sizeDiffOffset = new Vector2(xOffset, yOffset);
                    spriteBatch.Draw(animationTex, spawnPos + sizeDiffOffset, (Rectangle?)animCropRect, Color.White * 0.55f, 0, animationTex.Size() * 0.5f, 1, 0, 0f);
                }
            }
        }

        public static void CloseAllUI(bool closeMain)
        {
            // Close and reset all UI variables.
            if (closeMain)
            {
                UIOpen = false;
                Timer = 0;
                Frame = -1;
                IntroTimer = 0;
            }
            UpgradesUIManager.IsDrawing = false;
            LocksUIManager.IsDrawing = false;
            PowersUIManager.IsDrawing = false;
            PowersUIManager.PageNumber = 1;
            WorldUIManager.IsDrawing = false;
            WorldUIManager.PageNumber = 1;
            MiscUIManager.IsDrawing = false;
            MiscUIManager.PageNumber = 1;
            BossTogglesUIManager.IsDrawing = false;
            PotionUI.PotionUIManager.IsDrawing = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // This is kinda useless but oh well.
            if (ShouldDraw)
                DrawElements(spriteBatch);
            // NOT ANYMORE
            else if (OutroTimer >= 0)
            {
                for (int i = 0; i < UIElementList.Count; i++)
                {
                    // Size of the icon
                    TogglesUIElement uiElement = UIElementList[i];
                    Vector2 drawCenter;
                    drawCenter.X = Main.screenWidth / 2;
                    drawCenter.Y = Main.screenHeight / 2;
                    Vector2 drawAngle = (MathHelper.TwoPi * i / UIElementList.Count + MathHelper.Pi + MathHelper.PiOver2).ToRotationVector2();
                    float distance = 100f;
                    float opacity = 1f;
                    if (OutroTimer >= 0)
                    {
                        distance = (float)OutroTimer / 60 * 100;
                        opacity = (float)OutroTimer / 60 / 2;
                        OutroTimer--;

                    }
                    Vector2 spawnPos = drawCenter + drawAngle * distance;
                    float scale = 1;
                    Texture2D texture;
                    if (uiElement.AltTexture != default)
                        texture = uiElement.AltTexture;
                    else
                        texture = uiElement.IconTexture;
                    if (OutroTimer > 0)
                        spriteBatch.Draw(texture, spawnPos, null, Color.White * opacity, 0f, texture.Size() * 0.5f, scale, 0, 0f);
                }
            }
        }

        #region Utilities
        public enum SpecialToggleOnClick
        {
            None,
            MapReveal,
            BeginRain,
            EndRain,
            DisableEvents,
            ToggleWorldDifficulty,
            TogglePlayerDificulty,
            HPUpgrades,
            ManaUpgrades,
            RageUpgrades,
            AdrenUpgrades,
            AccUpgrades,
            SassMode,
            ToggleTime,
            ToggleRain,
            BossDPS,
            TesterTimes
        }

        public static void DrawElementWithBasicToggle(SpriteBatch spriteBatch, Texture2D baseTexture, Texture2D glowTexture, Player player, int index, float baseVerticalOffset, float baseOffsetAmount, string displayText, string hoverTextFormatted, ref bool thingToToggle, SpecialToggleOnClick extraOnClickEffect = SpecialToggleOnClick.None)
        {
            Texture2D fancyTexture;
            fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/whiteTangle", (AssetRequestMode)2).Value;
            if (index == 0)
            {
                baseOffsetAmount = 0;
            }
            Vector2 backgroundDrawCenter2;
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseOffsetAmount * index) / 2;
            Vector2 drawPos2 = backgroundDrawCenter2;

            Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseOffsetAmount * index) / 2);
            // Rectangle area of the icon and mouse to check for hovering.
            Rectangle IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {

                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, baseTexture.Size());

                if (mouseHitbox.Intersects(hoverArea))
                {
                    spriteBatch.Draw(glowTexture, drawPos2, null, Color.White, 0, glowTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = hoverTextFormatted;
                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && ClickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    thingToToggle = !thingToToggle;
                    ClickCooldownTimer = ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);

                    switch (extraOnClickEffect)
                    {
                        case SpecialToggleOnClick.SassMode:
                            Toggles.MNLIndicator = true;
                            if (thingToToggle == false)
                                SoundEngine.PlaySound(new SoundStyle("CalNohitQoL/Assets/Sounds/Custom/babyLaugh"), player.Center);
                            else
                                Toggles.MNLIndicator = true;
                            break;
                        case SpecialToggleOnClick.BossDPS:
                            Toggles.MNLIndicator = true;
                            break;
                        case SpecialToggleOnClick.TesterTimes:
                            GenericUpdatesModPlayer.UpdateActiveLengthDictFlag = true;
                            break;
                    }

                }
            }

            spriteBatch.Draw(baseTexture, drawPos2, null, Color.White, 0, baseTexture.Size() * 0.5f, 1f, 0, 0);
            Texture2D statusTexture = thingToToggle ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D statusTextureGlow = thingToToggle ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
            {
                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
                Main.hoverItemName = hoverTextFormatted + "\n" + (thingToToggle ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
            }
            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, displayText, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        }
        public static void DrawElementWithOneJob(SpriteBatch spriteBatch, Texture2D baseTexture, Texture2D glowTexture, Player player, int index, float baseVerticalOffset, float baseOffsetAmount, string displayText, string hoverTextFormatted, SpecialToggleOnClick extraOnClickEffect = SpecialToggleOnClick.None)
        {
            Texture2D fancyTexture;
            fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/whiteTangle", (AssetRequestMode)2).Value;
            if (index == 0)
            {
                baseOffsetAmount = 0;
            }
            Vector2 backgroundDrawCenter2;
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + baseOffsetAmount * index) / 2;
            Vector2 drawPos2 = backgroundDrawCenter2;

            Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset + baseOffsetAmount * index) / 2);
            // Rectangle area of the icon and mouse to check for hovering.
            Rectangle IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(IconRectangeArea2);

            if (isHovering)
            {

                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, baseTexture.Size());

                if (mouseHitbox.Intersects(hoverArea))
                {
                    spriteBatch.Draw(glowTexture, drawPos2, null, Color.White, 0, glowTexture.Size() * 0.5f, 1, 0, 0);

                    Main.hoverItemName = hoverTextFormatted;
                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && ClickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    ClickCooldownTimer = ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                    if (extraOnClickEffect != SpecialToggleOnClick.None)
                    {
                        GenericUpdatesModPlayer.UIUpdateTextTimer = 120;
                        switch (extraOnClickEffect)
                        {
                            case SpecialToggleOnClick.MapReveal:
                                MapSystem.MapReveal = true;
                                break;
                            case SpecialToggleOnClick.ToggleTime:
                                Main.dayTime = !Main.dayTime;
                                Main.time = 0.0;
                                if (Main.dayTime)
                                {
                                    TextToShow = "Time set to Day";
                                    ColorToUse = Color.Gold;
                                }
                                else
                                {
                                    TextToShow = "Time set to Night";
                                    ColorToUse = Color.DimGray;
                                }
                                break;
                            case SpecialToggleOnClick.ToggleRain:
                                Toggles.ToggleRain = true;
                                break;
                            case SpecialToggleOnClick.DisableEvents:
                                Toggles.DisableEvents = true;
                                TextToShow = "Events Disabled";
                                ColorToUse = Color.Coral;
                                break;
                            case SpecialToggleOnClick.ToggleWorldDifficulty:
                                string text;
                                switch (Main.GameMode)
                                {
                                    case 0:
                                        Main.GameMode = 1;
                                        player.difficulty = 0;
                                        text = "Expert enabled";
                                        ColorToUse = new Color(175, 75, 255);
                                        break;

                                    case 1:
                                        Main.GameMode = 2;
                                        player.difficulty = 0;
                                        text = "Master enabled";
                                        ColorToUse = new Color(255, 68, 68);
                                        break;

                                    case 2:
                                        Main.GameMode = 3;
                                        player.difficulty = 3;
                                        text = "Journey enabled";
                                        ColorToUse = new Color(255, 255, 102);
                                        break;

                                    default:
                                        Main.GameMode = 0;
                                        player.difficulty = 0;
                                        text = "Normal enabled";
                                        ColorToUse = Color.White;
                                        break;
                                }
                                TextToShow = text;
                                break;
                            case SpecialToggleOnClick.TogglePlayerDificulty:
                                switch (player.difficulty)
                                {
                                    case 0:
                                        player.difficulty = 1;
                                        text = "Mediumcore enabled";
                                        ColorToUse = new Color(175, 75, 255);
                                        break;
                                    case 1:
                                        player.difficulty = 2;
                                        text = "Hardcore Enabled";
                                        ColorToUse = new Color(255, 68, 68);
                                        break;
                                    default:
                                        player.difficulty = 0;
                                        text = "Classic enabled";
                                        ColorToUse = Color.White;
                                        break;
                                }
                                if (Main.GameMode == 3 && player.difficulty != 3)
                                {
                                    // Stops you and the world desyncing being journey.
                                    Main.GameMode = 1;
                                }
                                TextToShow = text;
                                break;
                        }

                    }
                }
            }
            spriteBatch.Draw(baseTexture, drawPos2, null, Color.White, 0, baseTexture.Size() * 0.5f, 1f, 0, 0);
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, displayText, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
        }
        #endregion
    }
}
