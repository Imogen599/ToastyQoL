using CalamityMod;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.Items
{
    public class PercentGun : ModItem
    {
        public override string Texture => "Terraria/Images/Item_1254";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Percenter");
            Tooltip.SetDefault("Deals 10% of the nearest targets max HP to them");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.crit = 69;
            Item.DamageType = DamageClass.Default;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = false;
            Item.useAnimation = 25;
            Item.knockBack = 1;
            Item.useTime = 25;
            Item.shoot = ProjectileID.ConfettiGun;
            Item.shootSpeed = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.width = Item.height = 25;
            Item.rare = ModContent.RarityType<CalamityRed>();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NPC target = player.Center.ClosestNPCAt(2500, true, true);
            if (target != null)
            {
                float lifeRatio = target.life / (float)target.lifeMax;
                if (lifeRatio > 0.1)
                {
                    int damageToDeal = target.lifeMax / 10;
                    target.life -= damageToDeal;
                }
                else
                {
                    target.NPCLoot();
                    target.life = 0;
                    target.active = false;
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(0);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}