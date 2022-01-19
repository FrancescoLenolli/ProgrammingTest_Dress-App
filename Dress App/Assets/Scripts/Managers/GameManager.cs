using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private CharacterControl characterControl = null;
    [SerializeField] private ObjectRotation characterRotation = null;
    // The item model that corresponds to the item the User will wear next.
    [SerializeField] private ItemObject newItemObject = null;
    [SerializeField] private Transform newItemStartingPosition = null;

    private Item newItem;
    private int newItemIndex;
    private bool canChangeItem;

    public CharacterControl CharacterControl { get => characterControl; }

    private void Awake()
    {
        items = Resources.LoadAll<Item>("Items").ToList();

        newItemObject.SetStartingPosition(newItemStartingPosition.position);
        newItemObject.ResetPosition();
        newItemIndex = 0;
        newItem = items[newItemIndex];
        canChangeItem = true;
    }

    /// <summary>
    /// Select the new item the User will be able to try on.
    /// </summary>
    public string SelectNewItem(int value)
    {
        newItemIndex += value;
        newItemIndex = newItemIndex < 0 ? items.Count - 1 : newItemIndex % items.Count;
        newItem = items[newItemIndex];

        return newItem.name;
    }

    /// <summary>
    /// Change item the User is currently wearing.
    /// </summary>
    public string ChangeItem()
    {
        StartCoroutine(ChangeItemRoutine(newItem));

        return newItem.name;
    }

    private IEnumerator ChangeItemRoutine(Item newItem)
    {
        if (!canChangeItem)
            yield return null;

        canChangeItem = false;
        float time = 0f;

        characterControl.WearNewItem(newItem);
        characterRotation.LockRotation();
        newItemObject.ChangeNewItem(newItem);

        while (!canChangeItem)
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0f, 1f);

            if (time < 1)
            {
                newItemObject.transform.position = Vector3.Lerp
                    (newItemObject.transform.position, characterControl.TopMeshPosition, time);
            }
            else
            {
                newItemObject.ResetPosition();
                characterControl.ShowCurrentItem(true);
                canChangeItem = true;
                characterRotation.CanRotate = true;
            }

            yield return null;
        }

        yield return null;
    }
}
