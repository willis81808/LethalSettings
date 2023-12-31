using LethalSettings.UI;
using LethalSettings.UI.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;

namespace LethalSettings;

[BepInPlugin(GeneratedPluginInfo.Identifier, GeneratedPluginInfo.Name, GeneratedPluginInfo.Version)]
public class LethalSettingsPlugin : BaseUnityPlugin
{
    public static LethalSettingsPlugin Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        Assets.LoadAssets();
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GeneratedPluginInfo.Identifier);

        //var test = Config.Bind<bool>("test", "test", true);
        //var CONFIDENCE = Config.Bind<float>("test", "confidence", 59);

        ModMenu.RegisterMod(new ModMenu.ModSettingsConfig
        {
            Name = GeneratedPluginInfo.Name,
            Id = GeneratedPluginInfo.Identifier,
            Version = GeneratedPluginInfo.Version,
            Description = "A centralized place for configuring mods from in-game",
        });
    }
}
