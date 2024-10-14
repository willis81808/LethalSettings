﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalSettings.UI.Components;

public class ButtonComponent : MenuComponent
{
    public string Text { internal get; set; } = "Button";
    public bool ShowCaret { internal get; set; } = true;
    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    public Action<ButtonComponent> OnClick { internal get; set; } = (self) => { };

    /// <summary>
    /// This callback is executed once the settings menu is initialized and your menu component has been instantiated into the scene.
    /// </summary>
    public Action<ButtonComponent> OnInitialize { get; set; } = (self) => { };

    public override GameObject Construct(GameObject root)
    {
        return GameObject.Instantiate(Assets.ButtonPrefab, root.transform).Initialize(this);
    }
}

internal class ButtonComponentObject : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private TextMeshProUGUI label;

    private ButtonComponent component;

    internal GameObject Initialize(ButtonComponent component)
    {
        this.component = component;

        button.onClick.AddListener(() => component.OnClick?.Invoke(component));

        component.OnInitialize?.Invoke(component);

        return gameObject;
    }

    private void FixedUpdate()
    {
        gameObject.SetActive(component.Visible);
        button.interactable = component.Enabled;
        label.text = $"{(component.ShowCaret ? "> " : "")}{component.Text}";
    }
}
