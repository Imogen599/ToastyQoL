using CalamityMod;
using CalamityMod.Items;
using CalNohitQoL.Core;
using CalNohitQoL.Core.ModPlayers;
using CalNohitQoL.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.UI.UIManagers
{
    public class MiscUIManager : BaseTogglesUIManager
    {
        public const string UIName = "MiscManager";

        public override string Name => UIName;

        public override void Initialize()
        {
            UIElements = new()
            {
                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/gravestone", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/gravestoneGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Gravestones",
                () => "Enable Gravestones dropping",
                1f,
                () => { Toggles.GravestonesEnabled = !Toggles.GravestonesEnabled; },
                typeof(Toggles).GetField("GravestonesEnabled", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/lightHack", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/lightHackGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Light Hack",
                () => Toggles.LightHack < 1 ? "Set Light Hack to " + ((Toggles.LightHack + 0.25f) * 100f).ToString() + "%" : "Turn Light Hack off",
                2f,
                () => 
                {
                    Toggles.LightHack = Toggles.LightHack switch
                    {
                        0f => 0.25f, 
                        0.25f => 0.5f,
                        0.5f => 0.75f,
                        0.75f => 1f,
                        _ => 0f, 
                    };

                    string text = $"Set to {Toggles.LightHack * 100f}%";
                    if (Toggles.LightHack == 0f)
                        text = "Turned Off";

                    GenericUpdatesModPlayer.UIUpdateTextTimer = 120;
                    TogglesUIManager.TextToShow = text;
                    TogglesUIManager.ColorToUse = Color.LightSkyBlue;
                }),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/shroom", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/shroomGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Shrooms Damage",
                () => "Toggles the bonus damage given by the Odd Mushroom family",
                3f,
                () => { Toggles.ShroomsExtraDamage = !Toggles.ShroomsExtraDamage; },
                typeof(Toggles).GetField("ShroomsExtraDamage", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/shroom", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/shroomGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Full Shrooms",
                () => "Toggles a custom odd mushroom effect that perfectly copies]\n[c/ffcc44:almost everything]",
                4f,
                () => { Toggles.ProperShrooms = !Toggles.ProperShrooms; },
                typeof(Toggles).GetField("ProperShrooms", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/shroom", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/shroomGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Full Shrooms RGB",
                () => "Toggles making proper shrooms have the RGB shader effect",
                5f,
                () => { Toggles.ShroomShader = !Toggles.ShroomShader; },
                typeof(Toggles).GetField("ShroomShader", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/charge", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/chargeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Arsenal Recharge",
                () => "Toggles fully charging Arsenal Weapons on respawn",
                6f,
                () =>
                { 
                    Toggles.AutoChargeDraedonWeapons = !Toggles.AutoChargeDraedonWeapons;
                    for (int i = 0; i < Main.LocalPlayer.inventory.Length; i++)
                    {
                        Item item = Main.LocalPlayer.inventory[i];
                        if (item.type >= 5125)
                        {
                            CalamityGlobalItem modItem = item.Calamity();
                            if (modItem != null && modItem.UsesCharge)
                                modItem.Charge = modItem.MaxCharge;
                        }
                    }
                },
                typeof(Toggles).GetField("AutoChargeDraedonWeapons", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/time", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/timeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle MNL Indicator",
                () => "Shows a chat message informing you how close you]\n[c/ffcc44:were to a bosses MNL according to nohit rules",
                7f,
                () =>
                {
                    Toggles.MNLIndicator = !Toggles.MNLIndicator;

                    if (!Toggles.MNLIndicator && Toggles.SassMode)
                        Toggles.SassMode = false;
                },
                typeof(Toggles).GetField("MNLIndicator", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/sass", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Powers/sassGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Sass Mode",
                () => "Shows a chat message when you die that]\n[c/ffcc44:insults you",
                8f,
                () =>
                { 
                    Toggles.SassMode = !Toggles.SassMode;
                    if (!Toggles.SassMode)
                        SoundEngine.PlaySound(new SoundStyle("CalNohitQoL/Assets/Sounds/Custom/babyLaugh"), Main.LocalPlayer.Center);
                },
                typeof(Toggles).GetField("SassMode", CalNohitQoLUtils.UniversalBindingFlags),
                new ToggleBlockInformation(() => Toggles.MNLIndicator, "Enable the MNL Indicator to toggle")),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/dps", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/dpsGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle DPS Stats",
                () => "Shows a chat message that tells you the average dps you had]\n[c/ffcc44:on a boss",
                9f,
                () => { Toggles.BossDPS = !Toggles.BossDPS; },
                typeof(Toggles).GetField("BossDPS", CalNohitQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/tester", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/testerGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Fight Length Type",
                () => "Enable to have the MNL Indicator use testing times instead]\n[c/ffcc44:of nohit times]",
                10f,
                () => 
                { 
                    Toggles.TesterTimes = !Toggles.TesterTimes;
                    GenericUpdatesModPlayer.UpdateActiveLengthDictFlag = true;
                },
                typeof(Toggles).GetField("TesterTimes", CalNohitQoLUtils.UniversalBindingFlags))
            };
        }      
    }
}
