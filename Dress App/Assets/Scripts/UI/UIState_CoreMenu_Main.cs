using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_CoreMenu_Main : UIState_CoreMenu
{
    private UIView_CoreMenu_Main view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.mainView;
        myView = view;

        view.onChangeItem += root.gameManager.SelectNewItem;
        //view.onChangeWaistSize += ;
        view.onWearItem += root.gameManager.ChangeItem;
    }
}
