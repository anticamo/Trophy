using MelonLoader;
using BTD_Mod_Helper;
using Trophy;
using BTD_Mod_Helper.Api.ModOptions;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Data.TrophyStore;
using Il2CppAssets.Scripts.Data;
using System.Collections.Generic;
using System.Linq;
using BTD_Mod_Helper.Api.Enums;
using System;

[assembly: MelonInfo(typeof(Trophy.Trophy), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Trophy
{
    public class Trophy : BloonsTD6Mod
    {
        private readonly ModSettingHotkey trophiesButton = new ModSettingHotkey(KeyCode.U, HotkeyModifier.None)
        {
            displayName = "Give Trophies",
            requiresRestart = false,
            icon = VanillaSprites.TrophyIcon
        };

        private readonly ModSettingHotkey giveMonkeyMoney = new ModSettingHotkey(KeyCode.B, HotkeyModifier.None)
        {
            displayName = "Give Monkey Money",
            requiresRestart = false,
            icon = VanillaSprites.PileOMonkeyMoneyShop
        };

        private readonly ModSettingHotkey unlockAllItems = new ModSettingHotkey(KeyCode.X, HotkeyModifier.None)
        {
            displayName = "Unlock All Trophy Items",
            requiresRestart = false,
            icon = VanillaSprites.WinningTrophy
        };

        private readonly ModSettingHotkey resetMonkeyMoney = new ModSettingHotkey(KeyCode.L, HotkeyModifier.None)
        {
            displayName = "Reset Monkey Money To 0",
            requiresRestart = false,
            icon = VanillaSprites.HalfCashIcon
        };

        private readonly ModSettingDouble monkeyMoneyAmount = new ModSettingDouble(1.0)
        {
            displayName = "Monkey Money Amount",
            min = 1,
            max = 2147483646,
            slider = false
        };

        private readonly ModSettingInt trophyAmount = new ModSettingInt(1)
        {
            displayName = "Trophy Amount",
            min = 1,
            max = 2147483646,
            slider = false
        };

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Game.instance == null || GameExt.GetBtd6Player(Game.instance) == null)
            {
                return;
            }

            var player = GameExt.GetBtd6Player(Game.instance);

            if (giveMonkeyMoney.JustPressed())
            {
                if (player != null)
                {
                    GameExt.AddMonkeyMoney(Game.instance, Convert.ToInt32(monkeyMoneyAmount.GetValue()));
                    MelonLogger.Msg($"Gained {monkeyMoneyAmount.GetValue()} monkey money!");
                }
                else
                {
                    MelonLogger.Warning("either u aint loaded in or something broke \\u{1F62D}");
                }
            }

            if (resetMonkeyMoney.JustPressed())
            {
                if (player != null)
                {
                    GameExt.SetMonkeyMoney(Game.instance, 0);
                    MelonLogger.Msg("Reset monkey money to 0!");
                }
                else
                {
                    MelonLogger.Warning("either u aint loaded in or something broke \\u{1F62D}");
                }
            }

            if (trophiesButton.JustPressed())
            {
                if (player != null)
                {
                    player.GainTrophies(Convert.ToInt32(trophyAmount.GetValue()), "event", null);
                    MelonLogger.Msg($"Gained {trophyAmount.GetValue()} trophies!");
                }
                else
                {
                    MelonLogger.Warning("either u aint loaded in or something broke \\u{1F62D}");
                }
            }

            if (unlockAllItems.JustPressed())
            {
                TrophyStoreItems items = GameData.Instance.trophyStoreItems;
                List<TrophyStoreItem> itemList = items.storeItems.ToList();

                if (player != null)
                {
                    foreach (TrophyStoreItem item in itemList)
                    {
                        player.AddTrophyStoreItem(item.name);
                        MelonLogger.Msg($"Added {item.name} to your inventory!");
                    }
                }
                else
                {
                    MelonLogger.Warning("either u aint loaded in or something broke \\u{1F62D}");
                }
            }
        }
    }
}
