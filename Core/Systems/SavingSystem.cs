using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalNohitQoL.Core.Systems
{
    public class SavingSystem : ModSystem
    {
        public static bool CalamityCallQueued = false;
        public override void PreWorldGen()
        {
            // BOOLS
            Toggles.PotionTooltips = true;
            Toggles.ItemTooltips = true;
            Toggles.PotionLock = false;
            Toggles.ItemLock = false;
            Toggles.AccLock = false;
            Toggles.InstantDeath = false;
            Toggles.GodmodeEnabled = false;
            Toggles.GravestonesEnabled = true;
            Toggles.InfiniteFlightTime = false;
            Toggles.DisableRain = false;
            Toggles.EnableRain = false;
            Toggles.InfiniteAmmo = true;
            Toggles.InfiniteConsumables = true;
            Toggles.InfinitePotions = true;
            Toggles.BiomeFountainsForceBiome = true;
            Toggles.DisableEvents = false;
            Toggles.LightHack = 0;
            Toggles.playerShouldBeJourney = false;
            Toggles.InfiniteMana = false;
            Toggles.AutoChargeDraedonWeapons = true;
            Toggles.AutomateProgressionUpgrades = true;
            CalNohitQoL.DownedBrain = false;
            CalNohitQoL.DownedEater = false;
            Toggles.MNLIndicator = true;
            Toggles.SassMode = false;
            Toggles.FrozenTime = false;
            Toggles.ShroomsExtraDamage = false;
            Toggles.NoSpawns = false;
            MapSystem.MapTeleport = true;
            Toggles.ProperShrooms = true;
            Toggles.ShroomShader = true;
        }
        public override void OnWorldLoad()
        {
            // BOOLS
            Toggles.PotionTooltips = true;
            Toggles.ItemTooltips = true;
            Toggles.PotionLock = false;
            Toggles.ItemLock = false;
            Toggles.AccLock = false;
            Toggles.InstantDeath = false;
            Toggles.GodmodeEnabled = false;
            Toggles.GravestonesEnabled = true;
            Toggles.InfiniteFlightTime = false;
            Toggles.DisableRain = false;
            Toggles.EnableRain = false;
            Toggles.InfiniteAmmo = true;
            Toggles.InfiniteConsumables = true;
            Toggles.InfinitePotions = true;
            Toggles.BiomeFountainsForceBiome = true;
            Toggles.DisableEvents = false;
            Toggles.LightHack = 0;
            Toggles.playerShouldBeJourney = false;
            Toggles.InfiniteMana = false;
            Toggles.AutoChargeDraedonWeapons = true;
            Toggles.AutomateProgressionUpgrades = true;
            CalNohitQoL.DownedBrain = false;
            CalNohitQoL.DownedEater = false;
            Toggles.MNLIndicator = true;
            Toggles.SassMode = false;
            Toggles.FrozenTime = false;
            Toggles.ShroomsExtraDamage = false;
            Toggles.NoSpawns = false;
            MapSystem.MapTeleport = true;
            Toggles.ProperShrooms = true;
            Toggles.ShroomShader = true;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            Toggles.PotionTooltips = tag.GetBool("PotionTooltips");
            Toggles.ItemTooltips = tag.GetBool("ItemTooltips");
            Toggles.PotionLock = tag.GetBool("PotionLock");
            Toggles.ItemLock = tag.GetBool("ItemLock");
            Toggles.AccLock = tag.GetBool("AccLock");
            Toggles.InstantDeath = tag.GetBool("InstantDeath");
            Toggles.GodmodeEnabled = tag.GetBool("GodmodeEnabled");
            Toggles.InfiniteFlightTime = tag.GetBool("InfiniteFlightTime");
            Toggles.DisableRain = tag.GetBool("DisableRain");
            Toggles.EnableRain = tag.GetBool("EnableRain");
            Toggles.InfiniteAmmo = tag.GetBool("InfiniteAmmo");
            Toggles.InfiniteConsumables = tag.GetBool("InfiniteConsumables");
            Toggles.InfinitePotions = tag.GetBool("InfinitePotions");
            Toggles.BiomeFountainsForceBiome = tag.GetBool("BiomeFountainsForceBiomes");
            Toggles.DisableEvents = tag.GetBool("DisableEvents");
            Toggles.LightHack = tag.GetFloat("LightHack");
            Toggles.playerShouldBeJourney = tag.GetBool("playerShouldBeJourney");
            Toggles.InfiniteMana = tag.GetBool("InfiniteMana");
            Toggles.AutoChargeDraedonWeapons = tag.GetBool("AutoChargeDraedonWeapons");
            Toggles.AutomateProgressionUpgrades = tag.GetBool("APU");
            CalNohitQoL.DownedBrain = tag.GetBool("DownedBrain");
            CalNohitQoL.DownedEater = tag.GetBool("DownedEater");
            Toggles.MNLIndicator = tag.GetBool("MNLI");
            Toggles.SassMode = tag.GetBool("SASS");
            Toggles.FrozenTime = tag.GetBool("TimePaused");
            Toggles.ShroomsExtraDamage = tag.GetBool("extraDamage");
            Toggles.NoSpawns = tag.GetBool("nospawns");
            MapSystem.MapTeleport = tag.GetBool("maptp");
            Toggles.ProperShrooms = tag.GetBool("propershrooms");
            Toggles.ShroomShader = tag.GetBool("shroomsshader"); ;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["PotionTooltips"] = Toggles.PotionTooltips;
            tag["ItemTooltips"] = Toggles.ItemTooltips;
            tag["PotionLock"] = Toggles.PotionLock;
            tag["ItemLock"] = Toggles.ItemLock;
            tag["AccLock"] = Toggles.AccLock;
            tag["InstantDeath"] = Toggles.InstantDeath;
            tag["GodmodeEnabled"] = Toggles.GodmodeEnabled;
            tag["InfiniteFlightTime"] = Toggles.InfiniteFlightTime;
            tag["DisableRain"] = Toggles.DisableRain;
            tag["EnabledRain"] = Toggles.EnableRain;
            tag["InfiniteAmmo"] = Toggles.InfiniteAmmo;
            tag["InfiniteConsumables"] = Toggles.InfiniteConsumables;
            tag["InfinitePotions"] = Toggles.InfinitePotions;
            tag["BiomeFountainsForceBiomes"] = Toggles.BiomeFountainsForceBiome;
            tag["DisableEvents"] = Toggles.DisableEvents;
            tag["LightHack"] = Toggles.LightHack;
            tag["playerShouldBeJourney"] = Toggles.playerShouldBeJourney;
            tag["InfiniteMana"] = Toggles.InfiniteMana;
            tag["AutoChargeDraedonWeapons"] = Toggles.AutoChargeDraedonWeapons;
            tag["APU"] = Toggles.AutomateProgressionUpgrades;
            tag["DownedBrain"] = CalNohitQoL.DownedBrain;
            tag["DownedEater"] = CalNohitQoL.DownedEater;
            tag["MNLI"] = Toggles.MNLIndicator;
            tag["SASS"] = Toggles.SassMode;
            tag["TimePaused"] = Toggles.FrozenTime;
            tag["extraDamage"] = Toggles.ShroomsExtraDamage;
            tag["nospawns"] = Toggles.NoSpawns;
            tag["maptp"] = MapSystem.MapTeleport;
            tag["propershrooms"] = Toggles.ProperShrooms;
            tag["shroomsshader"] = Toggles.ShroomShader;
        }

        public override void PreUpdateEntities()
        {
            if (CalamityCallQueued)
            {
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                {
                    bool instantDeathActive = Toggles.InstantDeath;
                    calamity.Call(new object[3] { "SetDifficultyActive", "armageddon", instantDeathActive });
                    calamity.Call(new object[2] { "DisableAllDodges", instantDeathActive });
                }
                CalamityCallQueued = false;
            }
        }

    }
}
