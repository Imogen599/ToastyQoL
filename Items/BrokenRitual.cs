using CalamityMod;
using CalamityMod.Events;
using CalamityMod.Items.SummonItems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Items
{
    public class BrokenRitual : ModItem
    {
        public int BRType = 1;
        public string TypeText = "King Slime";
        public int TierType = 1;
        public Color TextColor = new Color(225, 174, 255);

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            DisplayName.SetDefault("Broken Ritual");
            Tooltip.SetDefault("Set to a specific boss in Boss Rush\n" +
                "Right click to change boss forward or backward depending on mouse position\n" +
                "Use while the event is active to instantly end the event\nBoss Count" + (IsInfernumOn ? "\nInfernum Enabled" : "" )); 
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 68;
            Item.maxStack = 1;
            Item.rare = 10;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.UseSound = SoundID.Item123;
            Item.useStyle = 5;
        }

        public override bool AltFunctionUse(Player player) => true;
        private static bool IsInfernumOn => CalNohitQoL.InfernumMod is not null;
        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                // negative 1 is left of the player, positive 1 is right of the player.
                int Direction = Math.Sign(Main.MouseWorld.X - player.position.X);
                BRType += Direction;
                int maxBosses = IsInfernumOn? 45 :44;

                if (BRType > maxBosses)
                    BRType = 1;
                else if (BRType < 1)
                    BRType = maxBosses;

                switch(BRType)
                {
                    case 1:
                        TypeText = IsInfernumOn ? "King Slime" : "King Slime";
                        TextColor = new Color(225, 174, 255); //E1EAFF
                        TierType = 1;
                        break;
                    case 2:
                        TypeText = IsInfernumOn ? "Eye of Cthulhu" : "Desert Scourge";
                        break;
                    case 3:
                        TypeText = IsInfernumOn ? "Eater of Worlds":"Eye of Cthulhu";
                        break;
                    case 4:
                        TypeText = IsInfernumOn ? "Wall of Flesh" : "Crabulon";
                        break;
                    case 5:
                        TypeText = IsInfernumOn ? "The Perforators" : "Eater of Worlds";
                        break;
                    case 6:
                        TypeText = IsInfernumOn ? "Queen Bee" : "Brain of Cthulhu";
                        break;
                    case 7:
                        TypeText = IsInfernumOn ? "Queen Slime" : "The Hive Mind";
                        break;
                    case 8:
                        TypeText = IsInfernumOn ? "Astrum Aureus" : "The Perforators";
                        break;
                    case 9:
                        TypeText = IsInfernumOn ? "Crabulon" : "Queen Bee";
                        break;
                    case 10:
                        TypeText = IsInfernumOn ? "Aquatic Scourge" : "Deerclops";
                        break;
                    case 11:
                        TypeText = IsInfernumOn ? "Desert Scourge" : "Skeletron";
                        break;
                    case 12:
                        TypeText = IsInfernumOn ? "Profaned Guardians" : "The Slime God";
                        break;
                    case 13:
                        TypeText = IsInfernumOn ? "Ceaseless Void" : "Wall of Flesh";
                        TextColor = new Color(225, 174, 255);
                        TierType = 1;
                        break;
                    case 14:
                        TypeText = IsInfernumOn ? "Storm Weaver" : "Queen Slime";
                        TextColor = new Color(134, 151, 189); //8697BD
                        TierType = 2;
                        break;
                    case 15:
                        TypeText = IsInfernumOn ? "Brimstone Elemental" : "Cryogen";
                        break;
                    case 16:
                        TypeText = IsInfernumOn ? "Leviathan and Anahita" : "The Twins";
                        break;
                    case 17:
                        TypeText = IsInfernumOn ? "Wall of Flesh" : "Aquatic Scourge";
                        break;
                    case 18:
                        TypeText = IsInfernumOn ? "Hive Mind" : "The Destroyer";
                        break;
                    case 19:
                        TypeText = IsInfernumOn ? "Duke Fishron" : "Brimstone Elemental";
                        break;
                    case 20:
                        TypeText = IsInfernumOn ? "Cryogen" : "Skeletron Prime";
                        break;
                    case 21:
                        TypeText = IsInfernumOn ? "Brain of Cthulhu" : "Calamitas";
                        break;
                    case 22:
                        TypeText = IsInfernumOn ? "Deerclops" : "Plantera";
                        TextColor = new Color(134, 151, 189); //8697BD
                        TierType = 2;
                        break;
                    case 23:
                        TypeText = IsInfernumOn ? "Signus" : "Leviathan and Anahita";
                        TextColor = new Color(111, 214, 255);
                        TierType = 3;
                        break;
                    case 24:
                        TypeText = IsInfernumOn ? "The Dragonfolly" : "Astrum Aureus";
                        break;
                    case 25:
                        TypeText = IsInfernumOn ? "The Slime God" : "Golem";
                        break;
                    case 26:
                        TypeText = IsInfernumOn ? "Skeletron" : "The Plaguebringer Goliath";
                        break;
                    case 27:
                        TypeText = IsInfernumOn ? "Plantera" : "Empress of Light";
                        break;
                    case 28:
                        TypeText = IsInfernumOn ? "The Destroyer" : "Duke Fishron";
                        break;
                    case 29:
                        TypeText = IsInfernumOn ? "The Plaguebringer Goliath" : "Ravager";
                        break;
                    case 30:
                        TypeText = IsInfernumOn ? "Astrum Deus" : "Lunatic Cultist";
                        break;
                    case 31:
                        TypeText = IsInfernumOn ? "Lunatic Cultist" : "Astrum Deus";
                        TextColor = new Color(111, 214, 255);
                        TierType = 3;
                        break;
                    case 32:
                        TypeText = IsInfernumOn ? "Lunatic Cultist" : "Moon Lord";
                        TextColor = new Color(230, 126, 35); //E67E23
                        TierType = 4;
                        break;
                    case 33:
                        TypeText = IsInfernumOn ? "Skeletron Prime" : "Profaned Guardians";
                        break;
                    case 34:
                        TypeText = IsInfernumOn ? "Old Duke" : "The Dragonfolly";
                        break;
                    case 35:
                        TypeText = IsInfernumOn ? "Golem" : "Providence";
                        break;
                    case 36:
                        TypeText = IsInfernumOn ? "Empress of Light" : "Ceaseless Void";
                        break;
                    case 37:
                        TypeText = IsInfernumOn ? "The Twins" : "Storm Weaver";
                        break;
                    case 38:
                        TypeText = IsInfernumOn ? "Polterghast" : "Signus";
                        break;
                    case 39:
                        TypeText = IsInfernumOn ? "Calamitas" : "Polterghast";
                        break;
                    case 40:
                        TypeText = IsInfernumOn ? "Moon Lord" : "The Old Duke";
                        break;
                    case 41:
                        TypeText = IsInfernumOn ? "The Devourer of Gods" : "The Devourer of Gods";
                        TextColor = new Color(230, 126, 35); //E67E23
                        TierType = 4;
                        break;
                    case 42:
                        TypeText = IsInfernumOn ? "Yharon" : "Yharon";
                        TextColor = Color.White;
                        TierType = 5;
                        break;
                    case 43:
                        TypeText = IsInfernumOn ? "Providence" : "Exo Mechs";
                        break;
                    case 44:
                        TypeText = IsInfernumOn ? "Exo Mechs" : "SCal";
                        TextColor = Color.White;
                        TierType = 5;
                        break;
                    case 45:
                        TypeText = IsInfernumOn ? "SCal" : "SCal";
                        TextColor = Color.White;
                        TierType = 5;
                        break;
                }
                Main.NewText("Current Boss: " + TypeText + " (Tier #" + TierType + ", Boss #" + BRType + ")", TextColor);
            }
            else
            {
                if (BossRushEvent.BossRushActive || BossRushEvent.StartTimer > 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                       BossRushEvent.End();
                }
                else
                {
                    BossRushEvent.BossRushStage = BRType - 1;
                    BossRushEvent.BossRushActive = true;
                }
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Main.myPlayer];
            if (player is null)
                return;
            foreach (TooltipLine l in tooltips)
            {
                if (l.Text == null)
                    continue;

                if (l.Text.StartsWith("Boss Count"))
                {
                    l.OverrideColor = TextColor;
                   
                   l.Text = "Current Boss: " + TypeText + " (Tier #" + TierType + ", Boss #" + BRType + ")";

                }
                if (l.Text.StartsWith("Infernum Enabled"))
                    l.OverrideColor = CalNohitQoLUtils.TwoColorPulse(Color.Firebrick, Color.Orange, 2f);
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe().
            AddIngredient<Terminus>().
            AddTile(TileID.Anvils).
            Register();
        }
    }
}