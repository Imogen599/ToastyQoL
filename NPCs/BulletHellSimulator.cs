using CalamityMod;
using CalamityMod.NPCs;
using CalamityMod.CalPlayer;
using CalamityMod.World;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalNohitQoL.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.NPCs
{
    public class BulletHellSimulator : ModNPC
    {
        private float offset = 1f;
        private bool spawnArena = false;
        private bool firstMoonSpawned = false;
        private int spawnX = 0;
        private int spawnX2 = 0;
        private int spawnXReset = 0;
        private int spawnXReset2 = 0;
        private int spawnY = 0;
        private int spawnYReset = 0;
        public static float moonAI = 0;

        private Rectangle safeBox = default;

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
                    if (CalNohitQoL.BHTYPE == 1)
                        Music = MusicLoader.GetMusicSlot(MusicMod, "Sounds/Music/SupremeCalamitas1");
                    if (CalNohitQoL.BHTYPE == 2)
                        Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/SupremeCalamitas1lol");
                    if (CalNohitQoL.BHTYPE == 3)
                        Music = MusicLoader.GetMusicSlot(MusicMod, "Sounds/Music/SupremeCalamitas2");
                    if (CalNohitQoL.BHTYPE == 4)
                        Music = MusicLoader.GetMusicSlot(MusicMod, "Sounds/Music/SupremeCalamitas3");
                    if (CalNohitQoL.BHTYPE == 5)
                        Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/SupremeCalamitas3lol");
                }
                
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(spawnArena);
            writer.Write(spawnX);
            writer.Write(spawnX2);
            writer.Write(spawnXReset);
            writer.Write(spawnXReset2);
            writer.Write(spawnY);
            writer.Write(spawnYReset);

            writer.Write(safeBox.X);
            writer.Write(safeBox.Y);
            writer.Write(safeBox.Width);
            writer.Write(safeBox.Height);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            spawnArena = reader.ReadBoolean();
            spawnX = reader.ReadInt32();
            spawnX2 = reader.ReadInt32();
            spawnXReset = reader.ReadInt32();
            spawnXReset2 = reader.ReadInt32();
            spawnY = reader.ReadInt32();
            spawnYReset = reader.ReadInt32();

            safeBox = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        public override void AI()
        {
            #region Variables
            int BHType = CalNohitQoL.BHTYPE;
            NPC npc = NPC;
            bool enraged = npc.Calamity().enraged > 0;
			bool MaliceMode = BossRushEvent.BossRushActive || enraged;
            bool ExpertMode = Main.expertMode || MaliceMode;
			bool RevengeanceMode = CalamityWorld.revenge || MaliceMode;
            bool DeathMode = CalamityWorld.death || MaliceMode;
            int SpawnFrequency = RevengeanceMode ? 8 : ExpertMode ? 9 : 10;
            bool endBH = false;
            #endregion
            // Get a target
            if (npc.target < 0 || npc.target == Main.maxPlayers || Main.player[npc.target].dead || !Main.player[npc.target].active)
				npc.TargetClosest();

			// Despawn safety, make sure to target another player if the current player target is too far away
			if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) > CalamityGlobalNPC.CatchUpDistance200Tiles)
				npc.TargetClosest();
                
            Player player = Main.player[npc.target];

            #region ArenaCreation
            // Create the arena on the first frame. This does not run client-side.
            // If this is done on the server, a sync must be performed so that the arena box is known to the clients.
            if (!spawnArena)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (DeathMode)
                    {
                        safeBox.X = spawnX = spawnXReset = (int)(npc.Center.X - 1000f);
                        spawnX2 = spawnXReset2 = (int)(npc.Center.X + 1000f);
                        safeBox.Y = spawnY = spawnYReset = (int)(npc.Center.Y - 1000f);
                        safeBox.Width = 2000;
                        safeBox.Height = 2000;
                    }
                    else
                    {
                        safeBox.X = spawnX = spawnXReset = (int)(npc.Center.X - 1250f);
                        spawnX2 = spawnXReset2 = (int)(npc.Center.X + 1250f);
                        safeBox.Y = spawnY = spawnYReset = (int)(npc.Center.Y - 1250f);
                        safeBox.Width = 2500;
                        safeBox.Height = 2500;
                    }

                    int num52 = (int)(safeBox.X + (float)(safeBox.Width / 2)) / 16;
                    int num53 = (int)(safeBox.Y + (float)(safeBox.Height / 2)) / 16;
                    int num54 = safeBox.Width / 2 / 16 + 1;
                    for (int num55 = num52 - num54; num55 <= num52 + num54; num55++)
                    {
                        for (int num56 = num53 - num54; num56 <= num53 + num54; num56++)
                        {
                            if (!WorldGen.InWorld(num55, num56, 2))
                                continue;

                            if ((num55 == num52 - num54 || num55 == num52 + num54 || num56 == num53 - num54 || num56 == num53 + num54) && !Main.tile[num55, num56].HasTile)
                            {
                                Main.tile[num55, num56].TileType = (ushort)ModContent.TileType<ArenaTileClone>();
                                Tile arenaTile = Main.tile[num55, num56];
;                               arenaTile.HasTile = true;
                            }
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, num55, num56, 1, TileChangeType.None);
                            }
                            else
                            {
                                WorldGen.SquareTileFrame(num55, num56, true);
                            }
                        }
                    }

                    // Sync to update all clients on the state of the arena.
                    // Only after this will enrages be registered.
                    
                    spawnArena = true;
                    npc.netUpdate = true;
                }
            }
            #endregion
            #region Enrage
            if ((spawnArena && !player.Hitbox.Intersects(safeBox) || MaliceMode))
            {
                float projectileVelocityMultCap = !player.Hitbox.Intersects(safeBox) && spawnArena ? 2f : 1.5f;
                offset = MathHelper.Clamp(offset * 1.01f, 1f, projectileVelocityMultCap);
            }
            else
            {
                offset = MathHelper.Clamp(offset * 0.99f, 1f, 2f);
            }
            #endregion
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
            #region Timers and other things
            // Defense = Type of bullet hell this is dumb so i made an actual variable for it.
            // BHType = Which BH to use.
            // ai[2] = Stage of bullet hell
            // ai[1] = Running timer

            npc.ai[1] += 1f;

            if (npc.ai[1] <= 900)
            {
                if (npc.ai[1] % 900f == 0f)
                    npc.ai[2] = 1f; //Reset the cycle (0-5s)

                if (npc.ai[1] % 900f == 300f)
                    npc.ai[2] = 2f; //5-10s

                if (npc.ai[1] % 900f == 600f)
                    npc.ai[2] = 3f; //10-15s

                if (npc.ai[1] % 900f == 0)
                    npc.ai[2] = 4f;

                // Failsafes
                if (npc.ai[2] < 1f)
                    npc.ai[2] = 1f;
            }
            // Mark the BH as ended
            else
            {
                DespawnProjectiles(); // Despawn the projectiles from the bhs
                endBH = true; // Failsafe to stop any of the bhs trying to run for one frame
                npc.active = false; // Despawn the entity
                npc.netUpdate = true;
            }
                

            if (npc.ai[1] >= 5f)
            npc.Center = player.Center; //Keep the entity at the player to prevent despawning.
            #endregion
            #region BH Spawning Logic
            if(!endBH)
            {
                switch (BHType)
                {
                    case 1:
                        CalNohitQoLUtils.SCalBH1Sim(NPC, player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                    case 2:
                        CalNohitQoLUtils.SCalBH2Sim(NPC, player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                    case 3:
                        CalNohitQoLUtils.SCalBH3Sim(NPC, player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                    case 4:
                        int divisor = (RevengeanceMode ? 225 : (Main.expertMode ? 450 : 675));
                        if (npc.ai[1] % divisor == 0 && Main.expertMode) // Moons
                        {
                            if (!firstMoonSpawned)
                            {
                                moonAI = 0;
                                firstMoonSpawned = true;
                            }
                            else
                            {
                                moonAI++;
                            }
                        }
                        CalNohitQoLUtils.SCalBH4Sim(NPC, player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset, RevengeanceMode, moonAI);
                        break;
                    case 5:
                        if (!firstMoonSpawned)
                        {
                            CalNohitQoLUtils.SCalBH5Sim(NPC, player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset, RevengeanceMode, moonAI, true);
                            firstMoonSpawned = true;
                        }
                        else
                            CalNohitQoLUtils.SCalBH5Sim(NPC, player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset, RevengeanceMode, moonAI, false);
                        break;
                    default:
                        CalNohitQoLUtils.SCalBH1Sim(NPC, player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                }
            }    
            #endregion                  
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
        private void DespawnProjectiles()
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