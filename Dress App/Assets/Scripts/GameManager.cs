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

    private void Awake()
    {
        items = Resources.LoadAll<Item>("Items").ToList();
        newItemMeshFilter = newItemObject.GetComponent<MeshFilter>();
        newItemMeshRenderer = newItemObject.GetComponent<MeshRenderer>();

        newItemObject.position = newItemStartingPosition.position;

        ChangeItem(items[0]);
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

    private void ChangeItem(Item newItem)
    {
        StartCoroutine(ChangeItemRoutine(newItem));
    }

    private IEnumerator ChangeItemRoutine(Item newItem)
    {
        yield return new WaitForSeconds(3f);

        bool canAnimate = true;
        float time = 0f;
        characterControl.ShowCurrentItem(false);
        characterControl.ChangeItem(newItem);
        ChangeNewItem(newItem);

        while(canAnimate)
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
                canAnimate = false;
            }

            yield return null;
        }

        yield return null;
    }
}
