using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.NPCs.ExoMechs;
using System.Collections.Generic;
using System.Linq;

namespace CalNohitQoL.Items.BossSpawners.PostMoonlord
{
    public class DraedonSpawner : ModItem
    {
        public int Amount = 1;
        public Color TextColor = new Color(153, 0, 0);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draedon Spawner");
            Tooltip.SetDefault("Instantly summons Draedon.");
        }
        public override void SetDefaults()
        {
            Item item = Item;
            item.width = 58;
            item.height = 64;
            item.maxStack = 1;
            item.rare = 11;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool AltFunctionUse(Player player) => false;

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {    
            int idx = NPC.NewNPC(null, (int)player.Center.X, (int)player.Center.Y -200, ModContent.NPCType<Draedon>(), 1);
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Color color = CalNohitQoLUtils.TwoColorPulse(new Color(105, 105, 105), new Color(128, 128, 128), 2); 
            TooltipLine nameLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (nameLine != null)
            {
                nameLine.OverrideColor = color;
            }     
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.AuricBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.DraedonMisc.DraedonPowerCell>(), 100);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
