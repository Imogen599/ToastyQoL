using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.NPCs
{
    public class DollDummy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mildly Agitating Dummy");
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 48;
            NPC.lifeMax = 1000000000; //130 million is enough to get shcom to 25, but a little more just for failsafe
            NPC.damage = 1000;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true; //both the npc and projectile are static

            //we want to make sure that you can't see this dummy as much as possible -- no debuffs, no drops, no homing, no sounds
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.value = 0f;
            NPC.scale = 0.001f;
            NPC.Opacity = 0f;
            NPC.chaseable = false;
            NPC.HitSound = null;
            NPC.DeathSound = null;
        }

        public override void AI()
        {
            NPC.ai[1] += 1f;

            //despawn if bosses are alive or after a second
            if (CalamityPlayer.areThereAnyDamnBosses || NPC.ai[1] >= 75f)
                NPC.active = false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 0f;
            return null;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;
    }
}
