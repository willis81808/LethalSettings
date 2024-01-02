using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

namespace LethalSettings.UI.Components;

public class HorizontalComponent : MenuComponent
{
    public MenuComponent[] Children { internal get; set; } = Array.Empty<MenuComponent>();
    public int Spacing { internal get; set; } = 10;
    public TextAnchor ChildAlignment { internal get; set; } = TextAnchor.MiddleLeft;

    public override GameObject Construct(GameObject root, bool inGame) {
        var layoutGroup = GameObject.Instantiate(Assets.HorizontalWrapper, root.transform).GetComponent<HorizontalLayoutGroup>();
        layoutGroup.spacing = Spacing;
        layoutGroup.childAlignment = ChildAlignment;
        foreach (var child in Children)
        {
            child.Construct(layoutGroup.gameObject, inGame);
        }
        return layoutGroup.gameObject;
    }
}
