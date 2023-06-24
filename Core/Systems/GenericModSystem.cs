using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;
using Terraria.ID;
using ToastyQoL.Content.UI;

namespace ToastyQoL.Core.Systems
{
    public class GenericModSystem : ModSystem
    {
        private static void ResetUIStuff()
        {
            TogglesUIManager.ClickCooldownTimer = 0;
            TogglesUIManager.CloseUI();
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(Toggles.NoSpawns);
            writer.Write(Toggles.FrozenTime);
        }

        public override void NetReceive(BinaryReader reader)
        {
            Toggles.NoSpawns = reader.ReadBoolean();
            Toggles.FrozenTime = reader.ReadBoolean();
        }

        public override void OnWorldLoad()
        {
            ResetUIStuff();
        }

        public override void OnWorldUnload() => ResetUIStuff();

        public override void PostUpdateWorld()
        {
            if (Toggles.FrozenTime && Main.netMode == NetmodeID.SinglePlayer)
                Main.time -= Main.dayRate;

            if (Toggles.DisableEvents)
            {
                ClearEvents();
                Toggles.DisableEvents = false;
            }

            if (SavingSystem.DownedBrain || SavingSystem.DownedEater)
                NPC.downedBoss2 = true;
            else
                NPC.downedBoss2 = false;

            if (Toggles.ToggleRain)
            {
                if (Main.raining == true)
                {
                    Main.StopRain();
                    Sandstorm.Happening = false;
                    Sandstorm.StopSandstorm();
                    Sandstorm.WorldClear();
                    Main.SyncRain();
                }
                else
                {
                    Main.StartRain();
                    Main.rainTime = 12000;
                    Sandstorm.StartSandstorm();
                    Sandstorm.Happening = true;
                    Main.SyncRain();
                }
                Toggles.ToggleRain = false;
            }
        }

        internal static void ClearEvents()
        {
            if (Main.invasionType != 0)
                Main.invasionType = 0;

            if (Main.bloodMoon)
                Main.bloodMoon = false;

            if (Main.pumpkinMoon)
                Main.pumpkinMoon = false;

            if (Main.snowMoon)
                Main.snowMoon = false;

            if (Main.eclipse)
                Main.eclipse = false;

            if (Main.WindyEnoughForKiteDrops)
            {
                Main.windSpeedTarget = 0;
                Main.windSpeedCurrent = 0;
            }

            if (Main.slimeRain)
                Main.StopSlimeRain();

            if (DD2Event.Ongoing && Main.netMode != NetmodeID.MultiplayerClient)
                DD2Event.StopInvasion();

            if (Sandstorm.Happening)
            {
                Sandstorm.Happening = false;
                Sandstorm.TimeLeft = 0;
                Sandstorm.IntendedSeverity = 0;
            }

            if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
            {
                NPC.LunarApocalypseIsUp = false;
                NPC.ShieldStrengthTowerNebula = 0;
                NPC.ShieldStrengthTowerSolar = 0;
                NPC.ShieldStrengthTowerStardust = 0;
                NPC.ShieldStrengthTowerVortex = 0;

                // Kill the towers
                for (int i = 0; i < Main.maxNPCs; i++)
                    if (Main.npc[i].active && (Main.npc[i].type == NPCID.LunarTowerNebula || Main.npc[i].type == NPCID.LunarTowerSolar || Main.npc[i].type == NPCID.LunarTowerStardust || Main.npc[i].type == NPCID.LunarTowerVortex))
                        Main.npc[i].active = false;
            }
            return;
        }

        public static ModKeybind OpenTogglesUI { get; private set; }

        public static ModKeybind OpenPotionsUI { get; private set; }


        public override void Load()
        {
            OpenTogglesUI = KeybindLoader.RegisterKeybind(Mod, "Open Toggles UI", "L");
            OpenPotionsUI = KeybindLoader.RegisterKeybind(Mod, "Open Potions UI", "P");
        }
    }
}