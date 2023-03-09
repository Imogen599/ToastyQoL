using CalamityMod;
using CalamityMod.NPCs;
using CalamityMod.CalPlayer;
using CalamityMod.World;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.NPCs
{
    public class CloneBulletHellSimulator : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bullet Hell Simulator");
        }

        public override void SetDefaults()
        {
            NPC npc = NPC;
            npc.width = 1;
            npc.height = 1;
            npc.lifeMax = 3;
            npc.damage = 0;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.boss = true;
            npc.dontTakeDamage = true;

            //No debuffs, no drops, no homing, no sounds, no visuals. Effectively make the spawner invisible.
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.value = 0f;
            npc.scale = 0.001f;
            npc.Opacity = 0f;
            npc.chaseable = false;
            npc.HitSound = null;
            npc.DeathSound = null;
            if (!Main.dedServ)
            {
                Mod MusicMod;
                if (ModLoader.TryGetMod("CalamityModMusic", out MusicMod))
                {
                    Music = MusicLoader.GetMusicSlot(MusicMod, "Sounds/Music/Calamitas");
                }

            }
        }
        public override void AI()
        {
            #region Variables
            int BHType = CalNohitQoL.CLONEBHTYPE;
            NPC npc = NPC;
            bool enraged = npc.Calamity().enraged > 0;
            bool MaliceMode = BossRushEvent.BossRushActive || enraged;
            bool ExpertMode = Main.expertMode || MaliceMode;
            bool RevengeanceMode = CalamityWorld.revenge || MaliceMode;
            bool DeathMode = CalamityWorld.death || MaliceMode;
            #endregion
            // Get a target
            if (npc.target < 0 || npc.target == Main.maxPlayers || Main.player[npc.target].dead || !Main.player[npc.target].active)
                npc.TargetClosest();

            // Despawn safety, make sure to target another player if the current player target is too far away
            if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) > CalamityGlobalNPC.CatchUpDistance200Tiles)
                npc.TargetClosest();

            Player player = Main.player[npc.target];
            #region Despawn
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];

                // Gone.
                if (!player.active || player.dead)
                {
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
            #endregion
            ref float timer = ref npc.ai[0];
            if (timer < 900f)
            {
                timer += 1f;
                npc.damage = 0;
                npc.dontTakeDamage = true;
                npc.alpha = 255;
                float rotX = player.Center.X - npc.Center.X;
                float rotY = player.Center.Y - npc.Center.Y;
                npc.rotation = (float)Math.Atan2(rotY, rotX) - (float)Math.PI / 2f;
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    return;
                }
                if (BHType == 2)
                {
                    int type5 = ModContent.ProjectileType<SCalBrimstoneFireblast>();
                    int damage5 = npc.GetProjectileDamage(type5);
                    float gigaBlastFrequency = Main.getGoodWorld ? 120f : Main.expertMode ? 180f : 240f;
                    float projSpeed2 = 5f;
                    if (timer <= 300f)
                    {
                        if (timer % gigaBlastFrequency == 0f)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed2, type5, damage5, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                    else if (timer <= 600f && timer > 300f)
                    {
                        if (timer % gigaBlastFrequency == 0f)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 0f - projSpeed2, 0f, type5, damage5, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                    else if (timer > 600f && timer % gigaBlastFrequency == 0f)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed2, type5, damage5, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                npc.ai[1] += 1f;
                float hellblastGateValue = Main.expertMode ? 12f : 16f;
                if (npc.ai[1] >= hellblastGateValue)
                {
                    npc.ai[1] = 0f;
                    int type4 = ModContent.ProjectileType<BrimstoneHellblast2>();
                    int damage4 = npc.GetProjectileDamage(type4);
                    float projSpeed = 4f;
                    if (timer % (hellblastGateValue * 6f) == 0f)
                    {
                        float distance = Main.rand.NextBool() ? -1000f : 1000f;
                        float velocity = distance == -1000f ? projSpeed : 0f - projSpeed;
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + distance, player.position.Y, velocity, 0f, type4, damage4, 0f, Main.myPlayer, 2f, 0f);
                    }
                    if (timer < 300f)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed, type4, damage4, 0f, Main.myPlayer, 2f, 0f);
                    }
                    else if (timer < 600f)
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 0f - (projSpeed - 0.5f), 0f, type4, damage4, 0f, Main.myPlayer, 2f, 0f);
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), projSpeed - 0.5f, 0f, type4, damage4, 0f, Main.myPlayer, 2f, 0f);
                    }
                    else
                    {
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed - 1f, type4, damage4, 0f, Main.myPlayer, 2f, 0f);
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 0f - (projSpeed - 1f), 0f, type4, damage4, 0f, Main.myPlayer, 2f, 0f);
                        Projectile.NewProjectile(npc.GetSource_FromAI(null), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), projSpeed - 1f, 0f, type4, damage4, 0f, Main.myPlayer, 2f, 0f);
                    }
                }
            }
            else
            {
                DespawnProjectiles(); // Despawn the projectiles from the bhs
                npc.active = false; // Despawn the entity
                npc.netUpdate = true;
            }

        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 0f;
            return null;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.Heart;
        }
        private static void DespawnProjectiles()
        {
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectileToCheck = Main.projectile[i];
                if (!projectileToCheck.active)
                {
                    continue;
                }
                if (projectileToCheck.type == ModContent.ProjectileType<BrimstoneHellblast2>() || projectileToCheck.type == ModContent.ProjectileType<BrimstoneBarrage>() || projectileToCheck.type == ModContent.ProjectileType<BrimstoneWave>())
                {
                    if (projectileToCheck.timeLeft > 60)
                    {
                        projectileToCheck.timeLeft = 60;
                    }
                }
                else if (projectileToCheck.type == ModContent.ProjectileType<SCalBrimstoneFireblast>() || projectileToCheck.type == ModContent.ProjectileType<SCalBrimstoneGigablast>())
                {
                    projectileToCheck.ai[1] = 1f;
                    if (projectileToCheck.timeLeft > 60)
                    {
                        projectileToCheck.timeLeft = 60;
                    }
                }
            }
        }
    }
}