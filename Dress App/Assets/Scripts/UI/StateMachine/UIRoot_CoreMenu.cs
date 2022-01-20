using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

/// <summary>
/// Root for the application's UI. Can contain other useful references.
/// </summary>
public class UIRoot_CoreMenu : UIRoot
{
    public GameManager gameManager;
    public UIView_CoreMenu_Title titleView;
    public UIView_CoreMenu_Main mainView;
}
