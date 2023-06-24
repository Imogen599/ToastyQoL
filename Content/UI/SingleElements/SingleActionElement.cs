using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ToastyQoL.Content.UI.SingleElements
{
    public class SingleActionElement : IToggleWheelElement
    {
        #region Statics
        public static Dictionary<string, SingleActionElement> UISingleElements
        {
            get;
            private set;
        } = new();
        #endregion

        #region Fields/Properties
        public readonly string Name;

        public Texture2D IconTexture { get; set; }

        public string Description { get; set; }

        public Action OnClick { get; set; }

        public float Layer { get; set; }

        public Action<SpriteBatch> DrawMethod { get; private set; }

        public SingleActionElement(string name, Texture2D iconTexture, string description, Action onClick, float layer, Action<SpriteBatch> drawMethod = null)
        {
            Name = name;
            IconTexture = iconTexture;
            Description = description;
            OnClick = onClick;
            Layer = layer;
            DrawMethod = drawMethod;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Call this to register the element into the dictonary.
        /// </summary>
        public SingleActionElement TryRegister()
        {
            if (UISingleElements.ContainsKey(Name))
            {
                // If it contains the key, but not the value, then throw an error informing that a different name needs to be used.
                if (!UISingleElements.ContainsValue(this))
                    throw new Exception("Name is already in use by another single element, please pick a different one.");

                return this;
            }

            UISingleElements.Add(Name, this);
            TogglesUIManager.SortWheel();
            return this;
        }

        public void Draw(SpriteBatch spriteBatch) => DrawMethod?.Invoke(spriteBatch);
        #endregion
    }
}
