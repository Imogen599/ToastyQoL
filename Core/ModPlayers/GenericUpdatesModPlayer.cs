using ToastyQoL.Content.UI.PotionUI;
using ToastyQoL.Core.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using ToastyQoL.Content.UI;

namespace ToastyQoL.Core.ModPlayers
{
    public class GenericUpdatesModPlayer : ModPlayer
    {
        public static int ToggleUICooldownTimer { get; internal set; }

        public static int PotionUICooldownTimer { get; internal set; }

        public const int UICooldownTimerLength = 10;

        internal static int GMHitCooldownTimer = 0;

        public override void PreUpdate()
        {
            //Timer Updates
            if (GMHitCooldownTimer > 0)
                GMHitCooldownTimer--;
            if (PotionUICooldownTimer > 0)
                PotionUICooldownTimer--;
            if (ToggleUICooldownTimer > 0)
                ToggleUICooldownTimer--;
            if (TogglesUIManager.ClickCooldownTimer > 0)
                TogglesUIManager.ClickCooldownTimer--;


            PotionUIManager.GiveBuffs(Player);

            // Update any cheat effects.
            if (Toggles.GodmodeEnabled)
                Player.statLife = Player.statLifeMax2;
            if (Toggles.InfiniteFlightTime)
                Player.wingTime = Player.wingTimeMax;
            if (Toggles.InfiniteMana)
                Player.statMana = Player.statManaMax2;

            // Check for if Sass Mode should be turned off.
            if (!Toggles.MNLIndicator && Toggles.SassMode)
                Toggles.SassMode = false;

            // Update extra Instant Death toggle effects
            if (Toggles.InstantDeath)
            {
                if (Player.HasBuff(BuffID.BrainOfConfusionBuff))
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s brain is not as big as they think"), 1000.0, 0, false);

                Player.buffImmune[BuffID.ShadowDodge] = true;
                Player.onHitDodge = false;
            }

            // Update our UI.
            TogglesUIManager.Update();
        }

        public override void PostUpdateEquips()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                if (Toggles.InstantDeath)
                {
                    Player.shadowDodgeTimer = 2;
                    Player.blackBelt = false;
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (GenericModSystem.OpenTogglesUI.JustPressed)
            {
                if (PotionUIManager.IsDrawing == true)
                {
                    TogglesUIManager.CloseUI(true);
                    ToggleUICooldownTimer = UICooldownTimerLength;
                    PotionUICooldownTimer = UICooldownTimerLength;
                }
                else if (ToggleUICooldownTimer == 0)
                {
                    if (TogglesUIManager.UIOpen)
                        TogglesUIManager.CloseUI(true);
                    else
                        TogglesUIManager.OpenUI(true);
                }

            }

            if (GenericModSystem.OpenPotionsUI.JustPressed)
            {
                if (PotionUIManager.IsDrawing && PotionUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    PotionUIManager.IsDrawing = false;
                    PotionUICooldownTimer = UICooldownTimerLength;
                }
                else if (PotionUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.Center);
                    PotionUIManager.IsDrawing = true;
                    PotionUICooldownTimer = UICooldownTimerLength;
                }

            }
        }
    }
}
