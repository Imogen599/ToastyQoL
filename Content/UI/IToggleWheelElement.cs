using Microsoft.Xna.Framework.Graphics;
using System;

namespace ToastyQoL.Content.UI
{
    public interface IToggleWheelElement
    {
        public Texture2D IconTexture { get; set; }

        public string Description { get; set; }

        public Action OnClick { get; set; }

        public float Layer { get; set; }

        public void Draw(SpriteBatch spriteBatch);
    }
}
