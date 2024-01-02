using System;
using System.Collections.Generic;
using System.Text;
using LethalSettings.UI;
using UnityEngine;

namespace LethalSettings.Patches;

[HarmonyPatch(typeof(MenuManager))]
internal class MenuManager_Patches
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(MenuManager.Start))]
    static void Start_Postfix(MenuManager __instance)
    {
        var settingsContainer = __instance.transform.parent.Find("MenuContainer/SettingsPanel");
        var menu = GameObject.Instantiate(Assets.ModSettingsView, settingsContainer);
        menu.transform.SetSiblingIndex(menu.transform.GetSiblingIndex() - 2);

        // tell it that it isn't
        menu.GetComponent<ModMenu>().inGame = false;
    }
}
