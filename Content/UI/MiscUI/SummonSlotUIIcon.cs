using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ToastyQoL.Content.UI.MiscUI
{
    public class SummonSlotUIIcon
    {
        // This is a basic UI element. We define this in our Mod Class, and draw it in ModifyInterfaceLayers in a ModSystem Class.

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Variables to get all of the Minion Slot related info we want to show with this UI element.
            Player player = Main.LocalPlayer;
            float totalMinionSlots = player.maxMinions;
            float MinionSlotsFree = totalMinionSlots - player.slotsMinions;
            string maxMinions = totalMinionSlots.ToString();
            string freeMinions = MinionSlotsFree.ToString();
            string amountOfMinions = player.slotsMinions.ToString();
            string smallMinionText = amountOfMinions + "/" + maxMinions;

            // The Textures of the icon, and when you hover over it (optional).
            Texture2D Icon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/MiscUI/summonUI", (AssetRequestMode)2).Value;
            Texture2D HoverIcon = ModContent.Request<Texture2D>("ToastyQoL/Content/UI/MiscUI/summonUIHover", (AssetRequestMode)2).Value;

            // These set the center of the icon, and the "hitbox" around it. Play around with the Vector floats to change position.
            // This does not scale properly
            // Vector2 IconCenter = new Vector2((float)Main.screenWidth - /*Main.UIScale*/ 285f, /*Main.UIScale*/ (float)Main.screenHeight - 5f) + Utils.Size(Icon) * 0.5f;
            // This stays in the same place, do it like this :)
            Vector2 IconCenter = new Vector2(Main.screenWidth - 400f, 10f) + Icon.Size() * 0.5f;
            // Rectangle area of the icon to check for hovering.
            Rectangle iconRectangeArea = Utils.CenteredRectangle(IconCenter, Icon.Size() * Main.UIScale);



            // This is for the hover text. We draw it offset from the Center of the icon, and the final float changes the size of it.
            Vector2 minionSlotsTextArea = FontAssets.MouseText.Value.MeasureString(smallMinionText);
            Vector2 minionSlotsTextDrawPosition = IconCenter + new Vector2(6f, 16f) - minionSlotsTextArea * 0.5f;

            // This gets the "hitbox" of the mouse, and checks if its intersecting with the "hitbox" of our icon.
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(iconRectangeArea);

            // If we are hovering over it, change the Icon Texture to the Hover Icon Texture.
            if (isHovering)
            {
                Icon = HoverIcon;
            }

            // Now, draw the icon in the correct place. Use Color.White here.
            spriteBatch.Draw(Icon, IconCenter, null, Color.White, 0f, Icon.Size() * 0.5f, 1, 0, 0f);

            //spriteBatch.Draw(Icon, drawCenter, null, Color.White, 0f, Utils.Size(Icon) * 0.5f, 1f, (SpriteEffects)0, 0f);
            // This is optional, it simply makes the colorToUse a gradient between red and green based on your free minion slots.
            Color colorToUse = Color.Lerp(Color.Red, Color.Green, MinionSlotsFree / totalMinionSlots);
            // This draws the text over the Icon, using the above color and text we defined in the variables.
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, smallMinionText, minionSlotsTextDrawPosition, colorToUse, 0f, Vector2.Zero, Vector2.One * Main.UIScale * 0.5f, -1f, 2f);

            // We check if we're hovering again
            if (isHovering)
            {
                // If so, use the Interpolated String Handler to make a string that displays more in depth text.
                // You do not need to do it this way, you could use a simple string that you manually format, but this is more readable and nicer.
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new(3, 4);
                defaultInterpolatedStringHandler.AppendFormatted("Max Minion Slots: " + maxMinions);
                defaultInterpolatedStringHandler.AppendLiteral("\n");
                defaultInterpolatedStringHandler.AppendFormatted("Free Minion Slots: " + freeMinions);
                defaultInterpolatedStringHandler.AppendLiteral("\n");
                defaultInterpolatedStringHandler.AppendFormatted("Used Minion Slots: " + amountOfMinions);

                // Set Main.hoverItemName to our string appear when hovered over.
                Main.hoverItemName = defaultInterpolatedStringHandler.ToStringAndClear();
            }
        }




    }
}