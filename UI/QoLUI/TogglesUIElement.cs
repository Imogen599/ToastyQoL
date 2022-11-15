using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalNohitQoL.UI.QoLUI
{
    // This uses the system from CalTestHelpers, only changed to fit my new requirements.
    public struct TogglesUIElement
    {
        // Text to show up when hovering.
        public string Description;

        // The Texture of the icon.
        public Texture2D IconTexture;

        // What happens when we click it. Action doesnt do much by itself, we check for being clicked elsewhere.
        public Action OnClick;

        // An alternate texture for if this things toggle is on or off.
        public Texture2D AltTexture;

        public TogglesUIElement(string description, Texture2D icon, Action onClickEffect = null, Texture2D altTexture = default)
        {
            Description = description;
            IconTexture = icon;
            OnClick = onClickEffect;
            AltTexture = altTexture;
        }

    }
}
