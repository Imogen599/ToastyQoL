using CalNohitQoL.Globals;
using CalNohitQoL.Items;
using CalNohitQoL.ModPlayers;
using Terraria;
using Terraria.ModLoader;

namespace CalNohitQoL.Buffs
{
    public class Trippy : ModBuff
	{
        public override void SetStaticDefaults()
        {      
			DisplayName.SetDefault("Trippy");
			Description.SetDefault("So pretty...");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ShroomsPlayer>().Trippy = true;
		}
	}
}