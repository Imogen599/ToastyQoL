using CalNohitQoL.UI.QoLUI;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalNohitQoL
{
    public class CalNohitQoLModSystem : ModSystem
    {
        public static bool CalamityCallQueued = false;
        public override void PreWorldGen()
        {
            // BOOLS
            CalNohitQoL.Instance.PotionTooltips = true;
            CalNohitQoL.Instance.ItemTooltips = true;
            CalNohitQoL.Instance.PotionLock = false;
            CalNohitQoL.Instance.ItemLock = false;
            CalNohitQoL.Instance.AccLock = false;
            CalNohitQoL.Instance.InstantDeath = false;
            CalNohitQoL.Instance.GodmodeEnabled = false;
            CalNohitQoL.Instance.GravestonesEnabled = true;
            CalNohitQoL.Instance.InfiniteFlightTime = false;
            CalNohitQoL.Instance.DisableRain = false;
            CalNohitQoL.Instance.EnableRain = false;
            CalNohitQoL.Instance.InfiniteAmmo = true;
            CalNohitQoL.Instance.InfiniteConsumables = true;
            CalNohitQoL.Instance.InfinitePotions = true;
            CalNohitQoL.Instance.BiomeFountainsForceBiome = true;
            CalNohitQoL.Instance.DisableEvents = false;
            CalNohitQoL.Instance.LightHack = 0;
            CalNohitQoL.Instance.playerShouldBeJourney = false;
            CalNohitQoL.Instance.InfiniteMana = false;
            CalNohitQoL.Instance.AutoChargeDraedonWeapons = true;
            CalNohitQoL.Instance.AutomateProgressionUpgrades = true;
            CalNohitQoL.DownedBrain = false;
            CalNohitQoL.DownedEater = false;
            CalNohitQoL.Instance.ShroomsInvisProjectilesVisible = true;
            CalNohitQoL.Instance.MNLIndicator = true;
            CalNohitQoL.Instance.SassMode = false;
            CalNohitQoLWorld.FrozenTime = false;
            CalNohitQoL.Instance.ShroomsExtraDamage = false;
            CalNohitQoLWorld.NoSpawns = false;
            CalNohitQoLWorld.MapTeleport = true;
        }
        public override void OnWorldLoad()
        {
            // BOOLS
            CalNohitQoL.Instance.PotionTooltips = true;
            CalNohitQoL.Instance.ItemTooltips = true;
            CalNohitQoL.Instance.PotionLock = false;
            CalNohitQoL.Instance.ItemLock = false;
            CalNohitQoL.Instance.AccLock = false;
            CalNohitQoL.Instance.InstantDeath = false;
            CalNohitQoL.Instance.GodmodeEnabled = false;
            CalNohitQoL.Instance.GravestonesEnabled = true;
            CalNohitQoL.Instance.InfiniteFlightTime = false;
            CalNohitQoL.Instance.DisableRain = false;
            CalNohitQoL.Instance.EnableRain = false;
            CalNohitQoL.Instance.InfiniteAmmo = true;
            CalNohitQoL.Instance.InfiniteConsumables = true;
            CalNohitQoL.Instance.InfinitePotions = true;
            CalNohitQoL.Instance.BiomeFountainsForceBiome = true;
            CalNohitQoL.Instance.DisableEvents = false;
            CalNohitQoL.Instance.LightHack = 0;
            CalNohitQoL.Instance.playerShouldBeJourney = false;
            CalNohitQoL.Instance.InfiniteMana = false;
            CalNohitQoL.Instance.AutoChargeDraedonWeapons = true;
            CalNohitQoL.Instance.AutomateProgressionUpgrades = true;
            CalNohitQoL.DownedBrain = false;
            CalNohitQoL.DownedEater = false;
            CalNohitQoL.Instance.ShroomsInvisProjectilesVisible = true;
            CalNohitQoL.Instance.MNLIndicator = true;
            CalNohitQoL.Instance.SassMode = false;
            CalNohitQoLWorld.FrozenTime = false;
            CalNohitQoL.Instance.ShroomsExtraDamage = false;
            CalNohitQoLWorld.NoSpawns = false;
            CalNohitQoLWorld.MapTeleport = true;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            CalNohitQoL.Instance.PotionTooltips = tag.GetBool("PotionTooltips");
            CalNohitQoL.Instance.ItemTooltips = tag.GetBool("ItemTooltips");
            CalNohitQoL.Instance.PotionLock = tag.GetBool("PotionLock");
            CalNohitQoL.Instance.ItemLock = tag.GetBool("ItemLock");
            CalNohitQoL.Instance.AccLock = tag.GetBool("AccLock");
            CalNohitQoL.Instance.InstantDeath = tag.GetBool("InstantDeath");
            CalNohitQoL.Instance.GodmodeEnabled = tag.GetBool("GodmodeEnabled");
            CalNohitQoL.Instance.InfiniteFlightTime = tag.GetBool("InfiniteFlightTime");
            CalNohitQoL.Instance.DisableRain = tag.GetBool("DisableRain");
            CalNohitQoL.Instance.EnableRain = tag.GetBool("EnableRain");
            CalNohitQoL.Instance.InfiniteAmmo = tag.GetBool("InfiniteAmmo");
            CalNohitQoL.Instance.InfiniteConsumables = tag.GetBool("InfiniteConsumables");
            CalNohitQoL.Instance.InfinitePotions = tag.GetBool("InfinitePotions");
            CalNohitQoL.Instance.BiomeFountainsForceBiome = tag.GetBool("BiomeFountainsForceBiomes");
            CalNohitQoL.Instance.DisableEvents = tag.GetBool("DisableEvents");
            CalNohitQoL.Instance.LightHack = tag.GetFloat("LightHack");
            CalNohitQoL.Instance.playerShouldBeJourney = tag.GetBool("playerShouldBeJourney");
            CalNohitQoL.Instance.InfiniteMana = tag.GetBool("InfiniteMana");
            CalNohitQoL.Instance.AutoChargeDraedonWeapons = tag.GetBool("AutoChargeDraedonWeapons");
            CalNohitQoL.Instance.AutomateProgressionUpgrades = tag.GetBool("APU");
            CalNohitQoL.DownedBrain = tag.GetBool("DownedBrain");
            CalNohitQoL.DownedEater = tag.GetBool("DownedEater");
            CalNohitQoL.Instance.ShroomsInvisProjectilesVisible = tag.GetBool("SIBV");
            CalNohitQoL.Instance.MNLIndicator = tag.GetBool("MNLI");
            CalNohitQoL.Instance.SassMode = tag.GetBool("SASS");
            CalNohitQoLWorld.FrozenTime = tag.GetBool("TimePaused");
            CalNohitQoL.Instance.ShroomsExtraDamage = tag.GetBool("extraDamage");
            CalNohitQoLWorld.NoSpawns = tag.GetBool("nospawns");
            CalNohitQoLWorld.MapTeleport = tag.GetBool("maptp");
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["PotionTooltips"] = CalNohitQoL.Instance.PotionTooltips;
            tag["ItemTooltips"] = CalNohitQoL.Instance.ItemTooltips;
            tag["PotionLock"] = CalNohitQoL.Instance.PotionLock;
            tag["ItemLock"] = CalNohitQoL.Instance.ItemLock;
            tag["AccLock"] = CalNohitQoL.Instance.AccLock;
            tag["InstantDeath"] = CalNohitQoL.Instance.InstantDeath;
            tag["GodmodeEnabled"] = CalNohitQoL.Instance.GodmodeEnabled;
            tag["InfiniteFlightTime"] = CalNohitQoL.Instance.InfiniteFlightTime;
            tag["DisableRain"] = CalNohitQoL.Instance.DisableRain;
            tag["EnabledRain"] = CalNohitQoL.Instance.EnableRain;
            tag["InfiniteAmmo"] = CalNohitQoL.Instance.InfiniteAmmo;
            tag["InfiniteConsumables"] = CalNohitQoL.Instance.InfiniteConsumables;
            tag["InfinitePotions"] = CalNohitQoL.Instance.InfinitePotions;
            tag["BiomeFountainsForceBiomes"] = CalNohitQoL.Instance.BiomeFountainsForceBiome;
            tag["DisableEvents"] = CalNohitQoL.Instance.DisableEvents;
            tag["LightHack"] = CalNohitQoL.Instance.LightHack;
            tag["playerShouldBeJourney"] = CalNohitQoL.Instance.playerShouldBeJourney;
            tag["InfiniteMana"] = CalNohitQoL.Instance.InfiniteMana;
            tag["AutoChargeDraedonWeapons"] = CalNohitQoL.Instance.AutoChargeDraedonWeapons;
            tag["APU"] = CalNohitQoL.Instance.AutomateProgressionUpgrades;
            tag["DownedBrain"] = CalNohitQoL.DownedBrain;
            tag["DownedEater"] = CalNohitQoL.DownedEater;
            tag["SIBV"] = CalNohitQoL.Instance.ShroomsInvisProjectilesVisible;
            tag["MNLI"] = CalNohitQoL.Instance.MNLIndicator;
            tag["SASS"] = CalNohitQoL.Instance.SassMode;
            tag["TimePaused"] = CalNohitQoLWorld.FrozenTime;
            tag["extraDamage"] = CalNohitQoL.Instance.ShroomsExtraDamage;
            tag["nospawns"] = CalNohitQoLWorld.NoSpawns;
            tag["maptp"] = CalNohitQoLWorld.MapTeleport;
        }

        public override void PreUpdateEntities()
        {
            if (CalamityCallQueued)
            {
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                {
                    bool instantDeathActive = CalNohitQoL.Instance.InstantDeath;
                    calamity.Call(new object[3] { "SetDifficultyActive", "armageddon", instantDeathActive });
                    calamity.Call(new object[2] { "DisableAllDodges", instantDeathActive });
                }
                CalamityCallQueued = false;
            }
        }

    }
}
