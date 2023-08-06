using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using ToastyQoL.Content.Buffs;

namespace ToastyQoL.Content.Projectiles
{
    public class ToasterProj : ModProjectile
    {
        
        public Player Owner => Main.player[Projectile.owner];
        
        public ref float Timer => ref Projectile.ai[0];
        
        //public ThanatosSmokeParticleSet SmokeDrawer = new(-1, 3, 0f, 16f, 0.4f);
       
        public bool HoleBelow
        {
            get
            {
                int tileWidth = 5;
                int tileX = (int)(Projectile.Center.X / 16f) - tileWidth;
                if (Projectile.velocity.X > 0f)
                    tileX += tileWidth;

                int tileY = (int)(Projectile.Bottom.Y / 16f);
                for (int y = tileY; y < tileY + 2; y++)
                {
                    for (int x = tileX; x < tileX + tileWidth; x++)
                    {
                        if (Main.tile[x, y].HasTile)
                            return false;
                    }
                }
                return true;
            }
        }
       
        public int playerStill;

        public bool fly;
       
        public bool easyfix = true;
        
        public override string Texture => "ToastyQoL/Assets/ExtraTextures/toasterSheet";
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 30;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {

            Vector2 center2 = Projectile.Center;
            Vector2 distance = Owner.Center - center2;
            float playerDistance = distance.Length();
            fallThrough = playerDistance > 200f;
            return true;
        }

        public override void AI()
        {

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!Owner.dead && Owner.HasBuff(ModContent.BuffType<ToasterBuff>()))
                Projectile.timeLeft = 2;

            #region Movement
            if (easyfix)
            {
                Projectile.position.Y += -3f;
                easyfix = false;
            }
            if (!fly)
            {
                Projectile.rotation = 0f;
                Vector2 center2 = Projectile.Center;
                Vector2 vector4 = Owner.Center - center2;
                float playerDistance2 = vector4.Length();
                if (Projectile.velocity.Y == 0f && (HoleBelow || playerDistance2 > 110f && Projectile.position.X == Projectile.oldPosition.X))
                {
                    Projectile.velocity.Y = -5f;
                }
                Projectile.velocity.Y += 0.2f;
                if (Projectile.velocity.Y > 7f)
                {
                    Projectile.velocity.Y = 7f;
                }
                if (playerDistance2 > 600f)
                {
                    fly = true;
                    Projectile.velocity.X = 0f;
                    Projectile.velocity.Y = 0f;
                    Projectile.tileCollide = false;
                }
                if (playerDistance2 > 100f)
                {
                    if (Owner.position.X - Projectile.position.X > 0f)
                    {
                        Projectile.velocity.X += 0.1f;
                        if (Projectile.velocity.X > 7f)
                        {
                            Projectile.velocity.X = 7f;
                        }
                    }
                    else
                    {
                        Projectile.velocity.X -= 0.1f;
                        if (Projectile.velocity.X < -7f)
                        {
                            Projectile.velocity.X = -7f;
                        }
                    }
                }
                if (playerDistance2 < 100f && Projectile.velocity.X != 0f)
                {
                    if (Projectile.velocity.X > 0.5f)
                    {
                        Projectile.velocity.X -= 0.15f;
                    }
                    else if (Projectile.velocity.X < -0.5f)
                    {
                        Projectile.velocity.X += 0.15f;
                    }
                    else if (Projectile.velocity.X < 0.5f && Projectile.velocity.X > -0.5f)
                    {
                        Projectile.velocity.X = 0f;
                    }
                }
            }
            else if (fly)
            {
                float num16 = 0.3f;
                Projectile.tileCollide = false;
                Vector2 vector3 = new(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float horiPos = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector3.X;
                float vertiPos = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector3.Y;
                vertiPos += Main.rand.Next(-10, 21);
                horiPos += Main.rand.Next(-10, 21);
                horiPos += 60f * (0f - Main.player[Projectile.owner].direction);
                vertiPos -= 60f;
                float playerDistance = (float)Math.Sqrt((double)(horiPos * horiPos + vertiPos * vertiPos));
                float num17 = 18f;
                Math.Sqrt((double)(horiPos * horiPos + vertiPos * vertiPos));
                if (playerDistance > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
                    Projectile.netUpdate = true;
                }
                if (playerDistance < 100f)
                {
                    num16 = 0.1f;
                    if (Owner.velocity.Y == 0f)
                    {
                        playerStill++;
                    }
                    else
                    {
                        playerStill = 0;
                    }
                    if (playerStill > 60 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                    {
                        fly = false;
                        Projectile.tileCollide = true;
                    }
                }
                if (playerDistance < 50f)
                {
                    if (Math.Abs(Projectile.velocity.X) > 2f || Math.Abs(Projectile.velocity.Y) > 2f)
                    {
                        Projectile projectile = Projectile;
                        projectile.velocity *= 0.9f;
                    }
                    num16 = 0.01f;
                }
                else
                {
                    if (playerDistance < 100f)
                    {
                        num16 = 0.1f;
                    }
                    if (playerDistance > 300f)
                    {
                        num16 = 1f;
                    }
                    playerDistance = num17 / playerDistance;
                    horiPos *= playerDistance;
                    vertiPos *= playerDistance;
                }
                if (Projectile.velocity.X <= horiPos)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num16;
                    if (num16 > 0.05f && Projectile.velocity.X < 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num16;
                    }
                }
                if (Projectile.velocity.X > horiPos)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num16;
                    if (num16 > 0.05f && Projectile.velocity.X > 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num16;
                    }
                }
                if (Projectile.velocity.Y <= vertiPos)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num16;
                    if (num16 > 0.05f && Projectile.velocity.Y < 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num16 * 2f;
                    }
                }
                if (Projectile.velocity.Y > vertiPos)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num16;
                    if (num16 > 0.05f && Projectile.velocity.Y > 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num16 * 2f;
                    }
                }
                Projectile.rotation = Projectile.velocity.X * 0.03f;
            }
            if (Projectile.velocity.X > 0.25f)
            {
                Projectile.spriteDirection = -1;
            }
            else if (Projectile.velocity.X < -0.25f)
            {
                Projectile.spriteDirection = 1;
            }
            #endregion

            //if (Projectile.frame == 3)
             //   SmokeDrawer.ParticleSpawnRate = 12;
            //else
               // SmokeDrawer.ParticleSpawnRate = 99999999;
            //SmokeDrawer.BaseMoveRotation = MathHelper.PiOver2;
            //SmokeDrawer.Update();

            if (Timer >= 600 && Timer < 700)
            {
                if (Projectile.frame < 3 && Timer % 2 == 0)
                    Projectile.frame++;
            }

            if (Timer >= 900)
            {
                if (Projectile.frame > 0 && Timer % 2 == 0)
                    Projectile.frame--;
                else if (Projectile.frame == 0)
                    Timer = 0;
            }

            Timer++;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //SmokeDrawer.DrawSet(Projectile.Center + new Vector2(0, -10));
            return true;
        }
    }
}
