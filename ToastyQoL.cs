using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Shaders;
using ToastyQoL.Content.UI.UIManagers;
using ToastyQoL.Content.UI.PotionUI;
using ToastyQoL.Content.UI.SingleElements;
using ToastyQoL.Content.UI.BossUI;

namespace ToastyQoL
{
    public partial class ToastyQoL : Mod
	{
        internal static ToastyQoL Instance;

		public override void Load()
		{
			Instance = this;
            LoadShaders();
            UIManagerAutoloader.InitializeLocks();
            UIManagerAutoloader.InitializeMisc();
            UIManagerAutoloader.InitializePower();
            UIManagerAutoloader.InitializeWorld();
            SingleElementAutoloader.Initialize();
            BossTogglesUIManager.InitializeBossElements();
            PotionUIManager.InitializePotionElements();
        }

        public override void Unload()
		{
			Instance = null;
        }

        private void LoadShaders()
        {
            if (Main.netMode is not NetmodeID.Server)
            {
                Ref<Effect> shrooms = new(Assets.Request<Effect>("Assets/Effects/ShroomShader", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["ToastyQoL:Shrooms"] = new MiscShaderData(shrooms, "ShroomsPass");

                Ref<Effect> hologram = new(Assets.Request<Effect>("Assets/Effects/HologramShader", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["ToastyQoL:Hologram"] = new MiscShaderData(hologram, "HologramPass");
            }
        }
    }
}