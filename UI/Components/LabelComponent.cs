using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace LethalSettings.UI.Components;

public class LabelComponent : MenuComponent
{
    public string Text { internal get; set; } = "Label Text";
    public float FontSize { internal get; set; } = 23f;
    public TextAlignmentOptions Alignment { internal get; set; } = TextAlignmentOptions.MidlineLeft;

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

        return gameObject;
    }

    private void FixedUpdate()
    {
        label.text = component.Text;
        label.fontSize = component.FontSize;
        label.alignment = component.Alignment;
    }
}