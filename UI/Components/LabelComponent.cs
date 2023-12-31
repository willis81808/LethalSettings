using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace LethalSettings.UI.Components;

public class LabelComponent : MenuComponent
{
    public string Text { internal get; set; } = "Label Text";
    public float FontSize { internal get; set; } = 16;
    public TextAlignmentOptions Alignment { internal get; set; } = TextAlignmentOptions.MidlineLeft;

    /// <summary>
    /// This callback is executed once the settings menu is initialized and your menu component has been instantiated into the scene.
    /// </summary>
    public Action<LabelComponent> OnInitialize { get; set; } = (self) => { };

    public override GameObject Construct(GameObject root)
    {
        return GameObject.Instantiate(Assets.LabelPrefab, root.transform).Initialize(this);
    }
}

internal class LabelComponentObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label;

    private LabelComponent component;

    internal GameObject Initialize(LabelComponent component)
    {
        this.component = component;

        component.OnInitialize?.Invoke(component);

        return gameObject;
    }

    private void FixedUpdate()
    {
        label.text = component.Text;
        label.fontSize = component.FontSize;
        label.alignment = component.Alignment;
    }
}