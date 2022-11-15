using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Buffs.Potions;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.Placeables;
using CalamityMod.CalPlayer;
using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Armor;
using CalamityMod.Items.Armor.Daedalus;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Typeless.FiniteUse;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using System.Linq;

namespace CalNohitQoL.Items
{
    public class CalNohitQoLGlobalItem : GlobalItem
    {

        Color tooltipcolour = Color.Red;

        public override void ModifyTooltips(Item itemID, List<TooltipLine> tooltips)
        {
            //Fountains

            TooltipLine line;
            TooltipLine nameLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (nameLine != null)
            {
                ApplyRarityColor(itemID, nameLine);
            }
             
            if (itemID.type == ModContent.ItemType<TheCommunity>())
            {
                string bossName = CalNohitQoLUtils.GetLatestBossKilled();
                TooltipLine obj2 = new TooltipLine(Mod, "1", "People who have nohit SCal with this:");
                obj2.Text = "Current power level: "+bossName;
                tooltips.Add(obj2);
            }

            switch (itemID.type)
            {
                case ItemID.PureWaterFountain:
                    line = new TooltipLine(Mod, "Tooltip0", "Forces surrounding biome state to Ocean upon activation");
                    tooltips.Add(line);
                    break;

                case ItemID.OasisFountain:
                case ItemID.DesertWaterFountain:
                    line = new TooltipLine(Mod, "Tooltip0", "Forces surrounding biome state to Desert upon activation");
                    tooltips.Add(line);
                    break;

                case ItemID.JungleWaterFountain:
                    line = new TooltipLine(Mod, "Tooltip0", "Forces surrounding biome state to Jungle upon activation");
                    tooltips.Add(line);
                    break;

                case ItemID.IcyWaterFountain:
                    line = new TooltipLine(Mod, "Tooltip0", "Forces surrounding biome state to Snow upon activation");
                    tooltips.Add(line);
                    break;

                case ItemID.CorruptWaterFountain:
                    line = new TooltipLine(Mod, "Tooltip0", "Forces surrounding biome state to Corruption upon activation");
                    tooltips.Add(line);
                    break;

                case ItemID.CrimsonWaterFountain:
                    line = new TooltipLine(Mod, "Tooltip1", "Forces surrounding biome state to Crimson upon activation");
                    tooltips.Add(line);
                    break;

                case ItemID.HallowedWaterFountain:
                    line = new TooltipLine(Mod, "Tooltip1", "In hardmode, forces surrounding biome state to Hallow upon activation");
                    tooltips.Add(line);
                    break;

                //cavern fountain?
            }


            //potion tooltips
            if (CalNohitQoL.Instance.PotionTooltips)
            {
                if (itemID.type == ModContent.ItemType<TriumphPotion>())
                {
                    if (!DownedBossSystem.downedDesertScourge)
                    {
                        string text = "This is post Desert Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TriumphPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (itemID.type == ModContent.ItemType<SulphurskinPotion>())
                {
                    if (!NPC.downedBoss1)//boss1 = eoc, boss2 = eow/boc, boss3 = skeletron
                    {
                        string text = "This is post Eye Of Cthulhu, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SulphurskinPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                }
                if (itemID.type == ModContent.ItemType<TeslaPotion>())
                {
                    if (!DownedBossSystem.downedHiveMind && !DownedBossSystem.downedPerforator)
                    {
                        string text = "This is post Hive Mind/Perforators, you should probably not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TeslaPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (itemID.type == ItemID.BundleofBalloons)
                {
                    if (!DownedBossSystem.downedHiveMind && !DownedBossSystem.downedPerforator)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Hive Mind / Perforators, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                }
                if (itemID.type == ItemID.FlaskofPoison || itemID.type == ItemID.FlaskofFire || itemID.type == ItemID.FlaskofParty)
                {
                    if (!NPC.downedQueenBee)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Queen Bee, you should not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                }
                if (!NPC.downedBoss3)
                {
                    if (itemID.type == ModContent.ItemType<PotionofOmniscience>())
                    {
                        string text = "This is post Skeletron, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: PotionofOmniscience", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<YharimsStimulants>())
                    {
                        string text = "This is post Skeletron, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: YharimsStimulants", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                }
                if (!Main.hardMode)
                {
                    if (itemID.type == ModContent.ItemType<FabsolsVodka>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: FabsolsVodka", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<GrapeBeer>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: GrapeBeer", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Tequila>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Tequila", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Whiskey>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Whiskey", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Rum>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Rum", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Fireball>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Fireball", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<RedWine>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: RedWine", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SoaringPotion>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SoaringPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<CrumblingPotion>())
                    {
                        string text = "This is post Wall Of Flesh, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CrumblingPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (!NPC.downedMechBoss1 && !NPC.downedMechBoss2 && !NPC.downedMechBoss3)
                {
                    if (itemID.type == ModContent.ItemType<Vodka>())
                    {
                        string text = "This is post 3 Mechanical Bosses, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Vodka", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Screwdriver>())
                    {
                        string text = "This is post 3 Mechanical Bosses, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Screwdriver", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<WhiteWine>())
                    {
                        string text = "This is post 3 Mechanical Bosses, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: WhiteWine", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (!NPC.downedPlantBoss)
                {
                    if (itemID.type == ModContent.ItemType<EvergreenGin>())
                    {
                        string text = "This is post Plantera, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: EvergreenGin", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Margarita>())
                    {
                        string text = "This is post Plantera, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Margarita", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<CaribbeanRum>())
                    {
                        string text = "This is post Plantera, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CaribbeanRum", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (!DownedBossSystem.downedAstrumAureus)
                {
                    if (itemID.type == ModContent.ItemType<Everclear>())
                    {
                        string text = "This is post Astrum Aureus, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Everclear", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<StarBeamRye>())
                    {
                        string text = "This is post Astrum Aureus, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: StarBeamRye", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<BloodyMary>())
                    {
                        string text = "This is post Astrum Aureus, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: BloodyMary", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<GravityNormalizerPotion>())
                    {
                        string text = "This is post Astrum Aureus, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: GravityNormalizerPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<AureusCell>())
                    {
                        string text = "This is post Astrum Aureus, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: AureusCell", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<AstralInjection>())
                    {
                        string text = "This is post Astrum Aureus, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: AstralInjection", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (!NPC.downedGolemBoss)
                {
                    if (itemID.type == ModContent.ItemType<MoscowMule>())
                    {
                        string text = "This is post Golem, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: MoscowMule", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<TequilaSunrise>())
                    {
                        string text = "This is post Golem, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TequilaSunrise", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Moonshine>())
                    {
                        string text = "This is post Golem, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Moonshine", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<CinnamonRoll>())
                    {
                        string text = "This is post Golem, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CinnamonRoll", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    //if (itemID.type == ModContent.ItemType<ShatteringPotion>())
                    //{
                    //    string text = "This is post Golem, you should not be using it.";
                    //    TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ShatteringPotion", text) { OverrideColor = Color.Red };
                    //    tooltips.Add(tooltip);
                    //}
                    if (itemID.type == ModContent.ItemType<TitanScalePotion>())
                    {
                        string text = "This is post Golem, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TitanScalePotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (!DownedBossSystem.downedCalamitas)
                {
                    if (itemID.type == ModContent.ItemType<FlaskOfBrimstone>())
                    {
                        string text = "This is post Calamitas, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: FlaskOfBrimstone", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<PenumbraPotion>())
                    {
                        string text = "This is post Calamitas, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: PenumbraPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
                if (!NPC.downedMoonlord)
                {
                    if (itemID.type == ModContent.ItemType<HolyWrathPotion>())
                    {
                        string text = "This is post Moon Lord, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: HolyWrathPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);

                    }
                    if (itemID.type == ModContent.ItemType<ProfanedRagePotion>())
                    {
                        string text = "This is post Moon Lord, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ProfanedRagePotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);

                    }
                    if (itemID.type == ModContent.ItemType<SupremeHealingPotion>())
                    {
                        string text = "This is post Moon Lord, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SupremeHealingPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);

                    }

                    if (itemID.type == ModContent.ItemType<SupremeManaPotion>())
                    {
                        string text = "This is post Moon Lord, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SupremeManaPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);

                    }

                }
                if (!DownedBossSystem.downedProvidence)
                {
                    if (itemID.type == ModContent.ItemType<Bloodfin>())
                    {
                        string text = "This is post Providence, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Bloodfin", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);

                    }
                }
                if (!DownedBossSystem.downedCeaselessVoid)
                {
                    if (itemID.type == ModContent.ItemType<CeaselessHungerPotion>())
                    {
                        string text = "This is post Ceaseless Void, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CeaselessHungerPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                }
                if (!DownedBossSystem.downedDoG)
                {
                    if (itemID.type == ModContent.ItemType<OmegaHealingPotion>())
                    {
                        string text = "This is post DoG, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: OmegaHealingPotion", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }


                }
                if (!DownedBossSystem.downedYharon)
                {
                    if (itemID.type == ModContent.ItemType<DraconicElixir>())
                    {
                        string text = "This is post Yharon, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: DraconicElixir", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }
            }
            //early hm blocks
            if (CalNohitQoL.Instance.ItemTooltips)
            {
                if (!NPC.downedMechBoss1)
                {
                    if (itemID.type == ModContent.ItemType<GaussPistol>())
                    {
                        string text = "This is post Destroyer and another mech, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: GaussPistol", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ItemID.Megashark)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Destroyer, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }


                    }

                    if (itemID.type == ModContent.ItemType<MajesticGuard>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Destroyer and another mech, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: MajesticGuard", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                    if (!NPC.downedMechBoss2 && !NPC.downedMechBoss3)
                    {

                    }

                }
                if (NPC.downedMechBoss1)
                {
                    if (!NPC.downedMechBoss2)
                    {
                        if (!NPC.downedMechBoss3)
                        {
                            if (itemID.type == ModContent.ItemType<MajesticGuard>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                            {
                                string text = "This is post Destroyer and another mech, you should not be using it.";
                                TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: MajesticGuard", text) { OverrideColor = Color.Red };
                                tooltips.Add(tooltip);
                            }

                        }
                    }

                }
                if (!NPC.downedMechBoss2)
                {
                    if (itemID.type == ModContent.ItemType<SpeedBlaster>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Twins, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SpeedBlaster", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<FrequencyManipulator>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Twins, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: FrequencyManipulator", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<HydraulicVoltCrasher>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Twins, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: HydraulicVoltCrasher", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ItemID.RainbowRod)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Twins, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }


                    }
                    if (itemID.type == ItemID.MagicalHarp)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Twins, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }


                    }
                    if (itemID.type == ItemID.OpticStaff)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Destroyer, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }


                    }

                }
                if (!NPC.downedMechBoss3)
                {

                    if (itemID.type == ItemID.Flamethrower)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Skeletron Prime, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ModContent.ItemType<InfernaCutter>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Skeletron Prime, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: InfernaCutter", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<MatterModulator>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Skeletron Prime, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: MatterModulator", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<MountedScanner>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Skeletron Prime, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: MountedScanner", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }               
                    if (itemID.type == ModContent.ItemType<ForbiddenOathblade>())//if destroyer is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Skeletron Prime, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ForbiddenOathblade", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    //the above comments are wrong but im lazy to change them lol

                }
                if (NPC.downedMechBoss3)
                {
                    if (!NPC.downedMechBoss1)
                    {
                        if (!NPC.downedMechBoss2)
                        {
                            if (itemID.type == ModContent.ItemType<IonBlaster>())//if prime is dead but neither of the other two are, so no hallowed bars.
                            {
                                string text = "This is post Skeletron Prime and another Mech, you should not be using it.";
                                TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: IonBlaster", text) { OverrideColor = Color.Red };
                                tooltips.Add(tooltip);
                            }
                        }
                    }
                }
                if (!NPC.downedMechBoss2 && !NPC.downedMechBoss1)
                {
                    if (!NPC.downedMechBoss3)
                    {
                        //if no mech is dead:

                        if (itemID.type == ItemID.SpiritFlame)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.SkyFracture)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.OnyxBlaster)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }


                        if (itemID.type == ModContent.ItemType<TundraFlameBlossomsStaff>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TundraFlameBlossomsStaff", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<ElectriciansGlove>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ElectriciansGlove", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<RuinMedallion>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: RuinMedallion", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<HoneyDew>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: HoneyDew", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<BrimstoneFury>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: BrimstoneFury", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<BrimstoneSword>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: BrimstoneSword", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<BurningSea>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: BurningSea", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<BrimroseStaff>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: BrimroseStaff", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<Brimblade>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Brimblade", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<IgneousExaltation>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: IgneousExaltation", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<WyvernsCall>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: WyvernsCall", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<Nychthemeron>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Nychthemeron", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<StormfrontRazor>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: StormfrontRazor", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<MythrilKnife>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: MythrilKnife", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<OrichalcumSpikedGemstone>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: OrichalcumSpikedGemstone", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<AnarchyBlade>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: AnarchyBlade", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<SHPC>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SHPC", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                            if (itemID.type == ModContent.ItemType<Nychthemeron>())//if prime is dead but neither of the other two are, so no hallowed bars.
                            {
                                string text = "This is post a Mech, you should not be using it.";
                                TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Nychthemeron", text) { OverrideColor = Color.Red };
                                tooltips.Add(tooltip);
                            }
                            //back to vanilla, jesus this is a slog to write..

                            if (itemID.type == ItemID.OrichalcumSword)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.OrichalcumRepeater)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.OrichalcumHalberd)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.OrichalcumChainsaw)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.OrichalcumDrill)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.OrichalcumPickaxe)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.OrichalcumWaraxe)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MythrilSword)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MythrilRepeater)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MythrilHalberd)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MythrilDrill)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MythrilChainsaw)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MythrilPickaxe)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MythrilWaraxe)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.Yelets)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MushroomSpear)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.Hammush)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.Code2)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.BookStaff)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.DD2PhoenixBow)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MonkStaffT1)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.MonkStaffT2)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }
                        if (itemID.type == ItemID.DD2SquireDemonSword)
                        {
                            foreach (TooltipLine line3 in tooltips)
                            {
                                if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                                {
                                    string extratext = "This is post a Mech, you should probably not be using it";
                                    line3.Text = extratext;
                                    line3.OverrideColor = Color.Red;
                                }
                            }
                        }

                    }
                }

                if (!DownedBossSystem.downedCryogen)
                {
                    //if cryogen is not dead
                    if (itemID.type == ModContent.ItemType<EffluviumBow>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: EffluviumBow", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                    if (itemID.type == ModContent.ItemType<Icebreaker>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Icebreaker", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Avalanche>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Avalanche", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SnowstormStaff>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SnowstormStaff", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<KelvinCatalyst>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: KelvinCatalyst", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<FrostbiteBlaster>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: FrostbiteBlaster", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<IcicleTrident>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: IcicleTrident", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<IceStar>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Cryogen, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: IceStar", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }


                }

                if (!DownedBossSystem.downedAquaticScourge)
                {
                    if (itemID.type == ModContent.ItemType<SubmarineShocker>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SubmarineShocker", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Barinautical>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Barinautical", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Downpour>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Downpour", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<DeepseaStaff>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: DeepseaStaff", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<ScourgeoftheSeas>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ScourgeoftheSeas", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SulphurousGrabber>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SulphurousGrabber", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<FlakToxicannon>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: FlakToxicannon", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SlitheringEels>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SlitheringEels", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<BelchingSaxophone>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: BelchingSaxophone", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<OrthoceraShell>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: OrthoceraShell", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SkyfinBombers>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SkyfinBombers", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SpentFuelContainer>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SpentFuelContainer", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Bonebreaker>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Bonebreaker", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<CorrodedCaustibow>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CorrodedCaustibow", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Miasma>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Miasma", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<AcidicRainBarrel>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Aquatic Scourge, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: AcidicRainBarrel", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }

                if (!DownedBossSystem.downedBrimstoneElemental)
                {
                    if (itemID.type == ModContent.ItemType<Brimlance>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Brimstone Elemental, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Brimlance", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SeethingDischarge>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Brimstone Elemental, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SeethingDischarge", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<DormantBrimseeker>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post Brimstone Elemental, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Brimlance", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }



                }

                bool titaniumallowed = false;
                if ((!NPC.downedMechBoss1 || !NPC.downedMechBoss2) && (!NPC.downedMechBoss2 || !NPC.downedMechBoss3))
                {
                    if (NPC.downedMechBoss1)
                    {
                        titaniumallowed = NPC.downedMechBoss3;


                    }
                    else
                    {


                        titaniumallowed = false;
                    }
                }
                else
                {
                    titaniumallowed = true;
                }
                if (!titaniumallowed)
                {
                    if (itemID.type == ModContent.ItemType<ConsecratedWater>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ConsecratedWater", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<DesecratedWater>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: DesecratedWater", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<ForgottenApexWand>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ForgottenApexWand", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<SpearofPaleolith>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: SpearofPaleolith", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<ForsakenSaber>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ForsakenSaber", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);

                    }
                    if (itemID.type == ModContent.ItemType<MineralMortar>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: MineralMortar", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                    if (itemID.type == ItemID.TitaniumSword)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.TitaniumTrident)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.TitaniumRepeater)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.TitaniumPickaxe)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.TitaniumDrill)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.TitaniumChainsaw)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.TitaniumWaraxe)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.AdamantiteRepeater)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.AdamantiteSword)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.AdamantitePickaxe)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.AdamantiteDrill)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.AdamantiteChainsaw)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.AdamantiteWaraxe)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.AdamantiteGlaive)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post two Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }

                    if (itemID.type == ModContent.ItemType<TitaniumRailgun>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TitaniumRailgun", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<TitaniumShuriken>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TitaniumShuriken", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<AdamantiteThrowingAxe>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: AdamantiteThrowingAxe", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }



                    if (itemID.type == ModContent.ItemType<AdamantiteParticleAccelerator>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: AdamantiteParticleAccelerator", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<DeathValleyDuster>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: DeathValleyDuster", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<RelicofRuin>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: RelicofRuin", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                   
                    if (itemID.type == ModContent.ItemType<Bazooka>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post two Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Bazooka", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (!NPC.downedMechBoss3)
                    {
                        if (itemID.type == ModContent.ItemType<IonBlaster>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Prime and another Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: IonBlaster", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                    }
                }

                if (!DownedBossSystem.downedCryogen)
                {

                    if (!NPC.downedMechBossAny)
                    {
                        if (itemID.type == ModContent.ItemType<CrystalPiercer>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CrystalPiercer", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }

                        if (itemID.type == ModContent.ItemType<FlarefrostBlade>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: FlarefrostBlade", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<DarklightGreatsword>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: DarklightGreatsword", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<StarnightLance>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: StarnightLance", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<DarkechoGreatbow>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: DarkechoGreatbow", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<Shimmerspark>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Shimmerspark", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<ShadecrystalTome>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ShadecrystalTome", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<DaedalusGolemStaff>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: DaedalusGolemStaff", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<ShardlightPickaxe>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ShardlightPickaxe", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                        if (itemID.type == ModContent.ItemType<AbyssalWarhammer>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {

                            string text = "This is post Cryo and a Mech, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: AbyssalWarhammer", text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }



                    }
                }

                bool finaltierstore = false;
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    finaltierstore = DownedBossSystem.downedCryogen;
                }
                else
                {
                    finaltierstore = false;
                }
                if (!finaltierstore)
                {
                    if (itemID.type == ModContent.ItemType<ArcticBearPaw>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post Cryo and all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ArcticBearPaw", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<FrostyFlare>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post Cryo and all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: FrostyFlare", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<CryogenicStaff>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post Cryo and all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CryogenicStaff", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }

                }

                if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
                {
                    if (itemID.type == ItemID.PickaxeAxe)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Skeletron Prime, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.Drax)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Skeletron Prime, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }


                    if (itemID.type == ModContent.ItemType<ValkyrieRay>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: ValkyrieRay", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<CatastropheClaymore>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: CatastropheClaymore", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Pwnagehammer>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Pwnagehammer", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ModContent.ItemType<Exorcism>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: Exorcism", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ItemID.LightDisc)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Skeletron Prime, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.Gungnir)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Skeletron Prime, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.HallowedRepeater)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is post Skeletron Prime, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.Excalibur)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ModContent.ItemType<GleamingMagnolia>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: GleamingMagnolia", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ItemID.TrueExcalibur)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.TrueNightsEdge)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ModContent.ItemType<TerrorTalons>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {
                        string text = "This is post all Mechs, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: TerrorTalons", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                    if (itemID.type == ItemID.ChlorophyteClaymore)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophyteSaber)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophytePartisan)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophyteShotbow)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophytePickaxe)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophyteDrill)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophyteChainsaw)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophyteGreataxe)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophyteWarhammer)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.ChlorophyteJackhammer)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.VenomStaff)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }
                    if (itemID.type == ItemID.DeathSickle)
                    {
                        foreach (TooltipLine line3 in tooltips)
                        {
                            if (line3.Mod == "Terraria" && line3.Name == "Tooltip1")
                            {
                                string extratext = "This is all Mechs, you should probably not be using it";
                                line3.Text = extratext;
                                line3.OverrideColor = Color.Red;
                            }
                        }
                    }

                }

                /*if (!NPC.downedSlimeKing)
                {
                    foreach (string i in PostKingSlimeItems)
                    {
                        string itemtype = i;
                        if (itemID.type == ModContent.ItemType<itemtype>())//if prime is dead but neither of the other two are, so no hallowed bars.
                        {
                            string text = "This is post King Slime, you should not be using it.";
                            TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: " + i, text) { OverrideColor = Color.Red };
                            tooltips.Add(tooltip);
                        }
                    }
                }*/

                if (!NPC.downedSlimeKing)
                {
                    PostKingSlimeTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedDesertScourge)
                {
                    PostDesertScourgeTooltips(itemID, tooltips);
                }
                if (!NPC.downedBoss1)
                {
                    PostEyeOfCthulhuTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedCrabulon)
                {
                    PostCrabulonTooltips(itemID, tooltips);
                }
                if (!NPC.downedBoss2)
                {
                    PostEvil1Tooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedHiveMind || !DownedBossSystem.downedPerforator)
                {
                    PostEvil2Tooltips(itemID, tooltips);
                }
                if (!NPC.downedQueenBee)
                {
                    PostQueenBeeTooltips(itemID, tooltips);
                }
                if (!NPC.downedDeerclops)
                {
                    PostDeerclopsTooltips(itemID, tooltips);
                }
                if (!NPC.downedBoss3)
                {
                    PostSkeletronTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedSlimeGod)
                {
                    PostSlimeGodTooltips(itemID, tooltips);
                }
                if (!Main.hardMode)
                {
                    PostWallOfFleshTooltips(itemID, tooltips);
                }
                if (!NPC.downedQueenSlime)
                {
                    PostQueenSlimeTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedCryogen)
                {
                    PostCryogenTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedAquaticScourge)
                {
                    PostAquaticScourgeTooltips(itemID, tooltips);
                }
                if (!NPC.downedMechBoss2)
                {
                    if (CalNohitQoLLists.PostTwins.Contains(itemID.type))
                    {
                        string KStext = "This is post Twins, you should not be using it.";
                        TooltipLine KingSlimeTooltip = new TooltipLine(Mod, "CalamityMod: " + itemID.type, KStext)
                        {
                            OverrideColor = tooltipcolour
                        };
                        tooltips.Add(KingSlimeTooltip);
                    }

                }
                if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
                {
                    PostAllMechsTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedCalamitas)
                {
                    PostCalamitasTooltips(itemID, tooltips);
                }
                if (!NPC.downedPlantBoss)
                {
                    PostPlanteraTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedAstrumAureus)
                {
                    PostAstrumAureusTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedLeviathan)
                {
                    PostAnahitaTooltips(itemID, tooltips);
                }
                if (!NPC.downedGolemBoss)
                {
                    PostGolemTooltips(itemID, tooltips);
                }
                if (!NPC.downedEmpressOfLight)
                {
                    PostEmpressTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedPlaguebringer)
                {
                    PostPlaguebringerGoliathTooltips(itemID, tooltips);
                }
                if (!NPC.downedFishron)
                {
                    PostDukeFishronTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedRavager)
                {
                    PostRavagerTooltips(itemID, tooltips);
                }
                if (!NPC.downedAncientCultist)
                {
                    PostCultistTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedAstrumDeus)
                {
                    PostAstrumDeusTooltips(itemID, tooltips);
                }
                if (!NPC.downedMoonlord)
                {
                    PostMoonlordTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedGuardians)
                {
                    PostProfanedGuardiansTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedDragonfolly)
                {
                    PostDragonFollyTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedProvidence)
                {
                    PostProvidenceTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedStormWeaver)
                {
                    PostStormWeaverTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedCeaselessVoid)
                {
                    PostCeaselessVoidTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedSignus)
                {
                    PostSignusTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedPolterghast)
                {
                    PostPolterghastTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedBoomerDuke)
                {
                    PostOldDukeTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedDoG)
                {
                    PostDoGTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedYharon)
                {
                    PostYharonTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedExoMechs)
                {
                    PostDraedonTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedSCal)
                {
                    PostSCalTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedSCal || !DownedBossSystem.downedExoMechs)
                {
                    EndgameTooltips(itemID, tooltips);
                }
                if (!DownedBossSystem.downedAdultEidolonWyrm)
                {
                    if (itemID.type == ModContent.ItemType<HalibutCannon>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        string text = "This is post Adult Eidolon Wyrm, you should not be using it.";
                        TooltipLine tooltip = new TooltipLine(Mod, "CalamityMod: HalibutCannon", text) { OverrideColor = Color.Red };
                        tooltips.Add(tooltip);
                    }
                }

            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (CalNohitQoL.Instance.PotionLock)

            {
                if (item.type == ModContent.ItemType<TriumphPotion>())
                {
                    return DownedBossSystem.downedDesertScourge;
                }
                if (item.type == ModContent.ItemType<SulphurskinPotion>())
                {
                    return NPC.downedBoss1;
                }
                if (item.type == ModContent.ItemType<TeslaPotion>())
                {
                    if (DownedBossSystem.downedHiveMind)
                    {
                        return DownedBossSystem.downedPerforator;
                    }
                    return false;
                }
                if (item.type == ItemID.FlaskofPoison || item.type == ItemID.FlaskofFire || item.type == ItemID.FlaskofParty)
                {
                    return NPC.downedQueenBee;
                }
                if (item.type == ModContent.ItemType<PotionofOmniscience>() || item.type == ModContent.ItemType<YharimsStimulants>())
                {
                    return NPC.downedBoss3;
                }
                if (item.type == ModContent.ItemType<FabsolsVodka>() || item.type == ModContent.ItemType<GrapeBeer>() || item.type == ModContent.ItemType<Tequila>() || item.type == ModContent.ItemType<Whiskey>() || item.type == ModContent.ItemType<Rum>() || item.type == ModContent.ItemType<Fireball>() || item.type == ModContent.ItemType<RedWine>())
                {
                    return Main.hardMode;
                }
                if (item.type == ModContent.ItemType<Vodka>() || item.type == ModContent.ItemType<Screwdriver>() || item.type == ModContent.ItemType<WhiteWine>())
                {
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2)
                    {
                        return NPC.downedMechBoss3;
                    }
                    return false;
                }
                if (item.type == ModContent.ItemType<EvergreenGin>() || item.type == ModContent.ItemType<Margarita>() || item.type == ModContent.ItemType<CaribbeanRum>())
                {
                    return NPC.downedPlantBoss;
                }
                if (item.type == ModContent.ItemType<Everclear>() || item.type == ModContent.ItemType<StarBeamRye>() || item.type == ModContent.ItemType<BloodyMary>())
                {
                    return DownedBossSystem.downedAstrumAureus;
                }
                if (item.type == ModContent.ItemType<MoscowMule>() || item.type == ModContent.ItemType<TequilaSunrise>() || item.type == ModContent.ItemType<Moonshine>() || item.type == ModContent.ItemType<CinnamonRoll>())
                {
                    return NPC.downedGolemBoss;
                }
                if (item.type == ModContent.ItemType<SoaringPotion>() || item.type == 1355 || item.type == 1356 || item.type == 1353 || item.type == 2352 || item.type == 2209)
                {
                    return Main.hardMode;
                }

                if (item.type == ModContent.ItemType<FlaskOfBrimstone>() || item.type == ModContent.ItemType<PenumbraPotion>())
                {
                    return DownedBossSystem.downedCalamitas;
                }
                if (item.type == 1357 || item.type == 1340)
                {
                    return NPC.downedPlantBoss;
                }
                if (item.type == ModContent.ItemType<GravityNormalizerPotion>() || item.type == ModContent.ItemType<AureusCell>() || item.type == ModContent.ItemType<AstralInjection>())
                {
                    return DownedBossSystem.downedAstrumAureus;
                }
                if (item.type == ModContent.ItemType<TitanScalePotion>())
                {
                    return NPC.downedGolemBoss;
                }
                if (item.type == 3544)
                {
                    return NPC.downedAncientCultist;
                }
                if (item.type == ModContent.ItemType<HolyWrathPotion>() || item.type == ModContent.ItemType<ProfanedRagePotion>() || item.type == ModContent.ItemType<SupremeHealingPotion>())
                {
                    return NPC.downedMoonlord;
                }
                if (item.type == ModContent.ItemType<Bloodfin>())
                {
                    return DownedBossSystem.downedProvidence;
                }
                if (item.type == ModContent.ItemType<CeaselessHungerPotion>())
                {
                    return DownedBossSystem.downedCeaselessVoid;
                }
                if (item.type == ModContent.ItemType<OmegaHealingPotion>())
                {
                    return DownedBossSystem.downedDoG;
                }
                if (item.type == ModContent.ItemType<DraconicElixir>())
                {
                    return DownedBossSystem.downedYharon;
                }
            }
            if (CalNohitQoL.Instance.ItemLock)
            {

                if (item.type == 533 || item.type == ModContent.ItemType<GaussPistol>())
                {
                    return NPC.downedMechBoss1;
                }
                if (item.type == ModContent.ItemType<MajesticGuard>())
                {
                    if (NPC.downedMechBoss1)
                    {
                        if (!NPC.downedMechBoss2)
                        {
                            return NPC.downedMechBoss3;
                        }
                        return true;
                    }
                    return false;
                }
                if (item.type == 494 || item.type == 495 || item.type == 2535 || item.type == ModContent.ItemType<SpeedBlaster>() || item.type == ModContent.ItemType<FrequencyManipulator>() || item.type == ModContent.ItemType<HydraulicVoltCrasher>())
                {
                    return NPC.downedMechBoss2;
                }
                if (item.type == 506 || item.type == ModContent.ItemType<InfernaCutter>() || item.type == ModContent.ItemType<MatterModulator>() || item.type == ModContent.ItemType<MountedScanner>() || item.type == ModContent.ItemType<ForbiddenOathblade>())
                {
                    return NPC.downedMechBoss3;
                }
                if (item.type == ModContent.ItemType<IonBlaster>())
                {
                    if (NPC.downedMechBoss3)
                    {
                        if (!NPC.downedMechBoss1)
                        {
                            return NPC.downedMechBoss2;
                        }
                        return true;
                    }
                    return false;
                }
                if (item.type == 3779 || item.type == 3787 || item.type == 3788 || item.type == ModContent.ItemType<TundraFlameBlossomsStaff>() || item.type == ModContent.ItemType<BrimstoneFury>() || item.type == ModContent.ItemType<BrimstoneSword>() || item.type == ModContent.ItemType<BurningSea>() || item.type == ModContent.ItemType<BrimroseStaff>() || item.type == ModContent.ItemType<Brimblade>() || item.type == ModContent.ItemType<IgneousExaltation>() || item.type == ModContent.ItemType<WyvernsCall>() || item.type == ModContent.ItemType<Nychthemeron>() || item.type == ModContent.ItemType<StormfrontRazor>() || item.type == ModContent.ItemType<MythrilKnife>() || item.type == ModContent.ItemType<OrichalcumSpikedGemstone>() || item.type == 1192 || item.type == 1194 || item.type == 1193 || item.type == 1196 || item.type == 1195 || item.type == 1197 || item.type == 1223 || item.type == 484 || item.type == 436 || item.type == 390 || item.type == 386 || item.type == 777 || item.type == 384 || item.type == 992 || item.type == 3286 || item.type == 756 || item.type == 787 || item.type == 3284 || item.type == 3852 || item.type == 3854 || item.type == 3835 || item.type == 3836 || item.type == 3823)
                {
                    if (!NPC.downedMechBoss1 && !NPC.downedMechBoss2)
                    {
                        return NPC.downedMechBoss3;
                    }
                    return true;
                }
                if (item.type == ModContent.ItemType<EffluviumBow>() || item.type == ModContent.ItemType<Icebreaker>() || item.type == ModContent.ItemType<Avalanche>() || item.type == ModContent.ItemType<SnowstormStaff>() || item.type == ModContent.ItemType<KelvinCatalyst>() || item.type == ModContent.ItemType<FrostbiteBlaster>() || item.type == ModContent.ItemType<IcicleTrident>() || item.type == ModContent.ItemType<IceStar>())
                {
                    return DownedBossSystem.downedCryogen;
                }
                if (item.type == ModContent.ItemType<SubmarineShocker>() || item.type == ModContent.ItemType<Barinautical>() || item.type == ModContent.ItemType<Downpour>() || item.type == ModContent.ItemType<DeepseaStaff>() || item.type == ModContent.ItemType<ScourgeoftheSeas>() || item.type == ModContent.ItemType<SulphurousGrabber>() || item.type == ModContent.ItemType<FlakToxicannon>() || item.type == ModContent.ItemType<SlitheringEels>() || item.type == ModContent.ItemType<BelchingSaxophone>() || item.type == ModContent.ItemType<OrthoceraShell>() || item.type == ModContent.ItemType<SkyfinBombers>() || item.type == ModContent.ItemType<SpentFuelContainer>())
                {
                    return DownedBossSystem.downedAquaticScourge;
                }
                if (item.type == ModContent.ItemType<Bonebreaker>() || item.type == ModContent.ItemType<CorrodedCaustibow>() || item.type == ModContent.ItemType<Miasma>() || item.type == ModContent.ItemType<AcidicRainBarrel>())
                {
                    if (DownedBossSystem.downedAquaticScourge)
                    {
                        return true;
                    }
                    return false;
                }

                if (item.type == ModContent.ItemType<Brimlance>() || item.type == ModContent.ItemType<SeethingDischarge>() || item.type == ModContent.ItemType<DormantBrimseeker>())
                {
                    return DownedBossSystem.downedBrimstoneElemental;
                }
                if (item.type == ModContent.ItemType<ConsecratedWater>() || item.type == ModContent.ItemType<DesecratedWater>() || item.type == ModContent.ItemType<ForgottenApexWand>() || item.type == ModContent.ItemType<SpearofPaleolith>() || item.type == ModContent.ItemType<ForsakenSaber>() || item.type == ModContent.ItemType<MineralMortar>() || item.type == 1199 || item.type == 1202 || item.type == 1201 || item.type == 1203 || item.type == 1224 || item.type == 1204 || item.type == 1199 || item.type == 1202 || item.type == 1201 || item.type == 1203 || item.type == 1224 || item.type == 482 || item.type == 778 || item.type == 481 || item.type == 388 || item.type == 993 || item.type == 387 || item.type == 482 || item.type == 778 || item.type == 481 || item.type == 388 || item.type == 993 || item.type == 1204 || item.type == 1200 || item.type == 406 || item.type == ModContent.ItemType<TitaniumRailgun>() || item.type == ModContent.ItemType<TitaniumShuriken>() || item.type == ModContent.ItemType<AdamantiteThrowingAxe>() || item.type == ModContent.ItemType<AdamantiteParticleAccelerator>())
                {
                    if ((!NPC.downedMechBoss1 || !NPC.downedMechBoss2) && (!NPC.downedMechBoss2 || !NPC.downedMechBoss3))
                    {
                        if (NPC.downedMechBoss1)
                        {
                            return NPC.downedMechBoss3;
                        }
                        return false;
                    }
                    return true;
                }
                if (item.type == ModContent.ItemType<CrystalPiercer>() || item.type == ModContent.ItemType<FlarefrostBlade>() || item.type == ModContent.ItemType<DarklightGreatsword>() || item.type == ModContent.ItemType<StarnightLance>() || item.type == ModContent.ItemType<DarkechoGreatbow>() || item.type == ModContent.ItemType<ShadecrystalTome>() || item.type == ModContent.ItemType<DaedalusGolemStaff>() || item.type == ModContent.ItemType<ShardlightPickaxe>() || item.type == ModContent.ItemType<AbyssalWarhammer>())
                {
                    if (DownedBossSystem.downedCryogen)
                    {
                        if ((!NPC.downedMechBoss1 || !NPC.downedMechBoss2) && (!NPC.downedMechBoss2 || !NPC.downedMechBoss3))
                        {
                            if (NPC.downedMechBoss1)
                            {
                                return NPC.downedMechBoss3;
                            }
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
                if (item.type == 990 || item.type == 579 || item.type == ModContent.ItemType<ValkyrieRay>() || item.type == ModContent.ItemType<CatastropheClaymore>() || item.type == ModContent.ItemType<Pwnagehammer>() || item.type == ModContent.ItemType<Exorcism>() || item.type == ModContent.ItemType<SunGodStaff>() || item.type == 561 || item.type == 368 || item.type == 550 || item.type == 578 || item.type == ModContent.ItemType<GleamingMagnolia>() || item.type == 675 || item.type == 674 || item.type == ModContent.ItemType<TerrorTalons>() || item.type == 1228 || item.type == 1226 || item.type == 1227 || item.type == 1229 || item.type == 1230 || item.type == 1231 || item.type == 1233 || item.type == 1232 || item.type == 1262 || item.type == 1234 || item.type == 2188 || item.type == 1327)
                {
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2)
                    {
                        return NPC.downedMechBoss3;
                    }
                    return false;
                }
                if (item.type == ModContent.ItemType<ArcticBearPaw>() || item.type == ModContent.ItemType<FrostyFlare>() || item.type == ModContent.ItemType<CryogenicStaff>())
                {
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        return DownedBossSystem.downedCryogen;
                    }
                    return false;
                }
            }
            if (CalNohitQoL.Instance.ItemLock)
            {
                if (!NPC.downedSlimeKing)
                {
                    if (CalNohitQoLLists.PostKingSlime.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedDesertScourge)
                {
                    if (CalNohitQoLLists.PostDesertScourge.Contains(item.type))
                    {
                        return false;
                    }

                }
                if (!NPC.downedBoss1)
                {
                    if (CalNohitQoLLists.PostEyeOfCthulhu.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCrabulon)
                {
                    if (CalNohitQoLLists.PostCrabulon.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedBoss2)
                {
                    if (CalNohitQoLLists.PostEvil1.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedHiveMind || !DownedBossSystem.downedPerforator)
                {
                    if (CalNohitQoLLists.PostEvil2.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedQueenBee)
                {
                    if (CalNohitQoLLists.PostEvil2.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedDeerclops)
                {
                    if (CalNohitQoLLists.PostDeerclops.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedBoss3)
                {
                    if (CalNohitQoLLists.PostSkeletron.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSlimeGod)
                {
                    if (CalNohitQoLLists.PostSlimeGod.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!Main.hardMode)
                {
                    if (CalNohitQoLLists.PostWallOfFlesh.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedQueenSlime)
                {
                    if (CalNohitQoLLists.PostQueenSlime.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCryogen)
                {
                    if (CalNohitQoLLists.PostCryogen.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedAquaticScourge)
                {
                    if (CalNohitQoLLists.PostAquaticScourge.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedMechBoss2)
                {
                    if (CalNohitQoLLists.PostTwins.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
                {
                    if (CalNohitQoLLists.PostAllMechs.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCalamitas)
                {
                    if (CalNohitQoLLists.PostCalamitas.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedPlantBoss)
                {
                    if (CalNohitQoLLists.PostPlantera.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedAstrumAureus)
                {
                    if (CalNohitQoLLists.PostAstrumAureus.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedLeviathan)
                {
                    if (CalNohitQoLLists.PostAnahita.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedGolemBoss)
                {
                    if (CalNohitQoLLists.PostGolem.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedEmpressOfLight)
                {
                    if (CalNohitQoLLists.PostEmpress.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedPlaguebringer)
                {
                    if (CalNohitQoLLists.PostPlaguebringerGoliath.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedFishron)
                {
                    if (CalNohitQoLLists.PostDukeFishron.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedRavager)
                {
                    if (CalNohitQoLLists.PostRavager.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedAncientCultist)
                {
                    if (CalNohitQoLLists.PostCultist.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedAstrumDeus)
                {
                    if (CalNohitQoLLists.PostAstrumDeus.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedMoonlord)
                {
                    if (CalNohitQoLLists.PostMoonlord.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedGuardians)
                {
                    if (CalNohitQoLLists.PostProfanedGuardians.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedDragonfolly)
                {
                    if (CalNohitQoLLists.PostDragonFolly.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedProvidence)
                {
                    if (CalNohitQoLLists.PostProvidence.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedStormWeaver)
                {
                    if (CalNohitQoLLists.PostStormWeaver.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCeaselessVoid)
                {
                    if (CalNohitQoLLists.PostCeaselessVoid.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSignus)
                {
                    if (CalNohitQoLLists.PostSignus.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedPolterghast)
                {
                    if (CalNohitQoLLists.PostPolterghast.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedBoomerDuke)
                {
                    if (CalNohitQoLLists.PostOldDuke.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedDoG)
                {
                    if (CalNohitQoLLists.PostDoG.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedYharon)
                {
                    if (CalNohitQoLLists.PostYharon.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedExoMechs)
                {
                    if (CalNohitQoLLists.PostDraedon.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSCal)
                {
                    if (CalNohitQoLLists.PostSCal.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSCal || !DownedBossSystem.downedExoMechs)
                {
                    if (CalNohitQoLLists.Endgame.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedAdultEidolonWyrm)
                {
                    if (item.type == ModContent.ItemType<HalibutCannon>())//if prime is dead but neither of the other two are, so no hallowed bars.
                    {

                        return false;
                    }
                }

                if (!NPC.downedMechBossAny)
                {

                    if (item.type == ModContent.ItemType<SHPC>())
                    {
                        return false;
                    }
                    if (item.type == ModContent.ItemType<AnarchyBlade>())
                    {
                        return false;
                    }

                }

            }
            return true;

        }
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if (CalNohitQoL.Instance.AccLock)
            {
                if (!NPC.downedSlimeKing)
                {
                    if (CalNohitQoLLists.PostKingSlime.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedDesertScourge)
                {
                    if (CalNohitQoLLists.PostDesertScourge.Contains(item.type))
                    {
                        return false;
                    }

                }
                if (!NPC.downedBoss1)
                {
                    if (CalNohitQoLLists.PostEyeOfCthulhu.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCrabulon)
                {
                    if (CalNohitQoLLists.PostCrabulon.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedBoss2)
                {
                    if (CalNohitQoLLists.PostEvil1.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedHiveMind || !DownedBossSystem.downedPerforator)
                {
                    if (CalNohitQoLLists.PostEvil2.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedQueenBee)
                {
                    if (CalNohitQoLLists.PostEvil2.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedDeerclops)
                {
                    if (CalNohitQoLLists.PostDeerclops.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedBoss3)
                {
                    if (CalNohitQoLLists.PostSkeletron.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSlimeGod)
                {
                    if (CalNohitQoLLists.PostSlimeGod.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!Main.hardMode)
                {
                    if (CalNohitQoLLists.PostWallOfFlesh.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedQueenSlime)
                {
                    if (CalNohitQoLLists.PostQueenSlime.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCryogen)
                {
                    if (CalNohitQoLLists.PostCryogen.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedAquaticScourge)
                {
                    if (CalNohitQoLLists.PostAquaticScourge.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedMechBoss2)
                {
                    if (CalNohitQoLLists.PostTwins.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
                {
                    if (CalNohitQoLLists.PostAllMechs.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCalamitas)
                {
                    if (CalNohitQoLLists.PostCalamitas.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedPlantBoss)
                {
                    if (CalNohitQoLLists.PostPlantera.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedAstrumAureus)
                {
                    if (CalNohitQoLLists.PostAstrumAureus.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedLeviathan)
                {
                    if (CalNohitQoLLists.PostAnahita.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedGolemBoss)
                {
                    if (CalNohitQoLLists.PostGolem.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedEmpressOfLight)
                {
                    if (CalNohitQoLLists.PostEmpress.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedPlaguebringer)
                {
                    if (CalNohitQoLLists.PostPlaguebringerGoliath.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedFishron)
                {
                    if (CalNohitQoLLists.PostDukeFishron.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedRavager)
                {
                    if (CalNohitQoLLists.PostRavager.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedAncientCultist)
                {
                    if (CalNohitQoLLists.PostCultist.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedAstrumDeus)
                {
                    if (CalNohitQoLLists.PostAstrumDeus.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!NPC.downedMoonlord)
                {
                    if (CalNohitQoLLists.PostMoonlord.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedGuardians)
                {
                    if (CalNohitQoLLists.PostProfanedGuardians.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedDragonfolly)
                {
                    if (CalNohitQoLLists.PostDragonFolly.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedProvidence)
                {
                    if (CalNohitQoLLists.PostProvidence.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedStormWeaver)
                {
                    if (CalNohitQoLLists.PostStormWeaver.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedCeaselessVoid)
                {
                    if (CalNohitQoLLists.PostCeaselessVoid.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSignus)
                {
                    if (CalNohitQoLLists.PostSignus.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedPolterghast)
                {
                    if (CalNohitQoLLists.PostPolterghast.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedBoomerDuke)
                {
                    if (CalNohitQoLLists.PostOldDuke.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedDoG)
                {
                    if (CalNohitQoLLists.PostDoG.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedYharon)
                {
                    if (CalNohitQoLLists.PostYharon.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedExoMechs)
                {
                    if (CalNohitQoLLists.PostDraedon.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSCal)
                {
                    if (CalNohitQoLLists.PostSCal.Contains(item.type))
                    {
                        return false;
                    }
                }
                if (!DownedBossSystem.downedSCal || !DownedBossSystem.downedExoMechs)
                {
                    if (CalNohitQoLLists.Endgame.Contains(item.type))
                    {
                        return false;
                    }
                }
              
            }


            return true;
        }
        private void ApplyRarityColor(Item item, TooltipLine nameLine)
        {
            if (CalNohitQoLLists.SCalTooltips.Contains(item.type))
                nameLine.OverrideColor = CalNohitQoLUtils.TwoColorPulse(new Color(255, 132, 22), new Color(221, 85, 7), 4f);
        }
        private void PostKingSlimeTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostKingSlime.Contains(item.type))
            {
                string KStext = "This is post King Slime, you should not be using it.";
                TooltipLine KingSlimeTooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(KingSlimeTooltip);
            }
        }
        private void PostDesertScourgeTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostDesertScourge.Contains(item.type))
            {
                string KStext = "This is post Desert Scourge, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostEyeOfCthulhuTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostEyeOfCthulhu.Contains(item.type))
            {
                string KStext = "This is post Eye Of Cthulhu, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostCrabulonTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostCrabulon.Contains(item.type))
            {
                string KStext = "This is post Crabulon, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostEvil1Tooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostEvil1.Contains(item.type))
            {
                string KStext = "This is post Tier 1 Evil Bosses, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostEvil2Tooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostEvil2.Contains(item.type))
            {
                string KStext = "This is post Tier 2 Evil Bosses, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostQueenBeeTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostQueenBee.Contains(item.type))
            {
                string KStext = "This is post Queen Bee, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostDeerclopsTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostDeerclops.Contains(item.type))
            {
                string KStext = "This is post Deerclops, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostSkeletronTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostSkeletron.Contains(item.type))
            {
                string KStext = "This is post Skeletron, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostSlimeGodTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostSlimeGod.Contains(item.type))
            {
                string KStext = "This is post Slime God, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostWallOfFleshTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostWallOfFlesh.Contains(item.type))
            {
                string KStext = "This is post Wall Of Flesh, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostQueenSlimeTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostQueenSlime.Contains(item.type))
            {
                string KStext = "This is post Queen Slime, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostCryogenTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostCryogen.Contains(item.type))
            {
                string KStext = "This is post Cryogen, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostAquaticScourgeTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostAquaticScourge.Contains(item.type))
            {
                string KStext = "This is post Aquatic Scourge, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostAllMechsTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostAllMechs.Contains(item.type))
            {
                string KStext = "This is post all Mechs, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostBrimstoneElementalTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostBrimstoneElemental.Contains(item.type))
            {
                string KStext = "This is post Brimstone Elemental, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostCalamitasTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostCalamitas.Contains(item.type))
            {
                string KStext = "This is post Calamitas, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostPlanteraTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostPlantera.Contains(item.type))
            {
                string KStext = "This is post Plantera, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostAstrumAureusTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostAstrumAureus.Contains(item.type))
            {
                string KStext = "This is post Astrum Aureus, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostAnahitaTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostAnahita.Contains(item.type))
            {
                string KStext = "This is post Anahita and Leviathan, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostGolemTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostGolem.Contains(item.type))
            {
                string KStext = "This is post Golem, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostEmpressTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostEmpress.Contains(item.type))
            {
                string KStext = "This is post Empress Of Light, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostPlaguebringerGoliathTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostPlaguebringerGoliath.Contains(item.type))
            {
                string KStext = "This is post Plaguebringer Goliath, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostDukeFishronTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostDukeFishron.Contains(item.type))
            {
                string KStext = "This is post Duke Fishron, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostRavagerTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostRavager.Contains(item.type))
            {
                string KStext = "This is post Ravager, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostCultistTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostCultist.Contains(item.type))
            {
                string KStext = "This is post Lunatic Cultist, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostAstrumDeusTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostAstrumDeus.Contains(item.type))
            {
                string KStext = "This is post Astrum Deus, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostMoonlordTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostMoonlord.Contains(item.type))
            {
                string KStext = "This is post Moonlord, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostProfanedGuardiansTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostProfanedGuardians.Contains(item.type))
            {
                string KStext = "This is post Profaned Guardians, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostDragonFollyTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostDragonFolly.Contains(item.type))
            {
                string KStext = "This is post Dragonfolly, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostProvidenceTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostProvidence.Contains(item.type))
            {
                string KStext = "This is post Providence, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostStormWeaverTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostStormWeaver.Contains(item.type))
            {
                string KStext = "This is post Storm Weaver, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostCeaselessVoidTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostCeaselessVoid.Contains(item.type))
            {
                string KStext = "This is post Ceaseless Void, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostSignusTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostSignus.Contains(item.type))
            {
                string KStext = "This is post Signus, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostPolterghastTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostPolterghast.Contains(item.type))
            {
                string KStext = "This is post Polterghast, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostOldDukeTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostOldDuke.Contains(item.type))
            {
                string KStext = "This is post Old Duke, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostDoGTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostDoG.Contains(item.type))
            {
                string KStext = "This is post Devourer Of Gods, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostYharonTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostYharon.Contains(item.type))
            {
                string KStext = "This is post Yharon, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostDraedonTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostDraedon.Contains(item.type))
            {
                string KStext = "This is post Exo-Mechs, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void PostSCalTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.PostSCal.Contains(item.type))
            {
                string KStext = "This is post Supreme Calamitas, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }
        private void EndgameTooltips(Item item, IList<TooltipLine> tooltips)
        {
            if (CalNohitQoLLists.Endgame.Contains(item.type))
            {
                string KStext = "This is Endgame, you should not be using it.";
                TooltipLine Tooltip = new TooltipLine(Mod, "CalamityMod: " + item.type, KStext)
                {
                    OverrideColor = tooltipcolour
                };
                tooltips.Add(Tooltip);
            }
        }

        //end of tooltip methods

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!CalNohitQoL.Instance.AccLock)
            {
                return;
            }
            if (!NPC.downedMechBoss1 && !NPC.downedMechBoss2 && !NPC.downedMechBoss3)
            {
                if (item.type == ModContent.ItemType<EvasionScarf>())
                {
                    player.Calamity().dodgeScarf = false;
                    player.Calamity().evasionScarf = false;
                    // player.Calamity().dashMod = ((player.Calamity().dashMod != 1) ? player.Calamity().dashMod : 0);
                }
                /* if (item.type == ModContent.ItemType<RuinMedallion>())
                 {
                     player.Calamity().RogueDamage -= 0.06f;
                     player.Calamity().throwingCrit -= 6;
                     player.Calamity().stealthStrikeHalfCost = false;
                 }*/
                if (item.type == ModContent.ItemType<HoneyDew>())
                {
                    player.Calamity().beeResist = false;
                    if (player.ZoneJungle)
                    {
                        player.lifeRegen--;
                        player.statDefense -= 5;
                        player.endurance -= 0.05f;
                    }
                    player.buffImmune[70] = false;
                    player.buffImmune[20] = false;
                    player.lifeRegenTime--;
                    player.lifeRegen -= 4;
                }
                if (item.type == 3809 || item.type == 3810 || item.type == 3811 || item.type == 3812)
                {
                    player.GetDamage(DamageClass.Summon) -= 0.1f;
                    player.maxTurrets--;
                }
                if (item.type == ModContent.ItemType<ElectriciansGlove>())
                {
                    player.Calamity().electricianGlove = false;
                }
                if (item.type == 1515)
                {
                    player.buffImmune[48] = true;
                    player.noFallDmg = false;
                    _ = player.wingTimeMax;
                    player.wingTimeMax = 0;
                }
                if (item.type == 749)
                {
                    player.statManaMax2 -= 20;
                    player.GetDamage(DamageClass.Magic) -= 0.05f;
                    player.manaCost /= 0.95f;
                    player.GetCritChance(DamageClass.Magic) -= 5;
                    player.noFallDmg = false;
                    _ = player.wingTimeMax;
                    player.wingTimeMax = 0;
                }
                if (item.type == 1165)
                {
                    player.noFallDmg = false;
                    if (!Main.dayTime || Main.eclipse)
                    {
                        player.jumpSpeedBoost -= 0.5f;
                        player.GetDamage(DamageClass.Generic) -= 0.07f;
                        //player.Calamity().AllCritBoost(-3);
                        player.moveSpeed -= 0.1f;
                        _ = player.wingTimeMax;
                        player.wingTimeMax = 0;
                    }
                }
                if (item.type == 785)
                {
                    player.Calamity().harpyWingBoost = false;
                    player.moveSpeed -= 0.2f;
                    player.noFallDmg = false;
                    _ = player.wingTimeMax;
                    player.wingTimeMax = 0;
                }
                if (item.type == 822)
                {
                    player.noFallDmg = false;
                    if (player.head == 46 && player.body == 27 && player.legs == 26)
                    {
                        player.GetDamage(DamageClass.Melee) -= 0.02f;
                        player.GetDamage(DamageClass.Ranged) -= 0.02f;
                        player.GetCritChance(DamageClass.Melee)--;
                        player.GetCritChance(DamageClass.Ranged)--;
                        _ = player.wingTimeMax;
                        player.wingTimeMax = 0;
                    }
                }
                if (item.type == 821)
                {
                    player.GetDamage(DamageClass.Melee) -= 0.05f;
                    player.GetCritChance(DamageClass.Melee) -= 5;
                    player.noFallDmg = false;
                    _ = player.wingTimeMax;
                    player.wingTimeMax = 0;
                }
                if (item.type == 748 || item.type == 3580 || item.type == 3582 || item.type == 3588 || item.type == 3592 || item.type == 3924 || item.type == 3928 || item.type == 3228 || item.type == 665 || item.type == 1583 || item.type == 1584 || item.type == 1585 || item.type == 1586)
                {
                    _ = player.wingTimeMax;
                    player.wingTimeMax = 0;
                }
            }
            if (!DownedBossSystem.downedCryogen)
            {
                if (item.type == ModContent.ItemType<SoulofCryogen>())
                {
                    player.Calamity().cryogenSoul = false;
                    player.GetDamage(DamageClass.Generic) -= 0.07f;
                    player.noFallDmg = false;
                    _ = player.wingTimeMax;
                    player.wingTimeMax = 0;
                    player.Calamity().icicleCooldown = int.MaxValue;
                }
                if (item.type == ModContent.ItemType<FrostFlare>())
                {
                    player.Calamity().frostFlare = false;
                }
                if (item.type == ModContent.ItemType<PermafrostsConcoction>())
                {
                    player.Calamity().permafrostsConcoction = false;
                }
            }
            if (!DownedBossSystem.downedAquaticScourge)
            {
                //if (item.type == ModContent.ItemType<LeadCore>())
                // {
                //     player.buffImmune[ModContent.BuffType<Irradiated>()] = false;
                // }
                if (item.type == ModContent.ItemType<NuclearRod>())
                {
                    player.Calamity().nuclearRod = false;
                }
                if (item.type == ModContent.ItemType<AquaticEmblem>())
                {
                    player.Calamity().aquaticEmblem = false;
                    player.npcTypeNoAggro[65] = false;
                    player.npcTypeNoAggro[220] = false;
                    player.npcTypeNoAggro[64] = false;
                    player.npcTypeNoAggro[67] = false;
                    player.npcTypeNoAggro[221] = false;
                    player.gills = false;
                }
                if (item.type == ModContent.ItemType<CorrosiveSpine>())
                {
                    player.moveSpeed -= 0.1f;
                    player.Calamity().corrosiveSpine = false;
                }
            }
            if (!DownedBossSystem.downedBrimstoneElemental && item.type == ModContent.ItemType<Abaddon>())
            {
                player.Calamity().abaddon = false;
            }
            if (!DownedBossSystem.downedCryogen || ((!NPC.downedMechBoss1 || !NPC.downedMechBoss2) && (!NPC.downedMechBoss2 || !NPC.downedMechBoss3) && (!NPC.downedMechBoss1 || !NPC.downedMechBoss3)))
            {
                if (item.type == ModContent.ItemType<OrnateShield>())
                {
                    if ((player.armor[0].type == ModContent.ItemType<DaedalusHeadMelee>() || player.armor[0].type == ModContent.ItemType<DaedalusHeadRanged>() || player.armor[0].type == ModContent.ItemType<DaedalusHeadMagic>() || player.armor[0].type == ModContent.ItemType<DaedalusHeadSummon>() || player.armor[0].type == ModContent.ItemType<DaedalusHeadRogue>()) && player.armor[1].type == ModContent.ItemType<DaedalusBreastplate>() && player.armor[2].type == ModContent.ItemType<DaedalusLeggings>())
                    {
                        player.endurance -= 0.08f;
                        player.statLifeMax2 -= 20;
                    }
                    player.statDefense -= 8;
                    // player.Calamity().dashMod = ((player.Calamity().dashMod != 6) ? player.Calamity().dashMod : 0);
                }
                if (item.type == ModContent.ItemType<AmbrosialAmpoule>())
                {
                    player.Calamity().aAmpoule = false;
                    player.Calamity().rOoze = false;
                }
                if (item.type == ModContent.ItemType<StarlightWings>())
                {
                    player.GetDamage(DamageClass.Generic) -= 0.05f;
                    // player.Calamity().AllCritBoost(-5);
                }
            }
            if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
            {
                if (item.type == ItemID.AvengerEmblem)
                {
                    player.GetDamage(DamageClass.Generic) -= 0.12f;
                }
                if (item.type == ModContent.ItemType<HallowedRune>())
                {
                    player.Calamity().hallowedRune = false;
                }
                //if (item.type == ModContent.ItemType<MomentumCapacitor>())
                //{
                //    player.Calamity().momentumCapacitor = false;
                //} this was changed so this no longer works :)
                if (item.type == ModContent.ItemType<MOAB>())
                {
                    player.hasJumpOption_Cloud = false;
                    player.hasJumpOption_Blizzard = false;
                    player.hasJumpOption_Sandstorm = false;
                    player.jumpBoost = false;
                    player.autoJump = false;
                    player.noFallDmg = false;
                    player.jumpSpeedBoost -= 0.5f;
                    _ = player.wingTimeMax;
                    player.wingTimeMax = 0;
                }
                if (item.type == ModContent.ItemType<AngelTreads>())
                {
                    player.Calamity().angelTreads = false;
                    player.accRunSpeed = 0f;
                    player.rocketBoots = 0;
                    player.moveSpeed -= 0.12f;
                    player.iceSkate = false;
                    player.waterWalk = false;
                    player.fireWalk = false;
                    player.lavaMax -= 240;
                }
            }
        }

        public override void UpdateInventory(Item item, Player player)
        {
            TryUnlimBuff(item, player);
            if (item.type == ModContent.ItemType<NormalityRelocator>() && !NPC.downedMoonlord)
            {
                player.Calamity().normalityRelocator = false;
            }
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (CalNohitQoL.Instance.ItemLock)
            {
                if (type == 89 || type == 91 || type == 242)
                {
                    if (!NPC.downedMechBoss1 && !NPC.downedMechBoss2)
                    {
                        return NPC.downedMechBoss3;
                    }
                    return true;
                }
                if (type == 225 || type == 207)
                {
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2)
                    {
                        return NPC.downedMechBoss3;
                    }
                    return false;
                }
                if (type == ModContent.ProjectileType<ArcticArrowProj>() || type == ModContent.ProjectileType<VeriumBulletProj>())
                {
                    if (DownedBossSystem.downedCryogen)
                    {
                        if ((!NPC.downedMechBoss1 || !NPC.downedMechBoss2) && (!NPC.downedMechBoss2 || !NPC.downedMechBoss3))
                        {
                            if (NPC.downedMechBoss1)
                            {
                                return NPC.downedMechBoss3;
                            }
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
            }
            return true;
        }

        public override void SetDefaults(Item item)
        {
            if (item.maxStack > 10 && (item.maxStack != 100) && !(item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin))
            {
                item.maxStack = 9999;
            }        
        }

        public static void TryUnlimBuff(Item item, Player player)
        {
            if (CalNohitQoL.Instance.InfinitePotions)
            {


                if (item.buffType != 0&&item.stack > 1)// && GetInstance<FargoConfig>().UnlimitedPotionBuffsOn120)
                {
                    player.AddBuff(item.buffType, 2);

                    //compensate to account for luck potion being weaker based on remaining duration wtf
                    if (item.type == ItemID.LuckPotion)
                        player.GetModPlayer<CalNohitQoLPlayer>().luckPotionBoost = Math.Max(player.GetModPlayer<CalNohitQoLPlayer>().luckPotionBoost, 0.1f);
                    else if (item.type == ItemID.LuckPotionGreater)
                        player.GetModPlayer<CalNohitQoLPlayer>().luckPotionBoost = Math.Max(player.GetModPlayer<CalNohitQoLPlayer>().luckPotionBoost, 0.2f);
                }
                switch (item.type)
                {
                    case ItemID.SharpeningStation:
                        player.AddBuff(BuffID.Sharpened, 2);
                        break;
                    case ItemID.AmmoBox:
                        player.AddBuff(BuffID.AmmoBox, 2);
                        break;
                    case ItemID.CrystalBall:
                        player.AddBuff(BuffID.Clairvoyance, 2);
                        break;
                    case ItemID.BewitchingTable:
                        player.AddBuff(BuffID.Bewitched, 2);
                        break;
                    case ItemID.SliceOfCake:
                        player.AddBuff(BuffID.SugarRush, 2);
                        break;
                }
                if(item.type == ModContent.ItemType<VigorousCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusPinkCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<SpitefulCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusYellowCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<WeightlessCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusBlueCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<ResilientCandle>())
                    player.AddBuff(ModContent.BuffType<CirrusPurpleCandleBuff>(), 2);

                else if (item.type == ModContent.ItemType<CrimsonEffigy>())
                    player.AddBuff(ModContent.BuffType<CrimsonEffigyBuff>(), 2);

                else if (item.type == ModContent.ItemType<CorruptionEffigy>())
                    player.AddBuff(ModContent.BuffType<CorruptionEffigyBuff>(), 2);
            }
            
        }
        public override bool CanBeConsumedAsAmmo(Item ammo, Item weapon, Player player)
        {
            if (CalNohitQoL.Instance.InfiniteAmmo)
            {
                if (ammo.ammo != 0)
                    return false;
            }
            return true;
        }
        public override bool ConsumeItem(Item item, Player player)
        {
            if(CalNohitQoL.Instance.InfiniteConsumables)
            { 
                if (item.damage > 0 && item.ammo == 0)
                    return false;
                if ( (item.buffType > 0 || item.type == ItemID.RecallPotion || item.type == ItemID.PotionOfReturn || item.type == ItemID.WormholePotion) && (item.stack >= 30 || player.inventory.Any(i => i.type == item.type && !i.IsAir && i.stack >= 30)))
                    return false;
            }
            return true;
        }

        public override bool InstancePerEntity => true;
    }
}