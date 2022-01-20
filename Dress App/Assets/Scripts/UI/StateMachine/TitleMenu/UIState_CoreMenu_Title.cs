using UIFramework.StateMachine;

/// <summary>
/// Handles UI logic for the application's Title Menu.
/// </summary>
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
