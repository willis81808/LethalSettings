using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalSettings.UI.Components;

public class ToggleComponent : MenuComponent
{
    public string Text { get; set; } = "Toggle";
    public int FontSize { get; set; } = 15;
    public bool Enabled { get; set; } = true;
    public bool DefaultToggled { get; set; } = true;

    internal bool _toggled;
    public bool Toggled
    {
        get => _toggled;
        set
        {
            if (componentObject == null)
            {
                throw new Exception("Trying to set the value of a ToggleComponent before it has been initialized!");
            }
            _toggled = value;
            componentObject.SetToggled(value);
        }
    }
    public Action<ToggleComponent, bool> OnValueChanged { internal get; set; }

    private ToggleComponentObject componentObject;

    public override GameObject Construct(GameObject root)
    {
        componentObject = GameObject.Instantiate(Assets.TogglePrefab, root.transform);
        return componentObject.Initialize(this);
    }
}

internal class ToggleComponentObject : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private TextMeshProUGUI label;

    [SerializeField]
    private GameObject toggleImage;

    private ToggleComponent component;

    internal GameObject Initialize(ToggleComponent component)
    {
        this.component = component;

        component.Toggled = component.DefaultToggled;
        button.onClick.AddListener(() => SetToggled(!component.Toggled));

        return gameObject;
    }

    private void FixedUpdate()
    {
        button.interactable = component.Enabled;
        label.text = component.Text;
        label.fontSize = component.FontSize;
        toggleImage.SetActive(component.Toggled);
    }

    internal void SetToggled(bool toggled)
    {
        component._toggled = !component.Toggled;
        component.OnValueChanged?.Invoke(component, component.Toggled);
    }
}