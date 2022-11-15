using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalNohitQoL
{
	public class LightHackGlobalWall : GlobalWall
	{
		public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
		{
			if (CalNohitQoL.Instance.LightHack > 0)
			{
				r = MathHelper.Clamp(r + CalNohitQoL.Instance.LightHack, 0f, 1f);
				g = MathHelper.Clamp(g + CalNohitQoL.Instance.LightHack, 0f, 1f);
				b = MathHelper.Clamp(b + CalNohitQoL.Instance.LightHack, 0f, 1f);
			}
		}
	}
}