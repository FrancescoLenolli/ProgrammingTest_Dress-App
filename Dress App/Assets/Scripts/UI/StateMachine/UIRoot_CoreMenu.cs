using UIFramework.StateMachine;

/// <summary>
/// Root for the application's UI. Can contain other useful references.
/// </summary>
public class UIRoot_CoreMenu : UIRoot
{
    //TODO: Consider removing GameManager reference to decouple code (use a Messaging System?).
    
    public GameManager gameManager;
    public UIView_CoreMenu_Title titleView;
    public UIView_CoreMenu_Main mainView;
}
