using CalamityMod;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Items
{
    public class BrimstoneTorch : ModItem
    {
        public int Amount = 1;
        public Color TextColor = new Color(153, 0, 0);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brimstone Torch");
            Tooltip.SetDefault("Instantly summons Supreme Calamitas\n" +
                "Right-click to increase spawn count up to 10\nSpawn Count");
        }

        public override void SetDefaults()
        {
            Item item = Item;
            item.width = 58;
            item.height = 64;
            item.maxStack = 1;
            item.rare = ItemRarityID.Purple;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                int Direction = Math.Sign(Main.MouseWorld.X - player.position.X);
                Amount += Direction;
                if (Amount > 10)
                    Amount = 1;
                if (Amount < 1)
                    Amount = 10;
                
                Main.NewText("Supreme Calamitas Spawn Count: " + Amount, TextColor);
                return true;
            }
            else
            {
                for (int i = 0; i < Amount; i++)
                {
                    int idx = NPC.NewNPC(null,(int)player.Center.X - 56, (int)player.Center.Y - 102, ModContent.NPCType<SupremeCalamitas>(), 1);
                    if (idx != -1)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            CalamityUtils.BossAwakenMessage(idx);
                        else
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, idx);
                    }
                }
                SoundEngine.PlaySound(SupremeCalamitas.SpawnSound, player.Center);

            }
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (nameLine != null)
            {
                nameLine.OverrideColor = CalNohitQoLUtils.TwoColorPulse(new Color(104, 13, 13), new Color(190, 145, 55), 2f);
            }
            Player player = Main.player[Main.myPlayer];
            if (player is null)
                return;
            foreach (TooltipLine l in tooltips)
            {
                if (l.Text == null)
                    continue;

                if (l.Text.StartsWith("Spawn Count"))
                {
                    l.OverrideColor = Color.Firebrick;
                    l.Text = "Spawn Count: " + Amount;
                    

                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.AuricBar>(),5);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.AshesofCalamity>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}