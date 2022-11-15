// NostalgicDrugs.Items.Drug
using CalNohitQoL.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Linq;
using Microsoft.Xna.Framework;

namespace CalNohitQoL.Items
{
    public class NostShroom : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nostalgic Shrooms");
			Tooltip.SetDefault("The original\nThis gets replaced.");
		}
		
		public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine obj = tooltips.LastOrDefault((TooltipLine x) => x.Name == "Tooltip1" && x.Mod == "Terraria");
			obj.Text = "Creates 4 copies of things in various screen positions\nMay be hard on the eyes, be cautious with use.";
			obj.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);


			TooltipLine obj2 = new TooltipLine(Mod, "1", "People who have nohit SCal with this:");
			obj2.Text = "People who have nohit SCal with this:";
			tooltips.Add(obj2);

			TooltipLine obj3 = new TooltipLine(Mod, "2", "People who have nohit SCal with this:");
			obj3.Text = "Joey";
			obj3.OverrideColor = new Color(244, 127, 255, 255);
			tooltips.Add(obj3);

			TooltipLine obj4 = new TooltipLine(Mod, "3", "People who have nohit SCal with this:");
			obj4.Text = "Athos";
			obj4.OverrideColor = new Color(83, 169, 76, 255);
			tooltips.Add(obj4);

			TooltipLine obj5 = new TooltipLine(Mod, "4", "People who have nohit SCal with this:");
			obj5.Text = "Hobbes";
			obj5.OverrideColor = new Color(255, 151, 11, 255);
			tooltips.Add(obj5);

			TooltipLine obj6 = new TooltipLine(Mod, "5", "People who have nohit SCal with this:");
			obj6.Text = "Brue";
			obj6.OverrideColor = new Color(95, 205, 228, 255);
			tooltips.Add(obj6);

			TooltipLine obj7 = new TooltipLine(Mod, "6", "People who have nohit SCal with this:");
			obj7.Text = "Toasty";
			obj7.OverrideColor = new Color(209, 180, 128, 255);
			tooltips.Add(obj7);

			TooltipLine obj8 = new TooltipLine(Mod, "7", "People who have nohit SCal with this:");
			obj8.Text = "Creamy";
			obj8.OverrideColor = new Color(255, 242, 221, 255);
			tooltips.Add(obj8);
		}
        public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 48;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.rare = ItemRarityID.Orange;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			Item.buffType = ModContent.BuffType<Trippy>();
			Item.buffTime = 216000;
			Item.value = Item.buyPrice(1);
			Item.rare = -12;
		}
	}
}