using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalSettings.Patches;

[HarmonyPatch(typeof(QuickMenuManager))]
internal class QuickMenuManager_Patches
{

    [HarmonyPostfix]
    [HarmonyPatch(nameof(QuickMenuManager.Start))]
    static void Start_Postfix(QuickMenuManager __instance)
    {
        var settingsContainer = __instance.menuContainer.transform.Find("SettingsPanel");
        var menu = GameObject.Instantiate(Assets.ModSettingsView, settingsContainer);
        menu.InGame = true;
        menu.transform.SetSiblingIndex(menu.transform.GetSiblingIndex() - 2);
        menu.gameObject.transform.Find("Mod Settings Panel").gameObject.SetActive(false);
    }
}
