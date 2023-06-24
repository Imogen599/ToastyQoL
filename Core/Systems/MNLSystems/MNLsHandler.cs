using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Core.Systems.MNLSystems.Sets;

namespace ToastyQoL.Core.Systems.MNLSystems
{
    public class MNLsHandler : ModSystem
    {
        public enum BossStatuses
        {
            Default,
            Dead = 1,
            Alive = -1
        }

        #region Statics Dictonaries/Methods
        public static List<MNLSet> MNLSets
        {
            get;
            private set;
        } = new();

        public static Dictionary<int, int> ActiveSet
        {
            get
            {
                return MNLSets.OrderBy(s => s.Weight()).Last().AssosiatedIDsAndFightLength;
            }
        }

        public static List<MNLMonitor> ActiveMNLMonitors
        {
            get;
            private set;
        }

        public static IEnumerable<NPC> GetActiveBosses()
        {
            static bool CheckForNPC(NPC n) => (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsTail)
                && n.active && ActiveSet.ContainsKey(n.type);

            if (Main.npc.Any(CheckForNPC))
                return Main.npc.Where(CheckForNPC);

            return null;
        }

        public static void PlayerRespawnChecks()
        {
            foreach (var monitor in ActiveMNLMonitors)
            {
                // If they are not active, then they have despawned.
                if (!Main.npc[monitor.NPCToMonitorIndex].active)
                    monitor.StopMonitoring(false);
            }
        }

        public static void NPCKillChecks(NPC npc)
        {
            foreach (var monitor in ActiveMNLMonitors)
            {
                if (Main.npc[monitor.NPCToMonitorIndex].type == npc.type && monitor.NPCToMonitorIndex == npc.whoAmI)
                    monitor.StopMonitoring(true);
            }
        }

        public static void RegisterSet(MNLSet set) => MNLSets.Add(set);
        #endregion

        #region Overrides
        public override void Load()
        {
            ActiveMNLMonitors = new();
            ExpertSet.Load();
        }

        public override void Unload()
        {
            ActiveMNLMonitors = null;
            MNLSets = null;
        }

        public override void PostUpdateEverything()
        {
            // Only run this on the server, or singleplayer clients.
            if (Main.netMode is NetmodeID.MultiplayerClient)
                return;

            // Handle all active monitors.
            List<MNLMonitor> monitorsToRemove = new();

            foreach (var monitor in ActiveMNLMonitors)
            {
                if (monitor.ReadyToDisplay >= 2)
                {
                    monitor.DisplayMonitorInformation();
                    monitorsToRemove.Add(monitor);
                }
                else
                    monitor.Update();
            }

            foreach (var monitor in monitorsToRemove)
                monitor.Dispose();

            // Get a list of active bosses, that have an mnl.
            IEnumerable<NPC> bosses = GetActiveBosses();

            if (bosses is null)
                return;

            // Loop through each active boss.
            foreach (NPC boss in bosses)
            {
                // Loop through all active MNL Monitors. If the current boss is already monitored, it
                // does not need to be added.
                bool alreadyBeingMonitored = false;
                foreach (var monitor in ActiveMNLMonitors)
                {
                    if (monitor.NPCToMonitor == boss && monitor.NPCToMonitorIndex == boss.whoAmI)
                    {
                        // Mark it as already being monitored
                        alreadyBeingMonitored = true;
                        break;
                    }
                }

                // If it isn't in an active monitor, then one should be created.
                if (!alreadyBeingMonitored)
                    ActiveMNLMonitors.Add(new(boss));
            }
        }
        #endregion
    }
}
