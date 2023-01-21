using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalNohitQoL.Systems;
using CalNohitQoL.UI.QoLUI;
using CalNohitQoL.UI.QoLUI.PotionUI;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.ModPlayers
{
    public class GenericUpdatesModPlayer : ModPlayer
    {
        private static int LocalTimer = 0;

        public static int UIUpdateTextTimer { get; internal set; } = 0;

        public static int ToggleUICooldownTimer { get; internal set; }

        public static int PotionUICooldownTimer { get; internal set; }

        public static int KeepRageMaxedTimer { get; internal set; }

        public const int UICooldownTimerLength = 15;

        internal static int GMHitCooldownTimer = 0;

        internal static bool UpdateUpgradesTextFlag;
        internal static bool UpdateActiveLengthDictFlag = true;

        public override void PreUpdate()
        {
            //Timer Updates
            LocalTimer++;
            if (LocalTimer > 60)
                LocalTimer = 0;
            if (GMHitCooldownTimer > 0)
                GMHitCooldownTimer--;
            if (PotionUICooldownTimer > 0)
                PotionUICooldownTimer--;
            if (ToggleUICooldownTimer > 0)
                ToggleUICooldownTimer--;
            if (TogglesUIManager.ClickCooldownTimer > 0)
                TogglesUIManager.ClickCooldownTimer--;
            if (UIUpdateTextTimer > 0)
                UIUpdateTextTimer--;

            if (KeepRageMaxedTimer > 0)
            {
                KeepRageMaxedTimer--;
                Player.Calamity().rage = Player.Calamity().rageMax;
            }
            // Generic update calls
            if(UpdateUpgradesTextFlag)
                UpdateUpgradesTextFlag = UpgradesUIManager.SortOutTextures() || ProgressionSystem.CheckProgressionBossStatus();

            if(Main.LocalPlayer.GetModPlayer<PotionUIPlayer>().DPotionsAreActive.Count > 0)
                CalNohitQoL.potionUIManager.GiveBuffs();

            if (UpdateActiveLengthDictFlag)
                UpdateActiveLengthDictFlag = MNLSystem.UpdateActiveDictonary();

            // Update any cheat effects.
            if (Toggles.GodmodeEnabled)
                Player.statLife = Player.statLifeMax2;
            if (Toggles.InfiniteFlightTime)
                Player.wingTime = Player.wingTimeMax;
            if (Toggles.InfiniteMana)
                Player.statMana = Player.statManaMax2;

            // Check for if the infernum difficulty is active.
            if (CalNohitQoL.InfernumMod != null)
                CalNohitQoL.InfernumModeEnabled = (bool)CalNohitQoL.InfernumMod.Call("GetInfernumActive");

            // Check for if Sass Mode should be turned off.
            if (!Toggles.MNLIndicator && Toggles.SassMode)
                Toggles.SassMode = false;

            // Update extra Instant Death toggle effects
            if (Toggles.InstantDeath)
            {
                if (Player.FindBuffIndex(ModContent.BuffType<HolyInferno>()) > -1)
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " was burnt by the Holy Inferno"), 1000.0, 0, false);

                if (Player.HasBuff(BuffID.BrainOfConfusionBuff))
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s brain is not as big as they think"), 1000.0, 0, false);

                Player.buffImmune[59] = true;
                Player.onHitDodge = false;
                Player.buffImmune[ModContent.BuffType<TarragonImmunity>()] = true;
                Player.Calamity().tarragonImmunity = false;
            }
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
                    TogglesUIManager.CloseAllUI(true);
                    ToggleUICooldownTimer = UICooldownTimerLength;
                    PotionUICooldownTimer = UICooldownTimerLength;
                }
                else if (ToggleUICooldownTimer == 0)
                    TogglesUIManager.UIOpen = !TogglesUIManager.UIOpen;
                if (TogglesUIManager.UIOpen && ToggleUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.Center);
                    TogglesUIManager.CloseAllUI(false);
                    ToggleUICooldownTimer = UICooldownTimerLength;
                }
                else if (ToggleUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    TogglesUIManager.CloseAllUI(true);
                    ToggleUICooldownTimer = UICooldownTimerLength;
                }
            }
            if (GenericModSystem.OpenPotionsUI.JustPressed)
            {
                if (PotionUIManager.IsDrawing && PotionUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuClose, Main.LocalPlayer.Center);
                    PotionUIManager.IsDrawing = false;
                    PotionUICooldownTimer = UICooldownTimerLength;
                    PotionUIManager.Timer = 0;
                }
                else if (PotionUICooldownTimer == 0)
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.Center);
                    PotionUIManager.IsDrawing = true;
                    PotionUICooldownTimer = UICooldownTimerLength;
                    PotionUIManager.Timer = 0;
                }

            }
            /*if (GenericModSystem.OpenTipsUI.JustPressed)
            {
                TipsUIManager.IsDrawing = !TipsUIManager.IsDrawing;
            }*/
        }
    }
}
