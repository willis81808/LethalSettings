using System;
using System.Collections.Generic;
using System.Text;
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
        GameObject.Instantiate(Assets.ModSettingsView, settingsContainer);
    }
}
