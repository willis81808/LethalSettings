using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LethalSettings.UI.Components;

public class VerticalComponent : MenuComponent
{
    public MenuComponent[] Children { internal get; set; } = Array.Empty<MenuComponent>();
    public int Spacing { internal get; set; } = 10;
    public TextAnchor ChildAlignment { internal get; set; } = TextAnchor.MiddleLeft;

    public override GameObject Construct(GameObject root)
    {
        var layoutGroup = GameObject.Instantiate(Assets.VerticalWrapper, root.transform).GetComponent<VerticalLayoutGroup>();
        layoutGroup.spacing = Spacing;
        layoutGroup.childAlignment = ChildAlignment;
        foreach (var child in Children)
        {
            child.Construct(layoutGroup.gameObject);
        }
        return layoutGroup.gameObject;
    }
}
