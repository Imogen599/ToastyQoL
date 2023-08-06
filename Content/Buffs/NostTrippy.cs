using Terraria;
using Terraria.ModLoader;
using ToastyQoL.Core.Globals;

namespace ToastyQoL.Content.Buffs
{
    public class NostTrippy : ModBuff
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
			player.GetModPlayer<ShroomsPlayer>().NostTrippy = true;
		}
	}
}