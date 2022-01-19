using System;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class UIView_CoreMenu_Main : UIView
{
    public Func<int, int> onChangeWaistSize;
    public Func<string> onWearItem;
    public Func<int, string> onChangeItem;

    [SerializeField] private TextMeshProUGUI currentItemLabel = null;
    [SerializeField] private TextMeshProUGUI newItemLabel = null;
    [SerializeField] private Button wearItemButton = null;
    [SerializeField] private Button increaseWaistButton = null;
    [SerializeField] private Button decreaseWaistButton = null;

    public override void ShowView()
    {
        base.ShowView();
        currentItemLabel.text = "";
    }

    public void WearItem()
    {
        string currentItemName = onWearItem?.Invoke();

        if (currentItemName != null)
        {
            currentItemLabel.text = currentItemName;

            // Deactivate the button to prevent the User from wearing the same item twice.
            ActivateWearItemButton(false);

            // Waist size is reset when User wears new item, so buttons need to be reset.
            increaseWaistButton.interactable = true;
            decreaseWaistButton.interactable = true;
        }
    }

    public void IncreaseWaistSize()
    {
        int waistStatus = (int)onChangeWaistSize?.Invoke(1);

        if (waistStatus == 1)
            increaseWaistButton.interactable = false;

        decreaseWaistButton.interactable = true;
    }

    public void DecreaseWaistSize()
    {
        int waistStatus = (int)onChangeWaistSize?.Invoke(-1);

        if (waistStatus == -1)
            decreaseWaistButton.interactable = false;

        increaseWaistButton.interactable = true;
    }

    public void SelectNextItem() { SelectNewItem(1); }
    public void SelectPreviousItem() { SelectNewItem(-1); }

    public void SelectNewItem(int value)
    {
        string newItemName = onChangeItem?.Invoke(value);

        if (newItemName != null)
        {
            newItemLabel.text = $"Try:\n{newItemName}";
            ActivateWearItemButton(true);
        }
    }

    private void ActivateWearItemButton(bool isInteractable)
    {
        wearItemButton.interactable = isInteractable;
    }
}
