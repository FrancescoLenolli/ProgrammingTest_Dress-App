using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_CoreMenu_Title : UIState_CoreMenu
{
    private UIView_CoreMenu_Title view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.titleView;
        myView = view;

        view.onStart += StartGame;
    }

    private void StartGame()
    {
        owner.ChangeState(typeof(UIState_CoreMenu_Main));
    }
}
