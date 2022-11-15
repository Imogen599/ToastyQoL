using CalamityMod;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Buffs.Potions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.UI.QoLUI.PotionUI
{
    public class PotionUIManager
    {
        public static bool IsDrawing { get; internal set; }
        private static Vector2 PositionOffset;
        internal static int Timer;
        internal static int OutroTimer = 15;
        private static bool drawCalamityPotions = false;
        private static Vector2 BasePotionDrawPos = new(-260, -110);
        private static bool progressionOrder = false;

        public const int HorizontalOffset = 40;
        private static bool ShouldDraw
        {
            get
            {
                if (IsDrawing)
                {
                    return true;
                }
                return false;
            }
        }
        // 8 can fit.
        public List<PotionElement> CurrentPotionList;
        public List<PotionElement> VanillaPotionElementsMain;
        public List<PotionElement> CalamityPotinElementsMain;
        public static List<PotionElement> VanillaPotionElementsBase
        {
            get
            {
                List<PotionElement> list = new List<PotionElement>();
                #region vanilla potions                                                          \n
                list.Add(new PotionElement("Ammo Reservation", "20% chance to not consume ammo", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ammoReservation",(AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ammoReservationGlow", (AssetRequestMode)1).Value, BuffID.AmmoReservation, Boss.None));
                list.Add(new PotionElement("Archery", "10% increased bow damage and 20%\nincreased arrow speed", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/archery", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/archeryGlow", (AssetRequestMode)1).Value, BuffID.Archery, Boss.None));
                list.Add(new PotionElement("Battle", "Increases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/battle", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/battleGlow", (AssetRequestMode)1).Value, BuffID.Battle, Boss.None));
                list.Add(new PotionElement("Builder", "Increases placement speed and range", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/builder", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/builderGlow", (AssetRequestMode)1).Value, BuffID.Builder, Boss.None));
                list.Add(new PotionElement("Calming", "Decreases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/calming", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/calmingGlow", (AssetRequestMode)1).Value, BuffID.Calm, Boss.None));
                list.Add(new PotionElement("Crate", "Increases chance to get a crate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/crate", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/crateGlow", (AssetRequestMode)1).Value, BuffID.Crate, Boss.None));
                list.Add(new PotionElement("Dangersense", "Allows you to see nearby danger sources", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/dangersense", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/dangersenseGlow", (AssetRequestMode)1).Value, BuffID.Dangersense, Boss.None));
                list.Add(new PotionElement("Endurance", "Reduces damage taken by 10%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/endurance", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/enduranceGlow", (AssetRequestMode)1).Value, BuffID.Endurance, Boss.None));
                list.Add(new PotionElement("Exquisitely Stuffed", "Major improvements to all stats", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/goldenDelight", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/goldenDelightGlow", (AssetRequestMode)1).Value, BuffID.WellFed3, Boss.None, 0.9f));

                list.Add(new PotionElement("Featherfall", "Slows falling speed", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/featherfall", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/featherfallGlow", (AssetRequestMode)1).Value, BuffID.Featherfall, Boss.None));
                list.Add(new PotionElement("Fishing", "Increases fishing power by 15", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/fishing", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/fishingGlow", (AssetRequestMode)1).Value, BuffID.Fishing, Boss.None));
                list.Add(new PotionElement("Flask of Cursed Flames", "Weapon Imbue Cursed Flames", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfCursedFlames", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfCursedFlamesGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueCursedFlames, Boss.WallOfFlesh));
                list.Add(new PotionElement("Flask of Fire", "Weapon Imbue Fire", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfFire", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfFireGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueFire, Boss.QueenBee));
                list.Add(new PotionElement("Flask of Gold", "Weapon Imbue Gold", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfGold", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfGoldGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueGold, Boss.WallOfFlesh));
                list.Add(new PotionElement("Flask of Ichor", "Weapon Imbue Ichor", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfIchor", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfIchorGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueIchor, Boss.WallOfFlesh));
                list.Add(new PotionElement("Flask of Nanites", "Weapon Imbue Nanites", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfNanites", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfNanitesGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueNanites, Boss.Plantera));
                list.Add(new PotionElement("Flask of Party", "Weapon Imbue Party", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfParty", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfPartyGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueConfetti, Boss.QueenBee));
                list.Add(new PotionElement("Flask of Poison", "Weapon Imbue Poison", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfPoison", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfPoisonGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbuePoison, Boss.QueenBee));
                list.Add(new PotionElement("Flask of Venom", "Weapon Imbue Venom", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfVenom", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfVenomGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueVenom, Boss.Plantera));
                list.Add(new PotionElement("Flipper", "Lets you move swiftly in liquids", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flipper", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flipperGlow", (AssetRequestMode)1).Value, BuffID.Flipper, Boss.None));
                list.Add(new PotionElement("Gills", "Breathe water instead of air", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gills", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gillsGlow", (AssetRequestMode)1).Value, BuffID.Gills, Boss.None));
                list.Add(new PotionElement("Gravitation", "Allows the control of gravity", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gravitation", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gravitationGlow", (AssetRequestMode)1).Value, BuffID.Gravitation, Boss.None));
                list.Add(new PotionElement("Greater Luck", "Increases the Luck of the user", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/greaterLuck", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/greaterLuckGlow", (AssetRequestMode)1).Value, BuffID.Lucky, Boss.None));
                list.Add(new PotionElement("Heartreach", "Increases pickup range for life hearts", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/heartreach", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/heartreachGlow", (AssetRequestMode)1).Value, BuffID.Heartreach, Boss.None));
                list.Add(new PotionElement("Hunter", "Shows the location of enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/hunter", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/hunterGlow", (AssetRequestMode)1).Value, BuffID.Hunter, Boss.None));
                list.Add(new PotionElement("Inferno", "Ignites nearby enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/inferno", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/infernoGlow", (AssetRequestMode)1).Value, BuffID.Inferno, Boss.None));
                list.Add(new PotionElement("Invisibility", "Grants invisibility and lowers the spawn\nrate of enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/invisibility", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/invisibilityGlow", (AssetRequestMode)1).Value, BuffID.Invisibility, Boss.None));
                list.Add(new PotionElement("Ironskin", "Increase defense by 8", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ironskin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ironskinGlow", (AssetRequestMode)1).Value, BuffID.Ironskin, Boss.None));
                list.Add(new PotionElement("Lifeforce", "Increases max life by 20%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/lifeforce", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/lifeforceGlow", (AssetRequestMode)1).Value, BuffID.Lifeforce, Boss.None));
                list.Add(new PotionElement("Magic Power", "20% increased magic damage", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/magicPower", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/magicPowerGlow", (AssetRequestMode)1).Value, BuffID.MagicPower, Boss.None));
                list.Add(new PotionElement("Mana Regeneration", "Increased mana regeneration", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/manaRegeneration", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/manaRegenerationGlow", (AssetRequestMode)1).Value, BuffID.ManaRegeneration, Boss.None));
                list.Add(new PotionElement("Mining", "Increases mining speed by 25%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/mining", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/miningGlow", (AssetRequestMode)1).Value, BuffID.Mining, Boss.None));
                list.Add(new PotionElement("Night Owl Potion", "Increases night vision", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/nightOwl", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/nightOwlGlow", (AssetRequestMode)1).Value, BuffID.NightOwl, Boss.None));
                list.Add(new PotionElement("Obsidian Skin", "Provides immunity to lava", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/obsidianSkin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/obsidianSkinGlow", (AssetRequestMode)1).Value, BuffID.ObsidianSkin, Boss.None));
                list.Add(new PotionElement("Rage", "Increases critical chance by 10%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/rage", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/rageGlow", (AssetRequestMode)1).Value, BuffID.Rage, Boss.None));
                list.Add(new PotionElement("Regeneration", "Provides life regeneration", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/regeneration", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/regenerationGlow", (AssetRequestMode)1).Value, BuffID.Regeneration, Boss.None));
                list.Add(new PotionElement("Shine", "Emits an aura of light", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/shine", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/shineGlow", (AssetRequestMode)1).Value, BuffID.Shine, Boss.None));
                list.Add(new PotionElement("Sonar", "Detects hooked fish", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/sonar", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/sonarGlow", (AssetRequestMode)1).Value, BuffID.Sonar, Boss.None));
                list.Add(new PotionElement("Spelunker", "Shows the location of treasure and ore", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/spelunker", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/spelunkerGlow", (AssetRequestMode)1).Value, BuffID.Spelunker, Boss.None));
                list.Add(new PotionElement("Summoning", "Increases your max number of minions by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/summoning", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/summoningGlow", (AssetRequestMode)1).Value, BuffID.Summoning, Boss.None));
                list.Add(new PotionElement("Swiftness", "25% increased movement speed", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/swiftness", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/swiftnessGlow", (AssetRequestMode)1).Value, BuffID.Swiftness, Boss.None));
                list.Add(new PotionElement("Thorns", "Attackers also take damage", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/thorns", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/thornsGlow", (AssetRequestMode)1).Value, BuffID.Thorns, Boss.None));
                list.Add(new PotionElement("Tipsy", "Increased melee abilities, lowered defense", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ale", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/aleGlow", (AssetRequestMode)1).Value, BuffID.Tipsy, Boss.None));

                list.Add(new PotionElement("Titan", "Increases knockback", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/titan", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/titanGlow", (AssetRequestMode)1).Value, BuffID.Titan, Boss.None));
                list.Add(new PotionElement("Warmth", "Reduces damage from cold sources", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/warmth", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/warmthGlow", (AssetRequestMode)1).Value, BuffID.Warmth, Boss.None));
                list.Add(new PotionElement("Water Walking", "Allows the ability to walk on water", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/waterWalking", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/waterWalkingGlow", (AssetRequestMode)1).Value, BuffID.WaterWalking, Boss.None));
                list.Add(new PotionElement("Wrath", "Increases damage by 10%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/wrath", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/wrathGlow", (AssetRequestMode)1).Value, BuffID.Wrath, Boss.None));
                #endregion
                List<PotionElement> list2 = list;
                return list2;
            }
        }
        public static List<PotionElement> VanillaPotionProgressionElements
        {
            get
            {
                List<PotionElement> list = new List<PotionElement>();
                // Pre boss
                list.Add(new PotionElement("Ammo Reservation", "20% chance to not consume ammo", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ammoReservation", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ammoReservationGlow", (AssetRequestMode)1).Value, BuffID.AmmoReservation, Boss.None));
                list.Add(new PotionElement("Archery", "10% increased bow damage and 20%\nincreased arrow speed", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/archery", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/archeryGlow", (AssetRequestMode)1).Value, BuffID.Archery, Boss.None));
                list.Add(new PotionElement("Battle", "Increases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/battle", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/battleGlow", (AssetRequestMode)1).Value, BuffID.Battle, Boss.None));
                list.Add(new PotionElement("Builder", "Increases placement speed and range", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/builder", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/builderGlow", (AssetRequestMode)1).Value, BuffID.Builder, Boss.None));
                list.Add(new PotionElement("Calming", "Decreases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/calming", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/calmingGlow", (AssetRequestMode)1).Value, BuffID.Calm, Boss.None));
                list.Add(new PotionElement("Crate", "Increases chance to get a crate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/crate", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/crateGlow", (AssetRequestMode)1).Value, BuffID.Crate, Boss.None));
                list.Add(new PotionElement("Dangersense", "Allows you to see nearby danger sources", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/dangersense", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/dangersenseGlow", (AssetRequestMode)1).Value, BuffID.Dangersense, Boss.None));
                list.Add(new PotionElement("Endurance", "Reduces damage taken by 10%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/endurance", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/enduranceGlow", (AssetRequestMode)1).Value, BuffID.Endurance, Boss.None));
                list.Add(new PotionElement("Exquisitely Stuffed", "Major improvements to all stats", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/goldenDelight", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/goldenDelightGlow", (AssetRequestMode)1).Value, BuffID.WellFed3, Boss.None, 0.9f));

                list.Add(new PotionElement("Featherfall", "Slows falling speed", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/featherfall", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/featherfallGlow", (AssetRequestMode)1).Value, BuffID.Featherfall, Boss.None));
                list.Add(new PotionElement("Fishing", "Increases fishing power by 15", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/fishing", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/fishingGlow", (AssetRequestMode)1).Value, BuffID.Fishing, Boss.None));
                list.Add(new PotionElement("Flipper", "Lets you move swiftly in liquids", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flipper", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flipperGlow", (AssetRequestMode)1).Value, BuffID.Flipper, Boss.None));
                list.Add(new PotionElement("Gills", "Breathe water instead of air", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gills", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gillsGlow", (AssetRequestMode)1).Value, BuffID.Gills, Boss.None));
                list.Add(new PotionElement("Gravitation", "Allows the control of gravity", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gravitation", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/gravitationGlow", (AssetRequestMode)1).Value, BuffID.Gravitation, Boss.None));
                list.Add(new PotionElement("Greater Luck", "Increases the Luck of the user", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/greaterLuck", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/greaterLuckGlow", (AssetRequestMode)1).Value, BuffID.Lucky, Boss.None));
                list.Add(new PotionElement("Heartreach", "Increases pickup range for life hearts", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/heartreach", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/heartreachGlow", (AssetRequestMode)1).Value, BuffID.Heartreach, Boss.None));
                list.Add(new PotionElement("Hunter", "Shows the location of enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/hunter", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/hunterGlow", (AssetRequestMode)1).Value, BuffID.Hunter, Boss.None));
                list.Add(new PotionElement("Inferno", "Ignites nearby enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/inferno", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/infernoGlow", (AssetRequestMode)1).Value, BuffID.Inferno, Boss.None));
                list.Add(new PotionElement("Invisibility", "Grants invisibility and lowers the spawn\nrate of enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/invisibility", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/invisibilityGlow", (AssetRequestMode)1).Value, BuffID.Invisibility, Boss.None));
                list.Add(new PotionElement("Ironskin", "Increase defense by 8", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ironskin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ironskinGlow", (AssetRequestMode)1).Value, BuffID.Ironskin, Boss.None));
                list.Add(new PotionElement("Lifeforce", "Increases max life by 20%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/lifeforce", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/lifeforceGlow", (AssetRequestMode)1).Value, BuffID.Lifeforce, Boss.None));
                list.Add(new PotionElement("Magic Power", "20% increased magic damage", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/magicPower", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/magicPowerGlow", (AssetRequestMode)1).Value, BuffID.MagicPower, Boss.None));
                list.Add(new PotionElement("Mana Regeneration", "Increased mana regeneration", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/manaRegeneration", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/manaRegenerationGlow", (AssetRequestMode)1).Value, BuffID.ManaRegeneration, Boss.None));
                list.Add(new PotionElement("Mining", "Increases mining speed by 25%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/mining", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/miningGlow", (AssetRequestMode)1).Value, BuffID.Mining, Boss.None));
                list.Add(new PotionElement("Night Owl Potion", "Increases night vision", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/nightOwl", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/nightOwlGlow", (AssetRequestMode)1).Value, BuffID.NightOwl, Boss.None));
                list.Add(new PotionElement("Obsidian Skin", "Provides immunity to lava", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/obsidianSkin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/obsidianSkinGlow", (AssetRequestMode)1).Value, BuffID.ObsidianSkin, Boss.None));
                list.Add(new PotionElement("Rage", "Increases critical chance by 10%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/rage", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/rageGlow", (AssetRequestMode)1).Value, BuffID.Rage, Boss.None));
                list.Add(new PotionElement("Regeneration", "Provides life regeneration", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/regeneration", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/regenerationGlow", (AssetRequestMode)1).Value, BuffID.Regeneration, Boss.None));
                list.Add(new PotionElement("Shine", "Emits an aura of light", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/shine", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/shineGlow", (AssetRequestMode)1).Value, BuffID.Shine, Boss.None));
                list.Add(new PotionElement("Sonar", "Detects hooked fish", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/sonar", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/sonarGlow", (AssetRequestMode)1).Value, BuffID.Sonar, Boss.None));
                list.Add(new PotionElement("Spelunker", "Shows the location of treasure and ore", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/spelunker", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/spelunkerGlow", (AssetRequestMode)1).Value, BuffID.Spelunker, Boss.None));
                list.Add(new PotionElement("Summoning", "Increases your max number of minions by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/summoning", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/summoningGlow", (AssetRequestMode)1).Value, BuffID.Summoning, Boss.None));
                list.Add(new PotionElement("Swiftness", "25% increased movement speed", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/swiftness", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/swiftnessGlow", (AssetRequestMode)1).Value, BuffID.Swiftness, Boss.None));
                list.Add(new PotionElement("Thorns", "Attackers also take damage", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/thorns", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/thornsGlow", (AssetRequestMode)1).Value, BuffID.Thorns, Boss.None));
                list.Add(new PotionElement("Tipsy", "Increased melee abilities, lowered defense", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/ale", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/aleGlow", (AssetRequestMode)1).Value, BuffID.Tipsy, Boss.None));

                list.Add(new PotionElement("Titan", "Increases knockback", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/titan", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/titanGlow", (AssetRequestMode)1).Value, BuffID.Titan, Boss.None));
                list.Add(new PotionElement("Warmth", "Reduces damage from cold sources", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/warmth", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/warmthGlow", (AssetRequestMode)1).Value, BuffID.Warmth, Boss.None));
                list.Add(new PotionElement("Water Walking", "Allows the ability to walk on water", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/waterWalking", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/waterWalkingGlow", (AssetRequestMode)1).Value, BuffID.WaterWalking, Boss.None));
                list.Add(new PotionElement("Wrath", "Increases damage by 10%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/wrath", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/wrathGlow", (AssetRequestMode)1).Value, BuffID.Wrath, Boss.None));
                // Queen Bee
                list.Add(new PotionElement("Flask of Fire", "Weapon Imbue Fire", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfFire", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfFireGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueFire, Boss.QueenBee));
                list.Add(new PotionElement("Flask of Party", "Weapon Imbue Party", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfParty", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfPartyGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueConfetti, Boss.QueenBee));
                list.Add(new PotionElement("Flask of Poison", "Weapon Imbue Poison", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfPoison", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfPoisonGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbuePoison, Boss.QueenBee));
                // WoF
                list.Add(new PotionElement("Flask of Cursed Flames", "Weapon Imbue Cursed Flames", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfCursedFlames", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfCursedFlamesGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueCursedFlames, Boss.WallOfFlesh));
                list.Add(new PotionElement("Flask of Gold", "Weapon Imbue Gold", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfGold", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfGoldGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueGold, Boss.WallOfFlesh));
                list.Add(new PotionElement("Flask of Ichor", "Weapon Imbue Ichor", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfIchor", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfIchorGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueIchor, Boss.WallOfFlesh));
                // Plant
                list.Add(new PotionElement("Flask of Nanites", "Weapon Imbue Nanites", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfNanites", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfNanitesGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueNanites, Boss.Plantera));
                list.Add(new PotionElement("Flask of Venom", "Weapon Imbue Venom", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfVenom", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/flaskOfVenomGlow", (AssetRequestMode)1).Value, BuffID.WeaponImbueVenom, Boss.Plantera));
                List<PotionElement> list2 = list;
                return list2;
            }
        }
        public static List<PotionElement> CalamityPotinElementsBase
        {
            get
            {
                List<PotionElement> list = new List<PotionElement>();
                list.Add(new PotionElement("Bloody Mary", "Boosts damage and melee speed by 15%,\nmovement speed by 10% and critical strike\nchance by 7% during a Blood Moon", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/bloodyMary", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/bloodyMaryGlow", (AssetRequestMode)1).Value, ModContent.BuffType<BloodyMaryBuff>(), Boss.AstrumAureus, 0.8f));

                list.Add(new PotionElement("Bounding", "Grants 5% increased jump speed, 25\nextra blocks of fall damage resistance,\nand increased jump height", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/bounding", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/boundingGlow", (AssetRequestMode)1).Value, ModContent.BuffType<BoundingBuff>(), Boss.None, 0.8f));
                list.Add(new PotionElement("Cadance", "Increases life regen, heart pickup\nrange and increases max life by 25% ", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cadance", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cadanceGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CadancesGrace>(), Boss.Skeletron, 0.8f));
                list.Add(new PotionElement("Calcium", "Grants immunity to fall damage", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/calcium", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/calciumGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CalciumBuff>(), Boss.None, 0.8f));
                list.Add(new PotionElement("Caribbean Rum", "Boosts life regen by 2, movement speed\nby 10% and wing flight time by 20%\nchance. Makes you floaty, -10 defense", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/caribbeanRum", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/caribbeanRumGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CaribbeanRumBuff>(), Boss.Plantera, 0.8f));
                list.Add(new PotionElement("Cinnamon Roll", "Boosts mana regeneration rate and\nmultiplies all fire-based debuff damage\nby 1.5. -10% defense", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cinnamonRoll", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cinnamonRollGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CinnamonRollBuff>(), Boss.Golem, 0.8f));

                list.Add(new PotionElement("Draconic Elixir", "Greatly increases wing flight time and\nspeed and increases defense by 16.\nSilva invincibility heals you to half HP", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/draconicElixir", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/draconicElixirGlow", (AssetRequestMode)1).Value, ModContent.BuffType<DraconicSurgeBuff>(), Boss.Yharon, 0.6f));
                list.Add(new PotionElement("Everclear", "Boosts damage by 25%. Reduces life\nregen by 10 and defense by 30%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/everclear", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/everclearGlow", (AssetRequestMode)1).Value, ModContent.BuffType<EverclearBuff>(), Boss.AstrumAureus, 0.7f));
                list.Add(new PotionElement("Evergreen Gin", "Multiplies all sickness and water-related\ndebuff damage by 1.25. Reduces life\nregen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/evergreenGin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/evergreenGinGlow", (AssetRequestMode)1).Value, ModContent.BuffType<EvergreenGinBuff>(), Boss.Plantera, 0.8f));

                list.Add(new PotionElement("Fabsol's Vodka", "Boosts all damage stats by 8% but lowers\ndefense by 10%. Increases immune time\nafter being struck", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fabsolsVodka", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fabsolsVodkaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<FabsolVodkaBuff>(), Boss.WallOfFlesh, 0.7f));
                list.Add(new PotionElement("Fireball", "Multiplies all fire-based debuff damage\nby 1.25. Reduces life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fireball", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fireballGlow", (AssetRequestMode)1).Value, ModContent.BuffType<FireballBuff>(), Boss.WallOfFlesh, 0.8f));

                list.Add(new PotionElement("Flask of Brimstone", "Melee, Whip, and Rogue attacks inflict\nBrimstone Flames on enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfBrimstone", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfBrimstoneGlow", (AssetRequestMode)1).Value, ModContent.BuffType<AbyssalWeapon>(), Boss.Cloneamitas, 0.8f));
                list.Add(new PotionElement("Flask of Crumbling", "Melee, Whip, and Rogue attacks inflict\nArmor Crunch on enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfCrumbling", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfCrumblingGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ArmorCrumbling>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Gravity Normalizer", "Disables the low gravity of space and\ngrants immunity to the distorted debuff", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/gravityNormalizer", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/gravityNormalizerGlow", (AssetRequestMode)1).Value, ModContent.BuffType<GravityNormalizerBuff>(), Boss.AstrumAureus, 0.8f));
                list.Add(new PotionElement("Holy Wrath", "Increases damage by 12% and your\nattacks inflict holy fire", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/holyWrath", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/holyWrathGlow", (AssetRequestMode)1).Value, ModContent.BuffType<HolyWrathBuff>(), Boss.MoonLord, 0.6f));
                list.Add(new PotionElement("Moonshine", "Increases defense by 10 and damage\nreduction by 3%. Reduces life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moonshine", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moonshineGlow", (AssetRequestMode)1).Value, ModContent.BuffType<MoonshineBuff>(), Boss.Golem, 0.8f));
                list.Add(new PotionElement("Moscow Mule", "Boosts damage and knockback by 9% and\ncritical strike chance by 3%. Reduces\nlife regen by 2", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moscowMule", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moscowMuleGlow", (AssetRequestMode)1).Value, ModContent.BuffType<MoscowMuleBuff>(), Boss.Golem, 0.8f));

                list.Add(new PotionElement("Omniscience", "Highlights nearby creatures, enemy\nprojectiles,danger sources, and treasure", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/omniscience", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/omniscienceGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Omniscience>(), Boss.Skeletron, 0.8f));
                list.Add(new PotionElement("Penumbra", "Rogue stealth generates 10% faster while\nmoving and 15% faster while standing still", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/penumbra", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/penumbraGlow", (AssetRequestMode)1).Value, ModContent.BuffType<PenumbraBuff>(), Boss.Cloneamitas, 0.8f));
                list.Add(new PotionElement("Photosynthesis", "You regen life quickly while not moving,\nthis effect is five times as strong during\ndaytime. Dropped hearts heal more HP", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/photosynthesis", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/photosynthesisGlow", (AssetRequestMode)1).Value, ModContent.BuffType<PhotosynthesisBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Profaned Rage","Increases critical strike chance by 12%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/profanedRage", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/profanedRageGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ProfanedRageBuff>(), Boss.MoonLord, 0.7f));
                list.Add(new PotionElement("Rum", "Boosts life regen by 2 and movement\nspeed by 10%. Reduces defense by 5%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/rum", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/rumGlow", (AssetRequestMode)1).Value, ModContent.BuffType<RumBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Screwdriver", "Multiplies piercing projectile damage by\n1.05. Reduces life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/screwdriver", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/screwdriverGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ScrewdriverBuff>(), Boss.AllMechs, 0.8f));

                list.Add(new PotionElement("Shadow", "Rogue weapons spawn projectiles on hit\nStealth generation is increased by 8%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/shadow", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/shadowGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ShadowBuff>(), Boss.Skeletron, 0.8f));
                list.Add(new PotionElement("Soaring", "Increases flight time and horizontal flight\nspeed by 10%. Restores a fraction of your\nwing flight time after a true melee strike", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/soaring", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/soaringGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Soaring>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Star Beam Rye", "Boosts max mana by 50, magic damage\nby 8%, and reduces mana usage by 10%.\n-6% defense and -1 life regen", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/starBeamRye", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/starBeamRyeGlow", (AssetRequestMode)1).Value, ModContent.BuffType<StarBeamRyeBuff>(), Boss.AstrumAureus, 0.8f));

                list.Add(new PotionElement("Sulphurskin", "Reduces the effects of the sulphuric\nwaters", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/sulphurskin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/sulphurskinGlow", (AssetRequestMode)1).Value, ModContent.BuffType<SulphurskinBuff>(), Boss.None, 0.8f));
                list.Add(new PotionElement("Tequila", "Boosts damage, DR, and knockback by\n3%, critical strike chance by 2%, defense\nby 5 during daytime. -1 life regen", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequila", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequilaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TequilaBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Tequila Sunrise", "Boosts damage, DR, and knockback by\n7%, critical strike chance by 3%, defense\nby 10 during daytime. -1 life regen", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequilaSunrise", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequilaSunriseGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TequilaSunriseBuff>(), Boss.Golem, 0.8f));

                list.Add(new PotionElement("Tesla", "Summons an aura of electricity that\nelectrifies and slows enemies. Reduces the\nduration of the Electrified debuff", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tesla", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/teslaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TeslaBuff>(), Boss.Perforators, 0.8f));
                list.Add(new PotionElement("Titan Scale", "Increases knockback, defense by 5 and\ndamage reduction by 5% (20 & 5% a few\nseconds after a true melee strike)", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/titanScale", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/titanScaleGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TitanScale>(), Boss.Golem, 0.8f));
                list.Add(new PotionElement("Triumph", "Enemy contact damage is reduced, the\nlower their health the more it is\nreduced", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/triumph", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/triumphGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TriumphBuff>(), Boss.DesertScourge, 0.8f));
                list.Add(new PotionElement("Vodka", "Boosts damage by 6% and critical strike\nchance by 2%. Reduces life regen by 1\nand defense by 5%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/vodka", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/vodkaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<VodkaBuff>(), Boss.AllMechs, 0.8f));
                list.Add(new PotionElement("Whiskey", "Boosts damage and knockback by 4%\nand critical strike chance by 2%. Reduces\ndefense by 5%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiskey", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiskeyGlow", (AssetRequestMode)1).Value, ModContent.BuffType<WhiskeyBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("White Wine", "Restores 400 mana and boosts magic\ndamage by 10%. Reduces defense by 6%\nand life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiteWine", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiteWineGlow", (AssetRequestMode)1).Value, ModContent.BuffType<WhiteWineBuff>(), Boss.AllMechs, 0.7f));

                list.Add(new PotionElement("Yharim's Stimulants", "Gives decent buffs to ALL offensive\nand defensive stats", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/stims", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/stimsGlow", (AssetRequestMode)1).Value, ModContent.BuffType<YharimPower>(), Boss.Skeletron, 0.65f));
                
                list.Add(new PotionElement("Zen", "Vastly decreases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zen", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zenGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Zen>(), Boss.SlimeGod, 0.8f));
                list.Add(new PotionElement("Zerg", "Vastly increases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zerg", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zergGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Zerg>(), Boss.SlimeGod, 0.8f));

                List<PotionElement> list2 = list;
                return list2;
            }
        }
        public static List<PotionElement> CalamityPotionProgressionElements
        {
            get
            {
                // None
                List<PotionElement> list = new List<PotionElement>();
                list.Add(new PotionElement("Bounding", "Grants 5% increased jump speed, 25\nextra blocks of fall damage resistance,\nand increased jump height", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/bounding", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/boundingGlow", (AssetRequestMode)1).Value, ModContent.BuffType<BoundingBuff>(), Boss.None, 0.8f));
                list.Add(new PotionElement("Calcium", "Grants immunity to fall damage", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/calcium", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/calciumGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CalciumBuff>(), Boss.None, 0.8f));
                list.Add(new PotionElement("Sulphurskin", "Reduces the effects of the sulphuric\nwaters", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/sulphurskin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/sulphurskinGlow", (AssetRequestMode)1).Value, ModContent.BuffType<SulphurskinBuff>(), Boss.None, 0.8f));  
                // Desert Scourge
                list.Add(new PotionElement("Triumph", "Enemy contact damage is reduced, the\nlower their health the more it is\nreduced", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/triumph", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/triumphGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TriumphBuff>(), Boss.DesertScourge, 0.8f));
                // Perfs
                list.Add(new PotionElement("Tesla", "Summons an aura of electricity that\nelectrifies and slows enemies. Reduces the\nduration of the Electrified debuff", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tesla", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/teslaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TeslaBuff>(), Boss.Perforators, 0.8f));
                // Skeletron
                list.Add(new PotionElement("Cadance", "Increases life regen, heart pickup\nrange and increases max life by 25% ", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cadance", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cadanceGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CadancesGrace>(), Boss.Skeletron, 0.8f));
                list.Add(new PotionElement("Omniscience", "Highlights nearby creatures, enemy\nprojectiles,danger sources, and treasure", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/omniscience", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/omniscienceGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Omniscience>(), Boss.Skeletron, 0.8f));
                list.Add(new PotionElement("Shadow", "Rogue weapons spawn projectiles on hit\nStealth generation is increased by 8%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/shadow", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/shadowGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ShadowBuff>(), Boss.Skeletron, 0.8f));
                list.Add(new PotionElement("Yharim's Stimulants", "Gives decent buffs to ALL offensive\nand defensive stats", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/stims", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/stimsGlow", (AssetRequestMode)1).Value, ModContent.BuffType<YharimPower>(), Boss.Skeletron, 0.65f));
                // Slime God
                list.Add(new PotionElement("Zen", "Vastly decreases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zen", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zenGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Zen>(), Boss.SlimeGod, 0.8f));
                list.Add(new PotionElement("Zerg", "Vastly increases enemy spawn rate", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zerg", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/zergGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Zerg>(), Boss.SlimeGod, 0.8f));
                // Wall Of Flesh
                list.Add(new PotionElement("Flask of Crumbling", "Melee, Whip, and Rogue attacks inflict\nArmor Crunch on enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfCrumbling", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfCrumblingGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ArmorCrumbling>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Photosynthesis", "You regen life quickly while not moving,\nthis effect is five times as strong during\ndaytime. Dropped hearts heal more HP", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/photosynthesis", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/photosynthesisGlow", (AssetRequestMode)1).Value, ModContent.BuffType<PhotosynthesisBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Soaring", "Increases flight time and horizontal flight\nspeed by 10%. Restores a fraction of your\nwing flight time after a true melee strike", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/soaring", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/soaringGlow", (AssetRequestMode)1).Value, ModContent.BuffType<Soaring>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Fabsol's Vodka", "Boosts all damage stats by 8% but lowers\ndefense by 10%. Increases immune time\nafter being struck", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fabsolsVodka", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fabsolsVodkaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<FabsolVodkaBuff>(), Boss.WallOfFlesh, 0.7f));
                list.Add(new PotionElement("Fireball", "Multiplies all fire-based debuff damage\nby 1.25. Reduces life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fireball", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/fireballGlow", (AssetRequestMode)1).Value, ModContent.BuffType<FireballBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Rum", "Boosts life regen by 2 and movement\nspeed by 10%. Reduces defense by 5%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/rum", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/rumGlow", (AssetRequestMode)1).Value, ModContent.BuffType<RumBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Tequila", "Boosts damage, DR, and knockback by\n3%, critical strike chance by 2%, defense\nby 5 during daytime. -1 life regen", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequila", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequilaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TequilaBuff>(), Boss.WallOfFlesh, 0.8f));
                list.Add(new PotionElement("Whiskey", "Boosts damage and knockback by 4%\nand critical strike chance by 2%. Reduces\ndefense by 5%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiskey", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiskeyGlow", (AssetRequestMode)1).Value, ModContent.BuffType<WhiskeyBuff>(), Boss.WallOfFlesh, 0.8f));
                // All Mechs
                list.Add(new PotionElement("Screwdriver", "Multiplies piercing projectile damage by\n1.05. Reduces life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/screwdriver", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/screwdriverGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ScrewdriverBuff>(), Boss.AllMechs, 0.8f));
                list.Add(new PotionElement("Vodka", "Boosts damage by 6% and critical strike\nchance by 2%. Reduces life regen by 1\nand defense by 5%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/vodka", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/vodkaGlow", (AssetRequestMode)1).Value, ModContent.BuffType<VodkaBuff>(), Boss.AllMechs, 0.8f));
                list.Add(new PotionElement("White Wine", "Restores 400 mana and boosts magic\ndamage by 10%. Reduces defense by 6%\nand life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiteWine", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/whiteWineGlow", (AssetRequestMode)1).Value, ModContent.BuffType<WhiteWineBuff>(), Boss.AllMechs, 0.7f));
                // Clone
                list.Add(new PotionElement("Flask of Brimstone", "Melee, Whip, and Rogue attacks inflict\nBrimstone Flames on enemies", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfBrimstone", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/flaskOfBrimstoneGlow", (AssetRequestMode)1).Value, ModContent.BuffType<AbyssalWeapon>(), Boss.Cloneamitas, 0.8f)); 
                list.Add(new PotionElement("Penumbra", "Rogue stealth generates 10% faster while\nmoving and 15% faster while standing still", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/penumbra", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/penumbraGlow", (AssetRequestMode)1).Value, ModContent.BuffType<PenumbraBuff>(), Boss.Cloneamitas, 0.8f));
                // Plantera
                list.Add(new PotionElement("Caribbean Rum", "Boosts life regen by 2, movement speed\nby 10% and wing flight time by 20%\nchance. Makes you floaty, -10 defense", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/caribbeanRum", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/caribbeanRumGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CaribbeanRumBuff>(), Boss.Plantera, 0.8f));
                list.Add(new PotionElement("Evergreen Gin", "Multiplies all sickness and water-related\ndebuff damage by 1.25. Reduces life\nregen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/evergreenGin", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/evergreenGinGlow", (AssetRequestMode)1).Value, ModContent.BuffType<EvergreenGinBuff>(), Boss.Plantera, 0.8f));
                // Aureus
                list.Add(new PotionElement("Gravity Normalizer", "Disables the low gravity of space and\ngrants immunity to the distorted debuff", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/gravityNormalizer", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/gravityNormalizerGlow", (AssetRequestMode)1).Value, ModContent.BuffType<GravityNormalizerBuff>(), Boss.AstrumAureus, 0.8f)); //s
                list.Add(new PotionElement("Bloody Mary", "Boosts damage and melee speed by 15%,\nmovement speed by 10% and critical strike\nchance by 7% during a Blood Moon", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/bloodyMary", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/bloodyMaryGlow", (AssetRequestMode)1).Value, ModContent.BuffType<BloodyMaryBuff>(), Boss.AstrumAureus, 0.8f));
                list.Add(new PotionElement("Everclear", "Boosts damage by 25%. Reduces life\nregen by 10 and defense by 30%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/everclear", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/everclearGlow", (AssetRequestMode)1).Value, ModContent.BuffType<EverclearBuff>(), Boss.AstrumAureus, 0.7f));
                list.Add(new PotionElement("Star Beam Rye", "Boosts max mana by 50, magic damage\nby 8%, and reduces mana usage by 10%.\n-6% defense and -1 life regen", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/starBeamRye", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/starBeamRyeGlow", (AssetRequestMode)1).Value, ModContent.BuffType<StarBeamRyeBuff>(), Boss.AstrumAureus, 0.8f));
                // Golem
                list.Add(new PotionElement("Titan Scale", "Increases knockback, defense by 5 and\ndamage reduction by 5% (20 & 5% a few\nseconds after a true melee strike)", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/titanScale", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/titanScaleGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TitanScale>(), Boss.Golem, 0.8f));
                list.Add(new PotionElement("Cinnamon Roll", "Boosts mana regeneration rate and\nmultiplies all fire-based debuff damage\nby 1.5. -10% defense", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cinnamonRoll", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/cinnamonRollGlow", (AssetRequestMode)1).Value, ModContent.BuffType<CinnamonRollBuff>(), Boss.Golem, 0.8f));
                list.Add(new PotionElement("Moonshine", "Increases defense by 10 and damage\nreduction by 3%. Reduces life regen by 1", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moonshine", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moonshineGlow", (AssetRequestMode)1).Value, ModContent.BuffType<MoonshineBuff>(), Boss.Golem, 0.8f));
                list.Add(new PotionElement("Moscow Mule", "Boosts damage and knockback by 9% and\ncritical strike chance by 3%. Reduces\nlife regen by 2", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moscowMule", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/moscowMuleGlow", (AssetRequestMode)1).Value, ModContent.BuffType<MoscowMuleBuff>(), Boss.Golem, 0.8f));
                list.Add(new PotionElement("Tequila Sunrise", "Boosts damage, DR, and knockback by\n7%, critical strike chance by 3%, defense\nby 10 during daytime. -1 life regen", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequilaSunrise", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/tequilaSunriseGlow", (AssetRequestMode)1).Value, ModContent.BuffType<TequilaSunriseBuff>(), Boss.Golem, 0.8f));
                // Moonlord
                list.Add(new PotionElement("Holy Wrath", "Increases damage by 12% and your\nattacks inflict holy fire", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/holyWrath", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/holyWrathGlow", (AssetRequestMode)1).Value, ModContent.BuffType<HolyWrathBuff>(), Boss.MoonLord, 0.6f));
                list.Add(new PotionElement("Profaned Rage", "Increases critical strike chance by 12%", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/profanedRage", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/profanedRageGlow", (AssetRequestMode)1).Value, ModContent.BuffType<ProfanedRageBuff>(), Boss.MoonLord, 0.7f));
                // Yharon
                list.Add(new PotionElement("Draconic Elixir", "Greatly increases wing flight time and\nspeed and increases defense by 16.\nSilva invincibility heals you to half HP", ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/draconicElixir", (AssetRequestMode)1).Value, ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/Calamity/draconicElixirGlow", (AssetRequestMode)1).Value, ModContent.BuffType<DraconicSurgeBuff>(), Boss.Yharon, 0.6f));


                List<PotionElement> list2 = list;
                return list2;
            }
        }
        // <name, buffID>
        Dictionary<string, int> DPotionsAreActive => Main.LocalPlayer.GetModPlayer<PotionUIPlayer>().DPotionsAreActive;

        public void Draw(SpriteBatch spriteBatch)
        {
            BasePotionDrawPos = new(-260, -110);
            float opacity = 1;
            if (!ShouldDraw)
            {
                //if(OutroTimer > 0)
                //    opacity = DoClosingAnimation();
                // else
                    return;
            }
            
            TogglesUIManager.CloseAllUI(true);
            IsDrawing = true;
            // Draw the background.
            Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/baseSettingsUIBackgroundPotions", (AssetRequestMode)2).Value;
            Vector2 drawCenter;
            drawCenter.X = Main.screenWidth / 2;
            drawCenter.Y = Main.screenHeight / 2;
            // This spawn pos is very important. As it is affected by Main.screenWidth/Height, it will scale properly. Every single thing you draw needs to use
            // this vector, unless they are a completely new one and use Main.screenWidth.Height themselves for the VERY BASE of their definition.
            Vector2 spawnPos = drawCenter + PositionOffset /*+ new Vector2(325, 0)*/;
            
            float scale = 1;
            if (ShouldDraw)
            {
                OutroTimer = 15;
                if (Timer <= 15)
                {
                    opacity = (float)Timer / 15;
                    Timer++;
                }
            }
            spriteBatch.Draw(backgroundTexture, spawnPos, null, Color.White*opacity, 0, backgroundTexture.Size() * 0.5f, scale, 0, 0);

            // Block the mouse if we are hovering over it.
            Rectangle hoverArea = Utils.CenteredRectangle(spawnPos, backgroundTexture.Size()*scale);
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            bool isHovering = mouseHitbox.Intersects(hoverArea);
            if (isHovering)
            {
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
            }
            // Draw close button.
            Texture2D closeButtonTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/Cross", (AssetRequestMode)2).Value;
            Texture2D closeButtonGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/Powers/CrossGlow", (AssetRequestMode)2).Value;

            Vector2 closeButtonDrawPos = new Vector2(276,-179) + spawnPos;
            Rectangle closeButtonRect = Utils.CenteredRectangle(closeButtonDrawPos, closeButtonTexture.Size()*0.8f*scale);
            if (mouseHitbox.Intersects(closeButtonRect))
            {
                Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                spriteBatch.Draw(closeButtonGlowTexture, closeButtonDrawPos, null, Color.White*opacity, 0, closeButtonGlowTexture.Size() * 0.5f, 0.8f*scale, 0, 0);
                Main.hoverItemName = "[c/de4444:Close Menu]";
                if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                {
                    // ON CLICK AFFECT
                    IsDrawing = false;
                    SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    TogglesUIManager.UIOpen = true;
                    TogglesUIManager.IntroTimer = 60;
                }
            }
            spriteBatch.Draw(closeButtonTexture, closeButtonDrawPos, null, Color.White*opacity, 0, closeButtonTexture.Size() * 0.5f, 0.8f*scale, 0, 0);

            if (DPotionsAreActive.Count > 0)
            {
                closeButtonDrawPos = new Vector2(273, -18) + spawnPos;
                closeButtonRect = Utils.CenteredRectangle(closeButtonDrawPos, closeButtonTexture.Size() * 0.8f * scale);
                if (mouseHitbox.Intersects(closeButtonRect))
                {
                    Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                    spriteBatch.Draw(closeButtonGlowTexture, closeButtonDrawPos, null, Color.White * opacity, 0, closeButtonGlowTexture.Size() * 0.5f, 0.8f * scale, 0, 0);
                    Main.hoverItemName = "[c/de4444:Clear Potions]";
                    if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                    {
                        // ON CLICK AFFECT
                        DPotionsAreActive.Clear();
                        SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    }
                }
                spriteBatch.Draw(closeButtonTexture, closeButtonDrawPos, null, Color.White * opacity, 0, closeButtonTexture.Size() * 0.5f, 0.8f * scale, 0, 0);
            }

            DrawStaticText(spriteBatch, spawnPos, opacity, scale);

            if (drawCalamityPotions && CurrentPotionList != CalamityPotinElementsMain)
                CurrentPotionList = CalamityPotinElementsMain;
            else if (!drawCalamityPotions && CurrentPotionList != VanillaPotionElementsMain)
                CurrentPotionList = VanillaPotionElementsMain;

            if (progressionOrder && CalamityPotinElementsMain != CalamityPotionProgressionElements)
                CalamityPotinElementsMain = CalamityPotionProgressionElements;
            else if (!progressionOrder && CalamityPotinElementsMain != CalamityPotinElementsBase)
                CalamityPotinElementsMain = CalamityPotinElementsBase;

            if (progressionOrder && VanillaPotionElementsMain != VanillaPotionProgressionElements)
                VanillaPotionElementsMain = VanillaPotionProgressionElements;
            else if (!progressionOrder && VanillaPotionElementsMain != VanillaPotionElementsBase)
                VanillaPotionElementsMain = VanillaPotionElementsBase;

            DrawPotionElements(spriteBatch, spawnPos, opacity);

            DrawModToggleIcons(spriteBatch, spawnPos, opacity);
            DrawSortToggleIcons(spriteBatch, spawnPos, opacity);
            // Draw the active bit. Pass index through to get the position to draw it :p
            DrawActivePotions(spriteBatch, spawnPos, opacity);
        }

        private void DrawSortToggleIcons(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            Texture2D alphaTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/alpha", (AssetRequestMode)2).Value;
            Texture2D alphaGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/alphaGlow", (AssetRequestMode)2).Value;
            Texture2D progressionTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/progression", (AssetRequestMode)2).Value;
            Texture2D progressionGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/progressionGlow", (AssetRequestMode)1).Value;
            Vector2 alphaDrawPos = spawnPos + new Vector2(10, -155);
            Vector2 progressionDrawPos = spawnPos + new Vector2(40, -155);
            Color alphaColor;
            Color progressionColor;
            float alphaScale = 1f;
            float progressionScale = 1f;
            if (!progressionOrder)
            {
                alphaColor = new Color(40, 40, 40) * 0.7f;
                progressionColor = Color.White;     
            }
            else
            {
                alphaColor = Color.White;
                progressionColor = new Color(40, 40, 40) * 0.7f;
            }

            spriteBatch.Draw(alphaTexture, alphaDrawPos, null, alphaColor*opacity, 0, alphaTexture.Size() * 0.5f, alphaScale, 0, 0);
            spriteBatch.Draw(progressionTexture, progressionDrawPos, null, progressionColor * opacity, 0, progressionGlowTexture.Size() * 0.5f, progressionScale, 0, 0);

            Rectangle alphaHitbox = Utils.CenteredRectangle(alphaDrawPos, alphaTexture.Size());
            Rectangle progressionHitbox = Utils.CenteredRectangle(progressionDrawPos, progressionTexture.Size());
            Rectangle mouseHitbox = new(Main.mouseX, Main.mouseY, 2, 2);
            if (mouseHitbox.Intersects(alphaHitbox) && progressionOrder)
            {
                alphaScale = 1.1f;
                Main.hoverItemName = "[c/22a851:Sort by Name]";
                spriteBatch.Draw(alphaGlowTexture, alphaDrawPos, null, alphaColor * opacity, 0, alphaGlowTexture.Size() * 0.5f, alphaScale, 0, 0);
                if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                {
                    progressionOrder = false;
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
            }
            else if (mouseHitbox.Intersects(progressionHitbox) && !progressionOrder)
            {
                progressionScale = 1.1f;
                Main.hoverItemName = "[c/c05d32:Sort by Progression]";
                spriteBatch.Draw(progressionGlowTexture, progressionDrawPos, null, progressionColor * opacity, 0, progressionGlowTexture.Size() * 0.5f, progressionScale, 0, 0);
                if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                {
                    progressionOrder = true;
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
            }
            
        }

        private void DrawModToggleIcons(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            Texture2D calamityTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/calamityIcon", (AssetRequestMode)2).Value;
            Texture2D calamtiyGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/calamityIconGlow", (AssetRequestMode)2).Value;
            Texture2D terrariaTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/terrariaIcon", (AssetRequestMode)2).Value;
            Texture2D terrariaGlowTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/PotionUI/Textures/terrariaIconGlow", (AssetRequestMode)2).Value;
            Vector2 calamityDrawPos = spawnPos + new Vector2(-230, -155);
            Vector2 terrariaDrawPos = spawnPos + new Vector2(-260, -155);
            Color calamityColor;
            Color terrariaColor;
            float calamityScale = 1;
            float terrariaScale = 1;
            if (!drawCalamityPotions)
            {
                calamityColor = Color.White;
                terrariaColor = new Color(40, 40, 40) * 0.7f;
            }
            else
            {
                calamityColor = new Color(40,40,40) * 0.7f;
                terrariaColor = Color.White;
            }
            Rectangle calamityHitbox = Utils.CenteredRectangle(calamityDrawPos, calamityTexture.Size());
            Rectangle terrariaHitbox = Utils.CenteredRectangle(terrariaDrawPos, terrariaTexture.Size());
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            if (mouseHitbox.Intersects(calamityHitbox)&&!drawCalamityPotions)
            {
                calamityScale = 1.1f;
                Main.hoverItemName = "[c/c05d32:Calamity Potions]";
                spriteBatch.Draw(calamtiyGlowTexture, calamityDrawPos, null, calamityColor*opacity, 0, calamityTexture.Size() * 0.5f, calamityScale, 0, 0);
                if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                {
                    drawCalamityPotions = true;
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
            }
            else if(mouseHitbox.Intersects(terrariaHitbox)&&drawCalamityPotions)
            {
                terrariaScale = 1.1f;
                Main.hoverItemName = "[c/22a851:Terraria Potions]";
                spriteBatch.Draw(terrariaGlowTexture, terrariaDrawPos, null, terrariaColor*opacity, 0, terrariaTexture.Size() * 0.5f, terrariaScale, 0, 0);
                if ((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))
                {
                    drawCalamityPotions = false;
                    SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                }
            }
            spriteBatch.Draw(terrariaTexture, terrariaDrawPos, null, terrariaColor * opacity, 0, terrariaTexture.Size() * 0.5f, terrariaScale, 0, 0);
            spriteBatch.Draw(calamityTexture, calamityDrawPos, null, calamityColor * opacity, 0, calamityTexture.Size() * 0.5f, calamityScale, 0, 0);
        }

        private void DrawPotionElements(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity)
        {
            // This uses structs to draw these very very efficently. I am happy with this.
            // Define these outside the for loop. May be more efficient.
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            Vector2 potionDrawPos;
            PotionElement potionElement;
            bool isHovering;
            // Loop through every element in the list.
            int index = -1;
            for (int i = 0; i < CurrentPotionList.Count; i++)
            {
                // Get the PotionElement from the list using the index.
                potionElement = CurrentPotionList[i];              
                float scale = potionElement.Scale;
                #region Positioning
                // This is for the base ordering. Will need to make this check for the current list being used to detect which ordering to use here.
                int row = 0;
                int INDEX = i;
                if(i >= 8 && i < 16)
                {
                    row++;
                    INDEX -= 8;
                }
                else if (i >= 16 && i < 24)
                {
                    row += 2;
                    INDEX -= 16;
                }
                else if (i >= 24 && i < 32)
                {
                    row += 3;
                    INDEX -= 24;
                }
                else if (i >= 32 && i < 40)
                {
                    row += 4;
                    INDEX -= 32;
                }
                else if (i >= 40 && i < 48)
                {
                    row += 5;
                    INDEX -= 40;
                }
                else if (i >= 48 && i < 56)
                {
                    row += 6;
                    INDEX -= 48;
                }
                else if (i >= 56 && i < 64)
                {
                    row += 7;
                    INDEX -= 56;
                }
                // Set the Draw Pos. Use i to properly set it here.
                potionDrawPos = spawnPos + BasePotionDrawPos + new Vector2(HorizontalOffset * INDEX, row*45);
                #endregion
                Texture2D whiteRectTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleWhiteRect", (AssetRequestMode)2).Value;
                Rectangle whiteRectangle = Utils.CenteredRectangle(potionDrawPos, whiteRectTexture.Size()* 0.8f);
                // The area where we will be hovering. This will be changed to the white background later.
                bool shouldBeGreen = false;
                if (DPotionsAreActive.ContainsKey(potionElement.PotionName))
                {
                    // If its in the dict, add one to the index. This is used to position things in the active potions section.
                    index++;
                    shouldBeGreen = true;
                }
                // Is it in our current progression?
                bool drawRedBackground = false;
                // If it is locked behind a boss.
                if (potionElement.BossLocked != Boss.None)
                {
                    drawRedBackground = IsVanillaPotionOutOfProgression(potionElement.BossLocked);
                }
                // Are we hovering over it?
                isHovering = mouseHitbox.Intersects(whiteRectangle);
                // If we arent hovering and should draw the red background,
                if (!isHovering && drawRedBackground||shouldBeGreen)
                {
                    //draw the red background
                    Texture2D redRectTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleRedRect", (AssetRequestMode)2).Value;
                    Texture2D greenRectTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleGreenRect", (AssetRequestMode)2).Value;
                    Texture2D yellowRectTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleYellowRect", (AssetRequestMode)2).Value;
                    if (drawRedBackground&&!shouldBeGreen)
                        spriteBatch.Draw(redRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else if(shouldBeGreen&&!drawRedBackground)
                        spriteBatch.Draw(greenRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else
                        spriteBatch.Draw(yellowRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                }
                if (isHovering)
                {
                    Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                    Texture2D redRectHoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleRedHoverRect", (AssetRequestMode)2).Value;
                    Texture2D greenRectHoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleGreenHoverRect", (AssetRequestMode)2).Value;
                    Texture2D yellowRectHoverTexture = ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleYellowHoverRect", (AssetRequestMode)2).Value;
                    // Draw the white background if none others should be drawn.
                    if (drawRedBackground&&shouldBeGreen)
                        spriteBatch.Draw(yellowRectHoverTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectHoverTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else if(drawRedBackground)
                        spriteBatch.Draw(redRectHoverTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, redRectHoverTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else if(shouldBeGreen)
                        spriteBatch.Draw(greenRectHoverTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, whiteRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    else
                        spriteBatch.Draw(whiteRectTexture, potionDrawPos, null, Color.White * opacity * 0.3f, 0, whiteRectTexture.Size() * 0.5f, 0.8f, 0, 0);
                    // Draw the glow sprite.
                    spriteBatch.Draw(potionElement.PotionGlowTexture, potionDrawPos, null, Color.White, 0, potionElement.PotionGlowTexture.Size() * 0.5f, scale, 0, 0);
                    // Run the side information draw code here. Pass through potionElement for this.
                    DrawPotionSideInformation(spriteBatch, spawnPos, potionElement, opacity);
                    bool maxBuffs = Main.LocalPlayer.CountBuffs() >= 22;
                    if (maxBuffs)
                        Main.hoverItemName = "Max Buffs Reached";
                    // If this is true, we have clicked it.
                    if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease))&&TogglesUIManager.ClickCooldownTimer == 0)
                    {
                        // Set the generic click timer to its length, but a bit shorter in this case.
                        TogglesUIManager.ClickCooldownTimer = (int)(TogglesUIManager.ClickCooldownLength*0.75f);
                        // Play a sfx.
                        if(!maxBuffs)
                            SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);

                        // Check if this potion is already in the dictonary of selected potions. If so, remove it.
                        
                        if (DPotionsAreActive.ContainsKey(potionElement.PotionName))
                        {
                            // Remove it.
                            DPotionsAreActive.Remove(potionElement.PotionName);
                        }
                        else if (!maxBuffs)
                        {
                            // Else, add it.
                            RemoveNonCompatPotions(potionElement);
                            DPotionsAreActive.Add(potionElement.PotionName, potionElement.PotionBuffID);
                        }
                    }

                }
                
                spriteBatch.Draw(potionElement.PotionTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, scale, 0, 0);
               
            }

        }
        private void DrawPotionSideInformation(SpriteBatch spriteBatch, Vector2 spawnPos, PotionElement potionElement, float opacity)
        {
            // Draw the image.
            Color colorTouse;
            if (drawCalamityPotions)
                colorTouse = new Color(223,93,50);
            else
                colorTouse = new Color(34,168,81);

            spriteBatch.Draw(potionElement.PotionTexture, spawnPos + new Vector2(90, -152), null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
            // Draw the name.
            float vertical = -152;
            if (potionElement.Scale != 1)
                vertical = -155;
            Utils.DrawBorderString(spriteBatch, potionElement.PotionName, spawnPos + new Vector2(80, vertical)+new Vector2((potionElement.PotionTexture.Size().X*potionElement.Scale)+5,0), colorTouse * opacity, 0.8f);
            // Draw the description.
            Utils.DrawBorderString(spriteBatch, potionElement.PotionPositives, spawnPos + new Vector2(80, -132), new Color(255, 204, 68) * opacity, 0.6f);
            Vector2 size = FontAssets.MouseText.Value.MeasureString(potionElement.PotionPositives);
            bool drawRedText = IsVanillaPotionOutOfProgression(potionElement.BossLocked);
            
            if (DPotionsAreActive.ContainsKey(potionElement.PotionName))
            { 
                Utils.DrawBorderString(spriteBatch, "Currently Equipped", spawnPos + new Vector2(80, -132)+new Vector2(0,size.Y/1.6f), new Color(50,200,50) * opacity, 0.6f);
                if(drawRedText)
                    Utils.DrawBorderString(spriteBatch, "Past your progression", spawnPos + new Vector2(80, -117) + new Vector2(0, size.Y/1.6f), new Color(200, 50, 50) * opacity, 0.6f);
            }
            else if (drawRedText)
            {
                Utils.DrawBorderString(spriteBatch, "Past your progression", spawnPos + new Vector2(80, -132) + new Vector2(0, size.Y/1.6f), new Color(200,50,50) * opacity, 0.6f);
            }
        }

        private void DrawStaticText(SpriteBatch spriteBatch, Vector2 spawnPos, float opacity, float scale)
        {
            string currentPotionsText = "Active Potions:";
            Utils.DrawBorderString(spriteBatch, currentPotionsText, spawnPos + new Vector2(80, -22), Color.White * opacity);
            string currentSortText = "Sort Mode:";
            Utils.DrawBorderString(spriteBatch, currentSortText, spawnPos + new Vector2(-100, -163), Color.White * opacity);
        }

        private void DrawActivePotions(SpriteBatch spriteBatch, Vector2 spawnPos,float opacity)
        {
            // Define simple position variables.
            Vector2 basePotionPos = new Vector2(87, 22) + spawnPos;
            Vector2 basePotionInterval;
            Vector2 potionDrawPos;
            Rectangle mouseHitbox = new Rectangle(Main.mouseX, Main.mouseY, 2, 2);
            Rectangle hoverHitbox;
            int index = 0;
            int index2 = 0;
            int row = 0;
            // go through every potion in the lists.
            for (int i = 0; i < VanillaPotionElementsMain.Count; i++)
            {
                PotionElement potionElement = VanillaPotionElementsMain[i];
                if (DPotionsAreActive.TryGetValue(potionElement.PotionName, out int potionBuffID))
                {
                    index2 = index;
                    if (index2 >= 7 && index2 < 14)
                    {
                        row = 1;
                        index2 -= 7;
                        
                    }
                    else if (index2 >= 14 && index2 < 21)
                    {
                        row = 2;
                        index2 -= 14;
                    }
                    else if (index2 >= 21 && index2 < 28)
                    {
                        row = 3;
                        index2 -= 21;
                    }
                    
                    basePotionInterval = new(30 * index2, 45*row);
                    potionDrawPos = basePotionPos + basePotionInterval;
                    //ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleGreenRect", (AssetRequestMode)2).Value
                    spriteBatch.Draw(potionElement.PotionTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
                    index++;
                    hoverHitbox = Utils.CenteredRectangle(potionDrawPos, potionElement.PotionTexture.Size() * potionElement.Scale);
                    if (mouseHitbox.Intersects(hoverHitbox))
                    {
                        Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                        spriteBatch.Draw(potionElement.PotionGlowTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
                        DrawPotionSideInformation(spriteBatch, spawnPos, potionElement, opacity);
                        Main.hoverItemName = "Remove";
                        if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.ClickCooldownTimer == 0)
                        {
                            TogglesUIManager.ClickCooldownTimer = 10;
                            DPotionsAreActive.Remove(potionElement.PotionName);
                            SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                            index--;
                        }
                    }
                }
            }
            for (int i = 0; i < CalamityPotinElementsMain.Count; i++)
            {
                
                PotionElement potionElement = CalamityPotinElementsMain[i];
                if (DPotionsAreActive.TryGetValue(potionElement.PotionName, out int potionBuffID))
                {
                    index2 = index;
                    if (index2 >= 7 && index2 < 14)
                    {
                        row = 1;
                        index2 -= 7;

                    }
                    else if (index2 >= 14 && index2 < 21)
                    {
                        row = 2;
                        index2 -= 14;
                    }
                    else if (index2 >= 21 && index2 < 21)
                    {
                        row = 3;
                        index2 -= 21;
                    }

                    basePotionInterval = new(30 * index2, 45 * row);
                    potionDrawPos = basePotionPos + basePotionInterval;
                    //ModContent.Request<Texture2D>("CalNohitQoL/UI/QoLUI/Textures/circleGreenRect", (AssetRequestMode)2).Value
                    spriteBatch.Draw(potionElement.PotionTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
                    index++;
                    hoverHitbox = Utils.CenteredRectangle(potionDrawPos,potionElement.PotionTexture.Size() * potionElement.Scale);
                    if (mouseHitbox.Intersects(hoverHitbox))
                    {
                        Main.blockMouse = Main.LocalPlayer.mouseInterface = true;
                        spriteBatch.Draw(potionElement.PotionGlowTexture, potionDrawPos, null, Color.White * opacity, 0, potionElement.PotionTexture.Size() * 0.5f, potionElement.Scale, 0, 0);
                        DrawPotionSideInformation(spriteBatch, spawnPos, potionElement, opacity);
                        Main.hoverItemName = "Remove";
                        if (((Main.mouseLeft && Main.mouseLeftRelease) || (Main.mouseRight && Main.mouseRightRelease)) && TogglesUIManager.ClickCooldownTimer == 0)
                        {
                            TogglesUIManager.ClickCooldownTimer = 10;
                            DPotionsAreActive.Remove(potionElement.PotionName);
                            SoundEngine.PlaySound(SoundID.MenuTick, Main.LocalPlayer.Center);
                            index--;
                        }
                    }
                }
            }
            if (index == DPotionsAreActive.Count)
            {
                string no = index.ToString();
                Utils.DrawBorderString(spriteBatch, no, spawnPos + new Vector2(200, -22), Color.White * opacity, 1f);
            }
        }
        public void GiveBuffs()
        {
            foreach (PotionElement potionElement in VanillaPotionElementsMain)
            {              
                if (DPotionsAreActive.TryGetValue(potionElement.PotionName, out int potionBuffID))
                {
                    Main.LocalPlayer.AddBuff(potionBuffID, 2);
                }
            }
            foreach (PotionElement potionElement in CalamityPotinElementsMain)
            {
                if (DPotionsAreActive.TryGetValue(potionElement.PotionName, out int potionBuffID))
                {
                    Main.LocalPlayer.AddBuff(potionBuffID, 2);
                }
            }
        }

        public static bool IsVanillaPotionOutOfProgression(Boss bossName)
        {
            switch (bossName)
            {
                case Boss.Plantera:
                    return !NPC.downedPlantBoss;
                case Boss.QueenBee:
                    return !NPC.downedQueenBee;
                case Boss.WallOfFlesh:
                    return !Main.hardMode;
                case Boss.AllMechs:
                    return (!NPC.downedMechBoss1 && !NPC.downedMechBoss2 && !NPC.downedMechBoss3);
                case Boss.Golem:
                    return !NPC.downedGolemBoss;
                case Boss.MoonLord:
                    return !NPC.downedMoonlord;
                case Boss.Cloneamitas:
                    return !DownedBossSystem.downedCalamitas;
                case Boss.AstrumAureus:
                    return !DownedBossSystem.downedAstrumAureus;
                case Boss.Perforators:
                    return !DownedBossSystem.downedHiveMind || !DownedBossSystem.downedPerforator;
                case Boss.DesertScourge:
                    return !DownedBossSystem.downedDesertScourge;
                case Boss.SlimeGod:
                    return !DownedBossSystem.downedSlimeGod;
                case Boss.Yharon:
                    return !DownedBossSystem.downedYharon;
                case Boss.Skeletron:
                    return !NPC.downedBoss3;
            }
            return false;
        }

        private readonly Dictionary<string, string> ElementNames = new()
        {
            ["Holy Wrath"] = "Wrath",
            ["Profaned Rage"] = "Rage",
            ["Cadance"] = "Lifeforce",         
        };

        private void RemoveNonCompatPotions(PotionElement potionElementIn)
        {
            if (ElementNames.TryGetValue(potionElementIn.PotionName, out string potionToRemove))
            {
                if (potionToRemove == "Lifeforce")
                    DPotionsAreActive.Remove("Regeneration");
                DPotionsAreActive.Remove(potionToRemove);
            }
            else if(ElementNames.ContainsValue(potionElementIn.PotionName) || potionElementIn.PotionName == "Regeneration")
            {
                switch (potionElementIn.PotionName)
                {
                    case "Wrath":
                        DPotionsAreActive.Remove("Holy Wrath");
                        break;
                    case "Rage":
                        DPotionsAreActive.Remove("Profaned Rage");
                        break;
                    case "Lifeforce":
                        DPotionsAreActive.Remove("Cadance");
                        break;
                    case "Regeneration":
                        DPotionsAreActive.Remove("Cadance");
                        break;
                }
            }
        }
        public static void Load()
        {
            CalNohitQoL.potionUIManager.VanillaPotionElementsMain = VanillaPotionElementsBase;
            CalNohitQoL.potionUIManager.CalamityPotinElementsMain = CalamityPotinElementsBase;
        }
        public static void Unload()
        {
            CalNohitQoL.potionUIManager.VanillaPotionElementsMain = null;
            CalNohitQoL.potionUIManager.CalamityPotinElementsMain = null;
        }
    }
}
