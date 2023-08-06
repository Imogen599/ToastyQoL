using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ToastyQoL.Content.UI.PotionUI
{
    // TODO: Make this not shit.
    public class PotionUIManager
    {
        #region Fields/Properties
        public static bool IsDrawing
        {
            get;
            internal set;
        }

        private static Vector2 BasePotionDrawPos = new(-260, -110);

        public const int HorizontalOffset = 40;

        public const int VerticalOffset = 56;

        private static Dictionary<PotionMod, List<PotionElement>> ModsRelatedPotions
        {
            get;
            set;
        } = new();

        public static PotionMod CurrentlySelectedMod
        {
            get;
            private set;
        }

        public static IPotionSorting CurrentSortingMode
        {
            get;
            private set;
        }

        public static List<PotionElement> CurrentPotionElements
        {
            get
            {
                if (CurrentlySelectedMod == null || ModsRelatedPotions == null)
                    return new();

                if (ModsRelatedPotions.TryGetValue(CurrentlySelectedMod, out var list))
                    return CurrentSortingMode.SortPotions(ref list);

                return new();
            }
        }

        private static List<PotionElement> SelectedPotions
        {
            get;
            set;
        } = new();

        #endregion

        #region Initialization
        public static void InitializePotionElements()
        {
            ModsRelatedPotions = new();
            PotionMod vanilla = new("Terraria", "ToastyQoL/Content/UI/PotionUI/Textures/terrariaIcon");
            ModsRelatedPotions.Add(vanilla, new List<PotionElement>() 
            {
                new PotionElement("Ammo Reservation", "20% chance to not consume ammo", "ToastyQoL/Content/UI/PotionUI/Textures/ammoReservation", BuffID.AmmoReservation, () => true,
                Weights.PreBoss),

                new PotionElement("Archery", "10% increased bow damage and 20% increased arrow speed", "ToastyQoL/Content/UI/PotionUI/Textures/archery", BuffID.Archery, () => true,
                Weights.PreBoss),

                new PotionElement("Battle", "Increases enemy spawn rate", "ToastyQoL/Content/UI/PotionUI/Textures/battle", BuffID.Battle, () => true,
                Weights.PreBoss),

                new PotionElement("Builder", "Increases placement speed and range", "ToastyQoL/Content/UI/PotionUI/Textures/builder", BuffID.Builder, () => true,
                Weights.PreBoss),

                new PotionElement("Calming", "Decreases enemy spawn rate", "ToastyQoL/Content/UI/PotionUI/Textures/calming", BuffID.Calm, () => true,
                Weights.PreBoss),

                new PotionElement("Crate", "Increases chance to get a crate", "ToastyQoL/Content/UI/PotionUI/Textures/crate", BuffID.Crate, () => true,
                Weights.PreBoss),

                new PotionElement("Dangersense", "Allows you to see nearby danger sources", "ToastyQoL/Content/UI/PotionUI/Textures/dangersense", BuffID.Dangersense, () => true,
                Weights.PreBoss),

                new PotionElement("Endurance", "Reduces damage taken by 10%", "ToastyQoL/Content/UI/PotionUI/Textures/endurance", BuffID.Endurance, () => true,
                Weights.PreBoss),

                new PotionElement("Exquisitely Stuffed", "Major improvements to all stats", "ToastyQoL/Content/UI/PotionUI/Textures/goldenDelight", BuffID.WellFed3, () => true,
                Weights.PreBoss),

                new PotionElement("Featherfall", "Slows falling speed", "ToastyQoL/Content/UI/PotionUI/Textures/featherfall", BuffID.Featherfall, () => true,
                Weights.PreBoss),

                new PotionElement("Fishing", "Increases fishing power by 15", "ToastyQoL/Content/UI/PotionUI/Textures/fishing", BuffID.Fishing, () => true,
                Weights.PreBoss),

                new PotionElement("Flask of Cursed Flames", "Weapon Imbue Cursed Flames", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfCursedFlames", BuffID.WeaponImbueCursedFlames, () => Main.hardMode,
                Weights.PostWallOfFlesh),

                new PotionElement("Flask of Fire", "Weapon Imbue Fire", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfFire", BuffID.WeaponImbueFire, () => NPC.downedQueenBee,
                Weights.PostQueenBee),

                new PotionElement("Flask of Gold", "Weapon Imbue Gold", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfGold", BuffID.WeaponImbueGold, () => Main.hardMode,
                Weights.PostWallOfFlesh),

                new PotionElement("Flask of Ichor", "Weapon Imbue Ichor", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfIchor", BuffID.WeaponImbueIchor, () => Main.hardMode,
                Weights.PostWallOfFlesh),

                new PotionElement("Flask of Nanites", "Weapon Imbue Nanites", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfNanites", BuffID.WeaponImbueNanites, () => NPC.downedPlantBoss,
                Weights.PostPlantera),

                new PotionElement("Flask of Party", "Weapon Imbue Party", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfParty", BuffID.WeaponImbueConfetti, () => NPC.downedQueenBee,
                Weights.PostQueenBee),

                new PotionElement("Flask of Poison", "Weapon Imbue Poison", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfPoison", BuffID.WeaponImbuePoison, () => NPC.downedQueenBee,
                Weights.PostQueenBee),

                new PotionElement("Flask of Venom", "Weapon Imbue Venom", "ToastyQoL/Content/UI/PotionUI/Textures/flaskOfVenom", BuffID.WeaponImbueVenom, () => NPC.downedPlantBoss,
                Weights.PostPlantera),

                new PotionElement("Flipper", "Lets you move swiftly in liquids", "ToastyQoL/Content/UI/PotionUI/Textures/flipper", BuffID.Flipper, () => true,
                Weights.PreBoss),

                new PotionElement("Gills", "Breathe water instead of air", "ToastyQoL/Content/UI/PotionUI/Textures/gills", BuffID.Gills, () => true,
                Weights.PreBoss),

                new PotionElement("Gravitation", "Allows the control of gravity", "ToastyQoL/Content/UI/PotionUI/Textures/gravitation", BuffID.Gravitation, () => true,
                Weights.PreBoss),

                new PotionElement("Greater Luck", "Increases the Luck of the user", "ToastyQoL/Content/UI/PotionUI/Textures/greaterLuck", BuffID.Lucky, () => true,
                Weights.PreBoss),

                new PotionElement("Heartreach", "Increases pickup range for life hearts", "ToastyQoL/Content/UI/PotionUI/Textures/heartreach", BuffID.Heartreach, () => true,
                Weights.PreBoss),

                new PotionElement("Hunter", "Shows the location of enemies", "ToastyQoL/Content/UI/PotionUI/Textures/hunter", BuffID.Hunter, () => true,
                Weights.PreBoss),

                new PotionElement("Inferno", "Ignites nearby enemies", "ToastyQoL/Content/UI/PotionUI/Textures/inferno", BuffID.Inferno, () => true,
                Weights.PreBoss),

                new PotionElement("Invisibility", "Grants invisibility and lowers the spawn rate of enemies", "ToastyQoL/Content/UI/PotionUI/Textures/invisibility", BuffID.Invisibility, () => true,
                Weights.PreBoss),

                new PotionElement("Ironskin", "Increase defense by 8", "ToastyQoL/Content/UI/PotionUI/Textures/ironskin", BuffID.Ironskin, () => true,
                Weights.PreBoss),

                new PotionElement("Lifeforce", "Increases max life by 20%", "ToastyQoL/Content/UI/PotionUI/Textures/lifeforce", BuffID.Lifeforce, () => true,
                Weights.PreBoss),

                new PotionElement("Magic Power", "20% increased magic damage", "ToastyQoL/Content/UI/PotionUI/Textures/magicPower", BuffID.MagicPower, () => true,
                Weights.PreBoss),

                new PotionElement("Mana Regeneration", "Increased mana regeneration", "ToastyQoL/Content/UI/PotionUI/Textures/manaRegeneration", BuffID.ManaRegeneration, () => true,
                Weights.PreBoss),

                new PotionElement("Mining", "Increases mining speed by 25%", "ToastyQoL/Content/UI/PotionUI/Textures/mining", BuffID.Mining, () => true,
                Weights.PreBoss),

                new PotionElement("Night Owl Potion", "Increases night vision", "ToastyQoL/Content/UI/PotionUI/Textures/nightOwl", BuffID.NightOwl, () => true,
                Weights.PreBoss),

                new PotionElement("Obsidian Skin", "Provides immunity to lava", "ToastyQoL/Content/UI/PotionUI/Textures/obsidianSkin", BuffID.ObsidianSkin, () => true,
                Weights.PreBoss),

                new PotionElement("Rage", "Increases critical chance by 10%", "ToastyQoL/Content/UI/PotionUI/Textures/rage", BuffID.Rage, () => true,
                Weights.PreBoss),

                new PotionElement("Regeneration", "Provides life regeneration", "ToastyQoL/Content/UI/PotionUI/Textures/regeneration", BuffID.Regeneration, () => true,
                Weights.PreBoss),

                new PotionElement("Shine", "Emits an aura of light", "ToastyQoL/Content/UI/PotionUI/Textures/shine", BuffID.Shine, () => true,
                Weights.PreBoss),

                new PotionElement("Sonar", "Detects hooked fish", "ToastyQoL/Content/UI/PotionUI/Textures/sonar", BuffID.Sonar, () => true,
                Weights.PreBoss),

                new PotionElement("Spelunker", "Shows the location of treasure and ore", "ToastyQoL/Content/UI/PotionUI/Textures/spelunker", BuffID.Spelunker, () => true,
                Weights.PreBoss),

                new PotionElement("Summoning", "Increases your max number of minions by 1", "ToastyQoL/Content/UI/PotionUI/Textures/summoning", BuffID.Summoning, () => true,
                Weights.PreBoss),

                new PotionElement("Swiftness", "25% increased movement speed", "ToastyQoL/Content/UI/PotionUI/Textures/swiftness", BuffID.Swiftness, () => true,
                Weights.PreBoss),

                new PotionElement("Thorns", "Attackers also take damage", "ToastyQoL/Content/UI/PotionUI/Textures/thorns", BuffID.Thorns, () => true,
                Weights.PreBoss),

                new PotionElement("Tipsy", "Increased melee abilities, lowered defense", "ToastyQoL/Content/UI/PotionUI/Textures/ale", BuffID.Tipsy, () => true,
                Weights.PreBoss),

                new PotionElement("Titan", "Increases knockback", "ToastyQoL/Content/UI/PotionUI/Textures/titan", BuffID.Titan, () => true,
                Weights.PreBoss),

                new PotionElement("Warmth", "Reduces damage from cold sources", "ToastyQoL/Content/UI/PotionUI/Textures/warmth", BuffID.Warmth, () => true,
                Weights.PreBoss),

                new PotionElement("Water Walking", "Allows the ability to walk on water", "ToastyQoL/Content/UI/PotionUI/Textures/waterWalking", BuffID.WaterWalking, () => true,
                Weights.PreBoss),

                new PotionElement("Wrath", "Increases damage by 10%", "ToastyQoL/Content/UI/PotionUI/Textures/wrath", BuffID.Wrath, () => true,
                Weights.PreBoss)
            });

            CurrentlySelectedMod = vanilla;
            CurrentSortingMode = new AToZPotionSort();
        }

        public static PotionMod RegisterPotionMod(PotionMod mod)
        {
            if (ModsRelatedPotions.Keys.Any(pm => pm.Name == mod.Name))
            {
                ToastyQoL.Instance.Logger.Warn($"Warning: A mod with name \"{mod.Name}\" has already been registered.");
                return null;
            }

            ModsRelatedPotions.Add(mod, new());
            return mod;
        }

        public static PotionElement AddElementToModList(string modName,  PotionElement element)
        {
            PotionMod mod = null;

            foreach (var potionMod in  ModsRelatedPotions.Keys)
            {
                if (potionMod.Name == modName)
                {
                    mod = potionMod;
                    break;
                }
            }

            if (mod == null) 
            {
                ToastyQoL.Instance.Logger.Warn($"Warning: A mod with name \"{mod.Name}\" is not registered.");
                return element;
            }

            if (ModsRelatedPotions.TryGetValue(mod, out var list))
                list.Add(element);

            return element;
        }

        public static bool ModIsRegistered(string modName)
        {
            foreach (PotionMod mod in  ModsRelatedPotions.Keys)
            {
                if (mod.Name == modName)
                    return true;
            }
            return false;
        }

        // Structure is as follows:
        //
        // Player tag - The TagCompound for the entire mod player.
        // Mod tag - The TagCompound for a specific mod. Multiple of these are stored in Player Tag.
        // Potion Tag - The TagCompound for a specific potion. Multiple of these are stored in a single Mod Tag.
        public static void SavePotions(TagCompound playerTag)
        {
            foreach (var keyPair in ModsRelatedPotions)
            {
                playerTag.TryGet(keyPair.Key.Name, out TagCompound modTag);

                modTag ??= new TagCompound();

                foreach (PotionElement potionElement in keyPair.Value)
                {
                    modTag.TryGet(potionElement.PotionName + potionElement.PotionDescription, out TagCompound potionTag);
                    potionTag ??= new TagCompound();

                    potionElement.Save(potionTag);

                    modTag[potionElement.PotionName + potionElement.PotionDescription] = potionTag;
                }

                playerTag[keyPair.Key.Name] = modTag;
            }

            playerTag["PotionSortingType"] = CurrentSortingMode.Name;
        }

        public static void LoadPotions(TagCompound playerTag)
        {
            SelectedPotions = new();

            foreach (var keyPair in ModsRelatedPotions)
            {
                if (playerTag.TryGet<TagCompound>(keyPair.Key.Name, out var modTag))
                {
                    foreach (PotionElement potionElement in keyPair.Value)
                    {
                        if (modTag.TryGet<TagCompound>(potionElement.PotionName + potionElement.PotionDescription, out var potionTag))
                            potionElement.Load(potionTag);

                        if (potionElement.Selected)
                            SelectedPotions.Add(potionElement);
                    }
                }
            }

            AToZPotionSort aToZPotionSort = new();

            WeightPotionSort weightPotionSort = new();

            string sortingName = playerTag.GetString("PotionSortingType");

            if (sortingName == aToZPotionSort.Name)
                CurrentSortingMode = aToZPotionSort;
            else
                CurrentSortingMode = weightPotionSort;
        }
        #endregion

        #region Drawing
        public static void Draw(SpriteBatch spriteBatch)
        {
            BasePotionDrawPos = new(-260, -110);
            float opacity = 1;

            if (!IsDrawing)
                return;

            TogglesUIManager.CloseUI();
            IsDrawing = true;

            // Draw the background.
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/baseSettingsUIBackgroundPotions", (AssetRequestMode)2).Value;
            Vector2 drawCenter;
            drawCenter.X = Main.screenWidth / 2;
            drawCenter.Y = Main.screenHeight / 2;
            // This spawn pos is very important. As it is affected by Main.screenWidth/Height, it will scale properly. Every single thing you draw needs to use
            // this vector, unless they are a completely new one and use Main.screenWidth.Height themselves for the VERY BASE of their definition.
            Vector2 spawnPos = drawCenter;

            float scale = 1;

            spriteBatch.Draw(backgroundTexture, spawnPos, null, Color.White * opacity, 0, backgroundTexture.Size() * 0.5f, scale, 0, 0);

            // Block the mouse if we are hovering over it.
            Rectangle hoverArea = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size() * scale);
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(hoverArea);
            if (isHovering)
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;

            // Draw close button.
            Texture2D closeButtonTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D closeButtonGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;

            Vector2 closeButtonDrawPos = new Vector2(276, -179) + spawnPos;
            Rectangle closeButtonRect = Utils.CenteredRectangle(closeButtonDrawPos, closeButtonTexture.Size() * 0.8f * scale);

            if (mouseHitbox.Intersects(closeButtonRect))
            {
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                spriteBatch.Draw(closeButtonGlowTexture, closeButtonDrawPos, null, Color.White * opacity, 0, closeButtonGlowTexture.Size() * 0.5f, 0.8f * scale, 0, 0);
                Main.hoverItemName = "[c/de4444:Close Menu]";
                if (Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease)
                {
                    // ON CLICK AFFECT
                    IsDrawing = false;
                    SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    TogglesUIManager.OpenUI();
                }
            }
            spriteBatch.Draw(closeButtonTexture, closeButtonDrawPos, null, Color.White * opacity, 0, closeButtonTexture.Size() * 0.5f, 0.8f * scale, 0, 0);

            if (SelectedPotions.Count > 0)
            {
                closeButtonDrawPos = new Vector2(273, -18) + spawnPos;
                closeButtonRect = Utils.CenteredRectangle(closeButtonDrawPos, closeButtonTexture.Size() * 0.8f * scale);
                if (mouseHitbox.Intersects(closeButtonRect))
                {
                    Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                    spriteBatch.Draw(closeButtonGlowTexture, closeButtonDrawPos, null, Color.White * opacity, 0, closeButtonGlowTexture.Size() * 0.5f, 0.8f * scale, 0, 0);
                    Main.hoverItemName = "[c/de4444:Clear Potions]";
                    if (Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease)
                    {
                        // ON CLICK AFFECT
                        foreach (var potionElement in SelectedPotions) 
                            potionElement.Selected = false;

                        SelectedPotions.Clear();

                        SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    }
                }
                spriteBatch.Draw(closeButtonTexture, closeButtonDrawPos, null, Color.White * opacity, 0, closeButtonTexture.Size() * 0.5f, 0.8f * scale, 0, 0);
            }

            DrawStaticText(spriteBatch, spawnPos, opacity);

            DrawPotionElements(spriteBatch, spawnPos, opacity);

            DrawModToggleIcons(spriteBatch, spawnPos, opacity);

            DrawSortToggleIcons(spriteBatch, spawnPos, opacity);
            // Draw the active bit. Pass index through to get the position to draw it :p
            DrawActivePotions(spriteBatch, spawnPos, opacity);
        }

        private static void DrawSortToggleIcons(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            Texture2D alphaTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/PotionUI/Textures/alpha", (AssetRequestMode)2).Value;
            Texture2D alphaGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/PotionUI/Textures/alphaGlow", (AssetRequestMode)2).Value;
            Texture2D progressionTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/PotionUI/Textures/progression", (AssetRequestMode)2).Value;
            Texture2D progressionGlowTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/PotionUI/Textures/progressionGlow", (AssetRequestMode)1).Value;
            Vector2 alphaDrawPos = spawnPos + new Vector2(10, -155);
            Vector2 progressionDrawPos = spawnPos + new Vector2(40, -155);
            Color alphaColor;
            Color progressionColor;
            float alphaScale = 1f;
            float progressionScale = 1f;

            if (CurrentSortingMode is AToZPotionSort)
            {
                alphaColor = new Color(40, 40, 40) * 0.7f;
                progressionColor = Color.White;
            }
            else
            {
                alphaColor = Color.White;
                progressionColor = new Color(40, 40, 40) * 0.7f;
            }

            spriteBatch.Draw(alphaTexture, alphaDrawPos, null, alphaColor * opacity, 0, alphaTexture.Size() * 0.5f, alphaScale, 0, 0);
            spriteBatch.Draw(progressionTexture, progressionDrawPos, null, progressionColor * opacity, 0, progressionGlowTexture.Size() * 0.5f, progressionScale, 0, 0);

            Rectangle alphaHitbox = Utils.CenteredRectangle(alphaDrawPos, alphaTexture.Size());
            Rectangle progressionHitbox = Utils.CenteredRectangle(progressionDrawPos, progressionTexture.Size());
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);

            if (mouseHitbox.Intersects(alphaHitbox) && CurrentSortingMode is not AToZPotionSort)
            {
                alphaScale = 1.1f;
                Main.hoverItemName = "[c/22a851:Sort by Name]";
                spriteBatch.Draw(alphaGlowTexture, alphaDrawPos, null, alphaColor * opacity, 0, alphaGlowTexture.Size() * 0.5f, alphaScale, 0, 0);
                if (Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease)
                {
                    CurrentSortingMode = new AToZPotionSort();
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
            }
            else if (mouseHitbox.Intersects(progressionHitbox) && CurrentSortingMode is AToZPotionSort)
            {
                progressionScale = 1.1f;
                Main.hoverItemName = "[c/c05d32:Sort by Progression]";
                spriteBatch.Draw(progressionGlowTexture, progressionDrawPos, null, progressionColor * opacity, 0, progressionGlowTexture.Size() * 0.5f, progressionScale, 0, 0);
                if (Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease)
                {
                    CurrentSortingMode = new WeightPotionSort();
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
            }
        }

        // Yes this goes out of bounds if too many mods exist. However, i don't expect this to happen anytime soon so will put off fixing that until (if) it becomes an issue.
        private static void DrawModToggleIcons(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            Vector2 iconDrawPos = spawnPos + new Vector2(-260, -155);

            foreach (PotionMod potionMod in ModsRelatedPotions.Keys) 
            { 
                Color drawColor = Color.White;
                if (potionMod == CurrentlySelectedMod)
                    drawColor = new Color(40, 40, 40) * 0.7f;

                float scale = 1f;

                Rectangle hitbox = Utils.CenteredRectangle(iconDrawPos, potionMod.SmallUIModIcon.Size());
                if (ToastyQoLUtils.MouseRectangle.Intersects(hitbox) && potionMod != CurrentlySelectedMod)
                {
                    scale = 1.1f;
                    Main.hoverItemName = $"[c/22a851:{potionMod.Name} Potions]";
                    spriteBatch.Draw(potionMod.SmallUIModIconGlow, iconDrawPos, null, drawColor * opacity, 0, potionMod.SmallUIModIconGlow.Size() * 0.5f, scale, 0, 0);

                    if (ToastyQoLUtils.CanAndHasClickedUIElement)
                    {
                        CurrentlySelectedMod = potionMod;
                        SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                    }
                }
                spriteBatch.Draw(potionMod.SmallUIModIcon, iconDrawPos, null, drawColor * opacity, 0, potionMod.SmallUIModIcon.Size() * 0.5f, scale, 0, 0);
                iconDrawPos.X += 30f;
            }
        }

        private static void DrawPotionElements(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);
            Vector2 potionDrawPos = spawnPos + BasePotionDrawPos;
            PotionElement potionElement;
            bool isHovering;
            // Loop through every element in the list.
            int index = -1;
            for (int i = 0; i < CurrentPotionElements.Count; i++)
            {
                // Get the PotionElement from the list using the index.
                potionElement = CurrentPotionElements[i];
                float scale = potionElement.Scale;             

                float maxWidth = 8f;

                if (i > 0)
                {
                    if (i % maxWidth == 0)
                    {
                        potionDrawPos.X -= HorizontalOffset * (maxWidth - 1f);
                        potionDrawPos.Y += 48;
                    }
                    else
                        potionDrawPos.X += HorizontalOffset;
                }

                Texture2D whiteRectTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleWhiteRect", (AssetRequestMode)2).Value;
                Rectangle whiteRectangle = Utils.CenteredRectangle(potionDrawPos, whiteRectTexture.Size() * 0.8f);
                // The area where we will be hovering. This will be changed to the white background later.
                bool shouldBeGreen = false;
                if (potionElement.Selected)
                {
                    // If its in the dict, add one to the index. This is used to position things in the active potions section.
                    index++;
                    shouldBeGreen = true;
                }
                // Is it in our current progression?
                bool drawRedBackground = !potionElement.IsAvailable();
                // Are we hovering over it?
                isHovering = mouseHitbox.Intersects(whiteRectangle);
                // If we arent hovering and should draw the red background,
                if (!isHovering && drawRedBackground || shouldBeGreen)
                {
                    //draw the red background
                    Texture2D redRectTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleRedRect", (AssetRequestMode)2).Value;
                    Texture2D greenRectTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleGreenRect", (AssetRequestMode)2).Value;
                    Texture2D yellowRectTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleYellowRect", (AssetRequestMode)2).Value;
                    if (drawRedBackground && !shouldBeGreen)
                        spriteBatch.Draw(redRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else if (shouldBeGreen && !drawRedBackground)
                        spriteBatch.Draw(greenRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else
                        spriteBatch.Draw(yellowRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                }
                if (isHovering)
                {
                    Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                    Texture2D redRectHoverTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleRedHoverRect", (AssetRequestMode)2).Value;
                    Texture2D greenRectHoverTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleGreenHoverRect", (AssetRequestMode)2).Value;
                    Texture2D yellowRectHoverTexture = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleYellowHoverRect", (AssetRequestMode)2).Value;
                    // Draw the white background if none others should be drawn.
                    if (drawRedBackground && shouldBeGreen)
                        spriteBatch.Draw(yellowRectHoverTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectHoverTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else if (drawRedBackground)
                        spriteBatch.Draw(redRectHoverTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectHoverTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else if (shouldBeGreen)
                        spriteBatch.Draw(greenRectHoverTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, whiteRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else
                        spriteBatch.Draw(whiteRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, whiteRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    // Draw the glow sprite.
                    spriteBatch.Draw(potionElement.PotionGlowTexture, potionDrawPos, null, Color.White, 0, potionElement.PotionGlowTexture.Size() * 0.5f, scale, 0, 0);

                    // Run the side information draw code here. Pass through potionElement for this.
                    DrawPotionSideInformation(spriteBatch, spawnPos, potionElement, opacity);

                    // If this is true, we have clicked it.
                    if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                    {
                        // Set the generic click timer to its length, but a bit shorter in this case.
                        TogglesUIManager.ClickCooldownTimer = (int)(TogglesUIManager.ClickCooldownLength * 0.75f);

                        SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);

                        // Check if this potion is already in the dictonary of selected potions. If so, remove it.

                        if (potionElement.Selected)
                        {
                            // Remove it.
                            potionElement.Selected = false;
                            SelectedPotions.Remove(potionElement);
                        }
                        else
                        {
                            // Else, add it.
                            potionElement.Selected = true;
                            SelectedPotions.Add(potionElement);
                        }
                    }

                }

                spriteBatch.Draw(potionElement.PotionTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, scale, 0, 0);
            }
        }

        private static void DrawPotionSideInformation(SpriteBatch spriteBatch, Vector2 spawnPos, PotionElement potionElement, float opacity)
        {
            spriteBatch.Draw(potionElement.PotionTexture, spawnPos + new Vector2(90, -152), null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
            // Draw the name.
            float vertical = -152;
            if (potionElement.Scale != 1)
                vertical = -155;
            
            Utils.DrawBorderString(spriteBatch, potionElement.PotionName, spawnPos + new Vector2(80, vertical) + new Vector2(potionElement.PotionTexture.Size().X * potionElement.Scale + 5, 0), Color.LawnGreen * opacity, 0.8f);

            // Draw the description.
            int maxChars = 36;
            string description = potionElement.PotionDescription;
            List<char[]> extratext = new();
            float yPos = 0f;

            if (potionElement.PotionDescription.Length > maxChars)
            {
                extratext = description.Chunk(maxChars).ToList();

                string currentText = string.Empty;
                foreach (char[] characters in extratext)
                {
                    foreach (char character in characters)
                        currentText += character;

                    // Remove any spaces at the start of new lines.
                    if (currentText.StartsWith(' '))
                        currentText = currentText.Remove(0, 1);

                    // Add a "-" to not awkwardly cut off words.
                    if (currentText.Last() != ' ')
                        currentText += '-';

                    Utils.DrawBorderString(spriteBatch, currentText, spawnPos + new Vector2(80, -132 + yPos), new Color(255, 204, 68) * opacity, 0.6f);
                    yPos += FontAssets.MouseText.Value.MeasureString(currentText).Y * 0.5f;
                    currentText = string.Empty;
                }
            }
            else
            {
                Utils.DrawBorderString(spriteBatch, potionElement.PotionDescription, spawnPos + new Vector2(80, -132), new Color(255, 204, 68) * opacity, 0.6f);
                yPos += FontAssets.MouseText.Value.MeasureString(description).Y * 0.5f;
            }
            Vector2 size = FontAssets.MouseText.Value.MeasureString(potionElement.PotionDescription);
            bool drawRedText = !potionElement.IsAvailable();

            Vector2 redTextDrawPos = new(80f, -132f + yPos - 14f);

            if (SelectedPotions.Contains(potionElement))
            {
                Utils.DrawBorderString(spriteBatch, "Currently Equipped", spawnPos + redTextDrawPos + new Vector2(0, size.Y / 1.6f), new Color(50, 200, 50) * opacity, 0.6f);
                if (drawRedText)
                    redTextDrawPos.Y += 15f;
            }

            if (drawRedText)
                Utils.DrawBorderString(spriteBatch, "Past your progression", spawnPos + redTextDrawPos + new Vector2(0, size.Y / 1.6f), new Color(200, 50, 50) * opacity, 0.6f);
        }

        private static void DrawStaticText(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            string currentPotionsText = "Active Potions:";
            Utils.DrawBorderString(spriteBatch, currentPotionsText, spawnPos + new Vector2(80, -22), Color.White * opacity);
            string currentSortText = "Sort Mode:";
            Utils.DrawBorderString(spriteBatch, currentSortText, spawnPos + new Vector2(-100, -163), Color.White * opacity);
        }

        private static void DrawActivePotions(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            // Define simple position variables.
            Vector2 basePotionPos = new Vector2(87, 22) + spawnPos;
            Vector2 basePotionInterval;
            Vector2 potionDrawPos;
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);
            Rectangle hoverHitbox;
            int index = 0;
            int index2;
            int row = 0;
            // go through every potion in the lists.
            for (int i = 0; i < SelectedPotions.Count; i++)
            {
                PotionElement potionElement = SelectedPotions[i];

                index2 = index;
                if (index2 >= 7 && index2 < 14)
                {
                    row = 1;
                    index2 -= 7;
                }
                else if (index2 >= 14 && index2 < 21)
                {
                    row = 2;
                    index2 -= 14;
                }
                else if (index2 >= 21 && index2 < 28)
                {
                    row = 3;
                    index2 -= 21;
                }

                basePotionInterval = new(30 * index2, 45 * row);
                potionDrawPos = basePotionPos + basePotionInterval;
                //ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/circleGreenRect", (AssetRequestMode)2).Value
                spriteBatch.Draw(potionElement.PotionTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
                index++;
                hoverHitbox = Utils.CenteredRectangle(potionDrawPos, potionElement.PotionTexture.Size() * potionElement.Scale);
                if (mouseHitbox.Intersects(hoverHitbox))
                {
                    Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                    spriteBatch.Draw(potionElement.PotionGlowTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
                    DrawPotionSideInformation(spriteBatch, spawnPos, potionElement, opacity);
                    Main.hoverItemName = "Remove";
                    if ((Main.mouseLeft && Main.mouseLeftRelease || Main.mouseRight && Main.mouseRightRelease) && TogglesUIManager.ClickCooldownTimer == 0)
                    {
                        TogglesUIManager.ClickCooldownTimer = 10;
                        potionElement.Selected = false;
                        SelectedPotions.Remove(potionElement);
                        SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                        index--;
                    }
                }    
            }

            string no = SelectedPotions.Count.ToString();
            Utils.DrawBorderString(spriteBatch, no, spawnPos + new Vector2(200, -22), Color.White * opacity, 1f);
            
        }
        #endregion

        #region Methods
        public static void GiveBuffs(Player player)
        {
            foreach (var element in SelectedPotions)
                    player.AddBuff(element.PotionBuffID, 2);
        }
        #endregion
    }
}
