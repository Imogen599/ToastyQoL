using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using ToastyQoL.Core;

namespace ToastyQoL.Content.UI.MiscUI
{
    public class CheatIndicatorUIRenderer
    {
        public static void Draw(SpriteBatch spriteBatch)
        {

            Texture2D Icon;
            if (Toggles.GodmodeEnabled)
                Icon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/cheatGodUIIcon").Value;
            else if (Toggles.InfiniteFlightTime)
                Icon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/cheatWingsUIIcon").Value;
            else if (Toggles.InfiniteMana)
                Icon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/cheatManaUIIcon").Value;
            else if (Toggles.InstantDeath)
                Icon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/cheatDeathUIIcon").Value;
            else
                Icon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/baseUIIcon").Value;
            // The Textures of the icon, and when you hover over it.
            Texture2D HoverIcon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/UIIconOutline").Value;

            // These set the center of the icon, and the "hitbox" around it. Play around with the Vector floats to change position.
            // This does not scale properly
            // Vector2 IconCenter = new Vector2((float)Main.screen
            // Width - /*Main.UIScale*/ 285f, /*Main.UIScale*/ (float)Main.screenHeight - 5f) + Utils.Size(Icon) * 0.5f;
            // This stays in the same place, do it like this :)
            Vector2 IconCenter = new Vector2(Main.screenWidth - 350f, 80f) + Icon.Size() * 0.5f;
            // Rectangle area of the icon to check for hovering.
            Rectangle iconRectangeArea = Utils.CenteredRectangle(IconCenter, Icon.Size());

            // This gets the "hitbox" of the mouse, and checks if its intersecting with the "hitbox" of our icon.
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(iconRectangeArea);

            // If we are hovering over it, change the Icon Texture to the Hover Icon Texture.
            if (isHovering)
            {
                spriteBatch.Draw(HoverIcon, IconCenter, null, Color.White, 0f, Icon.Size() * 0.5f, 1, 0, 0f);

                string IconHighlight;
                if (Toggles.GodmodeEnabled)
                    IconHighlight = "[c/cdd00c:Godmode Enabled]";
                else if (Toggles.InfiniteFlightTime)
                    IconHighlight = "[c/78fa91:Infinite Flight Enabled]";
                else if (Toggles.InfiniteMana)
                    IconHighlight = "[c/393dc0:Infinite Mana Enabled]";
                else if (Toggles.InstantDeath)
                    IconHighlight = "[c/f92a07:Instant Death Enabled]";
                else
                    IconHighlight = "No Player Cheat";

                Main.hoverItemName = IconHighlight + "\n[c/ffcc44:Click to open UI!]";

                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                if (ToastyQoLUtils.CanAndHasClickedUIElement)
                {
                    if (TogglesUIManager.UIOpen)
                        TogglesUIManager.CloseUI();
                    else
                        TogglesUIManager.OpenUI();
                    TogglesUIManager.ClickCooldownTimer = TogglesUIManager.ClickCooldownLength;
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
            }

            // Now, draw the icon in the correct place.
            spriteBatch.Draw(Icon, IconCenter, null, Color.White, 0f, Icon.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
        }

    }
}