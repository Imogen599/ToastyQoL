using ToastyQoL.Content.UI;
using ToastyQoL.Content.UI.UIManagers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static ToastyQoL.Core.Systems.TieringSystem;
using ToastyQoL.Content.UI.Pages;
using ToastyQoL.Content.UI.SingleElements;
using ToastyQoL.Content.UI.BossUI;
using ToastyQoL.Core;
using ToastyQoL.Core.Systems;
using ToastyQoL.Core.Systems.MNLSystems.Sets;
using ToastyQoL.Core.Systems.MNLSystems;
using ToastyQoL.Content.UI.PotionUI;

namespace ToastyQoL
{
    public partial class ToastyQoL
    {
        public static readonly List<string> CallCommands = new()
        {
            "AddNewBossLockInformation",
            "AddNewUIToggleToRegisteredPage",
            "AddSingleActionElementToWheel",
            "AddNewEmptyPageUI",
            "CheckIfPageIsRegistered",
            "CheckIfSingleActionElementIsRegistered",
            "AddBossToggle",
            "GetToggleStatus",
            "AddShroomsDrawMethod",
            "AddMNLSet",
            "AddPotionMod",
            "AddPotionElementToMod",
            "CheckIfPotionModIsRegistered",
            "AddSassQuoteLose",
            "AddSassQuoteWin",
            "AddBossSpecificSassQuote"
        };

        public static readonly List<string> UIManangerStrings = new()
        {
            UIManagerAutoloader.LocksUIName,
            UIManagerAutoloader.MiscUIName,
            UIManagerAutoloader.PowerUIName,
            UIManagerAutoloader.WorldUIName
        };

        public override object Call(params object[] args)
        {
            if (args == null)
                return null;

            if (args[0].GetType() != typeof(string))
                throw new Exception("Error: Argument 1 must be a string.");

            if (CallCommands.Contains(args[0]))
            {
                int commandToUse = CallCommands.IndexOf((string)args[0]);

                switch (commandToUse)
                {
                    case 0:
                        AddNewBossLockInformation(args);
                        break;
                    case 1:
                        AddNewUIToggleToRegisteredPage(args);
                        break;
                    case 2:
                        AddSingleActionElementToWheel(args);
                        break;
                    case 3:
                        AddNewEmptyPageUI(args);
                        break;
                    case 4:
                        return CheckIfPageIsRegistered(args);
                    case 5:
                        return CheckIfSingleActionElementIsRegistered(args);
                    case 6:
                        AddBossToggle(args);
                        break;
                    case 7:
                        return GetToggleStatus(args);
                    case 8:
                        AddShroomsDrawFunc(args);
                        break;
                    case 9:
                        AddMNLSet(args);
                        break;
                    case 10:
                        AddPotionMod(args);
                        break;
                    case 11:
                        AddPotionElementToMod(args);
                        break;
                    case 12:
                        return CheckIfPotionModIsRegistered(args);
                    case 13:
                        AddSassQuoteLose(args);
                        break;
                    case 14:
                        AddSassQuoteWin(args);
                        break;
                    case 15:
                        AddBossSpecificSassQuote(args);
                        break;
                }
            }
            return null;
        }

        private static void AddNewBossLockInformation(object[] args)
        {
            if (args.Length < 4)
                throw new Exception("Not enough arguments provided, 4 is expected.");

            else if (args[1].GetType() != typeof(Func<bool>))
                throw new ArgumentException("Argument 2 must be a Func<bool>.");

            else if (args[2].GetType() != typeof(string))
                throw new ArgumentException("Argument 3 must be a string.");

            else if (args[3].GetType() != typeof(List<int>))
                throw new ArgumentException("Argument 3 must be an int list.");

            else if (args[4].GetType() != typeof(bool))
                throw new ArgumentException("Argument 4 must be a bool.");

            BossLockInformation bossLockInformation = new((Func<bool>)args[1], (string)args[2], (List<int>)args[3]);

            if ((bool)args[4])
                PotionsTieringInformation.Add(bossLockInformation);
            else
                ItemsTieringInformation.Add(bossLockInformation);
        }

        private static void AddNewUIToggleToRegisteredPage(object[] args)
        {
            if (args.Length < 10)
                throw new Exception("Not enough arguments provided, 10 or 12 exactly is required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 2 must be a string.");

            if (!TogglesPage.UIManagers.ContainsKey((string)args[1]))
                throw new Exception("Error: Invalid PageUI name provided. Consider checking whether it is registered before calling this.");

            else if (args[2].GetType() != typeof(Texture2D))
                throw new ArgumentException("Argument 3 must be a Texture2D.");

            else if (args[3].GetType() != typeof(Texture2D))
                throw new ArgumentException("Argument 4 must be a Texture2D.");

            else if (args[4].GetType() != typeof(Func<string>))
                throw new ArgumentException("Argument 5 must be a Func<string>.");

            else if (args[5].GetType() != typeof(Func<string>))
                throw new ArgumentException("Argument 6 must be a Func<string>.");

            else if (args[6].GetType() != typeof(float))
                throw new ArgumentException("Argument 7 must be a float.");

            else if (args[7].GetType() != typeof(Action))
                throw new ArgumentException("Argument 8 must be an Action");

            else if (args[8] is not FieldInfo _ && args[8].GetType() != typeof(string))
                throw new ArgumentException("Argument 9 must be a FieldInfo or string");

            ToggleBlockInformation? toggleBlockInformation = null;

            if (args.Length > 10)
            {
                if (args.Length < 12)
                    throw new Exception("Not enough arguments provided, 10 or 12 exactly is required");

                if (args[9].GetType() != typeof(bool))
                    throw new ArgumentException("Argument 10 must be a bool.");

                else if (args[10].GetType() != typeof(Func<bool>))
                    throw new ArgumentException("Argument 11 must be a Func<bool>.");

                if (args[11].GetType() != typeof(string))
                    throw new ArgumentException("Argument 12 must be a string.");

                if ((bool)args[9])
                    toggleBlockInformation = new((Func<bool>)args[10], (string)args[11]);
            }

            FieldInfo info;
            if (args[8].GetType() == typeof(string))
                info = null;
            else
                info = (FieldInfo)args[8];

            PageUIElement pageUIElement = new((Texture2D)args[2], (Texture2D)args[3], (Func<string>)args[4], (Func<string>)args[5],
                (float)args[6], (Action)args[7], info, toggleBlockInformation);

            TogglesPage.GetPageFromString((string)args[1]).UIElements.Add(pageUIElement);
        }

        private static void AddSingleActionElementToWheel(object[] args)
        {
            if (args.Length < 6)
                throw new Exception("Not enough arguments provided, 6 is expected.");

            else if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 2 must be a string.");

            else if (args[2].GetType() != typeof(Texture2D))
                throw new ArgumentException("Argument 3 must be a Texture2D.");

            else if (args[3].GetType() != typeof(string))
                throw new ArgumentException("Argument 4 must be a string.");

            else if (args[4] is not Action)
                throw new ArgumentException("Argument 5 must be an Action.");

            else if (args[5].GetType() != typeof(bool))
                throw new ArgumentException("Argument 6 must be a float.");

            else if (args[6] is not Action<SpriteBatch>)
                throw new ArgumentException("Argument 7 must be an Action with single parameter of type SpriteBatch.");

            new SingleActionElement((string)args[1], (Texture2D)args[2], (string)args[3], (Action)args[4], (float)args[5], 
                (Action<SpriteBatch>)args[6]).TryRegister();
        }

        private static void AddNewEmptyPageUI(object[] args)
        {
            if (args.Length < 8)
                throw new Exception("Not enough arguments provided, 8 is expected.");

            else if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 2 must be a string.");

            else if (args[2].GetType() != typeof(string))
                throw new ArgumentException("Argument 3 must be a string.");

            else if (args[3].GetType() != typeof(Texture2D))
                throw new ArgumentException("Argument 4 must be a Texture2D.");

            else if (args[4].GetType() != typeof(float))
                throw new ArgumentException("Argument 5 must be a float.");

            else if (args[5].GetType() != typeof(bool))
                throw new ArgumentException("Argument 6 must be a bool.");

            new TogglesPage(new List<PageUIElement>(), (string)args[1], (string)args[2], (Texture2D)args[3], (float)args[4], (bool)args[5]).TryRegister();
        }

        private static bool CheckIfPageIsRegistered(object[] args)
        {
            if (args[1].GetType() != typeof(string))
                throw new Exception("Argument 2 must be a string");

            return TogglesPage.UIManagers.ContainsKey((string)args[1]);
        }

        private static bool CheckIfSingleActionElementIsRegistered(object[] args)
        {
            if (args[1].GetType() != typeof(string))
                throw new Exception("Argument 2 must be a string");

            return SingleActionElement.UISingleElements.ContainsKey((string)args[1]);
        }

        private static void AddBossToggle(object[] args)
        {
            if (args.Length < 6)
                throw new Exception("Not enough arguments provided, 6 is required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 2 must be a string.");

            if (args[2].GetType() != typeof(string))
                throw new ArgumentException("Argument 3 must be a string.");

            if (args[3] is not FieldInfo)
                throw new ArgumentException("Argument 4 must be a FieldInfo.");

            if (args[4].GetType() != typeof(float))
                throw new ArgumentException("Argument 5 must be a float.");

            if (args[5].GetType() != typeof(float))
                throw new ArgumentException("Argument 6 must be a float.");

            new BossToggleElement((string)args[1], (string)args[2], (FieldInfo)args[3], (float)args[4], (float)args[5]).Register();
        }

        private static bool GetToggleStatus(object[] args)
        {
            if (args.Length < 2)
                throw new Exception("Not enough arguments provided, 2 is required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 2 must be a string.");

            FieldInfo toggle = typeof(Toggles).GetField((string)args[1]);
            if (toggle != null && toggle.GetType() == typeof(bool))
                return (bool)toggle.GetValue(null);
            return false;
        }

        private static void AddShroomsDrawFunc(object[] args)
        {
            if (args.Length < 2)
                throw new Exception("Not enough arguments provided, 2 are required");

            if (args[1] is not Action<SpriteBatch>)
                throw new ArgumentException("Argument 1 must be an Action with single parameter of type SpriteBatch.");

            ShroomsRenderTargetManager.ExtraDrawMethods.Add((Action<SpriteBatch>)args[1]);
        }

        private static void AddMNLSet(object[] args)
        {
            if (args.Length < 3)
                throw new Exception("Not enough arguments provided, 3 are required");

            if (args[1].GetType() != typeof(Dictionary<int, int>))
                throw new ArgumentException("Argument 1 must be a Dictonary<int, int>.");
            if (args[2].GetType() != typeof(Func<float>))
                throw new ArgumentException("Argument 2 must be a Func<float>.");

            MNLSet set = new((Dictionary<int, int>)args[1], (Func<float>)args[2]);
            MNLsHandler.RegisterSet(set);
        }

        private static void AddPotionMod(object[] args)
        {
            if (args.Length < 3)
                throw new Exception("Not enough arguments provided, 3 are required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 1 must be a string.");
            if (args[2].GetType() != typeof(string))
                throw new ArgumentException("Argument 2 must be a string.");

            PotionMod mod = new((string)args[1], (string)args[2]);
            PotionUIManager.RegisterPotionMod(mod);
        }

        private static void AddPotionElementToMod(object[] args)
        {
            if (args.Length < 9)
                throw new Exception("Not enough arguments provided, 9 are required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 1 must be a string.");
            if (args[2].GetType() != typeof(string))
                throw new ArgumentException("Argument 2 must be a string.");
            if (args[3].GetType() != typeof(string))
                throw new ArgumentException("Argument 3 must be a string.");
            if (args[4].GetType() != typeof(string))
                throw new ArgumentException("Argument 4 must be a string.");
            if (args[5].GetType() != typeof(int))
                throw new ArgumentException("Argument 5 must be a int.");
            if (args[6].GetType() != typeof(Func<bool>))
                throw new ArgumentException("Argument 6 must be a Func<bool>.");
            if (args[7].GetType() != typeof(float))
                throw new ArgumentException("Argument 7 must be a float.");
            if (args[8].GetType() != typeof(float))
                throw new ArgumentException("Argument 8 must be a float.");

            PotionElement potionElement = new((string)args[2], (string)args[3], (string)args[4], (int)args[5], (Func<bool>)args[6], (float)args[7], (float)args[8]);
            PotionUIManager.AddElementToModList((string)args[1], potionElement);
        }

        private static bool CheckIfPotionModIsRegistered(object[] args)
        {
            if (args.Length < 2)
                throw new Exception("Not enough arguments provided, 2 are required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 1 must be a string.");

            return PotionUIManager.ModIsRegistered((string)args[1]);
        }

        private static void AddSassQuoteLose(object[] args)
        {
            if (args.Length < 2)
                throw new Exception("Not enough arguments provided, 2 are required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 1 must be a string.");

             SassModeSystem.GenericSassQuotesLose.Add((string)args[1]);
        }

        private static void AddSassQuoteWin(object[] args)
        {
            if (args.Length < 2)
                throw new Exception("Not enough arguments provided, 2 are required");

            if (args[1].GetType() != typeof(string))
                throw new ArgumentException("Argument 1 must be a string.");

            SassModeSystem.GenericSassQuotesWin.Add((string)args[1]);
        }

        private static void AddBossSpecificSassQuote(object[] args)
        {
            if (args.Length < 3)
                throw new Exception("Not enough arguments provided, 3 are required");

            if (args[1].GetType() != typeof(int))
                throw new ArgumentException("Argument 1 must be an int.");
            if (args[2].GetType() != typeof(List<string>))
                throw new ArgumentException("Argument 1 must be a List<string>.");

            if (SassModeSystem.SassSpecificBossQuotes.TryGetValue((int)args[1], out var value))
                value.Add((string)args[2]);
            else
                SassModeSystem.SassSpecificBossQuotes.Add((int)args[1], (List<string>)args[2]);
        }
    }
}
