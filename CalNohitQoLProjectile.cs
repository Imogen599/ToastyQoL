using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalNohitQoL.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;

namespace CalNohitQoL
{
    public class CalNohitQoLProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool PreAI(Projectile projectile)
        {
			if (!CalNohitQoL.Instance.GravestonesEnabled && (projectile.type == 43 || projectile.type == 201 || projectile.type == 202 || projectile.type == 203 || projectile.type == 204 || projectile.type == 205 || projectile.type == 527 || projectile.type == 528 || projectile.type == 529 || projectile.type == 530 || projectile.type == 531))
			{
				((Entity)projectile).active = false;
				NetMessage.SendData(MessageID.SyncProjectile, -1, -1, (NetworkText)null, ((Entity)projectile).whoAmI, 0f, 0f, 0f, 0, 0, 0);
				return false;
			}
			return true;
		}
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (Main.LocalPlayer.GetModPlayer<CalNohitQoLPlayer>().Trippy||Main.LocalPlayer.GetModPlayer<CalNohitQoLPlayer>().DoubleTrippy)
            {
                return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, projectile.alpha);
            }
            return null;
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)      
		{
			//if (Main.LocalPlayer.GetModPlayer<CalNohitQoLPlayer>().DoubleTrippy)
			//{
			//	Texture2D texture2D = TextureAssets.Projectile[projectile.type].Value;
			//	SpriteEffects effects = SpriteEffects.None;
			//	if (projectile.spriteDirection == -1)
			//	{
			//		effects = SpriteEffects.FlipHorizontally;
			//	}
			//	float num = 0f;
			//	Vector2 origin = new Vector2(texture2D.Width / 2, texture2D.Height / Main.projFrames[projectile.type] / 2);
			//	Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
			//	Color alpha = projectile.GetAlpha(newColor);
			//	float num2 = 0.99f;
			//	alpha.R = (byte)((float)(int)alpha.R * num2);
			//	alpha.G = (byte)((float)(int)alpha.G * num2);
			//	alpha.B = (byte)((float)(int)alpha.B * num2);
			//	alpha.A = (byte)((float)(int)alpha.A * num2);
			//	for (int i = 0; i < 8; i++)
			//	{
			//		Vector2 position = projectile.position;
			//		float num3 = Math.Abs(projectile.Center.X - Main.player[Main.myPlayer].Center.X);
			//		float num4 = Math.Abs(projectile.Center.Y - Main.player[Main.myPlayer].Center.Y);
			//		if (i == 0 || i == 2)
			//		{
			//			position.X = Main.player[Main.myPlayer].Center.X + num3;
			//		}
			//		else
			//		{
			//			position.X = Main.player[Main.myPlayer].Center.X - num3;
			//		}
			//		if (i == 0 || i == 1)
			//		{
			//			position.Y = Main.player[Main.myPlayer].Center.Y + num4;
			//		}
			//		else
			//		{
			//			position.Y = Main.player[Main.myPlayer].Center.Y - num4;
			//		}
			//		if (i >= 4)
			//		{
			//			if (i == 4 || i == 6)
			//			{
			//				position.X = Main.player[Main.myPlayer].Center.X + num4;
			//			}
			//			else
			//			{
			//				position.X = Main.player[Main.myPlayer].Center.X - num4;
			//			}
			//			if (i == 4 || i == 5)
			//			{
			//				position.Y = Main.player[Main.myPlayer].Center.Y + num3;
			//			}
			//			else
			//			{
			//				position.Y = Main.player[Main.myPlayer].Center.Y - num3;
			//			}
			//		}

			//		//position.Y = Main.player[Main.myPlayer].Center.X + num4;//7
			//		//position.Y = Main.player[Main.myPlayer].Center.X - num4;//8
			//		int num5 = texture2D.Height / Main.projFrames[projectile.type];
			//		int y = num5 * projectile.frame;
			//		position.Y -= projectile.height / 2;//end of y
			//		Main.spriteBatch.Draw(texture2D, new Vector2(position.X - Main.screenPosition.X + (float)(projectile.width / 2) - (float)texture2D.Width * projectile.scale / 2f + origin.X * projectile.scale, position.Y - Main.screenPosition.Y + (float)projectile.height - (float)texture2D.Height * projectile.scale / (float)Main.projFrames[projectile.type] + 4f + origin.Y * projectile.scale + num + projectile.gfxOffY), new Rectangle(0, y, texture2D.Width, num5), alpha, projectile.rotation, origin, projectile.scale, effects, 0f);
			//	}
			//	return false;
			//}
			Player MainPlayer = Main.player[Main.myPlayer];
			if (MainPlayer.GetModPlayer<CalNohitQoLPlayer>().DoubleTrippy)
			{
				Texture2D texture2D = TextureAssets.Projectile[projectile.type].Value;
				SpriteEffects effects = SpriteEffects.None;
				if (projectile.spriteDirection == -1)
				{
					effects = SpriteEffects.FlipHorizontally;
				}
				float num = 0f;
				Vector2 origin = new Vector2(texture2D.Width / 2, texture2D.Height / Main.projFrames[projectile.type] / 2);
				Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
				Color alpha = projectile.GetAlpha(newColor);
				float num2 = 0.99f;
				alpha.R = (byte)((float)(int)alpha.R * num2);
				alpha.G = (byte)((float)(int)alpha.G * num2);	
				alpha.B = (byte)((float)(int)alpha.B * num2);
				alpha.A = (byte)((float)(int)alpha.A * num2);
				for (int i = 0; i < 8; i++)
				{
					Vector2 position = projectile.position;
					float num3 = Math.Abs(projectile.Center.X - Main.player[Main.myPlayer].Center.X);
					float num4 = Math.Abs(projectile.Center.Y - Main.player[Main.myPlayer].Center.Y);
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
					//position.Y = Main.player[Main.myPlayer].Center.X + num4;//7
					//position.Y = Main.player[Main.myPlayer].Center.X - num4;//8
					int num5 = texture2D.Height / Main.projFrames[projectile.type];
					int y = num5 * projectile.frame;
					position.X -= projectile.width / 2;
					position.Y -= projectile.height / 2;//end of y
					Main.spriteBatch.Draw(texture2D, new Vector2(position.X - Main.screenPosition.X + (float)(projectile.width / 2) - (float)texture2D.Width * projectile.scale / 2f + origin.X * projectile.scale, position.Y - Main.screenPosition.Y + (float)projectile.height - (float)texture2D.Height * projectile.scale / (float)Main.projFrames[projectile.type] + 4f + origin.Y * projectile.scale + num + projectile.gfxOffY), new Rectangle(0, y, texture2D.Width, num5), alpha, projectile.rotation, origin, projectile.scale, effects, 0f);
				}
				if (CalNohitQoLLists.ShroomsDrawProjs.Contains(projectile.type) && CalNohitQoL.Instance.ShroomsInvisProjectilesVisible)
					return true;
				else
					return false;
				
			}
			else if (MainPlayer.GetModPlayer<CalNohitQoLPlayer>().Trippy)
			{
				Texture2D texture2D = TextureAssets.Projectile[projectile.type].Value;
				SpriteEffects effects = 0;
				if (projectile.spriteDirection == -1)
				{
					effects = (SpriteEffects)1;
				}
				
                Vector2 origin = new Vector2(texture2D.Width / 2, texture2D.Height / Main.projFrames[projectile.type] / 2);
                Color newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
				Color alpha = projectile.GetAlpha(newColor);			
				for (int i = 0; i < 4; i++)
				{
					Vector2 position = projectile.position;
					float num3 = Math.Abs(projectile.Center.X - MainPlayer.Center.X);
					float num4 = Math.Abs(projectile.Center.Y - MainPlayer.Center.Y);
					if (i == 0 || i == 2)
					{
						position.X = MainPlayer.Center.X + num3;
					}
					else
					{
						position.X = MainPlayer.Center.X - num3;
					}
					position.X -= projectile.width / 2;
					if (i == 0 || i == 1)
					{
						position.Y = MainPlayer.Center.Y + num4;
					}
					else
					{
						position.Y = MainPlayer.Center.Y - num4;
					}


					//position.Y = Main.player[Main.myPlayer].Center.X + num4;//7
					//position.Y = Main.player[Main.myPlayer].Center.X - num4;//8
					int frames = texture2D.Height / Main.projFrames[projectile.type];
					int y = frames * projectile.frame;
					position.Y -= projectile.height / 2;//end of y
					Main.spriteBatch.Draw(texture2D, new Vector2(position.X - Main.screenPosition.X + (float)(((Entity)projectile).width / 2) - (float)texture2D.Width * projectile.scale / 2f + origin.X * projectile.scale, position.Y - Main.screenPosition.Y + (float)((Entity)projectile).height - (float)texture2D.Height * projectile.scale / (float)Main.projFrames[projectile.type] + 4f + origin.Y * projectile.scale + projectile.gfxOffY), (Rectangle?)new Rectangle(0, y, texture2D.Width, frames), alpha, projectile.rotation, origin, projectile.scale, effects, 0f);
				}
				if (CalNohitQoLLists.ShroomsDrawProjs.Contains(projectile.type) && CalNohitQoL.Instance.ShroomsInvisProjectilesVisible)
					return true;
				else
					return false;
			}
			return true;
		}

	}
}