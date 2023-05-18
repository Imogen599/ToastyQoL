using CalamityMod;
using CalamityMod.Items;
using CalNohitQoL.Core.Globals;
using CalNohitQoL.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalNohitQoL.Core.ModPlayers
{
    public class MiscModPlayer : ModPlayer
    {
        // Godmode Hit thing
        public const int HitCooldownFrames = 40;

        public override void UpdateDead()
        {
            if (Toggles.InstantDeath)
                Despawn();
            CalNohitQoLGlobalNPC.currentTimer = 0;
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (Player.whoAmI != Main.myPlayer)
                return false;

            else if (Toggles.GodmodeEnabled)
            {
                if (GenericUpdatesModPlayer.GMHitCooldownTimer == 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("CalNohitQoL/Assets/Sounds/Custom/godmodeHitSFX"), Main.player[Main.myPlayer].Center);
                    GenericUpdatesModPlayer.GMHitCooldownTimer = HitCooldownFrames;
                }
                return false;
            }
            else if (Toggles.InstantDeath)
            {
                if (Main.netMode == NetmodeID.Server || Player.whoAmI != Main.myPlayer)
                    return PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);

                Player.mount.Dismount(Player);
                Player.dead = true;
                Player.respawnTimer = 120;
                Player.immuneAlpha = 0;
                Player.palladiumRegen = false;
                Player.iceBarrier = false;
                Player.crystalLeaf = false;
                NetworkText deathText = damageSource.GetDeathText(Player.name);

                if (Main.netMode == NetmodeID.Server)
                    ChatHelper.BroadcastChatMessage(deathText, new Color(225, 25, 25), -1);
                else if (Main.netMode == NetmodeID.SinglePlayer)
                    CalNohitQoLUtils.DisplayText(deathText.ToString(), (Color?)new Color(225, 25, 25));

                if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
                    NetMessage.SendPlayerDeath(Player.whoAmI, damageSource, damage, hitDirection, pvp, -1, -1);
            }
            return true;
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
        public override void OnEnterWorld(Player player)
        {
            SavingSystem.CalamityCallQueued = false;
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

        public override void OnRespawn(Player player)
        {
            Player.immuneTime = 60;
            if (Toggles.AutoChargeDraedonWeapons)
            {
                for (int i = 0; i < player.inventory.Length; i++)
                {
                    Item item = player.inventory[i];
                    if (item.type >= 5125)
                    {
                        CalamityGlobalItem modItem = item.Calamity();
                        if (modItem != null && modItem.UsesCharge)
                        {
                            modItem.Charge = modItem.MaxCharge;
                        }
                    }
                }
            }

        }
    }
}