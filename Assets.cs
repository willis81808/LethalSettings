using LethalSettings.UI;
using LethalSettings.UI.Components;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalSettings;

internal static class Assets
{
    internal static ModMenu ModSettingsView { get; private set; }

    internal static GameObject VerticalWrapper { get; private set; }
    internal static GameObject HorizontalWrapper { get; private set; }

    internal static LabelComponentObject LabelPrefab { get; private set; }
    internal static ButtonComponentObject ButtonPrefab { get; private set; }
    internal static SliderComponentObject SliderPrefab { get; private set; }
    internal static ToggleComponentObject TogglePrefab { get; private set; }

    internal static void LoadAssets()
    {
        var bundle = AssetBundle.LoadFromMemory(Properties.Resources.settings_assets);
        ModSettingsView = bundle.LoadAsset<GameObject>("Mod Settings Container").GetComponent<ModMenu>();
        VerticalWrapper = bundle.LoadAsset<GameObject>("Vertical Wrapper");
        HorizontalWrapper = bundle.LoadAsset<GameObject>("Horizontal Wrapper");
        LabelPrefab = bundle.LoadAsset<GameObject>("Label").GetComponent<LabelComponentObject>();
        ButtonPrefab = bundle.LoadAsset<GameObject>("Button").GetComponent<ButtonComponentObject>();
        SliderPrefab = bundle.LoadAsset<GameObject>("Slider").GetComponent<SliderComponentObject>();
        TogglePrefab = bundle.LoadAsset<GameObject>("Toggle").GetComponent<ToggleComponentObject>();
    }
}

