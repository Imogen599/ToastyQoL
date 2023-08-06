using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Content.Items;
using ToastyQoL.Core.Systems;
using ToastyQoL.Core.Systems.MNLSystems;

namespace ToastyQoL.Core.Globals
{
    public class ToastyQoLGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
                shop.Add(new NPCShop.Entry(ModContent.ItemType<NostShroom>()), new NPCShop.Entry(ModContent.ItemType<DoubleShroom>()));
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