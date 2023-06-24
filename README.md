# ToastyQoL

## In-game Description

A QoL mod with a focus on more cheaty features.

### UI:

- Adds a UI wheel openable through a keybind or clicking the cheat indicator icon next to the map.
- Click any of the icons to toggle its effect/open up a menu that contains further toggles.
- Examples of these toggles include: Powers (such as godmode), Light Hack, Enemy Spawns, Changing time,
  a system that sends a chat message informing you if you were under Minimum Nohit Length for a boss fight,
  and by how much.
- You can also toggle the death state of any boss.
- There is a potion UI menu that allows you to give yourself any potion buff.
- Adds an Summoner Minion Slot UI icon left of the HP bar that gives you information about your current summon slots.

### Items:

- Adds boss spawners that allows you to spawn up to 10 at once.
- Adds Nostalgic Shroom: The OG Shroom, nothing more. Has a list of victors in its tooltip.
- Adds Double Shroom: doubles the effect of Nostalgic Shroom.
- Both of these are sold by the Merchant.
- Adds a Toaster Pet that occasionally burns toast.
- Adds "The Percenter", an item that drains 10% max hp from the nearest enemy. Useful for getting bosses to a certain phase.

# Cross-Mod Support:

The Mod has a lot of support for Mod.Call() to allow other mods to add their own toggles etc. A list of each one can be found below, and a mod that showcases using every command can be found [here.](https://github.com/toasty599/ToastyQoLCalamity)
The first parameter should *always* be the command name, and will be included in each of the following.

## Adding new Boss Lock Information for a set of items:
```c#
ModInstance.Call("AddNewBossLockInformation", Func<bool> pastTierCheck, string bossName, List<int> assosiatedItemTypes, bool forPotions);
```
``pastTierCheck`` - A function that should check if the assosiated items should be locked.

``bossName`` - The name of the boss these are locked behind, doesn't necessarily need to be a boss.

``assosiatedItemTypes`` - A list of Item Types for the items that should be affected by the lock.

``forPotions`` - Whether the items are potions, this is due to potions having their own toggle seperate from other items.


## Adding a new Toggle to an existing Page:
```c#
ModInstance.Call("AddNewUIToggleToRegisteredPage", string uiPageName, Texture2D texture, Texture2D glowTexture, Func<string> descriptionText, Func<string> hoverText, float layer, Action onClickAction, FieldInfo assosiatedField, bool useToggleBlock = false, Func<bool> canToggleFunc = null, string extraHoverText = null);
```
``uiPageName`` - The internal name of the page to add this toggle to. This will throw a null error if the page does not exist, so you should ensure it exists by calling the ``CheckIfPageIsRegistered`` command prior to this.

``texture`` - The texture of the toggle.

``glowTexture`` - The texture of the toggle when it is being hovered over by the players mouse.

``descriptionText`` - A function that returns a string to be used as the text adjacent to the icon on the page.

``hoverText`` - A function that returns a string to be used as the mouse text that appears when the toggle is being hovered over by it.

``layer`` - This is used to order the toggles on the page in an ascending order. All base pages have toggles increasing by ``1`` for each one, starting at 0.

``onClickAction`` - Custom code to be ran when the toggle is clicked by the player.

``assosiatedField`` - A FieldInfo that gets its value toggled automatically when the toggle is clicked by the player. Does nothing if it is not a bool.

``useToggleBlock`` - Whether to block clicking the toggle based on a provided function.

``canToggleFunc`` - A function that should determine whether the toggle can be clicked.

``extraHoverText`` - Additional mouse text on hover when the toggle is blocked, to inform the player why it is blocked.

**NOTE:** If an ``assosiatedField`` is not needed (if the toggle is single action for example), it should be replaced by a ``string``, which can be whatever. This is a workaround to it not liking passing ``null`` through if an ``assosiatedField`` was not needed.
Also, if ``useToggleBlock`` is false, you should *not* provide either of the following optional parameters, ``canToggleFunc`` or ``extraHoverText``.


## Checking if a Page is registered:
```c#
ModInstance.Call("CheckIfPageIsRegistered", string pageName);
```
``pageName`` - The internal name of the Page.


## Adding a Boss Death Toggle:
```c#
ModInstance.Call("AddBossToggle", string texturePath, string nameSingular, FieldInfo downedBool, float weight, float scale);
```
``texturePath`` - The file path (mod name included) of the texture. Note that the hover over texture is automatically loaded from ``texturePath + "Glow"``, so ensure that file exists too.

``nameSingular`` - The name of the boss in singular form (The Twin instead of The Twin's for example).

``downedBool`` - The FieldInfo of the bool being used to store the death status of the boss.

``weight`` - Used to determine the position in the order that the current boss should be at. Vanilla bosses go from 1f (King Slime) to 18f (Moonlord).

``scale`` - The scale to apply to the texture. 1 is default.

**NOTE:** This should be used in an if statement that if true, adds toggles to the page to avoid attempting to add toggles to a non existant page.

## Getting the value of a bool toggle from Core/Toggles.cs:
```c#
ModInstance.Call("GetToggleStatus", string toggle);
```
``toggle`` - The name of the field (for example, "PotionTooltips").

## Adding custom drawcode to the shrooms drawing system:
```c#
ModInstance.Call("AddShroomsDrawMethod", Action<SpriteBatch> action);
```
``action`` - A custom action that should be used to run the drawcode. The spritebatch is already begun, and expected to stil be begun after this has been called.

### Adding a custom MNL Set:
```c#
ModInstance.Call("AddMNLSet", Dictionary<int, int> assosiatedIDsAndFightLengths, Func<float> weight);
```
``assosiatedIDsAndFightLengths`` - A Dictonary that should be structured as <NPC Type, fightTimeInFrames>, used to store the minimum fight time for the NPC that the key specifies.

``weight`` - A function to determine the weight of the current MNL set. The set with the highest weight is used, so this should be used in a manner that gives this set a high weight when it should be used, and a low one when not.

## Adding a Mod to the Potion UI:
```c#
ModInstance.Call("AddPotionMod", string modName, string uiIconPath);
```
``modName`` - The display name for this potion mod.

``uiIconPath`` - The file path (mod name included) of the texture. Note that the hover over texture is automatically loaded from ``texturePath + "Glow"``, so ensure that file exists too.

**NOTE:** The PotionUI works by assigning each potion to a mod, by default only vanilla exists. To properly add potions, you add a Potion Mod, and add your potions to that mod.

## Adding Potions to the PotionUI:
```c#
ModInstance.Call("AddPotionElementToMod", string modName, string potionName, string potionDescription, string potionTexturePath, int potionBuffID, Func<bool> isAvailable, float weight, float scale);
```
``modName`` - The display name of the potion mod to add this potion to. You should ensure the mod is registered before trying to add anything to it.

``potionName`` - The display name of the potion.

``potionDescription`` - The description of the potion, typically its stats. For example, "20% chance to not consume ammo".

``potionTexturePath`` - The file path (mod name included) of the texture. Note that the hover over texture is automatically loaded from ``texturePath + "Glow"``, so ensure that file exists too.

``potionBuffID`` - The BuffID of the potion's buffs. Gotten via ``ModContent.BuffType<T>();`` for modded buffs.

``isAvailable`` - A function that should determine whether the potion is available at the players current progression.

``weight`` - Used to order the potions when sorted by progression. Vanilla ones range from 0f (Pre Boss) to 18f (Post Moonlord).

``scale`` - The scale to apply to the texture. 1 is default.

## Checking if a Potion Mod Exists:
```c#
ModInstance.Call("CheckIfPotionModIsRegistered", string modName);
```
``modName`` - The display name of the potion mod being checked.

## Adding a Sass Quote to the Lose Pool:
```c#
ModInstance.Call("AddSassQuoteLose", string quote);
```
``quote`` - The quote to add.

## Adding a Sass Quote to the Win Pool:
```c#
ModInstance.Call("AddSassQuoteWin", string quote);
```
``quote`` - The quote to add.

## Adding a Boss Specific Sass Quote:
```c#
ModInstance.Call("AddBossSpecificSassQuote", int bossID, string quote);
```
``bossID`` - The NPC Type that the quote should be added to.

``quote`` - The quote to add.
