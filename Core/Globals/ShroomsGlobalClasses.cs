using CalamityMod;
using CalNohitQoL.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Core.Globals
{
    public class ShroomsPlayer : ModPlayer
    {
        public bool NostTrippy = false;
        public bool DoubleTrippy = false;

        public override void ResetEffects()
        {
            NostTrippy = false;
            DoubleTrippy = false;
        }
        public override void UpdateDead()
        {
            NostTrippy = false;
            DoubleTrippy = false;
        }
        public override void PostUpdateBuffs()
        {
            if (Toggles.ShroomsExtraDamage && DoubleTrippy)
            {
                Player.GetDamage<GenericDamageClass>() += 0.5f;
            }
            else if (Toggles.ShroomsExtraDamage && NostTrippy)
            {
                Player.GetDamage<GenericDamageClass>() += 0.5f;
            }
            if (Player.Calamity().trippy && !Toggles.ShroomsExtraDamage)
            {
                Player.GetDamage<GenericDamageClass>() -= 0.5f;
            }
        }
    }

    public class ShroomsGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (Toggles.ProperShrooms)
                return null;
            if (Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().NostTrippy || Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().DoubleTrippy)
            {
                return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, projectile.alpha);
            }
            return null;
        }

        public override bool PreAI(Projectile projectile)
        {
            if (!Toggles.GravestonesEnabled && (projectile.type == 43 || projectile.type == 201 || projectile.type == 202 || projectile.type == 203 || projectile.type == 204 || projectile.type == 205 || projectile.type == 527 || projectile.type == 528 || projectile.type == 529 || projectile.type == 530 || projectile.type == 531))
            {
                projectile.active = false;
                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                return false;
            }
            return true;
        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (Toggles.ProperShrooms)
                return ShroomsRenderTargetManager.ShouldPreDraw;
            Player MainPlayer = Main.player[Main.myPlayer];
            if (MainPlayer.GetModPlayer<ShroomsPlayer>().DoubleTrippy)
            {
                Texture2D texture2D = TextureAssets.Projectile[projectile.type].Value;
                SpriteEffects effects = SpriteEffects.None;
                if (projectile.spriteDirection == -1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                float num = 0f;
                Vector2 origin = new Vector2(texture2D.Width / 2, texture2D.Height / Main.projFrames[projectile.type] / 2);
                Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
                Color alpha = projectile.GetAlpha(newColor);
                float num2 = 0.99f;
                alpha.R = (byte)(alpha.R * num2);
                alpha.G = (byte)(alpha.G * num2);
                alpha.B = (byte)(alpha.B * num2);
                alpha.A = (byte)(alpha.A * num2);
                for (int i = 0; i < 8; i++)
                {
                    Vector2 position = projectile.position;
                    float num3 = Math.Abs(projectile.Center.X - Main.player[Main.myPlayer].Center.X);
                    float num4 = Math.Abs(projectile.Center.Y - Main.player[Main.myPlayer].Center.Y);
                    if (i == 0 || i == 2)
                    {
                        position.X = Main.player[Main.myPlayer].Center.X + num3;
                    }
                    else
                    {
                        position.X = Main.player[Main.myPlayer].Center.X - num3;
                    }
                    if (i == 0 || i == 1)
                    {
                        position.Y = Main.player[Main.myPlayer].Center.Y + num4;
                    }
                    else
                    {
                        position.Y = Main.player[Main.myPlayer].Center.Y - num4;
                    }
                    if (i >= 4)
                    {
                        if (i == 4 || i == 6)
                        {
                            position.X = Main.player[Main.myPlayer].Center.X + num4;
                        }
                        else
                        {
                            position.X = Main.player[Main.myPlayer].Center.X - num4;
                        }
                        if (i == 4 || i == 5)
                        {
                            position.Y = Main.player[Main.myPlayer].Center.Y + num3;
                        }
                        else
                        {
                            position.Y = Main.player[Main.myPlayer].Center.Y - num3;
                        }
                    }
                    //position.Y = Main.player[Main.myPlayer].Center.X + num4;//7
                    //position.Y = Main.player[Main.myPlayer].Center.X - num4;//8
                    int frameHeight = texture2D.Height / Main.projFrames[projectile.type];
                    int y = frameHeight * projectile.frame;
                    position.X -= projectile.width / 2;
                    position.Y -= projectile.height / 2;//end of y

                    //Main.spriteBatch.EnterShaderRegion();

                    //GameShaders.Misc["CalNohitQoL:Hologram"].UseImage1("Images/Misc/Perlin");
                    //GameShaders.Misc["CalNohitQoL:Hologram"].UseOpacity(1f);
                    //GameShaders.Misc["CalNohitQoL:Hologram"].UseColor(Color.White);
                    //GameShaders.Misc["CalNohitQoL:Hologram"].Apply(); 

                    Main.spriteBatch.Draw(texture2D, new Vector2(position.X - Main.screenPosition.X + projectile.width / 2 - texture2D.Width * projectile.scale / 2f + origin.X * projectile.scale, position.Y - Main.screenPosition.Y + projectile.height - texture2D.Height * projectile.scale / Main.projFrames[projectile.type] + 4f + origin.Y * projectile.scale + num + projectile.gfxOffY), new Rectangle(0, y, texture2D.Width, frameHeight), alpha, projectile.rotation, origin, projectile.scale, effects, 0f);

                    //Main.spriteBatch.ExitShaderRegion();
                }
                return false;
            }
            else if (MainPlayer.GetModPlayer<ShroomsPlayer>().NostTrippy)
            {
                Texture2D texture2D = TextureAssets.Projectile[projectile.type].Value;
                SpriteEffects effects = 0;
                if (projectile.spriteDirection == -1)
                {
                    effects = (SpriteEffects)1;
                }

                Vector2 origin = new Vector2(texture2D.Width / 2, texture2D.Height / Main.projFrames[projectile.type] / 2);
                Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
                Color alpha = projectile.GetAlpha(newColor);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 position = projectile.position;
                    float num3 = Math.Abs(projectile.Center.X - MainPlayer.Center.X);
                    float num4 = Math.Abs(projectile.Center.Y - MainPlayer.Center.Y);
                    if (i == 0 || i == 2)
                    {
                        position.X = MainPlayer.Center.X + num3;
                    }
                    else
                    {
                        position.X = MainPlayer.Center.X - num3;
                    }
                    position.X -= projectile.width / 2;
                    if (i == 0 || i == 1)
                    {
                        position.Y = MainPlayer.Center.Y + num4;
                    }
                    else
                    {
                        position.Y = MainPlayer.Center.Y - num4;
                    }


                    //position.Y = Main.player[Main.myPlayer].Center.X + num4;//7
                    //position.Y = Main.player[Main.myPlayer].Center.X - num4;//8
                    int frames = texture2D.Height / Main.projFrames[projectile.type];
                    int y = frames * projectile.frame;
                    position.Y -= projectile.height / 2;//end of y
                    Main.spriteBatch.Draw(texture2D, new Vector2(position.X - Main.screenPosition.X + projectile.width / 2 - texture2D.Width * projectile.scale / 2f + origin.X * projectile.scale, position.Y - Main.screenPosition.Y + projectile.height - texture2D.Height * projectile.scale / Main.projFrames[projectile.type] + 4f + origin.Y * projectile.scale + projectile.gfxOffY), (Rectangle?)new Rectangle(0, y, texture2D.Width, frames), alpha, projectile.rotation, origin, projectile.scale, effects, 0f);
                }
                return false;
            }
            return true;
        }

    }

    public class ShroomsGlobalNPC : GlobalNPC
    {
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (Toggles.ProperShrooms || !ShroomsRenderTargetManager.ShouldPreDraw)
                return null;
            if (Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().NostTrippy || Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().DoubleTrippy)
            {
                return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, npc.alpha);
            }
            return null;
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Toggles.ProperShrooms)
                return ShroomsRenderTargetManager.ShouldPreDraw;
            if (Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().DoubleTrippy)
            {
                SpriteEffects effects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                float num = 0f;
                Vector2 origin = new Vector2(TextureAssets.Npc[npc.type].Value.Width / 2, TextureAssets.Npc[npc.type].Value.Height / Main.npcFrameCount[npc.type] / 2);
                Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
                Color alpha = npc.GetAlpha(newColor);
                float num2 = 0.99f;
                alpha.R = (byte)(alpha.R * num2);
                alpha.G = (byte)(alpha.G * num2);
                alpha.B = (byte)(alpha.B * num2);
                alpha.A = (byte)(alpha.A * num2);
                for (int i = 0; i < 8; i++)
                {
                    Vector2 position = npc.position;
                    float num3 = Math.Abs(npc.Center.X - Main.LocalPlayer.Center.X);
                    float num4 = Math.Abs(npc.Center.Y - Main.LocalPlayer.Center.Y);
                    if (i == 0 || i == 2)
                    {
                        position.X = Main.player[Main.myPlayer].Center.X + num3;
                    }
                    else
                    {
                        position.X = Main.player[Main.myPlayer].Center.X - num3;
                    }
                    if (i == 0 || i == 1)
                    {
                        position.Y = Main.player[Main.myPlayer].Center.Y + num4;
                    }
                    else
                    {
                        position.Y = Main.player[Main.myPlayer].Center.Y - num4;
                    }
                    if (i >= 4)
                    {
                        if (i == 4 || i == 6)
                        {
                            position.X = Main.player[Main.myPlayer].Center.X + num4;
                        }
                        else
                        {
                            position.X = Main.player[Main.myPlayer].Center.X - num4;
                        }
                        if (i == 4 || i == 5)
                        {
                            position.Y = Main.player[Main.myPlayer].Center.Y + num3;
                        }
                        else
                        {
                            position.Y = Main.player[Main.myPlayer].Center.Y - num3;
                        }
                    }
                    position.X -= npc.width / 2;
                    position.Y -= npc.height / 2;
                    Main.spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, new Vector2(position.X - Main.screenPosition.X + npc.width / 2 - TextureAssets.Npc[npc.type].Value.Width * npc.scale / 2f + origin.X * npc.scale, position.Y - Main.screenPosition.Y + npc.height - TextureAssets.Npc[npc.type].Value.Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + origin.Y * npc.scale + num + npc.gfxOffY), npc.frame, alpha, npc.rotation, origin, npc.scale, effects, 0f);
                }
                return false;
            }
            else if (Main.LocalPlayer.GetModPlayer<ShroomsPlayer>().NostTrippy)
            {
                SpriteEffects effects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                float num = 0f;
                Vector2 origin = new Vector2(TextureAssets.Npc[npc.type].Value.Width / 2, TextureAssets.Npc[npc.type].Value.Height / Main.npcFrameCount[npc.type] / 2);
                Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
                Color alpha = npc.GetAlpha(newColor);
                float num2 = 0.99f;
                alpha.R = (byte)(alpha.R * num2);
                alpha.G = (byte)(alpha.G * num2);
                alpha.B = (byte)(alpha.B * num2);
                alpha.A = (byte)(alpha.A * num2);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 position = npc.Center;
                    float num3 = Math.Abs(npc.Center.X - Main.LocalPlayer.Center.X);
                    float num4 = Math.Abs(npc.Center.Y - Main.LocalPlayer.Center.Y);
                    if (i == 0 || i == 2)
                    {
                        position.X = Main.player[Main.myPlayer].Center.X + num3;
                    }
                    else
                    {
                        position.X = Main.player[Main.myPlayer].Center.X - num3;
                    }
                    position.X -= npc.width / 2;
                    if (i == 0 || i == 1)
                    {
                        position.Y = Main.player[Main.myPlayer].Center.Y + num4;
                    }
                    else
                    {
                        position.Y = Main.player[Main.myPlayer].Center.Y - num4;
                    }
                    position.Y -= npc.height / 2;
                    Main.spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, new Vector2(position.X - Main.screenPosition.X + npc.width / 2 - TextureAssets.Npc[npc.type].Value.Width * npc.scale / 2f + origin.X * npc.scale, position.Y - Main.screenPosition.Y + npc.height - TextureAssets.Npc[npc.type].Value.Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + origin.Y * npc.scale + num + npc.gfxOffY), npc.frame, alpha, npc.rotation, origin, npc.scale, effects, 0f);
                }
                return false;
            }
            return true;
        }
    }
}
