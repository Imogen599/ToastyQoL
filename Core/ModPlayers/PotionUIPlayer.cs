using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ToastyQoL.Content.UI.PotionUI;

namespace ToastyQoL.Core.ModPlayers
{
    public class PotionUIPlayer : ModPlayer
    {
        public override void SaveData(TagCompound tag) => PotionUIManager.SavePotions(tag);

        public override void LoadData(TagCompound tag) => PotionUIManager.LoadPotions(tag);
    }
}