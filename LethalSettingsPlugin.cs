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
            Description = "A centralized place for configuring mods from in-game",
            MenuComponents = new[]
            {
                new ButtonComponent
                {
                    Text = "This is a test button!",
                    OnClick = (self) => Logger.LogInfo("You clicked the test button!")
                }
            }
        });


        ModMenu.RegisterMod(new ModMenu.ModSettingsConfig
        {
            Name = "Example Mod",
            Id = "com.willis.lc.examplemod",
            Description = "This is an example mod registration showing how easy it can be to give your mod configuration a vanilla-like feel!",
            MenuComponents =
            [
                new ButtonComponent
                {
                    Text = "This is yet another test button!",
                    OnClick = (self) => Logger.LogInfo("You clicked the second test button!")
                },
                new HorizontalComponent
                {
                    Children =
                    [
                        new SliderComponent
                        {
                            DefaultValue = 30,
                            MinValue = 10,
                            MaxValue = 50,
                            Text = "Example Slider",
                            OnValueChange = (self, value) => Logger.LogInfo($"New value: {value}")
                        },
                        new ToggleComponent
                        {
                            Text = "Toggle me!",
                            OnValueChanged = (self, value) => Logger.LogInfo($"New value: {value}")
                        }
                    ]
                },
                new LabelComponent
                {
                    Text = "Hello, World!"
                }
            ]
        });
    }
}
