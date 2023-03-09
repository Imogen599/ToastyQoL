using CalamityMod.NPCs.SupremeCalamitas;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CalNohitQoL.Core.Systems
{
    public class InfernumSepulcherWarningSystem : ModSystem
    {
        private static bool WarningGiven;

        public override void OnWorldLoad() => WarningGiven = false;

        public override void PostUpdateEverything()
        {
            if (!WarningGiven && CalNohitQoL.InfernumMod is not null && !CalNohitQoL.InfernumModeEnabled)
                for (int i = 0; i < Main.npc.Length; i++)
                    if (Main.npc[i].type == ModContent.NPCType<SepulcherHead>())
                    {
                        CalNohitQoLUtils.DisplayText("Warning: You have Infernum enabled. Sepulcher will not delete projectiles properly.", Color.OrangeRed);
                        WarningGiven = true;
                        break;
                    }
        }
    }
}
