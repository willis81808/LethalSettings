using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace LethalSettings.UI.Components;

public class InputComponent : MenuComponent
{
    public string Placeholder { get; set; } = "Enter text...";
    public Action<InputComponent, string> OnValueChanged { get; set; } = (self, value) => { };
    
    /// <summary>
    /// This callback is executed once the settings menu is initialized and your menu component has been instantiated into the scene.
    /// </summary>
    public Action<InputComponent> OnInitialize { get; set; } = (self) => { };

    /// <summary>
    /// Sets the current (or initial) text populating the input field.
    /// </summary>
    public string Value
    {
        get => _currentValue;
        set
        {
            if (componentObject != null)
            {
                componentObject.SetValue(value);
            }
            else
            {
                _currentValue = value;
            }
        }
    }
    internal string _currentValue = "";

    private InputComponentObject componentObject;

    public override GameObject Construct(GameObject root)
    {
        componentObject = GameObject.Instantiate(Assets.InputPrefab, root.transform);
        return componentObject.Initialize(this);
    }

    public TMP_InputField GetBackingObject()
    {
        if (componentObject == null)
        {
            return null;
        }
        return componentObject.input;
    }
}

internal class InputComponentObject : MonoBehaviour
{
    [SerializeField]
    internal TMP_InputField input;

    [SerializeField]
    private TextMeshProUGUI placeholder;

    private InputComponent component;

    public GameObject Initialize(InputComponent component)
    {
        this.component = component;

        input.onValueChanged.AddListener(SetValue);

        component.OnInitialize?.Invoke(component);

        return gameObject;
    }

    private void FixedUpdate()
    {
        placeholder.text = component.Placeholder;
        input.text = component.Value;
    }

    internal void SetValue(string value)
    {
        input.text = value;
        component._currentValue = value;
        component.OnValueChanged?.Invoke(component, value);
    }
}
