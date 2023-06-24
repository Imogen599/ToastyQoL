using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Content.UI.Pages;

namespace ToastyQoL.Content.UI.UIManagers
{
    public class TogglesPage : IToggleWheelElement
    {
        #region Statics/Constants
        public static Dictionary<string, TogglesPage> UIManagers
        {
            get;
            private set;
        } = new();

        public Texture2D UIBackgroundTexture => UseSmallerBackground ? UIBackgroundTextureSmall : UIBackgroundTextureLarge;
        public static Texture2D UIBackgroundTextureLarge => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/baseSettingsUIBackground", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D UIBackgroundTextureSmall => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/baseSettingsUIBackgroundSmall", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D HoverBackgroundTexture => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/whiteTangle", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D HoverBackgroundSmallTexture => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/SmallerWhiteRect", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D ArrowTexture => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Arrow").Value;
        public static Texture2D ArrowGlowTexture => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/ArrowGlow").Value;

        public static TogglesPage GetPageFromString(string managerName)
        {
            if (UIManagers.TryGetValue(managerName, out var value))
                return value;
            return null;
        }

        public const int MaxElementsPerPage = 5;

        public const int ElementVerticalOffset = 60;

        #endregion

        #region Fields/Properties
        public int CurrentPage = 1;

        public int MaxPages
        {
            get
            {
                if (UIElements is not null && UIElements.Count > 6)
                    // For example, 6 elements would give 1.2, which is rounded up to two pages.
                    return (int)MathF.Ceiling(UIElements.Count / (float)MaxElementsPerPage);
                return 1;
            }
        }

        /// <summary>
        /// Initialize this manually.
        /// </summary>
        public List<PageUIElement> UIElements;

        public readonly string Name;

        public readonly bool UseSmallerBackground;

        /// <summary>
        /// The offset to draw the UI at from the center of the screen. 300, 0  is default.
        /// </summary>
        public readonly Vector2 BaseDrawPositionOffset;

        public Texture2D IconTexture { get; set; }

        public string Description { get; set; }

        public Action OnClick { get; set; }

        public float Layer { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Represents a page of ui toggles. Create an instance and call <see cref="TryRegister"/> to add a new page to the main UI wheel.
        /// </summary>
        /// <param name="uIElements">A list of UI elements for the page to have.</param>
        /// <param name="name">The lookup name for this UI manager internally</param>
        /// <param name="useSmallerBackground">Whether to use a smaller background for the UI</param>
        /// <param name="drawOffset">Whether to use a different offset from the center of the screen from the default of 300, 0</param>
        public TogglesPage(List<PageUIElement> uIElements, string name, string description, Texture2D wheelIconTexture, float layer, bool useSmallerBackground = false)
        {
            UIElements = uIElements;
            Name = name;
            Description = description;
            IconTexture = wheelIconTexture;
            Layer = layer;
            UseSmallerBackground = useSmallerBackground;
            OnClick = null;
            BaseDrawPositionOffset = new(300f, 0f);
        }
        #endregion

        #region Instance Methods
        internal bool IsDrawing;

        public bool ShouldDraw
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

        /// <summary>
        /// Call this to register the manager into the dictonary.
        /// </summary>
        public TogglesPage TryRegister()
        {
            if (UIManagers.ContainsKey(Name))
            {
                // If it contains the key, but not the value, then throw an error informing that a different name needs to be used.
                if (!UIManagers.ContainsValue(this))
                    throw new Exception("Name is already in use by another UIManager, please pick a different one.");

                return this;
            }

            UIManagers.Add(Name, this);
            TogglesUIManager.SortWheel();
            return this;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the UI background.
            Vector2 bgDrawPosition = ToastyQoLUtils.ScreenCenter + BaseDrawPositionOffset;
            spriteBatch.Draw(UIBackgroundTexture, bgDrawPosition, null, Color.White, 0f, UIBackgroundTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);

            // Block the mouse if the background is behing hovered over.
            Rectangle hoverArea = Utils.CenteredRectangle(bgDrawPosition, UIBackgroundTexture.Size());
            if (hoverArea.Intersects(ToastyQoLUtils.MouseRectangle))
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;

            if (CurrentPage == 0)
                CurrentPage = 1;

            int maxElement = CurrentPage * MaxElementsPerPage;
            int minElement = maxElement - MaxElementsPerPage;

            float baseDrawOffset = UseSmallerBackground ? UIBackgroundTexture.Height * 0.15f : UIBackgroundTexture.Height * 0.25f;
            if (MaxPages <= 1)
                baseDrawOffset = UIBackgroundTexture.Height * 0.31f;


            // 6 Elements in total has an exception, to remove the arrows and let them all fit on one page.
            if (UIElements.Count == 6)
            {
                maxElement++;
                baseDrawOffset *= 1.185f;
            }

            // Sort the elements by layer.
            UIElements = UIElements.OrderBy(x => x.Layer).ToList();

            Vector2 elementDrawPosition = bgDrawPosition - Vector2.UnitY * baseDrawOffset;

            for (int i = minElement; i < maxElement; i++)
            {
                // Leave if not in range.
                if (!UIElements.IndexInRange(i))
                    break;

                var currentElement = UIElements[i];
                currentElement.Draw(elementDrawPosition, UIBackgroundTexture.Width);
                elementDrawPosition.Y += ElementVerticalOffset; 
            }

            // Draw the page ticks.
            if (MaxPages > 1)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    Vector2 arrowDrawPosition = bgDrawPosition - new Vector2(UIBackgroundTexture.Width * 0.345f * i, UIBackgroundTexture.Height * (UseSmallerBackground ? 0.345f : 0.392f));

                    Rectangle whiteHitbox = Utils.CenteredRectangle(arrowDrawPosition, HoverBackgroundSmallTexture.Size());
                    if (whiteHitbox.Intersects(ToastyQoLUtils.MouseRectangle))
                    {
                        spriteBatch.Draw(HoverBackgroundSmallTexture, arrowDrawPosition, null, Color.White * 0.3f, 0f, HoverBackgroundSmallTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);

                        if (ToastyQoLUtils.CanAndHasClickedUIElement)
                        {
                            // On click effects.
                            TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                            SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);

                            CurrentPage += i;

                            // Ensure it stays in bounds.
                            if (CurrentPage <= 0)
                                CurrentPage = MaxPages;
                            else if (CurrentPage > MaxPages)
                                CurrentPage = 1;
                        }
                    }

                    SpriteEffects arrowEffect = i == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                    Rectangle arrowHitbox = Utils.CenteredRectangle(arrowDrawPosition, ArrowTexture.Size());
                    if (arrowHitbox.Intersects(ToastyQoLUtils.MouseRectangle))
                        spriteBatch.Draw(ArrowGlowTexture, arrowDrawPosition, null, Color.White, 0f, ArrowGlowTexture.Size() * 0.5f, 1f, arrowEffect, 0f);
                    else
                        spriteBatch.Draw(ArrowTexture, arrowDrawPosition, null, Color.White, 0f, ArrowTexture.Size() * 0.5f, 1f, arrowEffect, 0f);
                }
            }
        }
        #endregion
    }
}
