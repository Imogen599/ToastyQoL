using Terraria;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.Projectiles
{
    public class DollStrike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("We Do A Little Damaging");
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.scale = 100f;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 9;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 3;
            Projectile.alpha = 255;
            Projectile.timeLeft = 750;
        }

        //CanHit is set in GlobalPlayer
    }
}
