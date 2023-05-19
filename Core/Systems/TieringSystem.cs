using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Typeless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Core.Systems
{
    // This is forced to be quite large considering the sheer amount of bosses that exist.
    public class TieringSystem : ModSystem
    {
        public delegate bool PastTierDelegate();

        public struct BossLockInformation
        {
            public PastTierDelegate PastTierCheck;

            public string BossName;

            public List<int> AssosiatedItemTypes;

            public float Weight;

            public BossLockInformation(PastTierDelegate pastTierCheck, string bossName, List<int> assosiatedItemTypes, float weight = 1f)
            {
                PastTierCheck = pastTierCheck;
                BossName = bossName;
                AssosiatedItemTypes = assosiatedItemTypes;
                Weight = weight;
            }

            public void AddItem(int itemType) => AssosiatedItemTypes.Add(itemType);
        }

        public static List<BossLockInformation> ItemsTieringInformation
        {
            get;
            private set;
        }

        public static List<BossLockInformation> PotionsTieringInformation
        {
            get;
            private set;
        }

        public static bool ItemShouldBeMarked(int itemType, out BossLockInformation assosiatedLockInformation)
        {
            assosiatedLockInformation = default;
            // Loop through every tier information.
            foreach (var lockInformation in ItemsTieringInformation)
            {
                // If the current item is assosiated,
                if (lockInformation.AssosiatedItemTypes.Contains(itemType))
                {
                    assosiatedLockInformation = lockInformation;
                    // And the past tier check fails,
                    if (!lockInformation.PastTierCheck())
                        return true;
                    // If it passes, it should not be marked
                    else
                        return false;
                }
            }
            return false;
        }

        public static bool PotionShouldBeMarked(int itemType, out BossLockInformation assosiatedLockInformation)
        {
            assosiatedLockInformation = default;
            // Loop through every tier information.
            foreach (var lockInformation in PotionsTieringInformation)
            {
                // If the current item is assosiated,
                if (lockInformation.AssosiatedItemTypes.Contains(itemType))
                {
                    assosiatedLockInformation = lockInformation;
                    // And the past tier check fails,
                    if (!lockInformation.PastTierCheck())
                        return true;
                    // If it passes, it should not be marked
                    else
                        return false;
                }
            }
            return false;
        }

        public static bool LockItemOrPotionFromUse(int itemType)
        {
            if (Toggles.ItemLock)
            {
                foreach (var lockInformation in ItemsTieringInformation)
                {
                    // If the current item is assosiated,
                    if (lockInformation.AssosiatedItemTypes.Contains(itemType))
                    {
                        // And the past tier check fails, return true.
                        if (!lockInformation.PastTierCheck())
                            return false;
                    }
                }
            }
            if (Toggles.PotionLock)
            {    
                foreach (var lockInformation in PotionsTieringInformation)
                {
                    // If the current item is assosiated,
                    if (lockInformation.AssosiatedItemTypes.Contains(itemType))
                    {
                        // And the past tier check fails, return true.
                        if (!lockInformation.PastTierCheck())
                            return false;
                    }
                }
            }
            return true;
        }

        public int GetMechsDead()
        {
            int amount = 0;
            if (NPC.downedMechBoss1)
                amount++;
            if (NPC.downedMechBoss2)
                amount++;
            if (NPC.downedMechBoss3)
                amount++;

            return amount;
        }

        public override void Load()
        {
            ItemsTieringInformation = new()
            {
                new BossLockInformation(() => NPC.downedSlimeKing,
                    "King Slime",
                    new()
                    {
                        ModContent.ItemType<CrownJewel>(),
                        ModContent.ItemType<LoreKingSlime>(),
                        ModContent.ItemType<HeartofDarkness>(),
                        ModContent.ItemType<Laudanum>(),
                        ModContent.ItemType<StressPills>(),
                        ItemID.SlimySaddle,
                        ItemID.SlimeHook,
                        ItemID.RoyalGel
                    }),

                new BossLockInformation(() => DownedBossSystem.downedDesertScourge,
                    "Desert Scourge",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<AeroStone>(),
                        ModContent.ItemType<OceanCrest>(),
                        ModContent.ItemType<AmidiasPendant>(),
                        ModContent.ItemType<SandCloak>(),
                        ModContent.ItemType<VoltaicJelly>(),
                        ModContent.ItemType<GiantPearl>(),
                        ModContent.ItemType<ShieldoftheOcean>(),
                        //Weapons
                        ModContent.ItemType<AquaticDischarge>(),
                        ModContent.ItemType<Riptide>(),
                        ModContent.ItemType<AmidiasTrident>(),
                        ModContent.ItemType<RedtideSpear>(),
                        ModContent.ItemType<UrchinMace>(),
                        ModContent.ItemType<UrchinFlail>(),
                        ModContent.ItemType<MonstrousKnives>(),
                        //Ranged Weapons
                        ModContent.ItemType<Barinade>(),
                        ModContent.ItemType<Shellshooter>(),
                        ModContent.ItemType<CoralCannon>(),
                        ModContent.ItemType<FirestormCannon>(),
                        ModContent.ItemType<ReedBlowgun>(),
                        ModContent.ItemType<StormSurge>(),
                        //Magic Weapons
                        ModContent.ItemType<AquamarineStaff>(),
                        ModContent.ItemType<StormSpray>(),
                        ModContent.ItemType<CoralSpout>(),
                        ModContent.ItemType<Waywasher>(),
                        ModContent.ItemType<SparklingEmpress>(),
                        //Summon Weapons
                        ModContent.ItemType<EnchantedConch>(),
                        ModContent.ItemType<PolypLauncher>(),
                        ModContent.ItemType<Cnidarian>(),
                        //Rogue Weapons
                        ModContent.ItemType<SeafoamBomb>(),
                        ModContent.ItemType<EnchantedAxe>(),
                        ModContent.ItemType<FishboneBoomerang>(),
                        ModContent.ItemType<SandDollar>(),
                        ModContent.ItemType<ScourgeoftheDesert>(),
                        ModContent.ItemType<SnapClam>(),
                    }),

                new BossLockInformation(() => NPC.downedBoss1,
                    "Eye of Cthulhu",
                    new()
                    {
                       //Weapons
                        ModContent.ItemType<Basher>(),
                        ModContent.ItemType<TeardropCleaver>(),
                        //Ranged Weapons
                        ModContent.ItemType<Toxibow>(),
                        //Magic Weapons
                        ModContent.ItemType<ParasiticSceptor>(),
                        ModContent.ItemType<SkyGlaze>(),
                        ModContent.ItemType<AcidGun>(),
                        //Summon Weapons
                        ModContent.ItemType<DeathstareRod>(),
                        ModContent.ItemType<RustyBeaconPrototype>(),
                        ModContent.ItemType<CausticCroakerStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<ContaminatedBile>(),
                        //Vanilla
                        ItemID.Binoculars,
                        ItemID.EoCShield,
                    }),

                new BossLockInformation(() => DownedBossSystem.downedCrabulon,
                    "Crabulon",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<FungalClump>(),
                        //Weapons
                        ModContent.ItemType<MycelialClaws>(),
                        //Ranged Weapons
                        ModContent.ItemType<Fungicide>(),
                        //Magic Weapons
                        ModContent.ItemType<HyphaeRod>(),
                        //Rogue Weapons
                        ModContent.ItemType<InfestedClawmerang>(),
                        ModContent.ItemType<Mycoroot>(),
                    }),

                 new BossLockInformation(() => CalNohitQoL.DownedBrain && CalNohitQoL.DownedEater,
                    "EoW/BoC",
                    new()
                    {
                       //Accessories
                        ModContent.ItemType<SilencingSheath>(),
                        ModContent.ItemType<EnchantedPearl>(),
                        //Weapons
                        ModContent.ItemType<CausticEdge>(),
                        //Magic Weapons
                        ModContent.ItemType<MagnaCannon>(),
                        //Summon Weapons
                        ModContent.ItemType<ScabRipper>(),
                        ModContent.ItemType<VileFeeder>(),
                        //Rogue Weapons
                        ModContent.ItemType<MeteorFist>(),
                        ModContent.ItemType<SludgeSplotch>(),
                        //Vanilla
                        ItemID.BrainOfConfusion,
                        ItemID.WormScarf,
                        ItemID.MeteorHelmet,
                        ItemID.MeteorSuit,
                        ItemID.MeteorLeggings,
                        ItemID.SpaceGun,
                        ItemID.StarCannon
                    }),

                 new BossLockInformation(() => DownedBossSystem.downedPerforator && DownedBossSystem.downedHiveMind,
                    "Hive Mind/Perforators",
                    new()
                    {
                       //Accessories   
                        ModContent.ItemType<HarpyRing>(),
                        ModContent.ItemType<BloodstainedGlove>(),
                        ModContent.ItemType<FilthyGlove>(),
                        ModContent.ItemType<BloodyWormTooth>(),
                        ModContent.ItemType<FeatherCrown>(),
                        ModContent.ItemType<RottenBrain>(),
                        ModContent.ItemType<SkylineWings>(),
                        //Weapons
                        ModContent.ItemType<BrokenBiomeBlade>(),
                        ModContent.ItemType<GaussDagger>(),
                        ModContent.ItemType<PerfectDark>(),
                        ModContent.ItemType<VeinBurster>(),
                        ModContent.ItemType<WindBlade>(),
                        ModContent.ItemType<AirSpinner>(),
                        ModContent.ItemType<Aorta>(),
                        ModContent.ItemType<GoldplumeSpear>(),             
                        //Ranged Weapons
                        ModContent.ItemType<Goobow>(),
                        ModContent.ItemType<LunarianBow>(),
                        ModContent.ItemType<Eviscerator>(),
                        ModContent.ItemType<BulletFilledShotgun>(),
                        ModContent.ItemType<Shadethrower>(),
                        ModContent.ItemType<AquashardShotgun>(),
                        ModContent.ItemType<FlurrystormCannon>(),
                        ModContent.ItemType<Taser>(),
                        //Magic Weapons
                        ModContent.ItemType<BloodBath>(),
                        ModContent.ItemType<ShaderainStaff>(),
                        ModContent.ItemType<PulsePistol>(),
                        ModContent.ItemType<Tradewinds>(),
                        //Summon Weapons
                        ModContent.ItemType<StarSwallowerContainmentUnit>(),
                        ModContent.ItemType<BloodClotStaff>(),
                        ModContent.ItemType<DankStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<TrackingDisk>(),
                        ModContent.ItemType<FeatherKnife>(),
                        ModContent.ItemType<Turbulance>(),
                        ModContent.ItemType<SkyStabber>(),
                        ModContent.ItemType<RotBall>(),
                        ModContent.ItemType<ToothBall>(),
                        //Classless
                        ModContent.ItemType<Aestheticus>(),
                        ModContent.ItemType<Skynamite>(),
                        ItemID.BundleofBalloons
                    }),

                  new BossLockInformation(() => NPC.downedQueenBee,
                    "Queen Bee",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<TheBee>(),
                        //Weapons
                        ModContent.ItemType<HardenedHoneycomb>(),
                        //Vanilla
                        ItemID.BeeHeadgear,
                        ItemID.BeeBreastplate,
                        ItemID.BeeGreaves,
                        ItemID.BeesKnees,
                        ItemID.BeeKeeper,
                        ItemID.BeeGun,
                        ItemID.HoneyComb,
                        ItemID.Nectar,
                        ItemID.HoneyedGoggles,
                        ItemID.Beenade,
                        ItemID.HiveBackpack
                    }),

                  new BossLockInformation(() => NPC.downedDeerclops,
                    "Deerclops",
                    new()
                    {
                        ItemID.BoneHelm,
                        ItemID.ChesterPetItem,
                        ItemID.Eyebrella,
                        ItemID.DontStarveShaderItem,
                        ItemID.PewMaticHorn,
                        ItemID.WeatherPain,
                        ItemID.HoundiusShootius,
                        ItemID.LucyTheAxe
                    }),

                  new BossLockInformation(() => NPC.downedBoss3,
                    "Skeletron",
                    new()
                    {
                       //Accessories
                        ModContent.ItemType<IronBoots>(),
                        ModContent.ItemType<CounterScarf>(),
                        ModContent.ItemType<MirageMirror>(),
                        ModContent.ItemType<OldDie>(),
                        ModContent.ItemType<AnechoicPlating>(),
                        ModContent.ItemType<DepthCharm>(),
                        ModContent.ItemType<ArchaicPowder>(),
                        //Weapons
                        ModContent.ItemType<BallOFugu>(),
                        //Ranged Weapons
                        ModContent.ItemType<Archerfish>(),
                        //Magic Weapons
                        ModContent.ItemType<AbyssShocker>(),
                        ModContent.ItemType<BlackAnurian>(),
                        //Summon Weapons
                        ModContent.ItemType<EyeOfNight>(),
                        ModContent.ItemType<StaffOfNecrosteocytes>(),
                        ModContent.ItemType<FleshOfInfidelity>(),
                        ModContent.ItemType<HerringStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<Glaive>(),
                        ModContent.ItemType<Kylie>(),
                        ModContent.ItemType<Cinquedea>(),
                        ModContent.ItemType<ShinobiBlade>(),
                        ModContent.ItemType<Lionfish>(),
                        //Vanilla
                        ItemID.ChippysCouch,
                        ItemID.BoneGlove,
                        ItemID.SkeletronHand,
                        ItemID.BookofSkulls,
                    }),

                  new BossLockInformation(() => DownedBossSystem.downedSlimeGod,
                    "Slime God",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<RadiantOoze>(),
                        ModContent.ItemType<JellyChargedBattery>(),
                        ModContent.ItemType<ManaPolarizer>(),
                        //ModContent.ItemType<BloodyEdge>(),
                        //Weapons
                        ModContent.ItemType<GeliticBlade>(),
                        ModContent.ItemType<TheGodsGambit>(),
                        ModContent.ItemType<FracturedArk>(),
                        //Ranged Weapons
                        ModContent.ItemType<GunkShot>(),
                        ModContent.ItemType<OverloadedBlaster>(),
                        //Magic Weapons
                        ModContent.ItemType<CarnageRay>(),
                        ModContent.ItemType<NightsRay>(),
                        ModContent.ItemType<AbyssalTome>(),
                        ModContent.ItemType<EldritchTome>(),
                        //Summon Weapons
                        ModContent.ItemType<CorroslimeStaff>(),
                        ModContent.ItemType<CrimslimeStaff>(),
                        ModContent.ItemType<SlimePuppetStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<GelDart>(),
                    }),

                  new BossLockInformation(() => Main.hardMode,
                    "Wall of Flesh",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<GrandGelatin>(),
                        ModContent.ItemType<AmalgamatedBrain>(),
                        ModContent.ItemType<BloodyWormScarf>(),
                        ModContent.ItemType<EvasionScarf>(),
                        ModContent.ItemType<RogueEmblem>(),
                        ModContent.ItemType<TheFirstShadowflame>(),
                        ModContent.ItemType<WifeinaBottle>(),
                        ModContent.ItemType<WifeinaBottlewithBoobs>(),
                        ModContent.ItemType<EyeoftheStorm>(),
                        ModContent.ItemType<GiantTortoiseShell>(),
                        ModContent.ItemType<HowlsHeart>(),
                        ModContent.ItemType<TheTransformer>(),
                        ModContent.ItemType<UrsaSergeant>(),
                        ModContent.ItemType<SpelunkersAmulet>(),
                        ModContent.ItemType<SupremeBaitTackleBoxFishingStation>(),
                        //Weapons
                        ModContent.ItemType<Aftershock>(),
                        ModContent.ItemType<AxeofPurity>(),
                        ModContent.ItemType<Carnage>(),
                        ModContent.ItemType<CelestialClaymore>(),
                        ModContent.ItemType<EutrophicScimitar>(),
                        ModContent.ItemType<EvilSmasher>(),
                        ModContent.ItemType<StormSaber>(),
                        ModContent.ItemType<TrueCausticEdge>(),
                        ModContent.ItemType<Roxcalibur>(),
                        ModContent.ItemType<YinYo>(),
                        ModContent.ItemType<EarthenPike>(),
                        ModContent.ItemType<ClamCrusher>(),
                        //Ranged Weapons
                        ModContent.ItemType<FlarewingBow>(),
                        ModContent.ItemType<Butcher>(),
                        ModContent.ItemType<ClamorRifle>(),
                        ModContent.ItemType<CursedCapper>(),
                        ModContent.ItemType<Needler>(),
                        ModContent.ItemType<P90>(),
                        ModContent.ItemType<SlagMagnum>(),
                        ModContent.ItemType<Meowthrower>(),
                        ModContent.ItemType<PolarisParrotfish>(),
                        ModContent.ItemType<MidasPrime>(),
                        //Magic Weapons
                        ModContent.ItemType<GloriousEnd>(),
                        ModContent.ItemType<FrigidflashBolt>(),
                        ModContent.ItemType<Poseidon>(),
                        ModContent.ItemType<Serpentine>(),
                        ModContent.ItemType<ClothiersWrath>(),
                        //Summon Weapons
                        ModContent.ItemType<AncientIceChunk>(),
                        ModContent.ItemType<BlackHawkRemote>(),
                        ModContent.ItemType<CausticStaff>(),
                        ModContent.ItemType<HauntedScroll>(),
                        ModContent.ItemType<ShellfishStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<BlastBarrel>(),
                        ModContent.ItemType<BlazingStar>(),
                        ModContent.ItemType<Equanimity>(),
                        ModContent.ItemType<CursedDagger>(),
                        ModContent.ItemType<Prismalline>(),
                        ModContent.ItemType<IchorSpear>(),
                        ModContent.ItemType<WaveSkipper>(),
                        ModContent.ItemType<BurningStrife>(),
                        ModContent.ItemType<GacruxianMollusk>(),
                        ModContent.ItemType<SlickCane>(),
                        //Classless
                        ModContent.ItemType<GoldenGun>(),
                        ModContent.ItemType<LunicEye>(),
                        ModContent.ItemType<StarStruckWater>(),
                        //Vanilla
                        ItemID.MechanicalGlove,
                        ItemID.DemonHeart,
                        ItemID.Pwnhammer,
                        ItemID.SorcererEmblem,
                        ItemID.WarriorEmblem,
                        ItemID.RangerEmblem,
                        ItemID.SummonerEmblem,
                        ItemID.BreakerBlade,
                        ItemID.ClockworkAssaultRifle,
                        ItemID.LaserRifle,
                        ItemID.FireWhip
                    }),

                  new BossLockInformation(() => NPC.downedQueenSlime,
                    "Queen Slime",
                    new()
                    {
                        ItemID.CrystalNinjaHelmet,
                        ItemID.CrystalNinjaChestplate,
                        ItemID.CrystalNinjaLeggings,
                        ItemID.VolatileGelatin,
                        ItemID.QueenSlimeHook,
                        ItemID.QueenSlimeMountSaddle,
                        ItemID.Smolstar
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedCryogen,
                    "Cryogen",
                    new()
                    {
                        ModContent.ItemType<SoulofCryogen>(),
                        ModContent.ItemType<StarlightWings>(),
                        ModContent.ItemType<FrostFlare>(),
                        ModContent.ItemType<AmbrosialAmpoule>(),
                        ModContent.ItemType<CryoStone>(),
                        ModContent.ItemType<OrnateShield>(),
                        ModContent.ItemType<PermafrostsConcoction>(),
                        ModContent.ItemType<ColdDivinity>(),
                        ModContent.ItemType<HoarfrostBow>(),
                        ModContent.ItemType<Icebreaker>(),
                        ModContent.ItemType<Avalanche>(),
                        ModContent.ItemType<SnowstormStaff>(),
                        ModContent.ItemType<KelvinCatalyst>(),
                        ModContent.ItemType<FrostbiteBlaster>(),
                        ModContent.ItemType<IcicleTrident>(),
                        ModContent.ItemType<IceStar>()
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedAquaticScourge,
                    "Aquatic Scourge",
                    new()
                    {
                        ModContent.ItemType<AquaticEmblem>(),
                        ModContent.ItemType<CorrosiveSpine>(),
                        ModContent.ItemType<NuclearRod>(),
                        ModContent.ItemType<DeepDiver>(),
                        ModContent.ItemType<SubmarineShocker>(),
                        ModContent.ItemType<Barinautical>(),
                        ModContent.ItemType<Downpour>(),
                        ModContent.ItemType<DeepseaStaff>(),
                        ModContent.ItemType<ScourgeoftheSeas>(),
                        ModContent.ItemType<SulphurousGrabber>(),
                        ModContent.ItemType<FlakToxicannon>(),
                        ModContent.ItemType<SlitheringEels>(),
                        ModContent.ItemType<BelchingSaxophone>(),
                        ModContent.ItemType<OrthoceraShell>(),
                        ModContent.ItemType<SkyfinBombers>(),
                        ModContent.ItemType<SpentFuelContainer>(),
                        ModContent.ItemType<Bonebreaker>(),
                        ModContent.ItemType<CorrodedCaustibow>(),
                        ModContent.ItemType<Miasma>(),
                        ModContent.ItemType<AcidicRainBarrel>()
                    }),

                   new BossLockInformation(() => NPC.downedMechBoss1,
                    "Destroyer",
                    new()
                    {
                        ModContent.ItemType<GaussPistol>(),
                        ItemID.Megashark
                    }),

                   new BossLockInformation(() => NPC.downedMechBoss1 && (NPC.downedMechBoss2 || NPC.downedMechBoss3),
                    "Destroyer and another Mech",
                    new()
                    {
                        ModContent.ItemType<MajesticGuard>()
                    }),

                   new BossLockInformation(() => NPC.downedMechBoss2,
                    "Twins",
                    new()
                    {
                        ModContent.ItemType<Arbalest>(),
                        ModContent.ItemType<SpeedBlaster>(),
                        ModContent.ItemType<FrequencyManipulator>(),
                        ModContent.ItemType<HydraulicVoltCrasher>(),
                        ItemID.MagicalHarp,
                        ItemID.RainbowRod,
                        ItemID.OpticStaff
                    }),

                   new BossLockInformation(() => NPC.downedMechBoss3,
                    "Skeletron Prime",
                    new()
                    {
                        ModContent.ItemType<GraveGrimreaver>(),
                        ModContent.ItemType<InfernaCutter>(),
                        ModContent.ItemType<MatterModulator>(),
                        ModContent.ItemType<MountedScanner>(),
                        ModContent.ItemType<ForbiddenOathblade>(),
                        ItemID.Flamethrower
                    }),

                   new BossLockInformation(() => NPC.downedMechBoss3 && (NPC.downedMechBoss2 || NPC.downedMechBoss1),
                    "Skeletron Prime and another Mech",
                    new()
                    {
                        ModContent.ItemType<IonBlaster>()
                    }),

                  new BossLockInformation(() => DownedBossSystem.downedCryogen && NPC.downedMechBossAny,
                    "Cryogen and a Mech",
                    new()
                    {
                        ModContent.ItemType<CrystalPiercer>(),
                        ModContent.ItemType<FlarefrostBlade>(),
                        ModContent.ItemType<DarklightGreatsword>(),
                        ModContent.ItemType<StarnightLance>(),
                        ModContent.ItemType<DarkechoGreatbow>(),
                        ModContent.ItemType<Shimmerspark>(),
                        ModContent.ItemType<ShadecrystalTome>(),
                        ModContent.ItemType<DaedalusGolemStaff>(),
                        ModContent.ItemType<ShardlightPickaxe>(),
                        ModContent.ItemType<AbyssalWarhammer>()
                    }),

                   new BossLockInformation(() => NPC.downedMechBossAny,
                    "Any Mech",
                    new()
                    {
                        ModContent.ItemType<SHPC>(),
                        ModContent.ItemType<AnarchyBlade>(),
                        ModContent.ItemType<TundraFlameBlossomsStaff>(),
                        ModContent.ItemType<ElectriciansGlove>(),
                        ModContent.ItemType<RuinMedallion>(),
                        ModContent.ItemType<HoneyDew>(),
                        ModContent.ItemType<BrimstoneFury>(),
                        ModContent.ItemType<BrimstoneSword>(),
                        ModContent.ItemType<BurningSea>(),
                        ModContent.ItemType<BrimroseStaff>(),
                        ModContent.ItemType<Brimblade>(),
                        ModContent.ItemType<IgneousExaltation>(),
                        ModContent.ItemType<WyvernsCall>(),
                        ModContent.ItemType<Nychthemeron>(),
                        ModContent.ItemType<StormfrontRazor>(),
                        ModContent.ItemType<MythrilKnife>(),
                        ModContent.ItemType<OrichalcumSpikedGemstone>(),
                        ModContent.ItemType<AnarchyBlade>(),
                        ModContent.ItemType<SHPC>(),
                        ItemID.OrichalcumSword,
                        ItemID.OrichalcumRepeater,
                        ItemID.OrichalcumHalberd,
                        ItemID.OrichalcumChainsaw,
                        ItemID.OrichalcumDrill,
                        ItemID.OrichalcumPickaxe,
                        ItemID.OrichalcumWaraxe,
                        ItemID.MythrilSword,
                        ItemID.MythrilRepeater,
                        ItemID.MythrilHalberd,
                        ItemID.MythrilDrill,
                        ItemID.MythrilChainsaw,
                        ItemID.MythrilPickaxe,
                        ItemID.MythrilWaraxe,
                        ItemID.Yelets,
                        ItemID.MushroomSpear,
                        ItemID.Hammush,
                        ItemID.Code2,
                        ItemID.BookStaff,
                        ItemID.DD2PhoenixBow,
                        ItemID.MonkStaffT1,
                        ItemID.MonkStaffT2,
                        ItemID.DD2SquireDemonSword,
                        ItemID.SpiritFlame,
                        ItemID.SkyFracture,
                        ItemID.OnyxBlaster,

                    }),

                   new BossLockInformation(() => GetMechsDead() >= 2,
                    "Two Mechs",
                    new()
                    {
                        ModContent.ItemType<ConsecratedWater>(),
                        ModContent.ItemType<DesecratedWater>(),
                        ModContent.ItemType<ForgottenApexWand>(),
                        ModContent.ItemType<SpearofPaleolith>(),
                        ModContent.ItemType<ForsakenSaber>(),
                        ModContent.ItemType<MineralMortar>(),
                        ModContent.ItemType<TitaniumRailgun>(),
                        ModContent.ItemType<TitaniumShuriken>(),
                        ModContent.ItemType<AdamantiteThrowingAxe>(),
                        ModContent.ItemType<AdamantiteParticleAccelerator>(),
                        ModContent.ItemType<DeathValleyDuster>(),
                        ModContent.ItemType<RelicofRuin>(),

                        ItemID.TitaniumSword,
                        ItemID.TitaniumTrident,
                        ItemID.TitaniumRepeater,
                        ItemID.TitaniumPickaxe,
                        ItemID.TitaniumDrill,
                        ItemID.TitaniumChainsaw,
                        ItemID.TitaniumWaraxe,
                        ItemID.AdamantiteRepeater,
                        ItemID.AdamantiteSword,
                        ItemID.AdamantitePickaxe,
                        ItemID.AdamantiteDrill,
                        ItemID.AdamantiteChainsaw,
                        ItemID.AdamantiteWaraxe,
                        ItemID.AdamantiteGlaive
                    }),

                   new BossLockInformation(() => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3,
                    "All Mechs",
                    new()
                    {
                        ModContent.ItemType<MOAB>(),
                        ModContent.ItemType<AngelTreads>(),
                        ModContent.ItemType<HallowedRune>(),
                        ModContent.ItemType<MomentumCapacitor>(),
                        ModContent.ItemType<TrueBiomeBlade>(),
                        ModContent.ItemType<Cryophobia>(),
                        ModContent.ItemType<SunGodStaff>(),
                        ModContent.ItemType<SpearofDestiny>(),
                        ModContent.ItemType<ValkyrieRay>(),
                        ModContent.ItemType<CatastropheClaymore>(),
                        ModContent.ItemType<Pwnagehammer>(),
                        ModContent.ItemType<Exorcism>(),
                        ModContent.ItemType<GleamingMagnolia>(),
                        ModContent.ItemType<TerrorTalons>(),
                        ItemID.MinecartMech,
                        ItemID.HallowedBar,
                        ItemID.AvengerEmblem,
                        ItemID.HallowJoustingLance,
                        ItemID.PickaxeAxe,
                        ItemID.Drax,
                        ItemID.Gungnir,
                        ItemID.Excalibur,
                        ItemID.LightDisc,
                        ItemID.HallowedRepeater,
                        ItemID.TrueExcalibur,
                        ItemID.TrueNightsEdge,
                        ItemID.ChlorophytePartisan,
                        ItemID.ChlorophyteSaber,
                        ItemID.ChlorophyteClaymore,
                        ItemID.ChlorophyteShotbow,
                        ItemID.ChlorophytePickaxe,
                        ItemID.ChlorophyteDrill,
                        ItemID.ChlorophyteChainsaw,
                        ItemID.ChlorophyteGreataxe,
                        ItemID.ChlorophyteWarhammer,
                        ItemID.ChlorophyteJackhammer,
                        ItemID.VenomStaff,
                        ItemID.DeathSickle,
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedCryogen && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3,
                    "Cryogen and All Mechs",
                    new()
                    {
                        ModContent.ItemType<ArcticBearPaw>(),
                        ModContent.ItemType<FrostyFlare>(),
                        ModContent.ItemType<CryogenicStaff>()
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedBrimstoneElemental,
                    "Brimstone Elemental",
                    new()
                    {
                        ModContent.ItemType<Gehenna>(),
                        ModContent.ItemType<Abaddon>(),
                        ModContent.ItemType<FlameLickedShell>(),
                        ModContent.ItemType<RoseStone>(),
                        ModContent.ItemType<Brimlance>(),
                        ModContent.ItemType<Hellborn>(),
                        ModContent.ItemType<Brimlance>(),
                        ModContent.ItemType<SeethingDischarge>(),
                        ModContent.ItemType<DormantBrimseeker>()
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedCalamitasClone,
                    "Calamitas",
                    new()
                    {
                        ModContent.ItemType<AbyssalDivingGear>(),
                        ModContent.ItemType<ChaosStone>(),
                        ModContent.ItemType<LumenousAmulet>(),
                        ModContent.ItemType<VampiricTalisman>(),
                        ModContent.ItemType<VoidofCalamity>(),
                        ModContent.ItemType<Regenator>(),
                        //Weapons
                        ModContent.ItemType<AbyssBlade>(),
                        ModContent.ItemType<Brimlash>(),
                        ModContent.ItemType<Floodtide>(),
                        ModContent.ItemType<Oblivion>(),
                        ModContent.ItemType<TyphonsGreed>(),
                        //Ranged Weapons
                        ModContent.ItemType<Animosity>(),
                        ModContent.ItemType<Megalodon>(),
                        ModContent.ItemType<HavocsBreath>(),
                        ModContent.ItemType<FlakKraken>(),
                        //Magic Weapons
                        ModContent.ItemType<ArtAttack>(),
                        ModContent.ItemType<UndinesRetribution>(),
                        ModContent.ItemType<LashesofChaos>(),
                        ModContent.ItemType<HadalUrn>(),
                        //Summon Weapons
                        ModContent.ItemType<EntropysVigil>(),
                        ModContent.ItemType<DreadmineStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<BallisticPoisonBomb>(),
                        ModContent.ItemType<TotalityBreakers>(),
                        ModContent.ItemType<Apoctolith>(),
                        ModContent.ItemType<CrushsawCrasher>(),
                        ModContent.ItemType<DeepWounder>(),
                    }),

                   new BossLockInformation(() => NPC.downedPlantBoss,
                    "Plantera",
                    new()
                    {
                        ModContent.ItemType<AureateBooster>(),
                        ModContent.ItemType<AbyssalMirror>(),
                        ModContent.ItemType<DeadshotBrooch>(),
                        ModContent.ItemType<GloveOfPrecision>(),
                        ModContent.ItemType<GloveOfRecklessness>(),
                        ModContent.ItemType<NecklaceofVexation>(),
                        ModContent.ItemType<SandSharkToothNecklace>(),
                        ModContent.ItemType<StatisBlessing>(),
                        ModContent.ItemType<BloomStone>(),
                        ModContent.ItemType<AsgardsValor>(),
                        //Weapons
                        ModContent.ItemType<AbsoluteZero>(),
                        ModContent.ItemType<CometQuasher>(),
                        ModContent.ItemType<FeralthornClaymore>(),
                        ModContent.ItemType<Hellkite>(),
                        ModContent.ItemType<MantisClaws>(),
                        ModContent.ItemType<TyrantYharimsUltisword>(),
                        ModContent.ItemType<Quagmire>(),
                        ModContent.ItemType<HellionFlowerSpear>(),
                        ModContent.ItemType<TerraLance>(),
                        ModContent.ItemType<Tumbleweed>(),
                        ModContent.ItemType<TrueArkoftheAncients>(),
                        //Ranged Weapons
                        ModContent.ItemType<TheBallista>(),
                        ModContent.ItemType<BlossomFlux>(),
                        ModContent.ItemType<CosmicBolter>(),
                        ModContent.ItemType<EternalBlizzard>(),
                        ModContent.ItemType<MarksmanBow>(),
                        ModContent.ItemType<BladedgeGreatbow>(),
                        ModContent.ItemType<MagnaStriker>(),
                        ModContent.ItemType<PearlGod>(),
                        ModContent.ItemType<TerraFlameburster>(),
                        ModContent.ItemType<SandstormGun>(),
                        //Magic Weapons
                        ModContent.ItemType<ShiftingSands>(),
                        ModContent.ItemType<TerraRay>(),
                        ModContent.ItemType<EvergladeSpray>(),
                        ModContent.ItemType<PrimordialEarth>(),
                        ModContent.ItemType<TearsofHeaven>(),
                        ModContent.ItemType<WintersFury>(),
                        ModContent.ItemType<WrathoftheAncients>(),
                        //Summon Weapons
                        ModContent.ItemType<PlantationStaff>(),
                        ModContent.ItemType<SandSharknadoStaff>(),
                        ModContent.ItemType<ViralSprout>(),
                        //Rogue Weapons
                        ModContent.ItemType<DuststormInABottle>(),
                        ModContent.ItemType<DefectiveSphere>(),
                        ModContent.ItemType<FrostcrushValari>(),
                        ModContent.ItemType<MangroveChakram>(),
                        ModContent.ItemType<TerraDisk>(),
                        ModContent.ItemType<MonkeyDarts>(),
                        ModContent.ItemType<FantasyTalisman>(),
                        ModContent.ItemType<Sandslasher>(),
                        ModContent.ItemType<PhantomLance>(),
                        //Classless
                        ModContent.ItemType<Hydra>(),
                        //Vanilla
                        ItemID.SporeSac,
                        ItemID.GrenadeLauncher,
                        ItemID.VenusMagnum,
                        ItemID.NettleBurst,
                        ItemID.LeafBlower,
                        ItemID.FlowerPow,
                        ItemID.WaspGun,
                        ItemID.Seedler,
                        ItemID.Seedling,
                        ItemID.TheAxe,
                        ItemID.PygmyStaff,
                        ItemID.ThornHook,
                        ItemID.ChainGun,
                        ItemID.ChristmasTreeSword
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedAstrumAureus,
                    "Astrum Aureus",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<StarbusterCore>(),
                        ModContent.ItemType<CelestialJewel>(),
                        ModContent.ItemType<GravistarSabaton>(),
                        ModContent.ItemType<HadarianWings>(),
                        //Weapons
                        ModContent.ItemType<AstralScythe>(),
                        ModContent.ItemType<TitanArm>(),
                        ModContent.ItemType<Nebulash>(),
                        //Ranged Weapons
                        ModContent.ItemType<AuroraBlazer>(),
                        ModContent.ItemType<StellarCannon>(),
                        //Magic Weapons
                        ModContent.ItemType<AlulaAustralis>(),
                        ModContent.ItemType<AstralachneaStaff>(),
                        //Summon Weapons
                        ModContent.ItemType<AbandonedSlimeStaff>(),
                        ModContent.ItemType<HivePod>(),
                        ModContent.ItemType<BorealisBomber>(), 
                        //Rogue Weapons
                        ModContent.ItemType<StellarKnife>(),
                        ModContent.ItemType<AuroradicalThrow>(),
                        ModContent.ItemType<HeavenfallenStardisk>(),
                        ModContent.ItemType<LeonidProgenitor>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedLeviathan,
                    "Leviathan",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<LeviathanAmbergris>(),
                        ModContent.ItemType<TheCommunity>(),
                        ModContent.ItemType<PearlofEnthrallment>(),
                        //Weapons
                        ModContent.ItemType<Greentide>(),
                        //Ranged Weapons
                        ModContent.ItemType<Leviatitan>(),
                        //Magic Weapons
                        ModContent.ItemType<Atlantis>(),
                        ModContent.ItemType<Keelhaul>(),
                        ModContent.ItemType<AnahitasArpeggio>(),
                        //Summon Weapons
                        ModContent.ItemType<GastricBelcherStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<BrackishFlask>(),
                        ModContent.ItemType<LeviathanTeeth>(),
                    }),

                   new BossLockInformation(() => NPC.downedGolemBoss,
                    "Golem",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<HadalMantle>(),
                        ModContent.ItemType<PlaguedFuelPack>(),
                        ModContent.ItemType<SigilofCalamitas>(),
                        ModContent.ItemType<StarTaintedGenerator>(),
                        ModContent.ItemType<VoidofExtinction>(),
                        ModContent.ItemType<AlchemicalFlask>(),
                        ModContent.ItemType<TheCamper>(),
                        //Weapons
                        ModContent.ItemType<AegisBlade>(),
                        ModContent.ItemType<HellfireFlamberge>(),
                        ModContent.ItemType<Lucrecia>(),
                        ModContent.ItemType<SoulHarvester>(),
                        ModContent.ItemType<FaultLine>(),
                        ModContent.ItemType<VulcaniteLance>(),
                        ModContent.ItemType<FallenPaladinsHammer>(),
                        ModContent.ItemType<Omniblade>(),
                        //Ranged Weapons
                        ModContent.ItemType<ContinentalGreatbow>(),
                        ModContent.ItemType<ClockGatlignum>(),
                        ModContent.ItemType<Helstorm>(),
                        ModContent.ItemType<BarracudaGun>(),
                        ModContent.ItemType<NullificationRifle>(),
                        //Magic Weapons
                        ModContent.ItemType<InfernalRift>(),
                        ModContent.ItemType<Wingman>(),
                        ModContent.ItemType<ForbiddenSun>(),
                        //Summon Weapons
                        ModContent.ItemType<ResurrectionButterfly>(),
                        ModContent.ItemType<WitherBlossomsStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<Plaguenade>(),
                        ModContent.ItemType<ShockGrenade>(),
                        ModContent.ItemType<EpidemicShredder>(),
                        ModContent.ItemType<SubductionSlicer>(),               
                        //Classless
                        ModContent.ItemType<YanmeisKnife>(),
                        //Vanilla
                        ItemID.FireworksLauncher,
                        ItemID.ShinyStone,
                        ItemID.Picksaw,
                        ItemID.Stynger,
                        ItemID.PossessedHatchet,
                        ItemID.SunStone,
                        ItemID.EyeoftheGolem,
                        ItemID.HeatRay,
                        ItemID.StaffofEarth,
                        ItemID.GolemFist,
                        ItemID.BeetleHusk,
                        ItemID.DestroyerEmblem,
                        ItemID.SniperScope,
                        ItemID.FireGauntlet
                    }),

                   new BossLockInformation(() => NPC.downedEmpressOfLight,
                    "Empress of Light",
                    new()
                    {
                        ItemID.PiercingStarlight,
                        ItemID.RainbowWhip,
                        ItemID.EmpressBlade,
                        ItemID.FairyQueenMagicItem,
                        ItemID.FairyQueenRangedItem,
                        ItemID.RainbowWings,
                        ItemID.SparkleGuitar,
                        ItemID.RainbowCursor
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedPlaguebringer,
                    "Plaguebringer Goliath",
                    new()
                    {
                        //Accessories
                        ModContent.ItemType<PlagueHive>(),
                        ModContent.ItemType<ToxicHeart>(),
                
                        //Weapons
                        ModContent.ItemType<ExaltedOathblade>(),
                        ModContent.ItemType<Virulence>(),
                        ModContent.ItemType<Pandemic>(),
                        ModContent.ItemType<DiseasedPike>(),
                        ModContent.ItemType<GalvanizingGlaive>(),
                        //Ranged Weapons
                        ModContent.ItemType<Malevolence>(),
                        ModContent.ItemType<TheHive>(),
                        ModContent.ItemType<BlightSpewer>(),
                        ModContent.ItemType<GaussRifle>(),
                        ModContent.ItemType<PestilentDefiler>(),
                        //Magic Weapons
                        ModContent.ItemType<PlagueStaff>(),
                        ModContent.ItemType<GatlingLaser>(),
                        //Summon Weapons
                        ModContent.ItemType<FuelCellBundle>(),
                        ModContent.ItemType<InfectedRemote>(),
                        ModContent.ItemType<PulseTurretRemote>(),
                        //Rogue Weapons
                        ModContent.ItemType<Malachite>(),
                        ModContent.ItemType<SystemBane>(),
                        ModContent.ItemType<TheSyringe>(),
                    }),

                   new BossLockInformation(() => NPC.downedFishron,
                    "Duke Fishron",
                    new()
                    {
                        ModContent.ItemType<BrinyBaron>(),
                        ModContent.ItemType<DukesDecapitator>(),
                        //Vanilla
                        ItemID.ShrimpyTruffle,
                        ItemID.BubbleGun,
                        ItemID.Flairon,
                        ItemID.RazorbladeTyphoon,
                        ItemID.TempestStaff,
                        ItemID.Tsunami,
                        ItemID.FishronWings
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedRavager,
                    "Ravager",
                    new()
                    {
                        ModContent.ItemType<BloodPact>(),
                        ModContent.ItemType<FleshTotem>(),
                        //Weapons
                        ModContent.ItemType<UltimusCleaver>(),
                        //Ranged Weapons
                        ModContent.ItemType<RealmRavager>(),
                        //Magic Weapons
                        ModContent.ItemType<Hematemesis>(),
                        ModContent.ItemType<Vesuvius>(),
                        //Summon Weapons
                        ModContent.ItemType<SpikecragStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<CraniumSmasher>(),
                        ModContent.ItemType<CorpusAvertor>(),
                    }),

                   new BossLockInformation(() => NPC.downedAncientCultist,
                    "Lunatic Cultist",
                    new()
                    {
                        ModContent.ItemType<DarkGodsSheath>(),
                        ModContent.ItemType<StatisCurse>(),
                        ModContent.ItemType<HeartoftheElements>(),
                        ModContent.ItemType<TheAbsorber>(),
                        //Weapons
                        ModContent.ItemType<BalefulHarvester>(),
                        ModContent.ItemType<EntropicClaymore>(),
                        ModContent.ItemType<GrandGuardian>(),
                        ModContent.ItemType<StormRuler>(),
                        ModContent.ItemType<TenebreusTides>(),
                        //Ranged Weapons
                        ModContent.ItemType<ConferenceCall>(),
                        ModContent.ItemType<Shroomer>(),
                        ModContent.ItemType<Vortexpopper>(),
                        ModContent.ItemType<Scorpio>(),
                        ModContent.ItemType<GodsBellows>(),
                        ModContent.ItemType<SpectralstormCannon>(),
                        //Magic Weapons
                        ModContent.ItemType<ArchAmaryllis>(),
                        ModContent.ItemType<Lazhar>(),
                        ModContent.ItemType<NanoPurge>(),
                        ModContent.ItemType<TheSwarmer>(),
                        ModContent.ItemType<TomeofFates>(),
                        ModContent.ItemType<CosmicRainbow>(),
                        //Summon Weapons
                        //Rogue Weapons
                        ModContent.ItemType<StarofDestruction>(),
                        ModContent.ItemType<LuminousStriker>(),
                        ModContent.ItemType<ShardofAntumbra>(),
                        //Classless 
                        ModContent.ItemType<EyeofMagnus>(),
                        //Vanilla
                        ItemID.LunarCraftingStation,
                        ItemID.SolarEruption,
                        ItemID.DayBreak,
                        ItemID.Phantasm,
                        ItemID.VortexBeater,
                        ItemID.NebulaBlaze,
                        ItemID.NebulaArcanum,
                        ItemID.StardustDragonStaff,
                        ItemID.StardustCellStaff
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedAstrumDeus,
                    "Astrum Deus",
                    new()
                    {
                        ModContent.ItemType<DeificAmulet>(),
                        ModContent.ItemType<HideofAstrumDeus>(),
                        //Weapons
                        ModContent.ItemType<AstralBlade>(),
                        ModContent.ItemType<OmegaBiomeBlade>(),
                        ModContent.ItemType<TheMicrowave>(),
                        ModContent.ItemType<AstralPike>(),
                        //Ranged Weapons
                        ModContent.ItemType<AstralRepeater>(),
                        ModContent.ItemType<StarSputter>(),
                        //Magic Weapons
                        ModContent.ItemType<Starfall>(),
                        ModContent.ItemType<AstralStaff>(),
                        //Summon Weapons
                        ModContent.ItemType<GodspawnHelixStaff>(),
                        ModContent.ItemType<RadiantStar>(),
                        ModContent.ItemType<RegulusRiot>(),
                    }),

                   new BossLockInformation(() => NPC.downedMoonlord,
                    "Moonlord",
                    new()
                    {
                        ModContent.ItemType<ExodusWings>(),
                        ModContent.ItemType<SeraphTracers>(),
                        ModContent.ItemType<StatisNinjaBelt>(),
                        ModContent.ItemType<DaawnlightSpiritOrigin>(),
                        ModContent.ItemType<EldritchSoulArtifact>(),
                        ModContent.ItemType<MoonstoneCrown>(),
                        ModContent.ItemType<AbyssalDivingSuit>(),
                        //Weapons
                        ModContent.ItemType<Devastation>(),
                        ModContent.ItemType<ElementalShiv>(),
                        ModContent.ItemType<GreatswordofJudgement>(),
                        ModContent.ItemType<PlagueKeeper>(),
                        ModContent.ItemType<SolsticeClaymore>(),
                        ModContent.ItemType<StellarStriker>(),
                        ModContent.ItemType<ElementalLance>(),
                        ModContent.ItemType<StellarContempt>(),
                        ModContent.ItemType<RemsRevenge>(),
                        ModContent.ItemType<ArkoftheElements>(),
                        //Ranged Weapons
                        ModContent.ItemType<AstrealDefeat>(),
                        ModContent.ItemType<ClockworkBow>(),
                        ModContent.ItemType<PlanetaryAnnihilation>(),
                        ModContent.ItemType<Disseminator>(),
                        ModContent.ItemType<OnyxChainBlaster>(),
                        ModContent.ItemType<PridefulHuntersPlanarRipper>(),
                        ModContent.ItemType<Shredder>(),
                        ModContent.ItemType<ElementalEruption>(),
                        ModContent.ItemType<ElementalBlaster>(),
                        ModContent.ItemType<Starfleet>(),
                        //Magic Weapons
                        ModContent.ItemType<AsteroidStaff>(),
                        ModContent.ItemType<ElementalRay>(),
                        ModContent.ItemType<UltraLiquidator>(),
                        ModContent.ItemType<ApoctosisArray>(),
                        ModContent.ItemType<Effervescence>(),
                        ModContent.ItemType<Genisis>(),
                        ModContent.ItemType<AuguroftheElements>(),
                        ModContent.ItemType<NuclearFury>(),
                        //Summon Weapons
                        ModContent.ItemType<ElementalAxe>(),
                        ModContent.ItemType<FlowersOfMortality>(),
                        ModContent.ItemType<TacticalPlagueEngine>(),
                        ModContent.ItemType<SanctifiedSpark>(),
                        //Rogue Weapons
                        ModContent.ItemType<ElementalDisk>(),
                        ModContent.ItemType<LunarKunai>(),
                        ModContent.ItemType<UtensilPoker>(),
                        ModContent.ItemType<HellsSun>(),
                        ModContent.ItemType<CelestialReaper>(),
                        //Vanilla
                        ItemID.GravityGlobe,
                        ItemID.SuspiciousLookingTentacle,
                        ItemID.LongRainbowTrailWings,
                        ItemID.Meowmere,
                        ItemID.Terrarian,
                        ItemID.StarWrath,
                        ItemID.SDMG,
                        ItemID.LastPrism,
                        ItemID.LunarFlareBook,
                        ItemID.RainbowCrystalStaff,
                        ItemID.MoonlordTurretStaff,
                        ItemID.Celeb2,
                        ItemID.PortalGun,
                        ItemID.LunarOre,
                        ItemID.MeowmereMinecart,
                        ItemID.MoonlordArrow,
                        ItemID.MoonlordBullet
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedGuardians,
                    "Profaned Guardians",
                    new()
                    {
                        ModContent.ItemType<WarbanneroftheSun>(),
                        ModContent.ItemType<RelicOfConvergence>(),
                        ModContent.ItemType<RelicOfDeliverance>(),
                        ModContent.ItemType<RelicOfResilience>()
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedDragonfolly,
                    "Dragonfolly",
                    new()
                    {
                        ModContent.ItemType<BlunderBooster>(),
                        ModContent.ItemType<DynamoStemCells>(),
                        //Weapons
                        ModContent.ItemType<Swordsplosion>(),
                        ModContent.ItemType<GildedProboscis>(),
                        //Ranged Weapons
                        ModContent.ItemType<GoldenEagle>(),
                        //Magic Weapon
                        ModContent.ItemType<MadAlchemistsCocktailGlove>(),
                        ModContent.ItemType<RougeSlash>()
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedProvidence,
                    "Providence",
                    new()
                    {
                        ModContent.ItemType<ElysianWings>(),
                        ModContent.ItemType<TarragonWings>(),
                        ModContent.ItemType<BloodflareCore>(),
                        ModContent.ItemType<BadgeofBravery>(),
                        ModContent.ItemType<YharimsInsignia>(),
                        ModContent.ItemType<BlazingCore>(),
                        ModContent.ItemType<ElysianAegis>(),
                        ModContent.ItemType<ProfanedSoulArtifact>(),               
                        //Weapons
                        ModContent.ItemType<GalactusBlade>(),
                        ModContent.ItemType<Grax>(),
                        ModContent.ItemType<HolyCollider>(),
                        ModContent.ItemType<LifefruitScythe>(),
                        ModContent.ItemType<TheMutilator>(),
                        ModContent.ItemType<Terratomere>(),
                        ModContent.ItemType<TrueTyrantYharimsUltisword>(),
                        ModContent.ItemType<Lacerator>(),
                        ModContent.ItemType<SolarFlare>(),
                        ModContent.ItemType<Verdant>(),
                        ModContent.ItemType<SeekingScorcher>(),
                        ModContent.ItemType<Mourningstar>(),
                        ModContent.ItemType<PulseDragon>(),
                        ModContent.ItemType<DevilsSunrise>(),
                        //Ranged Weapons
                        ModContent.ItemType<ArterialAssault>(),
                        ModContent.ItemType<NettlevineGreatbow>(),
                        ModContent.ItemType<TelluricGlare>(),
                        ModContent.ItemType<AngelicShotgun>(),
                        ModContent.ItemType<Auralis>(),
                        ModContent.ItemType<ClaretCannon>(),
                        ModContent.ItemType<Spyker>(),
                        ModContent.ItemType<BlissfulBombardier>(),
                        ModContent.ItemType<HandheldTank>(),
                        ModContent.ItemType<BloodBoiler>(),
                        ModContent.ItemType<PristineFury>(),
                        ModContent.ItemType<HeavyLaserRifle>(),
                        //Magic Weapons
                        ModContent.ItemType<DivineRetribution>(),
                        ModContent.ItemType<ThePrince>(),
                        ModContent.ItemType<SanguineFlare>(),
                        ModContent.ItemType<ThornBlossom>(),
                        ModContent.ItemType<Viscera>(),
                        ModContent.ItemType<PlasmaCaster>(),
                        ModContent.ItemType<PlasmaRifle>(),
                        ModContent.ItemType<PurgeGuzzler>(),
                        ModContent.ItemType<Biofusillade>(),
                        //Summon Weapons
                        ModContent.ItemType<DazzlingStabberStaff>(),
                        ModContent.ItemType<DragonbloodDisgorger>(),
                        ModContent.ItemType<SnakeEyes>(),
                        ModContent.ItemType<ViridVanguard>(),
                        ModContent.ItemType<GuidelightofOblivion>(),
                        //Rogue Weapons
                        ModContent.ItemType<WavePounder>(),
                        ModContent.ItemType<MoltenAmputator>(),
                        ModContent.ItemType<ShatteredSun>(),
                        ModContent.ItemType<TarragonThrowingDart>(),
                        ModContent.ItemType<ProfanedPartisan>(),
                        ModContent.ItemType<AlphaVirus>(),
                        ModContent.ItemType<BloodsoakedCrasher>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedStormWeaver,
                    "Storm Weaver",
                    new()
                    {
                        ModContent.ItemType<TheStorm>(),
                        ModContent.ItemType<Seadragon>(),
                        ModContent.ItemType<StormDragoon>(),
                        //Magic Weapons
                        ModContent.ItemType<Thunderstorm>(),
                        ModContent.ItemType<DeificThunderbolt>()
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedCeaselessVoid,
                    "Ceaseless Void",
                    new()
                    {
                        ModContent.ItemType<AstralArcanum>(),
                        ModContent.ItemType<TheEvolution>(),
                        ModContent.ItemType<QuiverofNihility>(),
                        //Weapons
                        ModContent.ItemType<MirrorBlade>(),
                        //Ranged Weapons
                        ModContent.ItemType<MolecularManipulator>(),
                        //Magic Weapons
                        ModContent.ItemType<MagneticMeltdown>(),
                        ModContent.ItemType<Mistlestorm>(),
                        ModContent.ItemType<TacticiansTrumpCard>(),
                        //Summon Weapons
                        ModContent.ItemType<VoidConcentrationStaff>(),
                        //Rogue Weapons
                        ModContent.ItemType<SealedSingularity>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedSignus,
                    "Signus",
                    new()
                    {
                        ModContent.ItemType<SpectralVeil>(),
                        //Ranged Weapons
                        ModContent.ItemType<TheSevensStriker>(),
                        //Magic Weapons
                        ModContent.ItemType<AethersWhisper>(),
                        //Summon Weapons
                        ModContent.ItemType<Cosmilamp>(),
                        //Rogue Weapons
                        ModContent.ItemType<CosmicKunai>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedPolterghast,
                    "Polterghast",
                    new()
                    {
                        ModContent.ItemType<PhantomicArtifact>(),
                        ModContent.ItemType<ReaperToothNecklace>(),
                        ModContent.ItemType<Affliction>(),
                        //Weapons
                        ModContent.ItemType<GalileoGladius>(),
                        ModContent.ItemType<TheLastMourning>(),
                        ModContent.ItemType<LionHeart>(),
                        ModContent.ItemType<NeptunesBounty>(),
                        ModContent.ItemType<SoulEdge>(),
                        ModContent.ItemType<TerrorBlade>(),
                        ModContent.ItemType<BansheeHook>(),
                        ModContent.ItemType<CrescentMoon>(),
                        ModContent.ItemType<PhosphorescentGauntlet>(),
                        ModContent.ItemType<DeathsAscension>(),
                        //Ranged Weapons
                        ModContent.ItemType<DaemonsFlame>(),
                        ModContent.ItemType<TheMaelstrom>(),
                        ModContent.ItemType<Monsoon>(),
                        ModContent.ItemType<DodusHandcannon>(),
                        ModContent.ItemType<HalleysInferno>(),
                        ModContent.ItemType<SulphuricAcidCannon>(),
                        //Magic Weapons
                        ModContent.ItemType<ClamorNoctus>(),
                        ModContent.ItemType<EidolonStaff>(),
                        ModContent.ItemType<FatesReveal>(),
                        ModContent.ItemType<PhantasmalFury>(),
                        ModContent.ItemType<ShadowboltStaff>(),
                        ModContent.ItemType<VenusianTrident>(),
                        ModContent.ItemType<EidolicWail>(),
                        ModContent.ItemType<DarkSpark>(),
                        ModContent.ItemType<GhastlyVisage>(),
                        //Summon Weapons
                        ModContent.ItemType<CalamarisLament>(),
                        ModContent.ItemType<EtherealSubjugator>(),
                        ModContent.ItemType<GammaHeart>(),
                        ModContent.ItemType<Sirius>(),
                        ModContent.ItemType<Valediction>(),
                        //Rogue Weapons
                        ModContent.ItemType<GhoulishGouger>(),
                        ModContent.ItemType<JawsOfOblivion>(),
                        ModContent.ItemType<TimeBolt>(),
                        ModContent.ItemType<PhantasmalRuin>(),
                        ModContent.ItemType<NightsGaze>(),
                        ModContent.ItemType<DeepSeaDumbbell>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedBoomerDuke,
                    "Old Duke",
                    new()
                    {
                        ModContent.ItemType<OldDukeScales>(),
                        ModContent.ItemType<MutatedTruffle>(),
                        //Weapons
                        ModContent.ItemType<InsidiousImpaler>(),
                
                        //Ranged Weapons
                        ModContent.ItemType<FetidEmesis>(),
                        ModContent.ItemType<SepticSkewer>(),
                        //Magic Weapons
                        ModContent.ItemType<VitriolicViper>(),
                        //Summon WeaponsS
                        ModContent.ItemType<CadaverousCarrion>(),
                        //Rogue Weapons
                        ModContent.ItemType<ToxicantTwister>(),
                        ModContent.ItemType<TheOldReaper>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedDoG,
                    "DoG",
                    new()
                    {
                        ModContent.ItemType<ElysianTracers>(),
                        ModContent.ItemType<SilvaWings>(),
                        ModContent.ItemType<DarkSunRing>(),
                        ModContent.ItemType<DimensionalSoulArtifact>(),
                        ModContent.ItemType<EclipseMirror>(),
                        ModContent.ItemType<ElementalGauntlet>(),
                        ModContent.ItemType<ElementalQuiver>(),
                        ModContent.ItemType<EtherealTalisman>(),
                        ModContent.ItemType<Nanotech>(),
                        ModContent.ItemType<NebulousCore>(),
                        ModContent.ItemType<Nucleogenesis>(),
                        ModContent.ItemType<OccultSkullCrown>(),
                        ModContent.ItemType<TheAmalgam>(),
                        ModContent.ItemType<VeneratedLocket>(),
                        ModContent.ItemType<AsgardianAegis>(),
                        ModContent.ItemType<CoreOfTheBloodGod>(),
                        ModContent.ItemType<RampartofDeities>(),
                        ModContent.ItemType<TheSponge>(),
                        //Weapons
                        ModContent.ItemType<CosmicShiv>(),
                        ModContent.ItemType<DevilsDevastation>(),
                        ModContent.ItemType<EssenceFlayer>(),
                        ModContent.ItemType<TheEnforcer>(),
                        ModContent.ItemType<Excelsus>(),
                        ModContent.ItemType<GreatswordofBlah>(),
                        ModContent.ItemType<PrismaticBreaker>(),
                        ModContent.ItemType<TheObliterator>(),
                        ModContent.ItemType<Nadir>(),
                        ModContent.ItemType<StreamGouge>(),
                        ModContent.ItemType<GalaxySmasher>(),
                        ModContent.ItemType<CosmicDischarge>(),
                        ModContent.ItemType<EmpyreanKnives>(),
                        ModContent.ItemType<FourSeasonsGalaxia>(),
                        ModContent.ItemType<Phaseslayer>(),
                        ModContent.ItemType<ScourgeoftheCosmos>(),
                        //Ranged Weapons
                        ModContent.ItemType<Alluvion>(),
                        ModContent.ItemType<Deathwind>(),
                        ModContent.ItemType<Phangasm>(),
                        ModContent.ItemType<Ultima>(),
                        ModContent.ItemType<AntiMaterielRifle>(),
                        ModContent.ItemType<Infinity>(),
                        ModContent.ItemType<Karasawa>(),
                        ModContent.ItemType<Onyxia>(),
                        ModContent.ItemType<RubicoPrime>(),
                        ModContent.ItemType<SDFMG>(),
                        ModContent.ItemType<UniversalGenesis>(),
                        ModContent.ItemType<ScorchedEarth>(),
                        ModContent.ItemType<ThePack>(),
                        ModContent.ItemType<CleansingBlaze>(),
                        ModContent.ItemType<PulseRifle>(),
                        ModContent.ItemType<Norfleet>(),
                        ModContent.ItemType<Starmada>(),
                        //Magic Weapons
                        ModContent.ItemType<DeathhailStaff>(),
                        ModContent.ItemType<IceBarrage>(),
                        ModContent.ItemType<SoulPiercer>(),
                        ModContent.ItemType<VoltaicClimax>(),
                        ModContent.ItemType<AlphaRay>(),
                        ModContent.ItemType<TeslaCannon>(),
                        ModContent.ItemType<EventHorizon>(),
                        ModContent.ItemType<LightGodsBrilliance>(),
                        ModContent.ItemType<PrimordialAncient>(),
                        ModContent.ItemType<RecitationoftheBeast>(),
                        ModContent.ItemType<FaceMelter>(),
                        //Summon Weapons
                        ModContent.ItemType<CorvidHarbringerStaff>(),
                        ModContent.ItemType<CosmicViperEngine>(),
                        ModContent.ItemType<EndoHydraStaff>(),
                        ModContent.ItemType<PoleWarper>(),
                        ModContent.ItemType<SarosPossession>(),
                        ModContent.ItemType<StaffoftheMechworm>(),
                        //Rogue Weapons
                        ModContent.ItemType<Penumbra>(),
                        ModContent.ItemType<PlasmaGrenade>(),
                        ModContent.ItemType<Eradicator>(),
                        ModContent.ItemType<EclipsesFall>(),
                        ModContent.ItemType<GodsParanoia>(),
                        ModContent.ItemType<ExecutionersBlade>(),
                        ModContent.ItemType<Hypothermia>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedYharon,
                    "Yharon",
                    new()
                    {
                        ModContent.ItemType<CelestialTracers>(),
                        ModContent.ItemType<DrewsWings>(),
                        ModContent.ItemType<DragonScales>(),
                        ModContent.ItemType<YharimsGift>(),
                        ModContent.ItemType<GodlySoulArtifact>(),
                        //Weapons
                        ModContent.ItemType<Ataraxia>(),
                        ModContent.ItemType<Oracle>(),
                        ModContent.ItemType<DragonPow>(),
                        ModContent.ItemType<ArkoftheCosmos>(),
                        ModContent.ItemType<TheBurningSky>(),
                        ModContent.ItemType<DragonRage>(),
                        ModContent.ItemType<Murasama>(),
                        //Ranged Weapons
                        ModContent.ItemType<Drataliornus>(),
                        ModContent.ItemType<AcesHigh>(),
                        ModContent.ItemType<DragonsBreath>(),
                        ModContent.ItemType<Minigun>(),
                        ModContent.ItemType<TyrannysEnd>(),
                        ModContent.ItemType<ChickenCannon>(),
                        //Magic Weapons
                        ModContent.ItemType<HeliumFlash>(),
                        ModContent.ItemType<PhoenixFlameBarrage>(),
                        ModContent.ItemType<VoidVortex>(),
                        ModContent.ItemType<TheWand>(),
                        ModContent.ItemType<AetherfluxCannon>(),
                        ModContent.ItemType<YharimsCrystal>(),
                        //Summon Weapons
                        ModContent.ItemType<MidnightSunBeacon>(),
                        ModContent.ItemType<YharonsKindleStaff>(),
                        // Rogue Weapons
                        ModContent.ItemType<Seraphim>(),
                        ModContent.ItemType<Wrathwing>(),
                        ModContent.ItemType<FinalDawn>(),
                        ModContent.ItemType<SearedPan>(),
                        ModContent.ItemType<DynamicPursuer>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedExoMechs,
                    "Exo Mechs",
                    new()
                    {
                        ModContent.ItemType<DraedonsHeart>(),
                        //Weapons
                        ModContent.ItemType<Exoblade>(),
                        //ModContent.ItemType<ExoGladius>(), :((((((
                        ModContent.ItemType<SpineOfThanatos>(),
                        ModContent.ItemType<PhotonRipper>(),
                        //Ranged Weapons
                        ModContent.ItemType<HeavenlyGale>(),
                        ModContent.ItemType<TheJailor>(),
                        ModContent.ItemType<SurgeDriver>(),
                        ModContent.ItemType<MagnomalyCannon>(),
                        ModContent.ItemType<Photoviscerator>(),
                        //Magic Weapon
                        ModContent.ItemType<VividClarity>(),
                        ModContent.ItemType<SubsumingVortex>(),
                        //Summon Weapon
                        ModContent.ItemType<CosmicImmaterializer>(),
                        ModContent.ItemType<AresExoskeleton>(),
                        //Rogue Weapons
                        ModContent.ItemType<Supernova>(),
                        ModContent.ItemType<Celestus>(),
                        ModContent.ItemType<TheAtomSplitter>(),
                        ModContent.ItemType<RefractionRotor>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedCalamitas,
                    "Supreme Calamitas",
                    new()
                    {
                        ModContent.ItemType<Calamity>(),
                        ModContent.ItemType<ShatteredCommunity>(),
                        //Weapons
                        ModContent.ItemType<GaelsGreatsword>(),
                        ModContent.ItemType<Violence>(),
                        //Ranged Weapons
                        ModContent.ItemType<Condemnation>(),
                        //Magic Weapons
                        ModContent.ItemType<Vehemence>(),
                        ModContent.ItemType<Heresy>(),
                        ModContent.ItemType<Rancor>(),
                        ModContent.ItemType<GruesomeEminence>(),
                        //Summon Weapons
                        ModContent.ItemType<Metastasis>(),
                        ModContent.ItemType<Vigilance>(),
                        ModContent.ItemType<Perdition>(),
                        ModContent.ItemType<CindersOfLament>(),
                        //Rogue Weapons
                        ModContent.ItemType<Sacrifice>(),
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedAdultEidolonWyrm,
                    "AEW",
                    new()
                    {
                        ModContent.ItemType<HalibutCannon>()
                    }),

                   new BossLockInformation(() => DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs,
                    "Endgame",
                    new()
                    {
                        ModContent.ItemType<AngelicAlliance>(),
                        ModContent.ItemType<ProfanedSoulCrystal>(),
                        //Weapons
                        ModContent.ItemType<DraconicDestruction>(),
                        ModContent.ItemType<Earth>(),
                        ModContent.ItemType<ElementalExcalibur>(),
                        ModContent.ItemType<RedSun>(),
                        ModContent.ItemType<Azathoth>(),
                        ModContent.ItemType<TriactisTruePaladinianMageHammerofMightMelee>(),
                        ModContent.ItemType<IllustriousKnives>(),
                        //Ranged Weapons
                        ModContent.ItemType<Contagion>(),
                        ModContent.ItemType<SomaPrime>(),
                        ModContent.ItemType<Svantechnical>(),
                        ModContent.ItemType<Voidragon>(),//gets confused with the projectile else lo
                        //Magic Weapons
                        ModContent.ItemType<Fabstaff>(),
                        ModContent.ItemType<RainbowPartyCannon>(),
                        ModContent.ItemType<Apotheosis>(),
                        ModContent.ItemType<TheDanceofLight>(),
                        ModContent.ItemType<Eternity>(),
                        //Summon Weapons
                        ModContent.ItemType<Endogenesis>(),
                        ModContent.ItemType<TemporalUmbrella>(),
                        ModContent.ItemType<FlamsteedRing>(),
                        ModContent.ItemType<UniverseSplitter>(),
                        //Rogue Weapons
                        ModContent.ItemType<NanoblackReaper>(),
                        ModContent.ItemType<ScarletDevil>(),
                    }),
            };

            PotionsTieringInformation = new()
            {
                new BossLockInformation(() => NPC.downedBoss1,
                    "Eye of Cthulhu",
                    new()
                    { 
                        ModContent.ItemType<SulphurskinPotion>()
                    }),

                new BossLockInformation(() => DownedBossSystem.downedPerforator && DownedBossSystem.downedHiveMind,
                    "Hive Mind/Perforators",
                    new()
                    {
                        ModContent.ItemType<TeslaPotion>()
                    }),

                new BossLockInformation(() => NPC.downedQueenBee,
                    "Queen Bee",
                    new()
                    {
                        ItemID.FlaskofFire,
                        ItemID.FlaskofPoison,
                        ItemID.FlaskofParty
                    }),

                new BossLockInformation(() => NPC.downedBoss3,
                    "Skeletron",
                    new()
                    {
                        ModContent.ItemType<PotionofOmniscience>()
                    }),

                new BossLockInformation(() => Main.hardMode,
                    "Wall of Flesh",
                    new()
                    {

                        ModContent.ItemType<FabsolsVodka>(),
                        ModContent.ItemType<GrapeBeer>(),
                        ModContent.ItemType<Tequila>(),
                        ModContent.ItemType<Whiskey>(),
                        ModContent.ItemType<Rum>(),
                        ModContent.ItemType<Fireball>(),
                        ModContent.ItemType<RedWine>(),
                        ModContent.ItemType<SoaringPotion>(),
                        ModContent.ItemType<FlaskOfCrumbling>()
                    }),

                new BossLockInformation(() => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3,
                    "All Mechs",
                    new()
                    {
                        ModContent.ItemType<Vodka>(),
                        ModContent.ItemType<Screwdriver>(),
                        ModContent.ItemType<WhiteWine>(),
                    }),

                new BossLockInformation(() => NPC.downedPlantBoss,
                    "Plantera",
                    new()
                    {
                        ModContent.ItemType<EvergreenGin>(),
                        ModContent.ItemType<Margarita>(),
                        ModContent.ItemType<CaribbeanRum>(),
                    }),

                new BossLockInformation(() => DownedBossSystem.downedAstrumAureus,
                    "Astrum Aureus",
                    new()
                    {
                        ModContent.ItemType<Everclear>(),
                        ModContent.ItemType<StarBeamRye>(),
                        ModContent.ItemType<BloodyMary>(),
                        ModContent.ItemType<GravityNormalizerPotion>(),
                        ModContent.ItemType<AureusCell>(),
                        ModContent.ItemType<AstralInjection>()
                    }),

                new BossLockInformation(() => NPC.downedGolemBoss,
                    "Golem",
                    new()
                    {
                        ModContent.ItemType<MoscowMule>(),
                        ModContent.ItemType<TequilaSunrise>(),
                        ModContent.ItemType<Moonshine>(),
                        ModContent.ItemType<CinnamonRoll>(),
                    }),

                new BossLockInformation(() => DownedBossSystem.downedCalamitasClone,
                    "Calamitas",
                    new()
                    {
                        ModContent.ItemType<FlaskOfBrimstone>(),
                    }),

                new BossLockInformation(() => NPC.downedMoonlord,
                    "Moonlord",
                    new()
                    {
                        ModContent.ItemType<SupremeHealingPotion>(),
                        ModContent.ItemType<SupremeManaPotion>(),
                        ModContent.ItemType<FlaskOfHolyFlames>(),
                    }),

                new BossLockInformation(() => DownedBossSystem.downedProvidence,
                    "Providence",
                    new()
                    {
                        ModContent.ItemType<Bloodfin>()
                    }),

                new BossLockInformation(() => DownedBossSystem.downedCeaselessVoid,
                    "Ceaseless Void",
                    new()
                    {
                        ModContent.ItemType<CeaselessHungerPotion>()
                    }),

                new BossLockInformation(() => DownedBossSystem.downedDoG,
                    "DoG",
                    new()
                    {
                        ModContent.ItemType<OmegaHealingPotion>()
                    }),
            };
        }

        public override void Unload()
        {
            ItemsTieringInformation = null;
        }
    }
}
