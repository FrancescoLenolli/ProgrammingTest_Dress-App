using UIFramework.StateMachine;

/// <summary>
/// Handles UI logic for the application's Main Menu.
/// </summary>
public class UIState_CoreMenu_Main : UIState_CoreMenu
{
    private UIView_CoreMenu_Main view;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.mainView;
        myView = view;

        view.onChangeItem += root.gameManager.SelectNewItem;
        view.onChangeWaistSize += root.gameManager.CharacterControl.ChangeWaistSize;
        view.onWearItem += root.gameManager.ChangeItem;
        view.onPlayAnimation += root.gameManager.CharacterControl.Animator.PlayAnimation;

        // Initialise newItemLabel text in the UIView with the first Item on the Items list.
        // Pass 0 as value to avoid switching the selected Item.
        view.SelectNewItem(0);
    }
}
