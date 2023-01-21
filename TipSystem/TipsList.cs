using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalNohitQoL.TipSystem
{
    public static class TipsList
    {
        //// Pre Hardmode
        //public static List<string> KSTipsList { get; private set; }
        //public static List<string> DSTipsList { get; private set; }
        //public static List<string> EoCTipsList { get; private set; }
        //public static List<string> CrabTipsList { get; private set; }
        //public static List<string> BoCTipsList { get; private set; }
        //public static List<string> EoWTipsList { get; private set; }
        //public static List<string> PerfsTipsList { get; private set; }
        //public static List<string> HMTipsList { get; private set; }
        //public static List<string> QBTipsList { get; private set; }
        //public static List<string> DCTipsList { get; private set; }
        //public static List<string> SKTipsList { get; private set; }
        //public static List<string> SGTipsList { get; private set; }
        //public static List<string> WoFTipsList { get; private set; }

        //// Hardmode
        //public static List<string> QSTipsList { get; private set; }
        //public static List<string> CryoTipsList { get; private set; }
        //public static List<string> TTTipsList { get; private set; }
        //public static List<string> BETipsList { get; private set; }
        //public static List<string> TDTipsList { get; private set; }
        //public static List<string> ASTipsList { get; private set; }
        //public static List<string> SPTipsList { get; private set; }
        //public static List<string> CCTipsList { get; private set; }
        //public static List<string> PTipsList { get; private set; }
        //public static List<string> AATipsList { get; private set; }
        //public static List<string> ALTipsList { get; private set; }
        //public static List<string> GTipsList { get; private set; }
        //public static List<string> EoLTipsList { get; private set; }
        //public static List<string> PBGTipsList { get; private set; }
        //public static List<string> DFTipsList { get; private set; }
        //public static List<string> RTipsList { get; private set; }
        //public static List<string> LCTipsList { get; private set; }
        //public static List<string> ADTipsList { get; private set; }
        //public static List<string> MLTipsList { get; private set; }

        //// Post Moonlord
        //public static List<string> PGTipsList { get; private set; }
        //public static List<string> BFTipsList { get; private set; }
        //public static List<string> ProvTipsList { get; private set; }
        //public static List<string> CVTipsList { get; private set; }
        //public static List<string> SWTipsList { get; private set; }
        //public static List<string> STipsList { get; private set; }
        //public static List<string> PoltTipsList { get; private set; }
        //public static List<string> ODTipsList { get; private set; }
        //public static List<string> DoGTipsList { get; private set; }
        //public static List<string> YTipsList { get; private set; }
        //public static List<string> EMTipsList { get; private set; }
        //public static List<string> SCalTipsList { get; private set; }

        //// Others
        //public static List<string> GCTipsList { get; private set; }
        //public static List<string> GSSTipsList { get; private set; }
        //public static List<string> AEWTipsList { get; private set; }

        //public static void LoadLists()
        //{
        //    #region Pre HM
        //    KSTipsList = new List<string>()
        //    {
        //        "When King Slime teleports, it will try to get ahead of you. Make sure you're ready to quickly change directions!",
        //        "Fire debuffs are very effective against Slime-type enemies.",
        //        "A thin layer of lava does an excellent job of killing its slime minions. Just don't get burned yourself!",
        //        "King Slime's jewel does little damage but can knock you into King Slime itself. Try to kill it before proceeding with King Slime itself.",
        //    };
        //    DSTipsList = new List<string>()
        //    {
        //        "Hunter Potions can help you keep track of the worms, even while they're underground.",
        //        "The Map Overlay is another way to keep an eye on any worm boss' location. Press Tab until it appears. Use Page Up and Page Down to adjust its transparency to your liking.",
        //        "The Desert Nuisance minions truly are a nuisance. You might want to focus on eliminating them as soon as possible.",
        //        "Try to move horizontally to stay outside of the Sand Blast spread.",
        //    };
        //    EoCTipsList = new List<string>()
        //    {
        //        "For the first phase, you'll need good crowd control. For second phase, you'll need good mobility. Try to juggle between those two and find a good combo.",
        //        "When Eye of Cthulhu starts to do the horizontal charge, make sure you go over its head by jumping or go below it. Otherwise the next horizontal charge will be extremely awkward to dodge as the eye 'drifts' into you.",
        //        "When Eye of Cthulhu starts to rapidly charges during Phase 2, do not panic. Simply keep running in the same direction and all of it will miss if you have enough movement speed. Hermes Boots is good enough.",
        //    };
        //    CrabTipsList = new List<string>()
        //    {
        //        "Crabulon is quite massive, but even then he can get quite fast in a couple of seconds. Pay attention to its movements.",
        //        "When above 66% HP, Crabulon will stomp 3 times, spawning a wall of mushrooms from his mouth on the 3rd stomp. When below 66% HP, Crabulon will stomp 4 times, spawning mushroom walls on the 1st and 3rd stomps.",
        //        "Craublon alternates between a 'small stomps' and a 'high jump'. The 'high jump' can hit you immediately if you are above Crabulon. Try to stay at same elevation as Crabulon during it.",
        //    };
        //    BoCTipsList = new List<string>()
        //    {
        //        "Brain of Cthulhu prefers to teleport to the corners of your screen.",
        //        "Although Creepers are fast and swarm in numbers, they can be easily repelled by hitting them with your weapon.",
        //        "Make sure to have a fast firing weapon for Phase 2 to consistently knockback the brain.",
        //    };
        //    EoWTipsList = new List<string>()
        //    {
        //        "In Revengeance and higher, the Eater of Worlds fires Cursed Fireballs from its heads, so make sure to have as less individual worms as possible.",
        //        "Fight Eater of World in an underground corruption arena. It reduce its aggressiveness compare to a surface arena (notably the acceleration and charge speed of the worm).",
        //        "Try not to split the worm as it makes the fight harder. While it may take time, the easiest way to fight it is to only fire your piercing weapon directly at its face when it is charging at you.",
        //        "Vile Spit can be killed, so using an AoE weapon can help reduce the risk from them.",
        //    };
        //    PerfsTipsList = new List<string>()
        //    {
        //        "The Medium Perforator can split like Eater of Worlds, and the Large Perforator has the ability to burrow into the ground and launch itself out at you.",
        //        "Perforator Hive will fire its projectile wall in a slanted pattern if you try to run horizontally when any perforator worm is alive, but will not actively hunt you down by contact damage."
        //    };
        //    HMTipsList = new List<string>()
        //    {
        //        "Dank Creepers leave behind a dangerous rain cloud when they die. Be mindful of where you kill them.",
        //        "Hive Mind can only teleport onto the ground in phase 1. If you are in the air, he will teleport onto the nearest ground below you.",
        //        "In phase 2 when Hive Mind teleports, stay calm and static to see which attack it is using. None of it will hit you immediately as long as you are standing still (as long as you already dealt with other summons). This also works for its spinning charge.",
        //    };
        //    #endregion
        //}
        //public static void UnloadLists()
        //{
        //    KSTipsList = null;
        //    DSTipsList = null;
        //    EoCTipsList = null;
        //    CrabTipsList = null;
        //    BoCTipsList = null;
        //    EoWTipsList = null;
        //    PerfsTipsList = null;
        //    HMTipsList = null;
        //    QBTipsList = null;
        //    DCTipsList = null;
        //    SKTipsList = null;
        //    SGTipsList = null;
        //    WoFTipsList = null;
        //    QSTipsList = null;
        //    CryoTipsList = null;
        //    TTTipsList = null;
        //    BETipsList = null;
        //    TDTipsList = null;
        //    ASTipsList = null;
        //    CCTipsList = null;
        //    PTipsList = null;
        //    AATipsList = null;
        //    ALTipsList = null;
        //    GTipsList = null;
        //    EoLTipsList = null;
        //    PBGTipsList = null;
        //    DFTipsList = null;
        //    RTipsList = null;
        //    LCTipsList = null;
        //    ADTipsList = null;
        //    MLTipsList = null;
        //    PGTipsList = null;
        //    BFTipsList= null;
        //    ProvTipsList = null;
        //    CVTipsList = null;
        //    SWTipsList = null;
        //    STipsList = null;
        //    PoltTipsList = null;
        //    ODTipsList = null;
        //    DoGTipsList = null;
        //    YTipsList = null;
        //    EMTipsList = null;
        //    SCalTipsList = null;
        //    GCTipsList = null;
        //    GSSTipsList = null;
        //    AEWTipsList = null;
        //}
    }
}
