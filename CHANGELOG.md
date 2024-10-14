# 1.4.1
- Fixed bug where mod settings menu is opened by default the first time the game settigns are accessed

# 1.4.0
- Updated components to use a new font map generated from an extended Unicode character set

# 1.2.2
- Fixed the `SliderComponent` initial value defect frfr
- Started adding documentation to significant properties on the built-in menu components

# 1.2.1
- Corrected a defect in `SliderComponent` that caused the incorrect initial value to be displayed due to an execution order issue with the initial and max value properties

# 1.2.0 **BREAKING**
- Layout should be much more stable and predictable now
- Standardized the built-in menu component callback naming convention to `OnValueChanged`
- Changed all built-in menu components that previously had separate `CurrentValue` and `Value` properties into a single `Value` property that supports dynamic updates as well as setting the initial value


# 1.1.0

- Added `InputComponent` for arbitrary text input
- Added `DropdownComponent` for selecting from predefined options
- Mod Settings list now sort mods in alphabetical order by name
- (Nearly) all built-in menu components now output their current value and support dynamic updates to their properties

