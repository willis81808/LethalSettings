using System;
using System.Collections.Generic;
using System.Text;
using GameNetcodeStuff;
using UnityEngine;

namespace LethalSettings.Patches;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlayerController_Patches
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(PlayerControllerB.Start))]
    static void Start_Postfix(PlayerControllerB __instance)
    {
        var settingsContainer = GameObject.Find("Systems/UI/Canvas/QuickMenu/SettingsPanel").transform; // FIXME: This probably isn't the best
        var menu = GameObject.Instantiate(Assets.ModSettingsView, settingsContainer);
        menu.transform.SetSiblingIndex(menu.transform.GetSiblingIndex() - 2);
    }
}
