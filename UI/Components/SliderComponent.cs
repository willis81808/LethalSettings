using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalSettings.UI.Components;

public class SliderComponent : MenuComponent
{
    public string Text { get; set; } = "Slider";
    public bool ShowValue { get; set; } = true;
    public bool WholeNumbers {  get; set; } = true;
    public bool Enabled { get; set;} = true;
    public float MinValue { get; set; } = 0f;
    public float MaxValue { get; set; } = 100f;
    public float DefaultValue { get; set; } = 50f;

    internal float _currentValue;
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (componentObject == null)
            {
                throw new Exception("Trying to set the value of a SliderComponent before it has been initialized!");
            }
            _currentValue = value;
            componentObject.SetValue(value);
        }
    }
    public Action<SliderComponent, float> OnValueChange { internal get; set; }

    private SliderComponentObject componentObject;

    public override GameObject Construct(GameObject root)
    {
        componentObject = GameObject.Instantiate(Assets.SliderPrefab, root.transform);
        return componentObject.Initialize(this);
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

        slider.value = component.CurrentValue = component.DefaultValue;
        slider.onValueChanged.AddListener(SetValue);

        return gameObject;
    }

    private void FixedUpdate()
    {
        slider.wholeNumbers = component.WholeNumbers;
        slider.interactable = component.Enabled;
        slider.minValue = component.MinValue;
        slider.maxValue = component.MaxValue;
        label.text = $"{component.Text} {(component.ShowValue ? slider.value : "")}";
    }

    internal void SetValue(float value)
    {
        slider.value = value;
        component._currentValue = value;
        component.OnValueChange?.Invoke(component, value);
    }
}
