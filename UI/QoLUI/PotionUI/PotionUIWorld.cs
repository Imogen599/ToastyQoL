using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalNohitQoL.UI.QoLUI.PotionUI
{
    public class PotionUIWorld : ModSystem
    {

    }
    public class PotionUIPlayer : ModPlayer
    {
        public Dictionary<string, int> DPotionsAreActive = new Dictionary<string, int>();

        string message = "Thanks to RegularPhoenix#1191 for help with getting this to work, caused me many headaches.";
        public override void SaveData(TagCompound tag)
        {
            if (Main.netMode != NetmodeID.Server && Player.whoAmI == Main.myPlayer)
            {
                if (DPotionsAreActive is null)
                    DPotionsAreActive = new Dictionary<string, int>();

                var list = new List<TagCompound>();
                foreach (var item in DPotionsAreActive)
                {
                    list.Add(new TagCompound()
                    {
                    {"key", item.Key},
                    {"value", item.Value},
                    });
                }
                tag["DPotionsAreActive"] = list;
            }
        }
        public override void LoadData(TagCompound tag)
        {
            if (Main.netMode != NetmodeID.Server && Player.whoAmI == Main.myPlayer)
            {
                var list = tag.GetList<TagCompound>("DPotionsAreActive");
                //CalNohitQoL.potionUIManager.DPotionsAreActive.Clear();
                if (list is not null)
                {
                    foreach (var item in list)
                    {
                        string key = item.GetString("key");
                        int value = item.GetInt("value");
                        DPotionsAreActive[key] = value;
                    }
                }
            }
        }
    }
}