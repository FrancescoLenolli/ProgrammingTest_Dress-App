using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the 3D Model of an item.
/// </summary>
public class ItemObject : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Vector3 startingPosition;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetStartingPosition(Vector3 startingPosition)
    {
        this.startingPosition = startingPosition;
    }

    public void ResetPosition()
    {
        transform.position = startingPosition;
    }

    /// <summary>
    /// Change the item appearance to correspond to the item the User will wear next.
    /// </summary>
    /// <param name="newItem"></param>
    public void ChangeNewItem(Item newItem)
    {
        meshFilter.mesh = newItem.mesh;
        meshRenderer.material = newItem.material;
    }
}
