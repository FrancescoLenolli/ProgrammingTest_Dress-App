using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIView_CoreMenu_Title : UIView
{
    public Action onStart;

    public void StartGame()
    {
        onStart?.Invoke();
    }
}
