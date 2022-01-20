using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

/// <summary>
/// Base UIState for every other application's UIState.
/// </summary>
public class UIState_CoreMenu : UIState
{
    protected UIRoot_CoreMenu root;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        if (!root)
            root = (UIRoot_CoreMenu)this.owner.Root;
    }
}
