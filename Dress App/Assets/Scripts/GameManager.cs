using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private CharacterControl characterControl = null;
    [SerializeField] private ObjectRotation characterRotation = null;
    [SerializeField] private Transform newItemObject = null;
    [SerializeField] private Transform newItemStartingPosition = null;

    private MeshFilter newItemMeshFilter;
    private MeshRenderer newItemMeshRenderer;
    private Item newItem;
    private int newItemIndex;
    private bool canChangeItem;

    private void Awake()
    {
        items = Resources.LoadAll<Item>("Items").ToList();
        newItemMeshFilter = newItemObject.GetComponent<MeshFilter>();
        newItemMeshRenderer = newItemObject.GetComponent<MeshRenderer>();

        newItemObject.position = newItemStartingPosition.position;
        newItemIndex = 0;
        newItem = items[newItemIndex];
        canChangeItem = true;
    }

    public string SelectNewItem(int value)
    {
        newItemIndex = (newItemIndex + value) % items.Count;
        newItem = items[newItemIndex];

        return newItem.name;
    }

    public string ChangeItem()
    {
        StartCoroutine(ChangeItemRoutine(newItem));

        return newItem.name;
    }

    private void ChangeNewItem(Item newItem)
    {
        newItemMeshFilter.mesh = newItem.mesh;
        newItemMeshRenderer.material = newItem.material;
    }

    private void ResetNewItemPosition()
    {
        newItemObject.position = newItemStartingPosition.position;
    }

    private IEnumerator ChangeItemRoutine(Item newItem)
    {
        if (!canChangeItem)
            yield return null;

        canChangeItem = false;
        float time = 0f;

        characterControl.ShowCurrentItem(false);
        characterControl.ChangeItem(newItem);
        characterRotation.ResetRotation();
        characterRotation.CanRotate = false;
        ChangeNewItem(newItem);

        while (!canChangeItem)
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0f, 1f);

            if (time < 1)
            {
                newItemObject.position = Vector3.Lerp(newItemObject.position, characterControl.TopMeshPosition, time);
            }
            else
            {
                ResetNewItemPosition();
                characterControl.ShowCurrentItem(true);
                canChangeItem = true;
                characterRotation.CanRotate = true;
            }

            yield return null;
        }

        yield return null;
    }
}
