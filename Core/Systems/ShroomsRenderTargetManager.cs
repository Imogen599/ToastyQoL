using CalamityMod;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using CalNohitQoL.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalNohitQoL.Core.Systems
{
    public class ShroomsRenderTargetManager : ModSystem
    {
        private static RenderTarget2D ShroomsRenderTarget;

        public static bool ShouldPreDraw = false;

        public static MiscShaderData ShroomsShader => GameShaders.Misc["CalNohitQoL:Shrooms"];

        #region Overrides
        public override void Load()
        {
            On.Terraria.Main.DrawInfernoRings += DrawShroomsRenderTarget;
            Main.OnResolutionChanged += ResizeShroomsRenderTarget;
            Main.OnPreDraw += DrawToRenderTarget;
        }

        private void DrawToRenderTarget(GameTime obj)
        {
            ShouldPreDraw = true;
            if (Main.gameMenu)
                return;
            if (!Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().NostTrippy || !Toggles.ProperShrooms)
                return;

            // Swap to the custom render target to prepare things to pixelation.
            ShroomsRenderTarget.SwapToRenderTarget();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            // Draw each projectile.
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active)
                {
                    Main.instance.DrawProj(projectile.whoAmI);

                    if (projectile.ModProjectile is IAdditiveDrawer additive)
                        additive.AdditiveDraw(Main.spriteBatch);
                }
            }

            // Draw each NPC
            foreach (NPC npc in Main.npc)
            {
                if (npc.active)
                    Main.instance.DrawNPC(npc.whoAmI, npc.behindTiles);
            }

            GeneralParticleHandler.DrawAllParticles(Main.spriteBatch);
            ShouldPreDraw = false;
            // Clear the current render target.
            Main.graphics.GraphicsDevice.SetRenderTarget(null);
            Main.spriteBatch.End();
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

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null);

                // Draw the RT.
                if (Toggles.ShroomShader)
                {
                    ShroomsShader.UseColor(Main.DiscoColor);
                    ShroomsShader.Apply();
                }
                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
                Main.spriteBatch.Draw(ShroomsRenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0f);
                Main.spriteBatch.ExitShaderRegion();
            }
            orig(self);
        }

        #endregion
    }
}
