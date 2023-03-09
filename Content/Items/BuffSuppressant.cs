using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Mounts;
using CalamityMod.Buffs.Pets;
using CalamityMod.Buffs.Placeables;
using CalamityMod.Buffs.Potions;
using CalamityMod.Buffs.StatBuffs;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.Items
{
    public class BuffSuppressant : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Buff Suppressant");
            Tooltip.SetDefault("Favorite to disable certain rule-breaking buffs\n" +
            "Instantly kills the player under the effects of Holy Inferno");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateInventory(Player player)
        {
            if (Item.favorited)
            {
                if (player.FindBuffIndex(ModContent.BuffType<HolyInferno>()) > -1)
                {
                    player.KillMe(PlayerDeathReason.ByOther(11), 1000.0, 0, false);
                }
                if (player.FindBuffIndex(BuffID.BrainOfConfusionBuff) > -1)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was unable to comprehend the big brain power."), 1000.0, 0, false);
                }

                player.buffImmune[BuffID.ShadowDodge] = true;
                player.onHitDodge = false;

                player.buffImmune[ModContent.BuffType<TarragonImmunity>()] = true;
                player.Calamity().tarragonImmunity = false;

                if (!NPC.downedSlimeKing)
                {
                    player.buffImmune[BuffID.SlimeMount] = true;
                }
                if (!DownedBossSystem.downedHiveMind || !DownedBossSystem.downedPerforator)
                {
                    player.buffImmune[ModContent.BuffType<TeslaBuff>()] = true;
                }
                if (!NPC.downedBoss1) //Eye of Cthulhu
                {
                    player.buffImmune[ModContent.BuffType<SulphurskinBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<EffigyOfDecayBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<RadiatorBuff>()] = true;
                }
                if (!NPC.downedQueenBee)
                {
                    //Vanilla
                    player.buffImmune[BuffID.WeaponImbuePoison] = true;
                    player.buffImmune[BuffID.WeaponImbueConfetti] = true;
                    player.buffImmune[BuffID.WeaponImbueFire] = true;
                    player.buffImmune[BuffID.BeeMount] = true;
                }
                if (!NPC.downedBoss3) //Skeletron
                {
                    player.buffImmune[ModContent.BuffType<ShadowBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<Omniscience>()] = true;
                    player.buffImmune[BuffID.Bewitched] = true;
                    //player.buffImmune[ModContent.BuffType<OceanSpiritBuff>()] = true;
                }
                if (!Main.hardMode)
                {
                    player.buffImmune[ModContent.BuffType<WeaponImbueCrumbling>()] = true;
                    player.buffImmune[ModContent.BuffType<Soaring>()] = true;
                    //Booze
                    player.buffImmune[ModContent.BuffType<FabsolVodkaBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<FireballBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<GrapeBeerBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<RedWineBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<RumBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<WhiskeyBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<TequilaBuff>()] = true;
                    //Station
                    player.buffImmune[ModContent.BuffType<CirrusBlueCandleBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<CirrusPinkCandleBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<CirrusPurpleCandleBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<CirrusYellowCandleBuff>()] = true;
                    player.buffImmune[BuffID.Clairvoyance] = true;
                    //Pets and Mounts
                    //player.buffImmune[ModContent.BuffType<SparksBuff>()] = true;
                    player.buffImmune[BuffID.BasiliskMount] = true;
                    player.buffImmune[BuffID.UnicornMount] = true;
                    player.buffImmune[BuffID.PigronMount] = true;
                    //Vanilla
                    player.buffImmune[BuffID.WeaponImbueGold] = true;
                    player.buffImmune[BuffID.WeaponImbueIchor] = true;
                    player.buffImmune[BuffID.WeaponImbueCursedFlames] = true;
                }
                //Destroyer = 1, Twins = 2, Prime = 3
                /*if (!NPC.downedMechBoss2)
                {
                    //Fairy Pet Moment
                    player.buffImmune[BuffID.FairyRed] = true;
                    player.buffImmune[BuffID.FairyGreen] = true;
                    player.buffImmune[BuffID.FairyBlue] = true;
                }
                if (!NPC.downedMechBoss1 && !NPC.downedMechBoss2 && !NPC.downedMechBoss3)
                {
                    player.buffImmune[BuffID.PetDD2Ghost] = true;
                }*/
                if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
                {
                    //Booze
                    player.buffImmune[ModContent.BuffType<VodkaBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<ScrewdriverBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<WhiteWineBuff>()] = true;
                    player.buffImmune[BuffID.WeaponImbueGold] = true;
                    //Pets and Mounts
                    player.buffImmune[BuffID.MinecartRightMech] = true;
                    player.buffImmune[BuffID.MinecartLeftMech] = true;
                }
                if (!NPC.downedPlantBoss)
                {
                    //Booze
                    player.buffImmune[ModContent.BuffType<EvergreenGinBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<MargaritaBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<CaribbeanRumBuff>()] = true;
                    //Station
                    player.buffImmune[ModContent.BuffType<ChaosCandleBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<TranquilityCandleBuff>()] = true;
                    //Vanilla
                    player.buffImmune[BuffID.WeaponImbueNanites] = true;
                    player.buffImmune[BuffID.WeaponImbueVenom] = true;
                    //player.buffImmune[BuffID.PumpkingPet] = true;
                    player.buffImmune[BuffID.Rudolph] = true;
                    //player.buffImmune[BuffID.Wisp] = true;
                }
                if (!DownedBossSystem.downedAstrumAureus)
                {
                    player.buffImmune[ModContent.BuffType<AstralInjectionBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<GravityNormalizerBuff>()] = true;
                    //Booze
                    player.buffImmune[ModContent.BuffType<BloodyMaryBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<EverclearBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<StarBeamRyeBuff>()] = true;
                }
                /*if (!NPC.downedEmpressOfLight)
                {
                    player.buffImmune[BuffID.FairyQueenPet] = true;
                }*/
                if (!NPC.downedGolemBoss)
                {
                    //player.buffImmune[ModContent.BuffType<ArmorShattering>()] = true;
                    //Booze
                    player.buffImmune[ModContent.BuffType<MoscowMuleBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<CinnamonRollBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<MoonshineBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<TequilaSunriseBuff>()] = true;
                    //Pets and Mounts
                    player.buffImmune[BuffID.UFOMount] = true;
                    player.buffImmune[BuffID.ScutlixMount] = true;
                    //player.buffImmune[BuffID.GolemPet] = true;
                }
                if (!NPC.downedFishron)
                {
                    player.buffImmune[BuffID.CuteFishronMount] = true;
                }
                /*if (!DownedBossSystem.downedAstrumDeus)
                {
                    player.buffImmune[ModContent.BuffType<Dreamfog>()] = true;
                }*/
                if (!NPC.downedMoonlord)
                {
                    player.buffImmune[BuffID.NebulaUpDmg1] = true;
                    player.buffImmune[BuffID.NebulaUpDmg2] = true;
                    player.buffImmune[BuffID.NebulaUpDmg3] = true;
                    player.buffImmune[ModContent.BuffType<SquishyBeanBuff>()] = true;
                    //player.buffImmune[BuffID.SuspiciousTentacle] = true;
                    player.buffImmune[BuffID.DrillMount] = true;
                }
                if (!DownedBossSystem.downedDragonfolly)
                {
                    player.buffImmune[ModContent.BuffType<BumbledogeMount>()] = true;
                }
                if (!DownedBossSystem.downedProvidence)
                {
                    player.buffImmune[ModContent.BuffType<BloodfinBoost>()] = true;
                    player.buffImmune[ModContent.BuffType<BrimroseMount>()] = true;
                }
                if (!DownedBossSystem.downedCeaselessVoid)
                {
                    player.buffImmune[ModContent.BuffType<CeaselessHunger>()] = true;
                }
                /*if (!DownedBossSystem.downedStormWeaver)
                {
                    player.buffImmune[ModContent.BuffType<LittleLightBuff>()] = true;
                }*/
                if (!DownedBossSystem.downedDoG)
                {
                    player.buffImmune[ModContent.BuffType<AlicornBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<GazeOfCrysthamyrBuff>()] = true;
                    player.buffImmune[ModContent.BuffType<TheCartofGodsBuff>()] = true;
                }

                if (!DownedBossSystem.downedExoMechs)
                {
                    player.buffImmune[ModContent.BuffType<DraedonGamerChairBuff>()] = true;
                }
            }
        }
    }
}