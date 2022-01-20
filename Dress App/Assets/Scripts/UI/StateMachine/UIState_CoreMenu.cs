using UIFramework.StateMachine;

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
