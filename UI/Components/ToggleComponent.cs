using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalSettings.UI.Components;

public class ToggleComponent : MenuComponent
{
    public string Text { internal get; set; } = "Toggle";
    public int FontSize { internal get; set; } = 15;
    public bool Enabled { get; set; } = true;
    public bool DefaultToggled { internal get; set; } = true;
    internal bool Toggled { get; set; }
    public Action<ToggleComponent, bool> OnValueChanged { internal get; set; }

    public override GameObject Construct(GameObject root)
    {
        Toggled = DefaultToggled;
        return GameObject.Instantiate(Assets.TogglePrefab, root.transform).Initialize(this);
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

        label.text = component.Text;
        label.fontSize = component.FontSize;

        button.onClick.AddListener(() =>
        {
            component.Toggled = !component.Toggled;
            component.OnValueChanged?.Invoke(component, component.Toggled);
        });

        return gameObject;
    }

    private void Update()
    {
        button.interactable = component.Enabled;
        toggleImage.SetActive(component.Toggled);
    }
}