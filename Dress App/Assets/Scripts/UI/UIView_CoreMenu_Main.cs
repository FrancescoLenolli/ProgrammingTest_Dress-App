using System;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;

public class UIView_CoreMenu_Main : UIView
{
    public Action<int> onChangeWaistSize;
    public Func<string> onWearItem;
    public Func<int, string> onChangeItem;

    [SerializeField] private TextMeshProUGUI currentItemLabel = null;
    [SerializeField] private TextMeshProUGUI newItemLabel = null;

    public override void ShowView()
    {
        base.ShowView();
        currentItemLabel.text = "";
    }

    public void WearItem()
    {
        string currentItemName = onWearItem?.Invoke();

        if (currentItemName != null)
            currentItemLabel.text = currentItemName;
    }

    public void IncreaseWaistSize() { onChangeWaistSize?.Invoke(1); }
    public void DecreaseWaistSize() { onChangeWaistSize?.Invoke(-1); }
    public void SelectNextItem() { SelectNewItem(1); }
    public void SelectPreviousItem() { SelectNewItem(-1); }

    public void SelectNewItem(int value)
    {
        string newItemName = onChangeItem?.Invoke(value);

        if (newItemName != null)
            newItemLabel.text = $"Try:\n{newItemName}";
    }
}
