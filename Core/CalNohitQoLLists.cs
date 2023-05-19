using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.Projectiles.Melee;
using CalNohitQoL.Content.Items;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalNohitQoL.Core
{
    // Yes these are all manually updated.
    // Yes it is painful to maintain.
    public class CalNohitQoLLists
    {
        public static List<int> SCalTooltips { get; private set; }
        public static List<int> ShroomsDrawProjs { get; private set; }

        public static void LoadLists()
        {
            SCalTooltips = new List<int>()
            {
                ModContent.ItemType<ReflectiveWand>(),
                ModContent.ItemType<BrimstoneTorch>(),
            };
            ShroomsDrawProjs = new List<int>()
            {
                ModContent.ProjectileType<MurasamaSlash>(),
                ModContent.ProjectileType<AresDeathBeamStart>(),
                ModContent.ProjectileType<AstralGodRay>(),
                ModContent.ProjectileType<AresLaserBeamStart>(),
                ModContent.ProjectileType<BirbAura>(),
                ModContent.ProjectileType<ArtemisSpinLaserbeam>(),
                ModContent.ProjectileType<BrimstoneRay>(),
                ModContent.ProjectileType<DeusRitualDrama>(),
                ModContent.ProjectileType<DoGDeathBoom>(),
                ModContent.ProjectileType<DoGTeleportRift>(),
                ModContent.ProjectileType<DoGP1EndPortal>(),
                ModContent.ProjectileType<AresDeathBeamStart>(),
                ModContent.ProjectileType<DraedonSummonLaser>(),
                ModContent.ProjectileType<DyingSun>(),
                ModContent.ProjectileType<HolyAura>(),
                ModContent.ProjectileType<HealOrbProv>(),
                ModContent.ProjectileType<HolyLight>(),
                ModContent.ProjectileType<HolyBurnOrb>(),
                ModContent.ProjectileType<InfernadoRevenge>(),
                ModContent.ProjectileType<MajesticSparkle>(),
                ModContent.ProjectileType<ProvidenceHolyRay>(),
                ModContent.ProjectileType<RedLightning>(),
                ModContent.ProjectileType<SCalRitualDrama>(),
                ModContent.ProjectileType<ThanatosBeamStart>(),
                ModContent.ProjectileType<TornadoHostile>(),
                ModContent.ProjectileType<AresGaussNukeProjectile>(),
                ModContent.ProjectileType<AresGaussNukeProjectileBoom>(),
                ModContent.ProjectileType<AresDeathBeamTelegraph>(),
                ModContent.ProjectileType<AresTeslaOrb>(),
                ModContent.ProjectileType<ArtemisLaser>(),
                ModContent.ProjectileType<ThanatosLaser>(),
                ModContent.ProjectileType<BrimstoneTargetRay>(),
                ModContent.ProjectileType<ThanatosBeamTelegraph>(),
                ModContent.ProjectileType<DoGDeath>(),
                ModContent.ProjectileType<StormWeaverFrostWaveTelegraph>(),
                ModContent.ProjectileType<DarkEnergyBall>(),
                ModContent.ProjectileType<DarkEnergyBall2>(),
                657, 466,455,447,923,919,872,961,962,
            };
        }
        public static void UnloadLists()
        {            
            SCalTooltips = null;
            ShroomsDrawProjs = null;
        }
    }
}