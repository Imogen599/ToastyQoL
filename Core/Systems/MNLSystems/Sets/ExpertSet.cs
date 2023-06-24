using System.Collections.Generic;
using Terraria.ID;

namespace ToastyQoL.Core.Systems.MNLSystems.Sets
{
    public static class ExpertSet
    {
        public static Dictionary<int, int> ExpertMNLs => new()
        {
            [NPCID.KingSlime] = 3000,
            [NPCID.EyeofCthulhu] = 3000,
            [NPCID.EaterofWorldsHead] = 3600,
            [NPCID.BrainofCthulhu] = 3000,
            [NPCID.QueenBee] = 4200,
            [NPCID.SkeletronHead] = 4800,
            [NPCID.Deerclops] = 4200,
            [NPCID.WallofFlesh] = 4800,
            [NPCID.QueenSlimeBoss] = 4200,
            [NPCID.Retinazer] = 6000,
            [NPCID.Spazmatism] = 6000,
            [NPCID.SkeletronPrime] = 6000,
            [NPCID.TheDestroyer] = 5400,
            [NPCID.Plantera] = 5100,
            [NPCID.Golem] = 3900,
            [NPCID.HallowBoss] = 3300,
            [NPCID.DukeFishron] = 5400,
            [NPCID.CultistBoss] = 2400,
            [NPCID.MoonLordCore] = 9000
        };

        public static void Load()
        {
            MNLSet expertSet = new(ExpertMNLs, () => MNLWeights.Expert);
            MNLsHandler.RegisterSet(expertSet);
        }
    }
}