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
    public Action<DropdownComponent, TMP_Dropdown.OptionData> OnValueChange { get; set; }

    internal TMP_Dropdown.OptionData _currentValue;
    public TMP_Dropdown.OptionData CurrentValue
    {
        get => _currentValue;
        set
        {
            if (componentObject == null)
            {
                throw new Exception("Trying to set the value of a DropdownComponent before it has been initialized!");
            }
            componentObject.SetSelected(Options.IndexOf(value));
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
        component.CurrentValue = component.Options.First();

        return gameObject;
    }

    internal void SetSelected(int index)
    {
        dropdown.value = index;
        component._currentValue = dropdown.options[index];
        component.OnValueChange?.Invoke(component, dropdown.options[index]);
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
