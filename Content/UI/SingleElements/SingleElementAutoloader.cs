using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Content.UI.BossUI;
using ToastyQoL.Content.UI.PotionUI;
using ToastyQoL.Core.ModPlayers;

namespace ToastyQoL.Content.UI.SingleElements
{
    public static class SingleElementAutoloader
    {
        public static void Initialize()
        {
            SingleActionElement setSpawnElement = new("SetSpawn", ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/setSpawnUIIcon", AssetRequestMode.ImmediateLoad).Value,
                "Set Spawn", () =>
                {
                    Main.spawnTileX = (int)(Main.LocalPlayer.position.X - 8f + Main.LocalPlayer.width / 2) / 16;
                    Main.spawnTileY = (int)(Main.LocalPlayer.position.Y + Main.LocalPlayer.height) / 16;
                    TogglesUIManager.QueueMessage("Spawn Set", Color.White);
                }, 1f);
            setSpawnElement.TryRegister();

            SingleActionElement potionElement = new("PotionUI", ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/potionUIIcon", AssetRequestMode.ImmediateLoad).Value,
                "Toggle Potions", () =>
                {
                    TogglesUIManager.CloseUI();
                    PotionUIManager.IsDrawing = !PotionUIManager.IsDrawing;
                    SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.Center);
                    Main.playerInventory = false;
                    GenericUpdatesModPlayer.PotionUICooldownTimer = GenericUpdatesModPlayer.UICooldownTimerLength;
                }, 2f);
            potionElement.TryRegister();

            SingleActionElement bossElement = new("BossUI", ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/bossDeathsUIIcon", AssetRequestMode.ImmediateLoad).Value,
                "Boss Toggles", () =>
                {

                }, 8f, 
                (spritebatch) =>
                {
                    BossTogglesUIManager.Draw(spritebatch);
                });
            bossElement.TryRegister();
        }
    }
}
