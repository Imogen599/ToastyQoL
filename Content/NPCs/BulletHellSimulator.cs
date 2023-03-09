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
using CalNohitQoL.Content.Tiles;

namespace CalNohitQoL.Content.NPCs
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
        private static float moonAI = 0;

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
                if (ModLoader.TryGetMod("CalamityModMusic", out var MusicMod))
                {
                    if (CalNohitQoL.BHTYPE == 1)
                        Music = MusicLoader.GetMusicSlot(MusicMod, "Sounds/Music/SupremeCalamitas1");
                    if (CalNohitQoL.BHTYPE == 2)
                        Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/SupremeCalamitas1lol");
                    if (CalNohitQoL.BHTYPE == 3)
                        Music = MusicLoader.GetMusicSlot(MusicMod, "Sounds/Music/SupremeCalamitas2");
                    if (CalNohitQoL.BHTYPE == 4)
                        Music = MusicLoader.GetMusicSlot(MusicMod, "Sounds/Music/SupremeCalamitas3");
                    if (CalNohitQoL.BHTYPE == 5)
                        Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/SupremeCalamitas3lol");
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
                                ; arenaTile.HasTile = true;
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
            if (spawnArena && !player.Hitbox.Intersects(safeBox) || MaliceMode)
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
            if (!endBH)
            {
                switch (BHType)
                {
                    case 1:
                        SCalBH1Sim(player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                    case 2:
                        SCalBH2Sim(player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                    case 3:
                        SCalBH3Sim(player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                    case 4:
                        int divisor = RevengeanceMode ? 225 : Main.expertMode ? 450 : 675;
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
                        SCalBH4Sim(player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset, RevengeanceMode, moonAI);
                        break;
                    case 5:
                        if (!firstMoonSpawned)
                        {
                            SCalBH5Sim(player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                            firstMoonSpawned = true;
                        }
                        else
                            SCalBH5Sim(player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
                        break;
                    default:
                        SCalBH1Sim(player, NPC.ai[1], NPC.ai[2], SpawnFrequency, offset);
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
                        projectileToCheck.timeLeft = 60;
                }
                else if (projectileToCheck.type == ModContent.ProjectileType<SCalBrimstoneFireblast>() || projectileToCheck.type == ModContent.ProjectileType<SCalBrimstoneGigablast>())
                {
                    projectileToCheck.ai[1] = 1f;
                    if (projectileToCheck.timeLeft > 60)
                        projectileToCheck.timeLeft = 60;
                }
            }
        }

        #region BHs
        private readonly static int fireBlastProj = ModContent.ProjectileType<SCalBrimstoneFireblast>();
        private readonly static int gigaBlastProj = ModContent.ProjectileType<SCalBrimstoneGigablast>();

        public void SCalBH1Sim(Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int BH1FrequencyAtPlayer = SpawnFrequency * 6;

                if (overallTimer % SpawnFrequency == SpawnFrequency - 1)
                {
                    if (overallTimer % BH1FrequencyAtPlayer == BH1FrequencyAtPlayer - 1) //Aimed projectile
                    {
                        float distance = Main.rand.NextBool() ? -1000f : 1000f;
                        float velocity = (distance == -1000f ? 4f : -4f) * offset;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    if (bhStageCounter == 1f) // Blasts from above
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from above, left, and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 3f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        public void SCalBH2Sim(Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (bhStageCounter == 1f) // Fireblasts
                {
                    if (overallTimer % 180 == 179) // Blasts from top
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);
                }
                else if (bhStageCounter == 2f)
                {
                    if (overallTimer % 180 == 179) // Blasts from right
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -5f * offset, 0f, fireBlastProj, 1, 0f, Main.myPlayer);
                }
                else if (bhStageCounter == 3f)
                {
                    if (overallTimer % 180 == 179) // Blasts from top
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);
                }
                int BH2Frequency = SpawnFrequency + 1;

                if (overallTimer % BH2Frequency == BH2Frequency - 1) // Hellblasts
                {
                    // This BH does not have aimed projectiles. May make this toggleable for  the funsies.
                    //
                    //if (npc.ai[1] % BH2FrequencyAtPlayer == (BH2FrequencyAtPlayer - 1)) // Aimed projectile
                    //{
                    //	float distance = Main.rand.NextBool() ? -1000f : 1000f;
                    //	float velocity = (distance == -1000f ? 4f : -4f) * offset;
                    //	Projectile.NewProjectile(player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    //}
                    if (bhStageCounter == 1f) // Blasts from below
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y + 1000f, 0f, -4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        public void SCalBH3Sim(Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) // Gigablasts and Fireblasts
            {
                if (overallTimer < 900)
                    overallTimer += 1800f;
                if (overallTimer % 180 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 240 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * offset, gigaBlastProj, 1, 0f, Main.myPlayer);

                int BH3Frequency = SpawnFrequency + 4;

                if (overallTimer % BH3Frequency == BH3Frequency - 1)
                {
                    // No aimed hellblasts
                    //
                    //if (npc.ai[1] % BH3FrequencyAtPlayer == (BH3FrequencyAtPlayer - 1)) // Aimed projectile
                    //{
                    //	float distance = Main.rand.NextBool() ? -1000f : 1000f;
                    //	float velocity = (distance == -1000f ? 4f : -4f) * offset;
                    //	Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    //}
                    if (bhStageCounter == 1f) // Blasts from above
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        public void SCalBH4Sim(Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset, bool RevengeanceMode, float moonAI)
        {

            if (Main.netMode != NetmodeID.MultiplayerClient) // Gigablasts and Fireblasts
            {
                if (overallTimer < 900)
                    overallTimer += 2700f;
                if (overallTimer % 180 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 240 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * offset, gigaBlastProj, 1, 0f, Main.myPlayer);

                int divisor = RevengeanceMode ? 225 : Main.expertMode ? 450 : 675;
                if (overallTimer % divisor == 0 && Main.expertMode) // Moons
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, moonAI);

                }

                int BH4Frequency = SpawnFrequency + 6;

                if (overallTimer % BH4Frequency == BH4Frequency - 1)
                {
                    // You get the idea, no aimed hellblasts.
                    //
                    //if (npc.ai[1] % BH4FrequencyAtPlayer == (BH4FrequencyAtPlayer - 1)) // Aimed projectile
                    //{
                    //	float distance = Main.rand.NextBool() ? -1000f : 1000f;
                    //	float velocity = (distance == -1000f ? 4f : -4f) * offset;
                    //	Projectile.NewProjectile(player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    //}
                    if (bhStageCounter == 1f) // Blasts from below
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y + 1000f, 0f, -4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        public void SCalBH5Sim(Player player, float overallTimer, float bhStageCounter, int SpawnFrequency, float offset)
        {
            // TODO: Make this be better, add a delay before the moons spawn or smth
            if (Main.netMode != NetmodeID.MultiplayerClient) // Gigablasts, Fireblasts, Skulls  
            {

                if (overallTimer < 900)
                {

                    overallTimer += 3600;
                }
                if (overallTimer == 3605)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 0);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 1);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 2);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X, player.position.Y - 500f, 0f, 1f * offset, ModContent.ProjectileType<BrimstoneMonster>(), 1, 0f, Main.myPlayer, 0f, 3);
                }
                if (overallTimer % 240 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * offset, fireBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 360 == 0)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * offset, gigaBlastProj, 1, 0f, Main.myPlayer);

                if (overallTimer % 30 == 0)
                {
                    int random = Main.rand.Next(-500, 501);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + random, -5f * offset, 0f, ModContent.ProjectileType<BrimstoneWave>(), 1, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y - random, 5f * offset, 0f, ModContent.ProjectileType<BrimstoneWave>(), 1, 0f, Main.myPlayer);
                }
                int BH5Frequency = SpawnFrequency + 8;
                int BH5FrequencyAtPlayer = BH5Frequency * 6;

                if (overallTimer % BH5Frequency == BH5Frequency - 1)
                {
                    if (overallTimer % BH5FrequencyAtPlayer == BH5FrequencyAtPlayer - 1) // Aimed projectile
                    {
                        float distance = Main.rand.NextBool() ? -1000f : 1000f;
                        float velocity = (distance == -1000f ? 4f : -4f) * offset;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    if (bhStageCounter == 1f) // Blasts from above
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else if (bhStageCounter == 2f) // Blasts from left and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3.5f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                    else // Blasts from above, left, and right
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 3f * offset, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + Main.rand.Next(-1000, 1001), -3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + Main.rand.Next(-1000, 1001), 3f * offset, 0f, ModContent.ProjectileType<BrimstoneHellblast2>(), 1, 0f, Main.myPlayer);
                    }
                }
            }
        }
        #endregion
    }
}