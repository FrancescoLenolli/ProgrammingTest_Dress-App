using System;
using UIFramework.StateMachine;

/// <summary>
/// Handles everything that happens visually on the application's Title Menu.
/// </summary>
public class UIView_CoreMenu_Title : UIView
{
    public Action onStart;

    public void StartGame()
    {
        onStart?.Invoke();
    }
}
