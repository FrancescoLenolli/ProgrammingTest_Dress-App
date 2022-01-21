using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private CharacterControl characterControl = null;
    [SerializeField] private ObjectRotation characterRotation = null;
    [SerializeField] private ItemObject newItemObject = null;               // The item model that corresponds to the item the User will wear next.
    [SerializeField] private Transform newItemStartingPosition = null;
    [Min(.1f)]
    [Tooltip("Speed of the newItemObject when moving from the starting position to the Character's position.")]
    [SerializeField] private float newItemSpeed = 1f;

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

    private void LockApplication()
    {
        /*
         * Lock Character's animation and rotation to prevent visual glitches
         * when changing weared item.
         */
        characterControl.WearNewItem(newItem);
        characterRotation.LockRotation();
        newItemObject.ChangeNewItem(newItem);
    }

    private void ReleaseApplication()
    {
        //Once the Character is wearing the new item, re-enable animation and rotation.
        newItemObject.ResetPosition();
        characterControl.ShowCurrentItem(true);
        characterControl.Animator.CanAnimate = true;
        canChangeItem = true;
        characterRotation.CanRotate = true;
    }

    private void MoveItemObject(float time)
    {
        newItemObject.transform.position = Vector3.Lerp
            (newItemObject.transform.position, characterControl.TopMeshPosition, time);

        /*
         * Brutal fix. The T-Pose animation shift a little forward
         * the character model causing the item falling down to clip horribly with the character model.
         */
        newItemObject.transform.position = new Vector3
            (newItemObject.StartingPosition.x, newItemObject.transform.position.y, newItemObject.StartingPosition.z);
    }

    private IEnumerator ChangeItemRoutine(Item newItem)
    {
        if (!canChangeItem)
            yield return null;

        canChangeItem = false;
        float time = 0f;
        LockApplication();

        while (!canChangeItem)
        {
            time = Mathf.Clamp(time + Time.deltaTime * newItemSpeed, 0f, 1f);

            if (time < 1)
                MoveItemObject(time);
            else
                ReleaseApplication();

            yield return null;
        }

        yield return null;
    }
}
