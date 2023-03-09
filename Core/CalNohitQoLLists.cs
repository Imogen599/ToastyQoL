using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.Projectiles.Melee;
using CalNohitQoL.Content.Items;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Core
{
    // Yes these are all manually updated.
    // Yes it is painful to maintain.
    public class CalNohitQoLLists
    {
        // Prehardmode.
        public static List<int> PostKingSlime { get; private set; }
        public static List<int> PostDesertScourge { get; private set; }
        public static List<int> PostEyeOfCthulhu { get; private set; }
        public static List<int> PostCrabulon { get; private set; }
        public static List<int> PostEvil1 { get; private set; }
        public static List<int> PostEvil2 { get; private set; }
        public static List<int> PostQueenBee { get; private set; }
        public static List<int> PostDeerclops { get; private set; }
        public static List<int> PostSkeletron { get; private set; }
        public static List<int> PostSlimeGod { get; private set; }
        // Hardmode.
        public static List<int> PostWallOfFlesh { get; private set; }
        public static List<int> PostQueenSlime { get; private set; }
        public static List<int> PostCryogen { get; private set; }
        public static List<int> PostAquaticScourge { get; private set; }
        public static List<int> PostTwins { get; private set; }
        public static List<int> PostSkeletronPrime { get; private set; }
        public static List<int> PostDestroyer { get; private set; }
        public static List<int> PostAnyMech { get; private set; }
        public static List<int> PostAllMechs { get; private set; }
        public static List<int> PostBrimstoneElemental { get; private set; }
        public static List<int> PostCalamitas { get; private set; }
        public static List<int> PostPlantera { get; private set; }
        public static List<int> PostAstrumAureus { get; private set; }
        public static List<int> PostAnahita { get; private set; }
        public static List<int> PostGolem { get; private set; }
        public static List<int> PostEmpress { get; private set; }
        public static List<int> PostPlaguebringerGoliath { get; private set; }
        public static List<int> PostDukeFishron { get; private set; }
        public static List<int> PostRavager { get; private set; }
        public static List<int> PostCultist { get; private set; }
        public static List<int> PostAstrumDeus { get; private set; }
        // Post Moonlord.
        public static List<int> PostMoonlord { get; private set; }
        public static List<int> PostProfanedGuardians { get; private set; }
        public static List<int> PostDragonFolly { get; private set; }
        public static List<int> PostProvidence { get; private set; }
        public static List<int> PostStormWeaver { get; private set; }
        public static List<int> PostCeaselessVoid { get; private set; }
        public static List<int> PostSignus { get; private set; }
        public static List<int> PostPolterghast { get; private set; }
        public static List<int> PostOldDuke { get; private set; }
        public static List<int> PostDoG { get; private set; }
        public static List<int> PostYharon { get; private set; }
        public static List<int> PostDraedon { get; private set; }
        public static List<int> PostSCal { get; private set; }
        public static List<int> Endgame { get; private set; }
        // Other lists.
        public static List<int> SCalTooltips { get; private set; }
        public static List<int> ShroomsDrawProjs { get; private set; }



        public static void LoadLists()
        {
            //prehardmode
            PostKingSlime = new List<int>
            {
                //Accessories
                ModContent.ItemType<CrownJewel>(),
                ModContent.ItemType<LoreKingSlime>(),
                ModContent.ItemType<HeartofDarkness>(),
                ModContent.ItemType<Laudanum>(),
                ModContent.ItemType<StressPills>(),
                //Weapons
                //Vanilla
                2430,
                2585,
                3090,
            };
            PostDesertScourge = new List<int>
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
            };
            PostEyeOfCthulhu = new List<int>
            {
                //Accessories

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
                1299,
                3097,
            };
            PostCrabulon = new List<int>
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
            };
            PostEvil1 = new List<int>
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
                3224,
                3223,
                127,
                197,
                123,
                124,
                125,
            };
            PostEvil2 = new List<int>
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
            };
            PostQueenBee = new List<int>
            {
                //Accessories
                ModContent.ItemType<TheBee>(),
                //Weapons
                ModContent.ItemType<HardenedHoneycomb>(),
                //Vanilla
                1121,
                1123,
                2888,
                1132,
                1170,
                2502,
                1130,
                3333,
            };
            PostDeerclops = new List<int>
            {
                //Accessories

                //Weapons
                //Vanilla
                5100,
                5098,
                5101,
                5113,
                5117,
                5118,
                5119,
                5095,
            };
            PostSkeletron = new List<int>
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
                4993,
                3245,
                1273,
                1313,
            };
            PostSlimeGod = new List<int>
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
            };


            //HARDMODE


            PostWallOfFlesh = new List<int>
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
                3335,
                367,
                490,
                491,
                489,
                2998,
                426,
                434,
                514,
                4912,
            };
            PostQueenSlime = new List<int>
            {
                //Accessories
                //Weapons
                //Vanilla
                4987,
                4986,
                4981,
                4980,
                4758,
                4982,
                4983,
                4984,
            };
            PostCryogen = new List<int>
            {
                //Accessories
                ModContent.ItemType<SoulofCryogen>(),
                ModContent.ItemType<StarlightWings>(),
                ModContent.ItemType<FrostFlare>(),
                ModContent.ItemType<AmbrosialAmpoule>(),
                ModContent.ItemType<CryoStone>(),
                ModContent.ItemType<OrnateShield>(),
                ModContent.ItemType<PermafrostsConcoction>(),
                //Weapons
                //Ranged Weapons
                //Magic Weapons
                //Summon Weapons
                ModContent.ItemType<ColdDivinity>(),
            };
            PostAquaticScourge = new List<int>
            {
                //Accessories
                ModContent.ItemType<AquaticEmblem>(),
                ModContent.ItemType<CorrosiveSpine>(),
                ModContent.ItemType<NuclearRod>(),
                ModContent.ItemType<DeepDiver>(),
                //Weapons
                //Ranger Weapons
                //Magic Weapons
                //Summon Weapons
                //Rogue Weapons
            };
            PostTwins = new List<int>
            {
                //Accessories
                //Weapons
                //Ranged Weapons
                ModContent.ItemType<Arbalest>(),
            };
            PostSkeletronPrime = new List<int>
            {
                //Accessories
                //Weapons
                ModContent.ItemType<GraveGrimreaver>(),
            };
            PostDestroyer = new List<int>
            {
                //Accessories
                //Weapons
            };
            PostAnyMech = new List<int>
            {
                ModContent.ItemType<SHPC>(),
                ModContent.ItemType<AnarchyBlade>(),
            };
            PostAllMechs = new List<int>
            {
                //Accessories
                ModContent.ItemType<MOAB>(),
                ModContent.ItemType<AngelTreads>(),
                ModContent.ItemType<HallowedRune>(),
                ModContent.ItemType<MomentumCapacitor>(),
                //Weapons
                ModContent.ItemType<TrueBiomeBlade>(),
                //ModContent.ItemType<TrueBloodyEdge>(),
                //Magic Weapons
                ModContent.ItemType<Cryophobia>(),
                //Summon Weapons
                ModContent.ItemType<SunGodStaff>(),
                //Rogue Weapons
                ModContent.ItemType<SpearofDestiny>(),
                //Classless
                //Vanilla
                3353,
                1225,
                935,
                4790,
            };
            PostBrimstoneElemental = new List<int>
            {
                //Accessories
                ModContent.ItemType<Gehenna>(),
                ModContent.ItemType<Abaddon>(),
                ModContent.ItemType<FlameLickedShell>(),
                ModContent.ItemType<RoseStone>(),
                //Weapons
                ModContent.ItemType<Brimlance>(),
                //Ranged Weapons
                ModContent.ItemType<Hellborn>(),
            };
            PostCalamitas = new List<int>
            {
                //Accessories
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
            };
            PostPlantera = new List<int>
            {
                //Accessories
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
                //ModContent.ItemType<TerraEdge>(),
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
                // ModContent.ItemType<SpectreRifle>(),
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
                3336,
                758,
                1255,
                788,
                1178,
                1259,
                1155,
                3018,
                1182,
                1305,
                1157,
                3021,
                1929,
                1928,
            };
            PostAstrumAureus = new List<int>
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
            };
            PostAnahita = new List<int>
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
            };
            PostGolem = new List<int>
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
                3337,
                1294,
                1258,
                1122,
                899,
                1248,
                1294,
                1295,
                1296,
                1297,
                2218,
                1301,
                1858,
                1343,
            };
            PostEmpress = new List<int>
            {
                //Accessories
                //Weapons
                //Vanilla
                4923,
                4914,
                5005,
                4952,
                4953,
                4823,
                4715,
                5075,
            };
            PostPlaguebringerGoliath = new List<int>
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
            };
            PostDukeFishron = new List<int>
            {
                //Accessories
                //Weapons
                ModContent.ItemType<BrinyBaron>(),
                ModContent.ItemType<DukesDecapitator>(),
                //Vanilla
                3367,
                2623,
                2611,
                2622,
                2621,
                2624,
                2609
            };
            PostRavager = new List<int>
            {
                //Accessories
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
            };
            PostCultist = new List<int>
            {
                //Accessories
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
                3549,
            };
            PostAstrumDeus = new List<int>
            {
                //Accessories
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
            };



            //POSTMOONLORD



            PostMoonlord = new List<int>
            {
                //Accessories
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
                //Classless
                //Vanilla
                1131,
                3577,
                4954,
                3063,
                3389,
                3065,
                1553,
                3541,
                3570,
                3571,
                3569,
                3930,
                3384,
                3460,
                4469,
            };
            PostProfanedGuardians = new List<int>
            {
                //Accessories
                ModContent.ItemType<WarbanneroftheSun>(),
                //Weapons
                ModContent.ItemType<RelicOfDeliverance>(),
            };
            PostDragonFolly = new List<int>
            {
                //Accessories
                ModContent.ItemType<BlunderBooster>(),
                ModContent.ItemType<DynamoStemCells>(),
                //Weapons
                ModContent.ItemType<Swordsplosion>(),
                ModContent.ItemType<GildedProboscis>(),
                //Ranged Weapons
                ModContent.ItemType<GoldenEagle>(),
                //Magic Weapon
                ModContent.ItemType<MadAlchemistsCocktailGlove>(),
                ModContent.ItemType<RougeSlash>(),

            };
            PostProvidence = new List<int>
            {
                //Accessories
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
            };
            PostStormWeaver = new List<int>
            {
                //Accessories
                //Ranged Weapons
                ModContent.ItemType<TheStorm>(),
                ModContent.ItemType<Seadragon>(),
                ModContent.ItemType<StormDragoon>(),
                //Magic Weapons
                ModContent.ItemType<Thunderstorm>(),
                ModContent.ItemType<DeificThunderbolt>(),
            };
            PostCeaselessVoid = new List<int>
            {
                //Accessories
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
                ModContent.ItemType<VoidConcentrationStaff>(),// this is also private, will be changed in next cal update (ty YuH)
                //Rogue Weapons
                ModContent.ItemType<SealedSingularity>(),
            };
            PostSignus = new List<int>
            {
                //Accessories
                ModContent.ItemType<SpectralVeil>(),
                //Weapons
                //Ranged Weapons
                ModContent.ItemType<TheSevensStriker>(),
                //Magic Weapons
                ModContent.ItemType<AethersWhisper>(),
                //Summon Weapons
                ModContent.ItemType<Cosmilamp>(),
                //Rogue Weapons
                ModContent.ItemType<CosmicKunai>(),
            };
            PostPolterghast = new List<int>
            {
                //Accessories
                ModContent.ItemType<PhantomicArtifact>(),// this is private for some reason????
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
            };
            PostOldDuke = new List<int>
            {
                //Accessories
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
            };
            PostDoG = new List<int>
            {
                //Accessories
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
            };
            PostYharon = new List<int>
            {
                //Accessories
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
            };
            PostDraedon = new List<int>
            {
                //Accessories
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
            };
            PostSCal = new List<int>
            {
                //Accessories
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
            };
            Endgame = new List<int>
            {
                //Accessories
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
            };


            //MISC


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
            PostKingSlime = null;
            PostDesertScourge = null;
            PostEyeOfCthulhu = null;
            PostCrabulon = null;
            PostEvil1 = null;
            PostEvil2 = null;
            PostQueenBee = null;
            PostDeerclops = null;
            PostSkeletron = null;
            PostSkeletron = null;
            PostWallOfFlesh = null;
            PostQueenSlime = null;
            PostCryogen = null;
            PostAquaticScourge = null;
            PostTwins = null;
            PostSkeletronPrime = null;
            PostDestroyer = null;
            PostAnyMech = null;
            PostAllMechs = null;
            PostBrimstoneElemental = null;
            PostCalamitas = null;
            PostPlantera = null;
            PostAstrumAureus = null;
            PostAnahita = null;
            PostGolem = null;
            PostEmpress = null;
            PostPlaguebringerGoliath = null;
            PostDukeFishron = null;
            PostRavager = null;
            PostCultist = null;
            PostAstrumDeus = null;
            PostMoonlord = null;
            PostProfanedGuardians = null;
            PostDragonFolly = null;
            PostProvidence = null;
            PostStormWeaver = null;
            PostCeaselessVoid = null;
            PostSignus = null;
            PostPolterghast = null;
            PostOldDuke = null;
            PostDoG = null;
            PostYharon = null;
            PostDraedon = null;
            PostSCal = null;
            Endgame = null;
            SCalTooltips = null;
            ShroomsDrawProjs = null;
        }
    }
}