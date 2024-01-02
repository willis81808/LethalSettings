using LethalSettings.UI.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LethalSettings.UI;

public class ModMenu : MonoBehaviour
{
    private static List<ModSettingsConfig> registeredMods = new List<ModSettingsConfig>();

    [SerializeField]
    internal Transform modListScrollView, modSettingsScrollView;
    internal bool inGame { set; private get; }

    public class ModSettingsConfig
    {
        public string Name {  get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public MenuComponent[] MenuComponents { get; set; } = Array.Empty<MenuComponent>();

        public Action<GameObject, ReadOnlyCollection<MenuComponent>> OnMenuOpen, OnMenuClose;

        internal ButtonComponent ShowSettingsButton { get; set; }
        internal GameObject Viewport { get; set; }
    }

    IEnumerator Start()
    {
        BuildMod(registeredMods.First());
        foreach (var mod in registeredMods.Skip(1).OrderBy(m => m.Name))
        {
            BuildMod(mod);
        }
        ShowModSettings(registeredMods.First());

        yield return new WaitUntil(() => modSettingsScrollView.gameObject.activeInHierarchy);
        yield return LayoutFix();
    }

    private IEnumerator LayoutFix()
    {
        modSettingsScrollView.gameObject.SetActive(false);
        yield return null;
        modSettingsScrollView.gameObject.SetActive(true);

        yield return null;

        modListScrollView.gameObject.SetActive(false);
        yield return null;
        modListScrollView.gameObject.SetActive(true);
    }

    private void BuildMod(ModSettingsConfig mod)
    {
        // Create menu button for mod
        mod.ShowSettingsButton = new ButtonComponent
        {
            Text = mod.Name,
            OnClick = (self) => ShowModSettings(mod),
            AvaliableInGame = true
        };
        mod.ShowSettingsButton.Construct(modListScrollView.gameObject, inGame);

        // Create mod settings menu contents
        mod.Viewport = new VerticalComponent
        {
            ChildAlignment = TextAnchor.UpperLeft,
            Children = new MenuComponent[] {
                new LabelComponent { Text = "Description", FontSize = 16 },
                new LabelComponent { Text = mod.Description, FontSize = 10 },
                new HorizontalComponent
                {
                    ChildAlignment = TextAnchor.MiddleRight,
                    Children = new MenuComponent[]
                    {
                        new VerticalComponent
                        {
                            Children = new MenuComponent[]
                            {
                                new LabelComponent { Text = "Version", FontSize = 16 },
                                new LabelComponent { Text = mod.Version, FontSize = 10 }
                            }
                        },
                        new VerticalComponent
                        {
                            Children = new MenuComponent[]
                            {
                                new LabelComponent { Text = "Mod ID", FontSize = 16 },
                                new LabelComponent { Text = mod.Id, FontSize = 10 }
                            }
                        }
                    }
                }
            }.AddRangeToArray(mod.MenuComponents)
        }.Construct(modSettingsScrollView.gameObject, inGame);
    }

    private static void ShowModSettings(ModSettingsConfig activeMod)
    {
        foreach (var mod in registeredMods)
        {
            bool wasClosed = mod.Viewport.activeSelf && mod != activeMod;
            bool wasOpened = !mod.Viewport.activeSelf && mod == activeMod;
            
            if (wasClosed)
                mod.OnMenuClose?.Invoke(mod.Viewport, new ReadOnlyCollection<MenuComponent>(mod.MenuComponents));

            mod.Viewport.SetActive(mod == activeMod);
            mod.ShowSettingsButton.ShowCaret = mod == activeMod;

            if (wasOpened)
                mod.OnMenuOpen?.Invoke(mod.Viewport, new ReadOnlyCollection<MenuComponent>(mod.MenuComponents));
        }
    }

    public static void RegisterMod(ModSettingsConfig config)
    {
        registeredMods.Add(config);
    }
}
