using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalNohitQoL.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.ID;
using System.Collections.Generic;
using System.Linq;
using CalNohitQoL.Items;

namespace CalNohitQoL
{
    public class CalNohitQoLNPC : GlobalNPC
    {
		public override bool InstancePerEntity => true;
		
		internal static bool bossActive = false;
		internal static NPC currentBoss;
		internal static float currentTimer;

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == ModContent.NPCType<CalamityMod.NPCs.TownNPCs.FAP>())
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<NostShroom>());
				shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1, 0, 0, 0);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<DoubleShroom>());
				shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1, 0, 0, 0);
				nextSlot++;
			}
        }
        public override Color? GetAlpha(NPC npc, Color drawColor)
		{
			if (Main.LocalPlayer.GetModPlayer<CalNohitQoLPlayer>().Trippy||Main.LocalPlayer.GetModPlayer<CalNohitQoLPlayer>().DoubleTrippy)
			{
				return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, npc.alpha);
			}
			return null;
		}
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)     
		{
			if (Main.LocalPlayer.GetModPlayer<CalNohitQoLPlayer>().DoubleTrippy)
			{
				SpriteEffects effects = SpriteEffects.None;
				if (npc.spriteDirection == 1)
				{
					effects = SpriteEffects.FlipHorizontally;
				}
				float num = 0f;
				Vector2 origin = new Vector2(TextureAssets.Npc[npc.type].Value.Width / 2, TextureAssets.Npc[npc.type].Value.Height / Main.npcFrameCount[npc.type] / 2);
				Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
				Color alpha = npc.GetAlpha(newColor);
				float num2 = 0.99f;
				alpha.R = (byte)((float)(int)alpha.R * num2);
				alpha.G = (byte)((float)(int)alpha.G * num2);
				alpha.B = (byte)((float)(int)alpha.B * num2);
				alpha.A = (byte)((float)(int)alpha.A * num2);
				for (int i = 0; i < 8; i++)
				{
					Vector2 position = npc.position;
					float num3 = Math.Abs(npc.Center.X - Main.LocalPlayer.Center.X);
					float num4 = Math.Abs(npc.Center.Y - Main.LocalPlayer.Center.Y);
					if (i == 0 || i == 2)
					{
						position.X = Main.player[Main.myPlayer].Center.X + num3;
					}
					else
					{
						position.X = Main.player[Main.myPlayer].Center.X - num3;
					}
					if (i == 0 || i == 1)
					{
						position.Y = Main.player[Main.myPlayer].Center.Y + num4;
					}
					else
					{
						position.Y = Main.player[Main.myPlayer].Center.Y - num4;
					}
					if (i >= 4)
					{
						if (i == 4 || i == 6)
						{
							position.X = Main.player[Main.myPlayer].Center.X + num4;
						}
						else
						{
							position.X = Main.player[Main.myPlayer].Center.X - num4;
						}
						if (i == 4 || i == 5)
						{
							position.Y = Main.player[Main.myPlayer].Center.Y + num3;
						}
						else
						{
							position.Y = Main.player[Main.myPlayer].Center.Y - num3;
						}
					}
					position.X -= npc.width / 2;
					position.Y -= npc.height / 2;
					Main.spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, new Vector2(position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)TextureAssets.Npc[npc.type].Value.Width * npc.scale / 2f + origin.X * npc.scale, position.Y - Main.screenPosition.Y + (float)npc.height - (float)TextureAssets.Npc[npc.type].Value.Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + origin.Y * npc.scale + num + npc.gfxOffY), npc.frame, alpha, npc.rotation, origin, npc.scale, effects, 0f);
				}
				return false;
			}
			else if (Main.LocalPlayer.GetModPlayer<CalNohitQoLPlayer>().Trippy)
            {
				SpriteEffects effects = SpriteEffects.None;
				if (npc.spriteDirection == 1)
				{
					effects = SpriteEffects.FlipHorizontally;
				}
				float num = 0f;
				Vector2 origin = new Vector2(TextureAssets.Npc[npc.type].Value.Width / 2, TextureAssets.Npc[npc.type].Value.Height / Main.npcFrameCount[npc.type] / 2);
				Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
				Color alpha = npc.GetAlpha(newColor);
				float num2 = 0.99f;
				alpha.R = (byte)((float)(int)alpha.R * num2);
				alpha.G = (byte)((float)(int)alpha.G * num2);
				alpha.B = (byte)((float)(int)alpha.B * num2);
				alpha.A = (byte)((float)(int)alpha.A * num2);
				for (int i = 0; i < 4; i++)
				{
					Vector2 position = npc.Center;
					float num3 = Math.Abs(npc.Center.X - Main.LocalPlayer.Center.X);
					float num4 = Math.Abs(npc.Center.Y - Main.LocalPlayer.Center.Y);
					if (i == 0 || i == 2)
					{
						position.X = Main.player[Main.myPlayer].Center.X + num3;
					}
					else
					{
						position.X = Main.player[Main.myPlayer].Center.X - num3;
					}
					position.X -= npc.width / 2;
					if (i == 0 || i == 1)
					{
						position.Y = Main.player[Main.myPlayer].Center.Y + num4;
					}
					else
					{
						position.Y = Main.player[Main.myPlayer].Center.Y - num4;
					}
					position.Y -= npc.height / 2;
					Main.spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, new Vector2(position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)TextureAssets.Npc[npc.type].Value.Width * npc.scale / 2f + origin.X * npc.scale, position.Y - Main.screenPosition.Y + (float)npc.height - (float)TextureAssets.Npc[npc.type].Value.Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + origin.Y * npc.scale + num + npc.gfxOffY), npc.frame, alpha, npc.rotation, origin, npc.scale, effects, 0f);
				}
				return false;
            }
			return true;
		}
		public override void OnKill(NPC npc)
		{
			switch (npc.type)
			{
				case NPCID.BrainofCthulhu:
					CalNohitQoL.DownedBrain = true;
					break;
				case NPCID.EaterofWorldsHead:
					if (npc.boss == true)
						CalNohitQoL.DownedEater = true;
					break;
			}
			if ( CalNohitQoLPlayer.FightStats.BossAliveFrames > 0 && CalNohitQoLUtils.BossMNLS.ContainsKey(npc.type) && CalNohitQoL.Instance.MNLIndicator && (!(CalNohitQoLPlayer.BossRushActiveFrames > 0)))
			{
				 CalNohitQoLUtils.DisplayMNLMessage(ref CalNohitQoLPlayer.FightStats.BossAliveFrames, ref CalNohitQoLPlayer.FightStats.Boss, true, null);
			}
			if(CalNohitQoLPlayer.IsBossRushActive&&CalNohitQoL.Instance.MNLIndicator&&npc.boss==true)
            {
				TimeSpan time = TimeSpan.FromSeconds(CalNohitQoLPlayer.BossRushActiveFrames / 60);
				string hours = "";
				if (time.Hours < 1 && time.Days < 1)
				{
					hours = "";
				}
				else
				{
					hours = (time.Days * 24 + time.Hours).ToString() + ":";
				}
				string minutes = "";
				if (hours != "" || time.Minutes >= 10)
				{
					minutes = time.Minutes.ToString() + ":";
				}
				else
				{
					minutes = "0" + time.Minutes.ToString() + ":";
				}
				string seconds;
				if (time.Seconds >= 10)
				{
					seconds = time.Seconds.ToString();
				}
				else
				{
					seconds = "0" + time.Seconds.ToString();
				}
				string line = hours + minutes + seconds;
				Main.NewText($"[c/e7684b:Current Time:] [c/fccccf:{line}]");
			}
		}
		public override void PostAI(NPC npc)
		{
		}
		
	}
}