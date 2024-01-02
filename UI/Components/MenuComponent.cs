using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalSettings.UI.Components;

public abstract class MenuComponent
{

     
    public abstract GameObject Construct(GameObject root, bool inGame);
}
