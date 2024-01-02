using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace LethalSettings.UI.Components;

public class DropdownComponent : MenuComponent
{
    public bool AvailableInGame { internal get; set; } = false;
    public string Text { get; set; }
    public bool Enabled { get; set; } = true;
    public List<TMP_Dropdown.OptionData> Options { get; set; } = new List<TMP_Dropdown.OptionData>();
    public Action<DropdownComponent, TMP_Dropdown.OptionData> OnValueChanged { get; set; } = (self, value) => { };

    /// <summary>
    /// This callback is executed once the settings menu is initialized and your menu component has been instantiated into the scene.
    /// </summary>
    public Action<DropdownComponent> OnInitialize { get; set; } = (self) => { };

    internal TMP_Dropdown.OptionData _currentValue;

    /// <summary>
    /// Sets the current (or initial) selection in the dropdown. Must be a valid, non-null, reference to an existing element in the Options collection
    /// </summary>
    public TMP_Dropdown.OptionData Value
    {
        get => _currentValue;
        set
        {
            if (componentObject != null)
            {
                var index = Options.IndexOf(value);
                if (index < 0)
                {
                    throw new Exception("Could not find the provided value in the Options list! Did you pass in a reference to a TMP_Dropdown.OptionData that is not in the list?");
                }
                componentObject.SetSelected(index);
            }
            else
            {
                _currentValue = value;
            }
        }
    }

    private DropdownComponentObject componentObject;

    public override GameObject Construct(GameObject root, bool inGame) {
        if (inGame && !AvailableInGame) return null;
        componentObject = GameObject.Instantiate(Assets.DropdownPrefab, root.transform);
        return componentObject.Initialize(this);
    }
}

internal class DropdownComponentObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TMP_Dropdown dropdown;

    private DropdownComponent component;

    public GameObject Initialize(DropdownComponent component)
    {
        this.component = component;

        dropdown.options = component.Options;
        dropdown.onValueChanged.AddListener(SetSelected);

        if (component.Value == null)
        {
            component.Value = component.Options.First();
        }
        else
        {
            var index = component.Options.IndexOf(component.Value);
            if (index >= 0)
            {
                SetSelected(index);
            }
        }

        component.OnInitialize?.Invoke(component);

        return gameObject;
    }

    internal void SetSelected(int index)
    {
        dropdown.value = index;
        component._currentValue = dropdown.options[index];
        component.OnValueChanged?.Invoke(component, dropdown.options[index]);
    }

    private void FixedUpdate()
    {
        dropdown.interactable = component.Enabled;
        title.text = component.Text;
        if (component.Options != dropdown.options)
        {
            dropdown.options = component.Options;
        }
    }
}
