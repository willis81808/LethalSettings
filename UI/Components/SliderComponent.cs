using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalSettings.UI.Components;

public class SliderComponent : MenuComponent
{
    public string Text { internal get; set; } = "Slider";
    public bool ShowValue { internal get; set; } = true;
    public bool WholeNumbers {  internal get; set; } = true;
    public bool Enabled { get; set;} = true;
    public float MinValue { internal get; set; } = 0f;
    public float MaxValue { internal get; set; } = 100f;
    public float DefaultValue { internal get; set; } = 50f;
    public Action<SliderComponent, float> OnValueChange { internal get; set; }

    public override GameObject Construct(GameObject root)
    {
        return GameObject.Instantiate(Assets.SliderPrefab, root.transform).Initialize(this);
    }
}

internal class SliderComponentObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label;

    [SerializeField]
    private Slider slider;

    private SliderComponent component;

    internal GameObject Initialize(SliderComponent component)
    {
        this.component = component;

        slider.wholeNumbers = component.WholeNumbers;
        slider.minValue = component.MinValue;
        slider.maxValue = component.MaxValue;
        slider.value = component.DefaultValue;
        slider.onValueChanged.AddListener((val) => component.OnValueChange?.Invoke(component, val));

        return gameObject;
    }

    private void FixedUpdate()
    {
        slider.interactable = component.Enabled;
        label.text = $"{component.Text} {(component.ShowValue ? slider.value : "")}";
    }
}
