using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.CalPlayer;
using CalNohitQoL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using CalNohitQoL.UI.QoLUI;
using Terraria.ID;

namespace CalNohitQoL
{
	public class CalNohitQoLWorld : ModSystem
	{
		internal static int OriginalTime;
		internal static bool NoSpawns = false;
		internal static bool FrozenTime = false;
		internal static bool MapReveal = false;
		internal static bool MapTeleport = true;
		public override void SaveWorldData(TagCompound tag)
		{
			tag.Set("NoSpawns", NoSpawns);
			tag.Set("FrozenTime", FrozenTime);
		}
		private void ResetFlags()
		{
			TogglesUIManager.clickCooldownTimer = 0;
			TogglesUIManager.CloseAllUI(true);
			TogglesUIManager.outroTimer = 0;
		}
		public override void LoadWorldData(TagCompound tag)
		{
			NoSpawns = tag.GetBool("NoSpawns");
			FrozenTime = tag.GetBool("FrozenTime");
		}
		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(NoSpawns);
			writer.Write(FrozenTime);
		}

		public override void NetReceive(BinaryReader reader)
		{
			NoSpawns = reader.ReadBoolean();
			FrozenTime = reader.ReadBoolean();
		}
		public override void OnWorldLoad()
		{
			ResetFlags();
			UpgradesUIManager.SortOutTextures();
		}

		public override void OnWorldUnload()
		{
			ResetFlags();
		}
		public override void PostUpdateWorld()
		{
			if (FrozenTime && Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.time -= Main.dayRate;
			}
			if (CalNohitQoL.Instance.DisableEvents)
            {
				CalNohitQoL.TryClearEvents();
				CalNohitQoL.Instance.DisableEvents = false;
            }
			if (CalNohitQoL.DownedBrain || CalNohitQoL.DownedEater)
				NPC.downedBoss2 = true;
			else
				NPC.downedBoss2 = false;
			if (MapReveal)
            {
				MapStuff.RevealTheEntireMap();
			}
		}
		public override void PostDrawFullscreenMap(ref string mouseText)
		{
			if(MapTeleport)
				MapStuff.TryToTeleportPlayerOnMap();
		}

		public static ModKeybind OpenTogglesUI { get; private set; }
		public static ModKeybind OpenPotionsUI { get; private set; }
		//public static ModKeybind OpenTipsUI { get; private set; }

		public override void Load()
		{
			OpenTogglesUI = KeybindLoader.RegisterKeybind(Mod, "Open Toggles UI", "L");
			OpenPotionsUI = KeybindLoader.RegisterKeybind(Mod, "Open Potions UI", "P");
			//OpenTipsUI = KeybindLoader.RegisterKeybind(Mod, "Open Tips UI", "O");
		}

	}
}