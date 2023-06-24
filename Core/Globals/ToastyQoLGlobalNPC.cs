using ToastyQoL.Content.Items;
using ToastyQoL.Core.ModPlayers;
using ToastyQoL.Core.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Core.Systems.MNLSystems;

namespace ToastyQoL.Core.Globals
{
    public class ToastyQoLGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<NostShroom>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1, 0, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DoubleShroom>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1, 0, 0, 0);
                nextSlot++;
            }
        }

        public override void OnKill(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.BrainofCthulhu:
                    SavingSystem.DownedBrain = true;
                    break;
                case NPCID.EaterofWorldsHead:
                    if (npc.boss == true)
                        SavingSystem.DownedEater = true;
                    break;
            }
            MNLsHandler.NPCKillChecks(npc);
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (Toggles.NoSpawns)
            {
                spawnRate = 0;
                maxSpawns = 0;
            }
        }

    }
}