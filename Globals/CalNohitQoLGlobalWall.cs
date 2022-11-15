using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalNohitQoL.Globals
{
    public class LightHackGlobalWall : GlobalWall
    {
        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
        {
            if (Toggles.LightHack > 0)
            {
                r = MathHelper.Clamp(r + Toggles.LightHack, 0f, 1f);
                g = MathHelper.Clamp(g + Toggles.LightHack, 0f, 1f);
                b = MathHelper.Clamp(b + Toggles.LightHack, 0f, 1f);
            }
        }
    }
}