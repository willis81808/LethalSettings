using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace LethalSettings.UI.Components;

public class DropdownComponent : MenuComponent
{
    public string Text { get; set; }
    public bool Enabled { get; set; } = true;
    public List<TMP_Dropdown.OptionData> Options { get; set; } = [];
    public Action<DropdownComponent, TMP_Dropdown.OptionData> OnValueChanged { get; set; } = (self, value) => { };
    public Action<DropdownComponent> OnInitialize { get; set; } = (self) => { };

    internal TMP_Dropdown.OptionData _currentValue;
    public TMP_Dropdown.OptionData Value
    {
        get => _currentValue;
        set
        {
            _currentValue = value;
            if (componentObject != null)
            {
                componentObject.SetSelected(Options.IndexOf(value));
            }
        }
    }

    private DropdownComponentObject componentObject;

    public override GameObject Construct(GameObject root)
    {
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
            SetSelected(component.Options.IndexOf(component.Value));
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
