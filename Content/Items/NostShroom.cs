using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Content.Buffs;

namespace ToastyQoL.Content.Items
{
    public class NostShroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nostalgic Shrooms");
            // Tooltip.SetDefault("The original\nThis gets replaced.");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine obj = tooltips.LastOrDefault((x) => x.Name == "Tooltip1" && x.Mod == "Terraria");
            obj.Text = "Creates 4 copies of things in various screen positions\nMay be hard on the eyes, be cautious with use.";
            obj.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);


            TooltipLine obj2 = new(Mod, "1", "People who have nohit SCal with this:")
            {
                Text = "People who have nohit SCal with this:"
            };
            tooltips.Add(obj2);

            TooltipLine obj3 = new(Mod, "2", "People who have nohit SCal with this:")
            {
                Text = "Joey",
                OverrideColor = new(244, 127, 255)
            };
            tooltips.Add(obj3);

            TooltipLine obj4 = new(Mod, "3", "People who have nohit SCal with this:")
            {
                Text = "Athos",
                OverrideColor = new(83, 169, 76)
            };
            tooltips.Add(obj4);

            TooltipLine obj5 = new(Mod, "4", "People who have nohit SCal with this:")
            {
                Text = "Hobbes",
                OverrideColor = new(255, 151, 11)
            };
            tooltips.Add(obj5);

            TooltipLine obj6 = new(Mod, "5", "People who have nohit SCal with this:")
            {
                Text = "Brue",
                OverrideColor = new(95, 205, 228)
            };
            tooltips.Add(obj6);

            TooltipLine obj7 = new(Mod, "6", "People who have nohit SCal with this:")
            {
                Text = "Toasty",
                OverrideColor = new(209, 180, 128)
            };
            tooltips.Add(obj7);

            TooltipLine obj8 = new(Mod, "7", "People who have nohit SCal with this:")
            {
                Text = "Creamy",
                OverrideColor = new(242, 221, 255)
            };
            tooltips.Add(obj8);

            TooltipLine obj9 = new(Mod, "7", "People who have nohit SCal with this:")
            {
                Text = "Xurkiderp",
                OverrideColor = new(242, 221, 255)
            };
            tooltips.Add(obj9);
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
            Item.buffType = ModContent.BuffType<NostTrippy>();
            Item.buffTime = 216000;
            Item.value = Item.buyPrice(1);
            Item.rare = -12;
        }
    }
}