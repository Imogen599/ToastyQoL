using System;
using System.Collections.Generic;
using System.Linq;
using static ToastyQoL.Core.Systems.MNLSystems.MNLsHandler;
using Terraria.ID;
using Terraria;

namespace ToastyQoL.Core.Systems.MNLSystems
{
    public class MNLMonitor
    {
        public NPC NPCToMonitor
        {
            get;
            private set;
        }

        public int NPCToMonitorIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// This an int due to adding a 1 frame delay so this shows after Nycro's information.
        /// </summary>
        public int ReadyToDisplay
        {
            get;
            private set;
        }

        public BossStatuses BossStatus
        {
            get;
            private set;
        }


        public int FramesAlive
        {
            get;
            private set;
        }

        public List<int> DPSDamage
        {
            get;
            private set;
        }

        public MNLMonitor(NPC npcToMonitor)
        {
            NPCToMonitor = npcToMonitor;
            NPCToMonitorIndex = npcToMonitor.whoAmI;
            ReadyToDisplay = 0;
            BossStatus = BossStatuses.Default;
            FramesAlive = 0;
            DPSDamage = new();
        }

        public void Update()
        {
            if (ReadyToDisplay > 0)
                ReadyToDisplay++;
            else
            {
                FramesAlive++;

                // Only do this on singleplayer. Why are you using this mod on MP anyway.
                if (Main.netMode is NetmodeID.SinglePlayer && Main.LocalPlayer.getDPS() != DPSDamage.LastOrDefault())
                    DPSDamage.Add(Main.LocalPlayer.getDPS());
            }
        }

        /// <summary>
        /// Call this to end the monitoring, and mark this as ready to list.
        /// </summary>
        public void StopMonitoring(bool bossWasKilled)
        {
            ReadyToDisplay++;
            BossStatus = (BossStatuses)bossWasKilled.ToDirectionInt();
        }

        public void DisplayMonitorInformation()
        {
            // Don't display a MNL for something that does not have one.
            if (!ActiveSet.TryGetValue(Main.npc[NPCToMonitorIndex].type, out int length))
                return;

            // Under MNL Message
            if (FramesAlive < length)
            {
                float secondsMNL = length / 60;
                float secondsTimer = FramesAlive / 60;
                float timerUnder = secondsMNL - secondsTimer;
                timerUnder = (float)Math.Truncate((double)timerUnder * 100f) / 100f;
                ToastyQoLUtils.DisplayText($"[c/ff2f2f:You were under the kill time by ][c/fccccf:{timerUnder}] [c/ff2f2f:seconds!]");
            }
            // Over MNL Message
            else
                ToastyQoLUtils.DisplayText($"[c/2fff2f:You were above the kill time!]");

            // Display DPS message if required.
            if (Toggles.BossDPS && DPSDamage.Any())
                ToastyQoLUtils.DisplayText($"[c/e7684b:Average DPS:] [c/fccccf:{(int)DPSDamage.Average()}]");

            // Say any sass message if required.
            if (Toggles.SassMode)
                SassModeSystem.SassModeHandler(NPCToMonitor, BossStatus is BossStatuses.Dead);
        }

        /// <summary>
        /// Call to remove this from the main list, disposing of it.
        /// </summary>
        public void Dispose() => ActiveMNLMonitors.Remove(this);
    }
}
