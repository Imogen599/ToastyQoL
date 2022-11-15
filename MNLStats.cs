using System;
using Terraria;

namespace CalNohitQoL
{
    public class MNLStats
    {
        public NPC Boss;
        public DateTime Start;
        public DateTime End;
        public float BossAliveFrames;
        public float BossLife;
        public bool BossDied;

        public MNLStats()
        {
            Boss = null;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            BossAliveFrames = 0;
            BossLife = 0;
            BossDied = false;
        }
    }
}
