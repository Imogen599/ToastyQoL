using CalamityMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Systems
{
    public class ProgressionSystem : ModSystem
    {
        public enum PUpgradeBossProgressionOrder
        {
            Crabulon,
            SlimeGod,
            WallOfFlesh,
            PostAllMechs,
            AstrumAureus,
            Golem,
            Ravager,
            Deus,
            MoonLord,
            Dragonfolly,
            Providence,
            Polterghast,
            Yharon,
            NA
        }
        private static readonly string Eater = "[c/745e61:Eater]";
        private static readonly string Perforators = "[c/cc5151:Perforators]";
        public static readonly string[] CommunityBossProgression = new string[43]{
            "[c/5a9aff:King Slime]", //0
            "[c/835f39:Desert Scourge]", //1
            "[c/fd9999:Eye of Cthulhu]", // 2
            "[c/b3bd9b:Crabulon]", // 3
            "[c/c87578:Brain]" + "/" + Eater, // 4
            "[c/42356d:Hive Mind]" + "/" + Perforators, // 5
            "[c/baaa16:Queen Bee]", // 6
            "[c/cccc9f:Skeletron]", // 7 
            "[c/d3ccc4:Deerclops]", // 8
            "[c/e34f4f:Slime God]", // 9
            "[c/b84e71:Wall of Flesh]", // 10
            "[c/f776e3:Queen Slime]", // 11
            "[c/6c8ff3:Cryogen]", // 12
            "[c/a0a0a0:The Twins]", // 13
            "[c/49a677:Aquatic Scourge]", // 14
            "[c/a0a0a0:The Destroyer]", // 15
            "[c/8a2030:Brimstone Elemental]", // 16
            "[c/a0a0a0:Skeletron Prime]", // 17
            "[c/ad3446:Calamitas]", // 18
            "[c/cb5498:Plantera]", // 19
            "[c/33a68e:Leviathan and Anahita]", // 20
            "[c/80809e:Astrum Aureus]", // 21
            "[c/8d3800:Golem]", // 22
            "[c/33634b:Plaguebringer Goliath]", // 23
            "[c/fff93b:Empress of Light]", // 24
            "[c/1dcf85:Duke Fishron]", // 25
            "[c/a1756d:Ravager]", // 26
            "[c/3454be:Lunatic Cultist]", // 27
            "[c/7b6382:Astrum Deus]", // 28
            "[c/a28859:Moon Lord]", // 29
            "[c/ffbf49:Profaned Guardians]", // 30
            "[c/c29151:Dragonfolly]", // 31
            "[c/ffbf49:Providence, the Profaned Goddess]", // 32
            "[c/726b85:Ceaseless Void]", // 33
            "[c/ba94b4:Storm Weaver]", // 34
            "[c/964478:Signus]", // 35
            "[c/23c8fe:Polterghast]", // 36
            "[c/8d794d:Old Duke]", // 37
            "[c/ff00ff:The Devourer of Gods]", // 38
            "[c/ffa500:Jungle Dragon, Yharon]", // 39
            "[c/505967:Exo Mechs]", // 40
            "[c/bd0000:Supreme Calamitas]", // 41
            "[c/00FF00:Pre Boss]" // 42
        };

        public static bool CheckProgressionBossStatus()
        {
            if (Toggles.AutomateProgressionUpgrades)
            {
                if (DownedBossSystem.downedYharon)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Yharon);
                else if (DownedBossSystem.downedPolterghast)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Polterghast);
                else if (DownedBossSystem.downedProvidence)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Providence);
                else if (DownedBossSystem.downedDragonfolly)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Dragonfolly);
                else if (NPC.downedMoonlord)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.MoonLord);
                else if (DownedBossSystem.downedAstrumDeus)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Deus);
                else if (DownedBossSystem.downedRavager)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Ravager);
                else if (NPC.downedGolemBoss)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Golem);
                else if (DownedBossSystem.downedAstrumAureus)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.AstrumAureus);
                else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.PostAllMechs);
                else if (Main.hardMode)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.WallOfFlesh);
                else if (DownedBossSystem.downedSlimeGod)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.SlimeGod);
                else if (DownedBossSystem.downedCrabulon)
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.Crabulon);
                else
                    HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder.NA);
            }
            return false;
        }
        private static void HandleAutomatedPermanentUpgrades(PUpgradeBossProgressionOrder bossJustKilled)
        {
            switch (bossJustKilled)
            {
                case PUpgradeBossProgressionOrder.NA:
                    HandleMassUpgradeSetting(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    break;
                case PUpgradeBossProgressionOrder.Crabulon:
                    HandleMassUpgradeSetting(false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
                    break;
                case PUpgradeBossProgressionOrder.SlimeGod:
                    HandleMassUpgradeSetting(false, false, false, false, false, false, false, true, false, false, true, false, false, false, false);
                    break;
                case PUpgradeBossProgressionOrder.WallOfFlesh:
                    HandleMassUpgradeSetting(false, false, false, false, true, false, false, true, false, false, true, false, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.PostAllMechs:
                    HandleMassUpgradeSetting(true, false, false, false, true, false, false, true, false, false, true, false, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.AstrumAureus:
                    HandleMassUpgradeSetting(true, false, false, false, true, false, false, true, false, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.Golem:
                    HandleMassUpgradeSetting(true, true, false, false, true, false, false, true, false, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.Ravager:
                    HandleMassUpgradeSetting(true, true, false, false, true, false, false, true, true, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.Deus:
                    HandleMassUpgradeSetting(true, true, false, false, true, true, false, true, true, false, true, true, false, true, false);
                    break;
                case PUpgradeBossProgressionOrder.MoonLord:
                    HandleMassUpgradeSetting(true, true, false, false, true, true, false, true, true, false, true, true, false, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Dragonfolly:
                    HandleMassUpgradeSetting(true, true, false, false, true, true, false, true, true, true, true, true, false, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Providence:
                    HandleMassUpgradeSetting(true, true, true, false, true, true, false, true, true, true, true, true, false, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Polterghast:
                    HandleMassUpgradeSetting(true, true, true, false, true, true, true, true, true, true, true, true, true, true, true);
                    break;
                case PUpgradeBossProgressionOrder.Yharon:
                    HandleMassUpgradeSetting(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    break;
            }
        }

        // This makes my life a lot easier, this progression thing is annoyin lo
        private static void HandleMassUpgradeSetting(bool bOrange, bool mFruit, bool eBerry, bool dFruit, bool cShard, bool eCore, bool pHeart, bool rageOne, bool rageTwo, bool rageThree, bool adrenOne, bool adrenTwo, bool adrenThree, bool accOne, bool accTwo)
        {
            Main.LocalPlayer.Calamity().bOrange = bOrange;
            Main.LocalPlayer.Calamity().mFruit = mFruit;
            Main.LocalPlayer.Calamity().eBerry = eBerry;
            Main.LocalPlayer.Calamity().dFruit = dFruit;
            Main.LocalPlayer.Calamity().cShard = cShard;
            Main.LocalPlayer.Calamity().eCore = eCore;
            Main.LocalPlayer.Calamity().pHeart = pHeart;
            Main.LocalPlayer.Calamity().rageBoostOne = rageOne;
            Main.LocalPlayer.Calamity().rageBoostTwo = rageTwo;
            Main.LocalPlayer.Calamity().rageBoostThree = rageThree;
            Main.LocalPlayer.Calamity().adrenalineBoostOne = adrenOne;
            Main.LocalPlayer.Calamity().adrenalineBoostTwo = adrenTwo;
            Main.LocalPlayer.Calamity().adrenalineBoostThree = adrenThree;
            Main.LocalPlayer.extraAccessory = accOne;
            Main.LocalPlayer.Calamity().extraAccessoryML = accTwo;
        }
        public static string GetLatestBossKilled()
        {
            string bossToReturn;
            // the pain..
            if (DownedBossSystem.downedSCal)
                bossToReturn = CommunityBossProgression[41];

            else if (DownedBossSystem.downedExoMechs)
                bossToReturn = CommunityBossProgression[40];

            else if (DownedBossSystem.downedYharon)
                bossToReturn = CommunityBossProgression[39];

            else if (DownedBossSystem.downedDoG)
                bossToReturn = CommunityBossProgression[38];

            else if (DownedBossSystem.downedBoomerDuke)
                bossToReturn = CommunityBossProgression[37];

            else if (DownedBossSystem.downedPolterghast)
                bossToReturn = CommunityBossProgression[36];

            else if (DownedBossSystem.downedSignus)
                bossToReturn = CommunityBossProgression[35];

            else if (DownedBossSystem.downedStormWeaver)
                bossToReturn = CommunityBossProgression[34];

            else if (DownedBossSystem.downedCeaselessVoid)
                bossToReturn = CommunityBossProgression[33];

            else if (DownedBossSystem.downedProvidence)
                bossToReturn = CommunityBossProgression[32];

            else if (DownedBossSystem.downedDragonfolly)
                bossToReturn = CommunityBossProgression[31];

            else if (DownedBossSystem.downedGuardians)
                bossToReturn = CommunityBossProgression[30];

            else if (NPC.downedMoonlord)
                bossToReturn = CommunityBossProgression[29];

            else if (DownedBossSystem.downedAstrumDeus)
                bossToReturn = CommunityBossProgression[28];

            else if (NPC.downedAncientCultist)
                bossToReturn = CommunityBossProgression[27];

            else if (DownedBossSystem.downedRavager)
                bossToReturn = CommunityBossProgression[26];

            else if (NPC.downedFishron)
                bossToReturn = CommunityBossProgression[25];

            else if (NPC.downedEmpressOfLight)
                bossToReturn = CommunityBossProgression[24];

            else if (DownedBossSystem.downedPlaguebringer)
                bossToReturn = CommunityBossProgression[23];

            else if (NPC.downedGolemBoss)
                bossToReturn = CommunityBossProgression[22];

            else if (DownedBossSystem.downedAstrumAureus)
                bossToReturn = CommunityBossProgression[21];

            else if (DownedBossSystem.downedLeviathan)
                bossToReturn = CommunityBossProgression[20];

            else if (NPC.downedPlantBoss)
                bossToReturn = CommunityBossProgression[19];

            else if (DownedBossSystem.downedCalamitas)
                bossToReturn = CommunityBossProgression[18];

            else if (NPC.downedMechBoss3)
                bossToReturn = CommunityBossProgression[17];

            else if (DownedBossSystem.downedBrimstoneElemental)
                bossToReturn = CommunityBossProgression[16];

            else if (NPC.downedMechBoss1)
                bossToReturn = CommunityBossProgression[15];

            else if (DownedBossSystem.downedAquaticScourge)
                bossToReturn = CommunityBossProgression[14];

            else if (NPC.downedMechBoss2)
                bossToReturn = CommunityBossProgression[13];

            else if (DownedBossSystem.downedCryogen)
                bossToReturn = CommunityBossProgression[12];

            else if (NPC.downedQueenSlime)
                bossToReturn = CommunityBossProgression[11];

            else if (Main.hardMode)
                bossToReturn = CommunityBossProgression[10];

            else if (DownedBossSystem.downedSlimeGod)
                bossToReturn = CommunityBossProgression[9];

            else if (NPC.downedDeerclops)
                bossToReturn = CommunityBossProgression[8];

            else if (NPC.downedBoss3)
                bossToReturn = CommunityBossProgression[7];

            else if (NPC.downedQueenBee)
                bossToReturn = CommunityBossProgression[6];

            else if (DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator)
                bossToReturn = CommunityBossProgression[5];

            else if (NPC.downedBoss2)
                bossToReturn = CommunityBossProgression[4];

            else if (DownedBossSystem.downedCrabulon)
                bossToReturn = CommunityBossProgression[3];

            else if (NPC.downedBoss1)
                bossToReturn = CommunityBossProgression[2];

            else if (DownedBossSystem.downedDesertScourge)
                bossToReturn = CommunityBossProgression[1];

            else if (NPC.downedSlimeKing)
                bossToReturn = CommunityBossProgression[0];
            else
                bossToReturn = CommunityBossProgression[42];
            return bossToReturn;
        }
    }
}
