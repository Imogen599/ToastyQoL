using ToastyQoL.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using System.Collections.Generic;
using ToastyQoL.Content.UI.Pages;

namespace ToastyQoL.Content.UI.UIManagers
{
    public static partial class UIManagerAutoloader
    {
        public const string MiscUIName = "MiscManager";

        public static void InitializeMisc()
        {
            List<PageUIElement> uIElements = new()
            {
                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/gravestone", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/gravestoneGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Gravestones",
                () => "Enable Gravestones dropping",
                1f,
                () => { Toggles.GravestonesEnabled = !Toggles.GravestonesEnabled; },
                typeof(Toggles).GetField("GravestonesEnabled", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/lightHack", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/lightHackGlow", AssetRequestMode.ImmediateLoad).Value,
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

                    TogglesUIManager.QueueMessage(text, Color.LightSkyBlue);
                }),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/shroom", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/shroomGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Shrooms Damage",
                () => "Toggles the bonus damage given by the Odd Mushroom family",
                3f,
                () => { Toggles.ShroomsExtraDamage = !Toggles.ShroomsExtraDamage; },
                typeof(Toggles).GetField("ShroomsExtraDamage", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/shroom", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/shroomGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Full Shrooms",
                () => "Toggles a custom odd mushroom effect that perfectly copies]\n[c/ffcc44:almost everything]",
                4f,
                () => { Toggles.ProperShrooms = !Toggles.ProperShrooms; },
                typeof(Toggles).GetField("ProperShrooms", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/shroom", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/shroomGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Full Shrooms RGB",
                () => "Toggles making proper shrooms have the RGB shader effect",
                5f,
                () => { Toggles.ShroomShader = !Toggles.ShroomShader; },
                typeof(Toggles).GetField("ShroomShader", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/time", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/timeGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle MNL Indicator",
                () => "Shows a chat message informing you how close you]\n[c/ffcc44:were to a bosses MNL according to nohit rules",
                6f,
                () =>
                {
                    Toggles.MNLIndicator = !Toggles.MNLIndicator;

                    if (!Toggles.MNLIndicator && Toggles.SassMode)
                        Toggles.SassMode = false;
                },
                typeof(Toggles).GetField("MNLIndicator", ToastyQoLUtils.UniversalBindingFlags)),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/sass", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/Powers/sassGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle Sass Mode",
                () => "Shows a chat message when you die that]\n[c/ffcc44:insults you",
                7f,
                () =>
                { 
                    Toggles.SassMode = !Toggles.SassMode;
                    if (!Toggles.SassMode)
                        SoundEngine.PlaySound(new SoundStyle("ToastyQoL/Assets/Sounds/Custom/babyLaugh"), Main.LocalPlayer.Center);
                },
                typeof(Toggles).GetField("SassMode", ToastyQoLUtils.UniversalBindingFlags),
                new ToggleBlockInformation(() => Toggles.MNLIndicator, "Enable the MNL Indicator to toggle")),

                new PageUIElement(ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/dps", AssetRequestMode.ImmediateLoad).Value,
                ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/dpsGlow", AssetRequestMode.ImmediateLoad).Value,
                () => "Toggle DPS Stats",
                () => "Shows a chat message that tells you the average dps you had]\n[c/ffcc44:on a boss",
                8f,
                () => { Toggles.BossDPS = !Toggles.BossDPS; },
                typeof(Toggles).GetField("BossDPS", ToastyQoLUtils.UniversalBindingFlags)),
            };

            TogglesPage uIManager = new(uIElements, MiscUIName, "Misc Toggles", ModContent.Request<Texture2D>("ToastyQoL/Content/UI/Textures/settingsUIIcon", AssetRequestMode.ImmediateLoad).Value, 5f);
            uIManager.TryRegister();
        }   
    }
}
