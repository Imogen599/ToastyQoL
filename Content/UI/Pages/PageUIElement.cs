using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static ToastyQoL.Content.UI.UIManagers.TogglesPage;

namespace ToastyQoL.Content.UI.Pages
{
    public class PageUIElement
    {
        public Func<string> DescriptionText;
        public Func<string> HoverText;

        public Texture2D Texture;
        public Texture2D GlowTexture;
        public float Layer;
        public FieldInfo AssosiatedField;
        public Action OnClickAction;
        public ToggleBlockInformation? BlockInformation;

        public const string ColorTag = "c/ffcc44:";
        public const string DisabledTag = "c/de4444:";
        public const string EnabledText = "[c/44de5a:Enabled]";
        public const string DisabledText = $"[{DisabledTag}Disabled]";

        public static Texture2D Lock => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/lock", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D LockGlow => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/lockGlow", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D Tick => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Tick", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D TickGlow => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/TickGlow", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D Cross => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/Cross", AssetRequestMode.ImmediateLoad).Value;
        public static Texture2D CrossGlow => ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/CrossGlow", AssetRequestMode.ImmediateLoad).Value;

        public static Vector2 IndicatorOffset => new(10f, 10f);

        public string HoverTextFormatted => $"[{ColorTag}{HoverText()}]";

        public PageUIElement(Texture2D texture, Texture2D glowTexture, Func<string> descriptionText, Func<string> hoverText, float layer, Action onClickAction = null, FieldInfo assosiatedField = null, ToggleBlockInformation? blockInformation = null)
        {
            Texture = texture;
            GlowTexture = glowTexture;
            DescriptionText = descriptionText;
            HoverText = hoverText;
            Layer = layer;
            AssosiatedField = assosiatedField;
            OnClickAction = onClickAction;
            BlockInformation = blockInformation;
        }

        public void Draw(Vector2 drawPosition, float backgroundWidth)
        {
            Vector2 iconDrawPosition = drawPosition - Vector2.UnitX * (backgroundWidth * 0.35f);
            Rectangle iconRectangle = Utils.CenteredRectangle(iconDrawPosition, Texture.Size());
            bool drawIcon = true;

            bool blocked = false;
            if (BlockInformation != null)
                blocked = !BlockInformation.Value.CanToggle();

            // Check if the element is being hovered over.
            Rectangle selectionRectangle = Utils.CenteredRectangle(drawPosition, HoverBackgroundTexture.Size());
            if (selectionRectangle.Intersects(ToastyQoLUtils.MouseRectangle))
            {

                Main.spriteBatch.Draw(HoverBackgroundTexture, drawPosition, null, Color.White * 0.15f, 0f, HoverBackgroundTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0.1f);

                // If hovering over the icon, draw the glow texture.
                if (iconRectangle.Intersects(ToastyQoLUtils.MouseRectangle))
                {
                    drawIcon = false;
                    Main.spriteBatch.Draw(GlowTexture, iconDrawPosition, null, Color.White, 0f, GlowTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0.2f);

                    string extraText = string.Empty;
                    if (blocked)
                        extraText = "\n" + $"[{DisabledTag}{BlockInformation.Value.ExtraHoverText}]";
                    Main.hoverItemName = HoverTextFormatted + extraText;
                }

                // Handle interactions
                if (ToastyQoLUtils.CanAndHasClickedUIElement && !blocked)
                {
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                    OnClickAction();
                }
            }

            if (drawIcon)
                Main.spriteBatch.Draw(Texture, iconDrawPosition, null, Color.White, 0f, Texture.Size() * 0.5f, 1f, SpriteEffects.None, 0.2f);

            if (blocked)
            {
                Rectangle lockRectangle = Utils.CenteredRectangle(iconDrawPosition + IndicatorOffset, Lock.Size());
                if (lockRectangle.Intersects(ToastyQoLUtils.MouseRectangle))
                    Main.spriteBatch.Draw(LockGlow, iconDrawPosition + IndicatorOffset, null, Color.White, 0f, LockGlow.Size() * 0.5f, 1f, SpriteEffects.None, 0.25f);
                else
                    Main.spriteBatch.Draw(Lock, iconDrawPosition + IndicatorOffset, null, Color.White, 0f, Lock.Size() * 0.5f, 1f, SpriteEffects.None, 0.25f);
            }

            else if (AssosiatedField != null)
            {
                if (AssosiatedField.FieldType == typeof(bool))
                {
                    bool toggleStatus = (bool)AssosiatedField.GetValue(null);

                    Texture2D indicatorTexture = blocked ? Lock : toggleStatus ? Tick : Cross;
                    Texture2D indicatorGlowTexture = blocked ? LockGlow : toggleStatus ? TickGlow : CrossGlow;

                    Rectangle indicatorRectangle = Utils.CenteredRectangle(iconDrawPosition + IndicatorOffset, indicatorTexture.Size());
                    if (indicatorRectangle.Intersects(ToastyQoLUtils.MouseRectangle))
                    {
                        Main.spriteBatch.Draw(indicatorGlowTexture, iconDrawPosition + IndicatorOffset, null, Color.White, 0f, indicatorGlowTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0.25f);

                        // Also update the hover text, if it isnt already been set due to being blocked.
                        if (!blocked)
                            Main.hoverItemName = HoverTextFormatted + "\n" + (toggleStatus ? EnabledText : DisabledText);
                    }
                    else
                        Main.spriteBatch.Draw(indicatorTexture, iconDrawPosition + IndicatorOffset, null, Color.White, 0f, indicatorTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0.25f);
                }
            }

            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, DescriptionText(), iconDrawPosition.X + 25, iconDrawPosition.Y - 7, Color.White, Color.Black, Vector2.Zero, 0.75f);
        }
    }
}
