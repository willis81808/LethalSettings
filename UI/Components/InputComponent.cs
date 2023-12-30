using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace LethalSettings.UI.Components;

public class InputComponent : MenuComponent
{
    public string Placeholder { get; set; } = "Enter text...";
    public string DefaultValue { get; set; } = "";
    public Action<InputComponent, string> OnValueChange { get; set; }
    internal string _currentValue;
    public string CurrentValue
    {
        get => _currentValue;
        set
        {
            if (componentObject == null)
            {
                throw new Exception("Trying to set the value of a InputComponent before it has been initialized!");
            }
            componentObject.SetValue(value);
        }
    }

    private InputComponentObject componentObject;

    public override GameObject Construct(GameObject root)
    {
        componentObject = GameObject.Instantiate(Assets.InputPrefab, root.transform);
        return componentObject.Initialize(this);
    }
}

internal class InputComponentObject : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField input;

    [SerializeField]
    private TextMeshProUGUI placeholder;

    private InputComponent component;

    public GameObject Initialize(InputComponent component)
    {
        this.component = component;

        component.CurrentValue = component.DefaultValue;
        input.onValueChanged.AddListener(SetValue);

        return gameObject;
    }

    private void FixedUpdate()
    {
        placeholder.text = component.Placeholder;
    }

    internal void SetValue(string value)
    {
        input.text = value;
        component._currentValue = value;
        component.OnValueChange?.Invoke(component, value);
    }
}
