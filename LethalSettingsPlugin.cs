using LethalSettings.UI;
using LethalSettings.UI.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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

        ModMenu.RegisterMod(new ModMenu.ModSettingsConfig
        {
            Name = GeneratedPluginInfo.Name,
            Id = GeneratedPluginInfo.Identifier,
            Description = "A centralized place for configuring mods from in-game"
        });
    }
}
