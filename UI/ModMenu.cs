using LethalSettings.UI.Components;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalSettings.UI;

public class ModMenu : MonoBehaviour
{
    private static List<ModSettingsConfig> registeredMods = new List<ModSettingsConfig>();

    [SerializeField]
    internal Transform modListScrollView, modSettingsScrollView;

    public class ModSettingsConfig
    {
        public string Name {  get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public MenuComponent[] MenuComponents { get; set; }

        internal ButtonComponent ShowSettingsButton { get; set; }
        internal GameObject Viewport { get; set; }
    }

    void Start()
    {
        foreach (var mod in registeredMods)
        {
            // Create menu button for mod
            mod.ShowSettingsButton = new ButtonComponent
            {
                Text = mod.Name,
                OnClick = (self) => ShowModSettings(mod)
            };
            mod.ShowSettingsButton.Construct(modListScrollView.gameObject);

            // Create mod settings menu contents
            mod.Viewport = new VerticalComponent
            {
                ChildAlignment = TextAnchor.UpperLeft,
                Children = [
                    new LabelComponent { Text = "Description" },
                    new LabelComponent { Text = mod.Description, FontSize = 10 },
                    new LabelComponent { Text = "Mod ID" },
                    new LabelComponent { Text = mod.Id, FontSize = 10 },
                    ..mod.MenuComponents
                ] 
            }.Construct(modSettingsScrollView.gameObject);

            /*
            mod.Viewport = Instantiate(Assets.VerticalWrapper, modSettingsScrollView);
            new LabelComponent { Text = "Description" }.Construct(mod.Viewport);
            new LabelComponent { Text = mod.Description, FontSize = 10 }.Construct(mod.Viewport);
            new LabelComponent { Text = "Mod ID" }.Construct(mod.Viewport);
            new LabelComponent { Text = mod.Id, FontSize = 10 }.Construct(mod.Viewport);
            foreach (var component in mod.MenuComponents)
            {
                component.Construct(mod.Viewport);
            }
            */
        }

        ShowModSettings(registeredMods.First());
    }

    private static void ShowModSettings(ModSettingsConfig activeMod)
    {
        foreach (var mod in registeredMods)
        {
            mod.Viewport.SetActive(mod == activeMod);
            mod.ShowSettingsButton.ShowCaret = mod == activeMod;
        }
    }

    public static void RegisterMod(ModSettingsConfig config)
    {
        registeredMods.Add(config);
    }
}
