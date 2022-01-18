using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer topMeshRenderer = null;

    public Vector3 TopMeshPosition { get => topMeshRenderer.transform.position; }

    public void ChangeItem(Item newItem)
    {
        topMeshRenderer.sharedMesh = newItem.mesh;
        topMeshRenderer.material = newItem.material;
    }

    public void ShowCurrentItem(bool isVisible)
    {
        topMeshRenderer.gameObject.SetActive(isVisible);
    }
}
