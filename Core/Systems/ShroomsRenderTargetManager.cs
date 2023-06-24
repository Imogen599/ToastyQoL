using ToastyQoL.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace ToastyQoL.Core.Systems
{
    public class ShroomsRenderTargetManager : ModSystem
    {
        private static RenderTarget2D ShroomsRenderTarget;

        internal static bool ShouldPreDraw = false;

        public static List<Action<SpriteBatch>> ExtraDrawMethods
        {
            get;
            private set;
        }

        public static MiscShaderData ShroomsShader => GameShaders.Misc["ToastyQoL:Shrooms"];

        #region Overrides
        public override void Load()
        {
            On.Terraria.Main.DrawInfernoRings += DrawShroomsRenderTarget;
            Main.OnResolutionChanged += ResizeShroomsRenderTarget;
            Main.OnPreDraw += DrawToRenderTarget;
            ExtraDrawMethods = new();
        }

        public override void Unload()
        {
            On.Terraria.Main.DrawInfernoRings -= DrawShroomsRenderTarget;
            Main.OnResolutionChanged -= ResizeShroomsRenderTarget;
            Main.OnPreDraw -= DrawToRenderTarget;
            ExtraDrawMethods = null;
        }

        private void DrawToRenderTarget(GameTime obj)
        {
            ShouldPreDraw = true;

            if (Main.gameMenu || !Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().NostTrippy || !Toggles.ProperShrooms)
                return;

            ShroomsRenderTarget.SwapToRenderTarget();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active)
                    Main.instance.DrawProj(projectile.whoAmI);
            }

            foreach (NPC npc in Main.npc)
            {
                if (npc.active)
                    Main.instance.DrawNPC(npc.whoAmI, npc.behindTiles);
            }

            foreach (var drawMethod in ExtraDrawMethods)
                drawMethod(Main.spriteBatch);

            Main.graphics.GraphicsDevice.SetRenderTarget(null);
            Main.spriteBatch.End();

            ShouldPreDraw = false;
        }

        private void ResizeShroomsRenderTarget(Vector2 obj)
        {
            // If it is not null and not already disposed, dispose it.
            if (ShroomsRenderTarget != null && !ShroomsRenderTarget.IsDisposed)
                ShroomsRenderTarget.Dispose();

            // Recreate the render target with the current, accurate screen dimensions.
            ShroomsRenderTarget = new(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
        }

        private void DrawShroomsRenderTarget(On.Terraria.Main.orig_DrawInfernoRings orig, Main self)
        {
            if (Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().NostTrippy && Toggles.ProperShrooms)
            {
                if (ShroomsRenderTarget is null)
                    ResizeShroomsRenderTarget(Vector2.Zero);

                Effect effect = null;
                // Draw the RT.
                if (Toggles.ShroomShader)
                {
                    ShroomsShader.UseColor(Main.DiscoColor.ToVector3());
                    ShroomsShader.Apply();
                    effect = ShroomsShader.Shader;
                }

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, effect);

                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0f);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
            }
            orig(self);
        }

        #endregion
    }
}
