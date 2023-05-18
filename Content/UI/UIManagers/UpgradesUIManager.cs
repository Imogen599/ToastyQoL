using CalamityMod;
using CalNohitQoL.Core;
using CalNohitQoL.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalNohitQoL.Content.UI.UIManagers
{
    public class UpgradesUIManager : BaseTogglesUIManager
    {
        public const string UIName = "UpgradesManager";

        public override string Name => UIName;

        public override bool UseSmallerBackground => false;

        public override void Initialize()
        {
            var info = new ToggleBlockInformation(() => !Toggles.AutomateProgressionUpgrades, "Disable Automated System to toggle");
            UIElements = new()
            {
                new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/Auto", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/AutoGlow", AssetRequestMode.ImmediateLoad).Value,
                    () => "Toggle Automated System",
                    () => "Automated System enables every upgrade]\n[c/ffcc44:up to your latest boss in progression killed",
                    1f,
                    () => { Toggles.AutomateProgressionUpgrades = !Toggles.AutomateProgressionUpgrades; },
                    typeof(Toggles).GetField("AutomateProgressionUpgrades", CalNohitQoLUtils.UniversalBindingFlags)),

                 new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/BloodOrange", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/BloodOrangeGlow", AssetRequestMode.ImmediateLoad).Value,
                    () => "Toggle HP Upgrades",
                    () => 
                    {
                        string nextUpgrade;
                        if (Main.LocalPlayer.Calamity().dFruit)
                            nextUpgrade = "None";
                        else if (Main.LocalPlayer.Calamity().eBerry)
                            nextUpgrade = "Dragonfruit";
                        else if (Main.LocalPlayer.Calamity().mFruit)
                            nextUpgrade = "Elderberry";
                        else if (Main.LocalPlayer.Calamity().bOrange)
                            nextUpgrade = "Miracle Fruit";
                        else
                            nextUpgrade = "BloodOrange";

                        return $"Next Upgrade: {nextUpgrade}";
                    },
                    2f,
                    () => 
                    {
                        // if you have max upgrades, turn them all off.
                        if (Main.LocalPlayer.Calamity().dFruit)
                        {
                            Main.LocalPlayer.Calamity().bOrange = false;
                            Main.LocalPlayer.Calamity().mFruit = false;
                            Main.LocalPlayer.Calamity().eBerry = false;
                            Main.LocalPlayer.Calamity().dFruit = false;
                        }
                        else if (Main.LocalPlayer.Calamity().eBerry)
                        {
                            Main.LocalPlayer.Calamity().bOrange = true;
                            Main.LocalPlayer.Calamity().mFruit = true;
                            Main.LocalPlayer.Calamity().eBerry = true;
                            Main.LocalPlayer.Calamity().dFruit = true;
                        }
                        else if (Main.LocalPlayer.Calamity().mFruit)
                        {
                            Main.LocalPlayer.Calamity().bOrange = true;
                            Main.LocalPlayer.Calamity().mFruit = true;
                            Main.LocalPlayer.Calamity().eBerry = true;
                            Main.LocalPlayer.Calamity().dFruit = false;
                        }
                        else if (Main.LocalPlayer.Calamity().bOrange)
                        {
                            Main.LocalPlayer.Calamity().bOrange = true;
                            Main.LocalPlayer.Calamity().mFruit = true;
                            Main.LocalPlayer.Calamity().eBerry = false;
                            Main.LocalPlayer.Calamity().dFruit = false;
                        }
                        else
                        {
                            Main.LocalPlayer.Calamity().bOrange = true;
                            Main.LocalPlayer.Calamity().mFruit = false;
                            Main.LocalPlayer.Calamity().eBerry = false;
                            Main.LocalPlayer.Calamity().dFruit = false;
                        }
                    },
                    blockInformation: info),

                 new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/CometShard", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/CometShardGlow", AssetRequestMode.ImmediateLoad).Value,
                    () => "Toggle Mana Upgrades",
                    () =>
                    {
                        string nextUpgrade;
                        if (Main.LocalPlayer.Calamity().pHeart)
                            nextUpgrade = "None";
                        else if (Main.LocalPlayer.Calamity().eCore)
                            nextUpgrade = "Phantom Heart";
                        else if (Main.LocalPlayer.Calamity().cShard)
                            nextUpgrade = "Ethereal Core";
                        else
                            nextUpgrade = "Comet Shard";

                        return $"Next Upgrade: {nextUpgrade}";
                    },
                    3f,
                    () =>
                    {
                        if (Main.LocalPlayer.Calamity().pHeart)
                        {
                            Main.LocalPlayer.Calamity().cShard = false;
                            Main.LocalPlayer.Calamity().eCore = false;
                            Main.LocalPlayer.Calamity().pHeart = false;
                        }
                        else if (Main.LocalPlayer.Calamity().eCore)
                        {
                            Main.LocalPlayer.Calamity().cShard = true;
                            Main.LocalPlayer.Calamity().eCore = true;
                            Main.LocalPlayer.Calamity().pHeart = true;
                        }
                        else if (Main.LocalPlayer.Calamity().cShard)
                        {
                            Main.LocalPlayer.Calamity().cShard = true;
                            Main.LocalPlayer.Calamity().eCore = true;
                            Main.LocalPlayer.Calamity().pHeart = false;
                        }
                        else
                        {
                            Main.LocalPlayer.Calamity().cShard = true;
                            Main.LocalPlayer.Calamity().eCore = false;
                            Main.LocalPlayer.Calamity().pHeart = false;
                        }
                    },
                    blockInformation: info),

                 new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/MushroomPlasmaRoot", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/MushroomPlasmaRootGlow", AssetRequestMode.ImmediateLoad).Value,
                    () => "Toggle Rage Upgrades",
                    () =>
                    {
                        string nextUpgrade;
                        if (Main.LocalPlayer.Calamity().rageBoostThree)
                            nextUpgrade = "None";
                        else if (Main.LocalPlayer.Calamity().rageBoostTwo)
                            nextUpgrade = "Red Lightning Container";
                        else if (Main.LocalPlayer.Calamity().rageBoostOne)
                            nextUpgrade = "Infernal Blood";
                        else
                            nextUpgrade = "Mushroom PlasmaRoot";

                        return $"Next Upgrade: {nextUpgrade}";
                    },
                    4f,
                    () =>
                    {
                        if (Main.LocalPlayer.Calamity().rageBoostThree)
                        {
                            Main.LocalPlayer.Calamity().rageBoostOne = false;
                            Main.LocalPlayer.Calamity().rageBoostTwo = false;
                            Main.LocalPlayer.Calamity().rageBoostThree = false;
                        }
                        else if (Main.LocalPlayer.Calamity().rageBoostTwo)
                        {
                            Main.LocalPlayer.Calamity().rageBoostOne = true;
                            Main.LocalPlayer.Calamity().rageBoostTwo = true;
                            Main.LocalPlayer.Calamity().rageBoostThree = true;
                        }
                        else if (Main.LocalPlayer.Calamity().rageBoostOne)
                        {
                            Main.LocalPlayer.Calamity().rageBoostOne = true;
                            Main.LocalPlayer.Calamity().rageBoostTwo = true;
                            Main.LocalPlayer.Calamity().rageBoostThree = false;
                        }
                        else
                        {
                            Main.LocalPlayer.Calamity().rageBoostOne = true;
                            Main.LocalPlayer.Calamity().rageBoostTwo = false;
                            Main.LocalPlayer.Calamity().rageBoostThree = false;
                        }
                    },
                    blockInformation: info),

                 new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/ElectrolyteGelPack", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/ElectrolyteGelPackGlow", AssetRequestMode.ImmediateLoad).Value,
                    () => "Toggle Adren Upgrades",
                    () =>
                    {
                        string nextUpgrade;
                        if (Main.LocalPlayer.Calamity().adrenalineBoostThree)
                            nextUpgrade = "None";
                        else if (Main.LocalPlayer.Calamity().adrenalineBoostTwo)
                            nextUpgrade = "Ectoheart";
                        else if (Main.LocalPlayer.Calamity().adrenalineBoostOne)
                            nextUpgrade = "Starlight Fuel Cell";
                        else
                            nextUpgrade = "Electrolyte Gel Pack";

                        return $"Next Upgrade: {nextUpgrade}";
                    },
                    5f,
                    () =>
                    {
                        if (Main.LocalPlayer.Calamity().adrenalineBoostThree)
                        {
                            Main.LocalPlayer.Calamity().adrenalineBoostOne = false;
                            Main.LocalPlayer.Calamity().adrenalineBoostTwo = false;
                            Main.LocalPlayer.Calamity().adrenalineBoostThree = false;
                        }
                        else if (Main.LocalPlayer.Calamity().adrenalineBoostTwo)
                        {
                            Main.LocalPlayer.Calamity().adrenalineBoostOne = true;
                            Main.LocalPlayer.Calamity().adrenalineBoostTwo = true;
                            Main.LocalPlayer.Calamity().adrenalineBoostThree = true;
                        }
                        else if (Main.LocalPlayer.Calamity().adrenalineBoostOne)
                        {
                            Main.LocalPlayer.Calamity().adrenalineBoostOne = true;
                            Main.LocalPlayer.Calamity().adrenalineBoostTwo = true;
                            Main.LocalPlayer.Calamity().adrenalineBoostThree = false;
                        }
                        else
                        {
                            Main.LocalPlayer.Calamity().adrenalineBoostOne = true;
                            Main.LocalPlayer.Calamity().adrenalineBoostTwo = false;
                            Main.LocalPlayer.Calamity().adrenalineBoostThree = false;
                        }
                    },
                    blockInformation: info),

                 new PageUIElement(ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/DemonHeart", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("CalNohitQoL/Content/UI/Textures/DemonHeartGlow", AssetRequestMode.ImmediateLoad).Value,
                    () => "Toggle Accessory Upgrades",
                    () =>
                    {
                        string nextUpgrade;
                        if (Main.LocalPlayer.Calamity().extraAccessoryML)
                            nextUpgrade = "None";
                        else if (Main.LocalPlayer.extraAccessory)
                            nextUpgrade = "Celestial Onion";
                        else
                            nextUpgrade = "Demon Heart";

                        return $"Next Upgrade: {nextUpgrade}";
                    },
                    6f,
                    () =>
                    {
                        if (Main.LocalPlayer.Calamity().extraAccessoryML)
                        {
                            Main.LocalPlayer.extraAccessory = false;
                            Main.LocalPlayer.Calamity().extraAccessoryML = false;
                        }
                        else if (Main.LocalPlayer.extraAccessory)
                        {
                            Main.LocalPlayer.extraAccessory = true;
                            Main.LocalPlayer.Calamity().extraAccessoryML = true;
                        }
                        else
                        {
                            Main.LocalPlayer.extraAccessory = true;
                            Main.LocalPlayer.Calamity().extraAccessoryML = false;
                        }
                    },
                    blockInformation: info),
            };
        }      
    }
}