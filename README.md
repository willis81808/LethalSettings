# LethalSettings

[![Thunderstore Version](https://img.shields.io/thunderstore/v/willis81808/LethalSettings?style=for-the-badge&logo=thunderstore&logoColor=white)](https://thunderstore.io/c/lethal-company/p/willis81808/LethalSettings/)
[![Thunderstore Downloads](https://img.shields.io/thunderstore/dt/willis81808/LethalSettings?style=for-the-badge&logo=thunderstore&logoColor=white)](https://thunderstore.io/c/lethal-company/p/willis81808/LethalSettings/)

This utility provides a centralized framework through which other Lethal Company mods can register and display custom configuration/settings through a new "Mod Settings" button added to the existing in-game Settings menu.

## Usage

It is simple to create and register a custom settings panel for your mod, you must simply provide your mod's basic details and a collection of `MenuComponents` which LethalSettings will use to construct your UI.

Here is a basic example:
```cs
ModMenu.RegisterMod(new ModMenu.ModSettingsConfig
{
    Name = "Example Mod",
    Id = "com.willis.lc.examplemod",
    Version = "0.0.1",
    Description = "This is an example mod registration showing how easy it can be to give your mod configuration a vanilla-like feel!",
    MenuComponents = new MenuComponent[]
    {
        new ButtonComponent
        {
            Text = "This is yet another test button!",
            OnClick = (self) => Logger.LogInfo("You clicked the second test button!")
        },
        new HorizontalComponent
        {
            Children = new MenuComponent[]
            {
                new ToggleComponent
                {
                    Text = "Toggle me!",
                    OnValueChanged = (self, value) => Logger.LogInfo($"New value: {value}")
                },
                new SliderComponent
                {
                    Value = 30,
                    MinValue = 10,
                    MaxValue = 50,
                    Text = "Example Slider",
                    OnValueChanged = (self, value) => Logger.LogInfo($"New value: {value}")
                }
            }
        },
        new LabelComponent
        {
            Text = "Hello, World!"
        }
    }
});
```

This example will end up being rendered like so:
![Example Settings Menu](https://i.imgur.com/aktZhLr.png)

## Extending LethalSettings

You can create custom UI elements that are compatible with LethalSettings by simply extending the base `MenuComponent` class.
Menu Components are required to have a `Construct` method, which accepts the parent UI element as input and must return the newly constructed UI element.

The built-in `LabelComponent` serves as a simple example:
```cs
public class LabelComponent : MenuComponent
{
    public string Text { internal get; set; } = "Label Text";
    public float FontSize { internal get; set; } = 23f;
    public TextAlignmentOptions Alignment { internal get; set; } = TextAlignmentOptions.MidlineLeft;

    public override GameObject Construct(GameObject root)
    {
        return GameObject.Instantiate(Assets.LabelPrefab, root.transform).Initialize(this);
    }
}
```

## Contributing

Contributions are welcome. Please submit a pull request with your proposed changes.

## License

This project is licensed under the terms of the MIT license.
