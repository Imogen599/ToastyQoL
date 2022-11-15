using CalNohitQoL.Items;
using Terraria;
using Terraria.ModLoader;

namespace CalNohitQoL.Buffs
{
	public class DoubleTrippy : ModBuff
	{
        public override void SetStaticDefaults()
        {      
			DisplayName.SetDefault("Trippier");
			Description.SetDefault("Ok, I think you've taken enough...");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalNohitQoLPlayer>().DoubleTrippy = true;
		}
	}
}