using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ToastyQoL.Core.ModPlayers
{
    public class MiscModPlayer : ModPlayer
    {
        // Godmode Hit thing
        public const int HitCooldownFrames = 40;

        public override void UpdateDead()
        {
            if (Toggles.InstantDeath)
                Despawn();
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (Toggles.GodmodeEnabled && !Toggles.InstantDeath)
            {
                if (GenericUpdatesModPlayer.GMHitCooldownTimer == 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("ToastyQoL/Assets/Sounds/Custom/godmodeHitSFX"), Main.player[Main.myPlayer].Center);
                    GenericUpdatesModPlayer.GMHitCooldownTimer = HitCooldownFrames;
                }
                return true;
            }
            return false;
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)/* tModPorter Override ImmuneTo, FreeDodge or ConsumableDodge instead to prevent taking damage */
        {
            if (Player.whoAmI != Main.myPlayer)
                return;

            if (Toggles.InstantDeath)
            {
                Player.mount.Dismount(Player);
                Player.dead = true;
                Player.respawnTimer = 120;
                Player.immuneAlpha = 0;
                Player.palladiumRegen = false;
                Player.iceBarrier = false;
                Player.crystalLeaf = false;
                NetworkText deathText = modifiers.DamageSource.GetDeathText(Player.name);
                ToastyQoLUtils.DisplayText(deathText.ToString(), (Color?)new Color(225, 25, 25));

                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendPlayerDeath(Player.whoAmI, modifiers.DamageSource, 999999, modifiers.HitDirection, modifiers.PvP, -1, -1);
            }
        }

        private void CheckBiomeFountains()
        {
            if (Toggles.BiomeFountainsForceBiome)
            {
                if (NPC.downedQueenBee)
                {
                    switch (Main.SceneMetrics.ActiveFountainColor)
                    {
                        case 0:
                            Player.ZoneBeach = true;
                            break;
                        case 2:
                            Player.ZoneCorrupt = true;
                            break;
                        case 3:
                            Player.ZoneJungle = true;
                            break;
                        case 4:
                            Player.ZoneHallow = true;
                            break;
                        case 5:
                            Player.ZoneSnow = true;
                            break;
                        case 10:
                            Player.ZoneCrimson = true;
                            break;
                        case 12:
                            Player.ZoneDesert = true;
                            break;
                    }
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            CheckBiomeFountains();
            Player.unlockedBiomeTorches = true;
        }

        public static void Despawn()
        {
            for (int i2 = 0; i2 < 1000; i2++)
            {
                if (Main.projectile[i2].hostile && Main.projectile[i2].active)
                {
                    Main.projectile[i2].active = false;
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, i2, 0f, 0f, 0f, 0, 0, 0);
                }
            }

            for (int l = 0; l < 200; l++)
            {
                if (!Main.npc[l].friendly && !Main.npc[l].boss && Main.npc[l].active)
                {
                    Main.npc[l].life = 0;
                    Main.npc[l].active = false;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, l, 0f, 0f, 0f, 0, 0, 0);

                }
            }
            for (int m = 0; m < 200; m++)
            {
                if (Main.npc[m].boss && Main.npc[m].active)
                {
                    Main.npc[m].life = 0;
                    Main.npc[m].active = false;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, m, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }

        public override void OnRespawn() => Player.immuneTime = 60;
    }
}