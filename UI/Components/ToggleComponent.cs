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
    public bool Visible { get; set; } = true;

    internal bool _toggled;
    public bool Value
    {
        get => _toggled;
        set
        {
            if (componentObject != null)
            {
                componentObject.SetToggled(value);
            }
            else
            {
                _toggled = value;
            }
        }
    }
    public Action<ToggleComponent, bool> OnValueChanged { internal get; set; } = (self, value) => { };

    /// <summary>
    /// This callback is executed once the settings menu is initialized and your menu component has been instantiated into the scene.
    /// </summary>
    public Action<ToggleComponent> OnInitialize { get; set; } = (self) => { };


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

        button.onClick.AddListener(() => SetToggled(!component.Value));

        component.OnInitialize?.Invoke(component);

        return gameObject;
    }

    private void FixedUpdate()
    {
        gameObject.SetActive(component.Visible);
        button.interactable = component.Enabled;
        label.text = component.Text;
        label.fontSize = component.FontSize;
        toggleImage.SetActive(component.Value);
    }

    internal void SetToggled(bool toggled)
    {
        component._toggled = toggled;
        component.OnValueChanged?.Invoke(component, toggled);
    }
}