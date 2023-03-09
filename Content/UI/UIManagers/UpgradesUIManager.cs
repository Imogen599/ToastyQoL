using CalamityMod;
using CalNohitQoL.Core;
using CalNohitQoL.Core.ModPlayers;
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
    public class UpgradesUIManager
    {
        #region Variables+Enums
        private static string NextHPUpgrade = "Blood Orange";
        private static string NextManaUpgrade = "Comet Shard";
        private static string NextRageUpgrade = "Mushroom Plasma Thing";
        private static string NextAdrenalineUpgrade = "Electrolite Gel Pack";
        private static string NextAccUpgrade = "Demon Heart";
        public enum UpgradeType
        {
            HP,
            Mana,
            Rage,
            Adren,
            AccSlots
        }
        #endregion
        #region Drawing
        public static bool IsDrawing { get; internal set; }
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
        public void DrawElements(SpriteBatch spriteBatch)
        {
            float baseVerticalOffset = -295;
            Texture2D fancyTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/whiteTangle", (AssetRequestMode)2).Value;
            Texture2D lockTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/lock", (AssetRequestMode)2).Value;
            Texture2D lockGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/lockGlow", (AssetRequestMode)2).Value;
            Player player = Main.LocalPlayer;
            // Go down in intervals of around 120
            #region Toggle All

            Texture2D autoTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Auto", (AssetRequestMode)2).Value;

            // Position of the Icon
            Vector2 backgroundDrawCenter2;
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset) / 2;
            Vector2 drawPos2 = backgroundDrawCenter2;

            Vector2 whiteDrawPos = new Vector2((Main.screenWidth + 600) / 2, (Main.screenHeight + baseVerticalOffset) / 2);
            // Rectangle area of the icon and mouse to check for hovering.
            Rectangle IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {

                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, autoTexture.Size());

                if (mouseHitbox.Intersects(hoverArea))
                {
                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/AutoGlow", (AssetRequestMode)2).Value;
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = "[c/ffcc44:Automated System enables every upgrade]\n[c/ffcc44:up to your latest boss in progression killed]";
                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    GenericUpdatesModPlayer.UpdateUpgradesTextFlag = true;
                    Toggles.AutomateProgressionUpgrades = !Toggles.AutomateProgressionUpgrades;
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }

            spriteBatch.Draw(autoTexture, drawPos2, null, Color.White, 0, autoTexture.Size() * 0.5f, 1f, 0, 0);
            Texture2D statusTexture = Toggles.AutomateProgressionUpgrades ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Tick", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D statusTextureGlow = Toggles.AutomateProgressionUpgrades ? ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/TickGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;
            if (isHovering && mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), statusTexture.Size())))
            {
                spriteBatch.Draw(statusTextureGlow, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTextureGlow.Size() * 0.5f, 1f, 0, 0);
                Main.hoverItemName = "[c/ffcc44:Automated System enables every upgrade]\n[c/ffcc44:up to your latest boss in progression killed]\n" + (Toggles.AutomateProgressionUpgrades ? "[c/44de5a:Enabled]" : "[c/de4444:Disabled]");
            }
            spriteBatch.Draw(statusTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, statusTexture.Size() * 0.5f, 1f, 0, 0);
            string textToShow2 = "Toggle Automated System";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion

            #region HP Upgrades 2

            Texture2D hpTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/BloodOrange", (AssetRequestMode)2).Value;

            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + 120) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = default;
            whiteDrawPos.X = (Main.screenWidth + 600) / 2;
            whiteDrawPos.Y = (Main.screenHeight + baseVerticalOffset + 120) / 2;
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {
                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, hpTexture.Size());

                if (mouseHitbox.Intersects(hoverArea))
                {
                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/BloodOrangeGlow", (AssetRequestMode)2).Value;
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextHPUpgrade}]";
                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                {
                    // ON CLICK AFFECT
                    if (!Toggles.AutomateProgressionUpgrades)
                        CheckNextUpgrade(UpgradeType.HP);
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }

            spriteBatch.Draw(hpTexture, drawPos2, null, Color.White, 0, hpTexture.Size() * 0.5f, 1f, 0, 0);
            if (Toggles.AutomateProgressionUpgrades)
                spriteBatch.Draw(lockTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockTexture.Size() * 0.5f, 1, 0, 0);
            if (isHovering)
            {
                if (mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), lockTexture.Size())))
                {
                    spriteBatch.Draw(lockGlowTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockGlowTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextHPUpgrade}]\n[c/de4444:Disable Automated System to toggle]";
                }
            }
            textToShow2 = "Toggle HP Upgrades";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);

            #endregion
            #region Mana Upgrades 3
            Texture2D manaTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/CometShard", (AssetRequestMode)2).Value;

            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + 240) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = default;
            whiteDrawPos.X = (Main.screenWidth + 600) / 2;
            whiteDrawPos.Y = (Main.screenHeight + baseVerticalOffset + 240) / 2;
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {
                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, manaTexture.Size());
                if (mouseHitbox.Intersects(hoverArea))
                {
                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/CometShardGlow", (AssetRequestMode)2).Value;
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);

                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextManaUpgrade}]";

                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                {
                    // ON CLICK EFFECT GOES HERE

                    if (!Toggles.AutomateProgressionUpgrades)
                        CheckNextUpgrade(UpgradeType.Mana);
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }

            spriteBatch.Draw(manaTexture, drawPos2, null, Color.White, 0, manaTexture.Size() * 0.5f, 1f, 0, 0);
            if (Toggles.AutomateProgressionUpgrades)
                spriteBatch.Draw(lockTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockTexture.Size() * 0.5f, 1, 0, 0);
            if (isHovering)
            {
                if (mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), lockTexture.Size())))
                {
                    spriteBatch.Draw(lockGlowTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockGlowTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextManaUpgrade}]\n[c/de4444:Disable Automated System to toggle]";
                }
            }
            textToShow2 = "Toggle Mana Upgrades";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion
            #region Rage Upgrades
            Texture2D rageTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/MushroomPlasmaRoot", (AssetRequestMode)2).Value;

            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + 360) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = default;
            whiteDrawPos.X = (Main.screenWidth + 600) / 2;
            whiteDrawPos.Y = (Main.screenHeight + baseVerticalOffset + 360) / 2;
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {
                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, rageTexture.Size());
                if (mouseHitbox.Intersects(hoverArea))
                {
                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/MushroomPlasmaRootGlow", (AssetRequestMode)2).Value;
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);

                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextRageUpgrade}]";
                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                {
                    // ON CLICK EFFECT GOES HERE
                    if (!Toggles.AutomateProgressionUpgrades)
                        CheckNextUpgrade(UpgradeType.Rage);
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }

            spriteBatch.Draw(rageTexture, drawPos2, null, Color.White, 0, rageTexture.Size() * 0.5f, 1f, 0, 0);
            if (Toggles.AutomateProgressionUpgrades)
                spriteBatch.Draw(lockTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockTexture.Size() * 0.5f, 1, 0, 0);
            if (isHovering)
            {
                if (mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), lockTexture.Size())))
                {
                    spriteBatch.Draw(lockGlowTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockGlowTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextRageUpgrade}]\n[c/de4444:Disable Automated System to toggle]";
                }
            }
            textToShow2 = "Toggle Rage Upgrades";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion
            #region Adren Upgrades
            Texture2D adrenUpgrade = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/ElectrolyteGelPack", (AssetRequestMode)2).Value;

            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + 480) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = default;
            whiteDrawPos.X = (Main.screenWidth + 600) / 2;
            whiteDrawPos.Y = (Main.screenHeight + baseVerticalOffset + 480) / 2;
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {
                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, adrenUpgrade.Size());
                if (mouseHitbox.Intersects(hoverArea))
                {
                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/ElectrolyteGelPackGlow", (AssetRequestMode)2).Value;
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);

                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextAdrenalineUpgrade}]";
                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                {
                    // ON CLICK EFFECT GOES HERE
                    if (!Toggles.AutomateProgressionUpgrades)
                        CheckNextUpgrade(UpgradeType.Adren);
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }

            spriteBatch.Draw(adrenUpgrade, drawPos2, null, Color.White, 0, adrenUpgrade.Size() * 0.5f, 1f, 0, 0);
            if (Toggles.AutomateProgressionUpgrades)
                spriteBatch.Draw(lockTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockTexture.Size() * 0.5f, 1, 0, 0);
            if (isHovering)
            {
                if (mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), lockTexture.Size())))
                {
                    spriteBatch.Draw(lockGlowTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockGlowTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextAdrenalineUpgrade}]\n[c/de4444:Disable Automated System to toggle]";
                }
            }
            textToShow2 = "Toggle Adren Upgrades";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion
            #region Acc Upgrades

            // I have hardcoded a scale here for testing purposes. Ensure you change the scale to be dependant on the sprite, and give one to the above.

            Texture2D accTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/DemonHeart", (AssetRequestMode)2).Value;

            // Position of the Icon
            backgroundDrawCenter2.X = (Main.screenWidth + 430) / 2;
            backgroundDrawCenter2.Y = (Main.screenHeight + baseVerticalOffset + 600) / 2;
            drawPos2 = backgroundDrawCenter2;

            whiteDrawPos = default;
            whiteDrawPos.X = (Main.screenWidth + 600) / 2;
            whiteDrawPos.Y = (Main.screenHeight + baseVerticalOffset + 600) / 2;
            // Rectangle area of the icon and mouse to check for hovering.
            IconRectangeArea2 = Utils.CenteredRectangle(whiteDrawPos, fancyTexture.Size());
            //Rectangle iconRectangeArea2 = new Rectangle((Main.screenWidth + 390) / 2, (Main.screenHeight - 255) / 2, (int)(105 * Main.UIScale), (int)(30*Main.UIScale));
            mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            isHovering = mouseHitbox.Intersects(IconRectangeArea2);
            if (isHovering)
            {
                spriteBatch.Draw(fancyTexture, whiteDrawPos, null, Color.White * 0.15f, 0, fancyTexture.Size() * 0.5f, 1, 0, 0);

                Rectangle hoverArea = Utils.CenteredRectangle(drawPos2, accTexture.Size() * 0.85f);
                if (mouseHitbox.Intersects(hoverArea))
                {
                    Texture2D hoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/DemonHeartGlow", (AssetRequestMode)2).Value;
                    spriteBatch.Draw(hoverTexture, drawPos2, null, Color.White, 0, hoverTexture.Size() * 0.5f, 1, 0, 0);

                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextAccUpgrade}]";
                }
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                {
                    // ON CLICK EFFECT GOES HERE
                    if (!Toggles.AutomateProgressionUpgrades)
                        CheckNextUpgrade(UpgradeType.AccSlots);
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, player.Center);
                }
            }

            spriteBatch.Draw(accTexture, drawPos2, null, Color.White, 0, accTexture.Size() * 0.5f, 1f, 0, 0);
            if (Toggles.AutomateProgressionUpgrades)
                spriteBatch.Draw(lockTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockTexture.Size() * 0.5f, 1, 0, 0);
            if (isHovering)
            {
                if (mouseHitbox.Intersects(Utils.CenteredRectangle(drawPos2 + new Vector2(10, 10), lockTexture.Size())))
                {
                    spriteBatch.Draw(lockGlowTexture, drawPos2 + new Vector2(10, 10), null, Color.White, 0, lockGlowTexture.Size() * 0.5f, 1, 0, 0);
                    Main.hoverItemName = $"[c/ffcc44:Next Upgrade: {NextAccUpgrade}]\n[c/de4444:Disable Automated System to toggle]";
                }
            }
            textToShow2 = "Toggle Accessory Upgrades";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textToShow2, drawPos2.X + 25, drawPos2.Y - 7, Color.White, Color.Black, default, 0.75f);
            #endregion

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!ShouldDraw)
                return;
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/baseSettingsUIBackground", (AssetRequestMode)2).Value;
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
                Main.blockMouse = player.mouseInterface = true;
            }
            DrawElements(spriteBatch);
        }
        #endregion
        //   #region Helper Methods
        private static void CheckNextUpgrade(UpgradeType upgradeType)
        {
            GenericUpdatesModPlayer.UpdateUpgradesTextFlag = true;
            switch (upgradeType)
            {
                case UpgradeType.HP:
                    HandleHPUpgrade();
                    break;
                case UpgradeType.Mana:
                    HandleManaUpgrade();
                    break;
                case UpgradeType.Rage:
                    HandleRageUpgrade();
                    break;
                case UpgradeType.Adren:
                    HandleAdrenUpgrade();
                    break;
                case UpgradeType.AccSlots:
                    HandleAccSlotsUpgrade();
                    break;
            }
        }
        public static void HandleAccSlotsUpgrade()
        {
            int type;
            if (Main.LocalPlayer.Calamity().extraAccessoryML)
            {
                Main.LocalPlayer.extraAccessory = false;
                Main.LocalPlayer.Calamity().extraAccessoryML = false;
                type = 0;
            }
            else if (Main.LocalPlayer.extraAccessory)
            {
                Main.LocalPlayer.extraAccessory = true;
                Main.LocalPlayer.Calamity().extraAccessoryML = true;
                type = 1;
            }
            else
            {
                Main.LocalPlayer.extraAccessory = true;
                Main.LocalPlayer.Calamity().extraAccessoryML = false;
                type = 2;
            }
            switch (type)
            {
                case 0:
                    NextAccUpgrade = "Demon Heart";
                    break;
                case 1:
                    NextAccUpgrade = "None";
                    break;
                case 2:
                    NextAccUpgrade = "Celestial Onion";
                    break;
            }
        }
        public static void HandleAdrenUpgrade()
        {
            int textType;
            if (Main.LocalPlayer.Calamity().adrenalineBoostThree)
            {
                Main.LocalPlayer.Calamity().adrenalineBoostOne = false;
                Main.LocalPlayer.Calamity().adrenalineBoostTwo = false;
                Main.LocalPlayer.Calamity().adrenalineBoostThree = false;
                textType = 0;
            }
            else if (Main.LocalPlayer.Calamity().adrenalineBoostTwo)
            {
                Main.LocalPlayer.Calamity().adrenalineBoostOne = true;
                Main.LocalPlayer.Calamity().adrenalineBoostTwo = true;
                Main.LocalPlayer.Calamity().adrenalineBoostThree = true;
                textType = 1;
            }
            else if (Main.LocalPlayer.Calamity().adrenalineBoostOne)
            {
                Main.LocalPlayer.Calamity().adrenalineBoostOne = true;
                Main.LocalPlayer.Calamity().adrenalineBoostTwo = true;
                Main.LocalPlayer.Calamity().adrenalineBoostThree = false;
                textType = 2;
            }
            else
            {
                Main.LocalPlayer.Calamity().adrenalineBoostOne = true;
                Main.LocalPlayer.Calamity().adrenalineBoostTwo = false;
                Main.LocalPlayer.Calamity().adrenalineBoostThree = false;
                textType = 3;
            }
            switch (textType)
            {
                case 0:
                    NextAdrenalineUpgrade = "Electrolyte Gel Pack";
                    break;
                case 1:
                    NextAdrenalineUpgrade = "None";
                    break;
                case 2:
                    NextAdrenalineUpgrade = "Ectoheart";
                    break;
                case 3:
                    NextAdrenalineUpgrade = "Starlight Fuel Cell";
                    break;
            }
        }
        public static void HandleRageUpgrade()
        {
            int textType;
            if (Main.LocalPlayer.Calamity().rageBoostThree)
            {
                Main.LocalPlayer.Calamity().rageBoostOne = false;
                Main.LocalPlayer.Calamity().rageBoostTwo = false;
                Main.LocalPlayer.Calamity().rageBoostThree = false;
                textType = 0;
            }
            else if (Main.LocalPlayer.Calamity().rageBoostTwo)
            {
                Main.LocalPlayer.Calamity().rageBoostOne = true;
                Main.LocalPlayer.Calamity().rageBoostTwo = true;
                Main.LocalPlayer.Calamity().rageBoostThree = true;
                textType = 1;
            }
            else if (Main.LocalPlayer.Calamity().rageBoostOne)
            {
                Main.LocalPlayer.Calamity().rageBoostOne = true;
                Main.LocalPlayer.Calamity().rageBoostTwo = true;
                Main.LocalPlayer.Calamity().rageBoostThree = false;
                textType = 2;
            }
            else
            {
                Main.LocalPlayer.Calamity().rageBoostOne = true;
                Main.LocalPlayer.Calamity().rageBoostTwo = false;
                Main.LocalPlayer.Calamity().rageBoostThree = false;
                textType = 3;
            }
            switch (textType)
            {
                case 0:
                    NextRageUpgrade = "Mushroom Plasma Root";
                    break;
                case 1:
                    NextRageUpgrade = "None";
                    break;
                case 2:
                    NextRageUpgrade = "Red Lightning Container";
                    break;
                case 3:
                    NextRageUpgrade = "Infernal Blood";
                    break;
            }
        }
        public static void HandleManaUpgrade()
        {
            int textType;
            // if you have max upgrades, turn them all off.
            if (Main.LocalPlayer.Calamity().pHeart)
            {
                Main.LocalPlayer.Calamity().cShard = false;
                Main.LocalPlayer.Calamity().eCore = false;
                Main.LocalPlayer.Calamity().pHeart = false;
                textType = 0;
            }
            else if (Main.LocalPlayer.Calamity().eCore)
            {
                Main.LocalPlayer.Calamity().cShard = true;
                Main.LocalPlayer.Calamity().eCore = true;
                Main.LocalPlayer.Calamity().pHeart = true;
                textType = 1;
            }
            else if (Main.LocalPlayer.Calamity().cShard)
            {
                Main.LocalPlayer.Calamity().cShard = true;
                Main.LocalPlayer.Calamity().eCore = true;
                Main.LocalPlayer.Calamity().pHeart = false;
                textType = 2;
            }
            else
            {
                Main.LocalPlayer.Calamity().cShard = true;
                Main.LocalPlayer.Calamity().eCore = false;
                Main.LocalPlayer.Calamity().pHeart = false;
                textType = 3;
            }
            switch (textType)
            {
                case 0:
                    NextManaUpgrade = "Comet Shard";
                    break;
                case 1:
                    NextManaUpgrade = "None";
                    break;
                case 2:
                    NextManaUpgrade = "Phantom Heart";
                    break;
                case 3:
                    NextManaUpgrade = "Ethereal Core";
                    break;
            }
        }
        public static void HandleHPUpgrade()
        {

            int textType;
            // if you have max upgrades, turn them all off.
            if (Main.LocalPlayer.Calamity().dFruit)
            {
                Main.LocalPlayer.Calamity().bOrange = false;
                Main.LocalPlayer.Calamity().mFruit = false;
                Main.LocalPlayer.Calamity().eBerry = false;
                Main.LocalPlayer.Calamity().dFruit = false;
                textType = 0;
            }
            else if (Main.LocalPlayer.Calamity().eBerry)
            {
                Main.LocalPlayer.Calamity().bOrange = true;
                Main.LocalPlayer.Calamity().mFruit = true;
                Main.LocalPlayer.Calamity().eBerry = true;
                Main.LocalPlayer.Calamity().dFruit = true;
                textType = 1;
            }
            else if (Main.LocalPlayer.Calamity().mFruit)
            {
                Main.LocalPlayer.Calamity().bOrange = true;
                Main.LocalPlayer.Calamity().mFruit = true;
                Main.LocalPlayer.Calamity().eBerry = true;
                Main.LocalPlayer.Calamity().dFruit = false;
                textType = 2;
            }
            else if (Main.LocalPlayer.Calamity().bOrange)
            {
                Main.LocalPlayer.Calamity().bOrange = true;
                Main.LocalPlayer.Calamity().mFruit = true;
                Main.LocalPlayer.Calamity().eBerry = false;
                Main.LocalPlayer.Calamity().dFruit = false;
                textType = 3;
            }
            else
            {
                Main.LocalPlayer.Calamity().bOrange = true;
                Main.LocalPlayer.Calamity().mFruit = false;
                Main.LocalPlayer.Calamity().eBerry = false;
                Main.LocalPlayer.Calamity().dFruit = false;
                textType = 4;
            }
            switch (textType)
            {
                case 0:
                    NextHPUpgrade = "Blood Orange";

                    break;
                case 1:
                    NextHPUpgrade = "None";
                    break;
                case 2:
                    NextHPUpgrade = "Dragonfruit";
                    break;
                case 3:
                    NextHPUpgrade = "Elderberry";
                    break;
                case 4:
                    NextHPUpgrade = "Miracle Fruit";
                    break;
            }
        }
        internal static bool SortOutTextures()
        {
            // HP Upgrades
            if (Main.LocalPlayer.Calamity().dFruit)
                NextHPUpgrade = "None";
            else if (Main.LocalPlayer.Calamity().eBerry)
                NextHPUpgrade = "Dragonfruit";
            else if (Main.LocalPlayer.Calamity().mFruit)
                NextHPUpgrade = "Elderberry";
            else if (Main.LocalPlayer.Calamity().bOrange)
                NextHPUpgrade = "Miracle Fruit";
            else
                NextHPUpgrade = "BloodOrange";

            // Mana Upgrades
            if (Main.LocalPlayer.Calamity().pHeart)
                NextManaUpgrade = "None";
            else if (Main.LocalPlayer.Calamity().eCore)
                NextManaUpgrade = "Phantom Heart";
            else if (Main.LocalPlayer.Calamity().cShard)
                NextManaUpgrade = "Ethereal Core";
            else
                NextManaUpgrade = "Comet Shard";

            // Rage Upgrades
            if (Main.LocalPlayer.Calamity().rageBoostThree)
                NextRageUpgrade = "None";
            else if (Main.LocalPlayer.Calamity().rageBoostTwo)
                NextRageUpgrade = "Red Lightning Container";
            else if (Main.LocalPlayer.Calamity().rageBoostOne)
                NextRageUpgrade = "Infernal Blood";
            else
                NextRageUpgrade = "Mushroom PlasmaRoot";

            // Adren Upgrades
            if (Main.LocalPlayer.Calamity().adrenalineBoostThree)
                NextAdrenalineUpgrade = "None";
            else if (Main.LocalPlayer.Calamity().adrenalineBoostTwo)
                NextAdrenalineUpgrade = "Ectoheart";
            else if (Main.LocalPlayer.Calamity().adrenalineBoostOne)
                NextAdrenalineUpgrade = "Starlight Fuel Cell";
            else
                NextAdrenalineUpgrade = "Electrolyte Gel Pack";

            // Acc Slot Upgrades
            if (Main.LocalPlayer.Calamity().extraAccessoryML)
                NextAccUpgrade = "None";
            else if (Main.LocalPlayer.extraAccessory)
                NextAccUpgrade = "Celestial Onion";
            else
                NextAccUpgrade = "Demon Heart";

            return false;
        }
        //	#endregion

    }
}