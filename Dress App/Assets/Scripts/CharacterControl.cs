using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer topMeshRenderer = null;
    [SerializeField] private Transform waist = null;
    [SerializeField] private float waistScalingSpeed = 1f;
    [SerializeField] private Vector2 waistScaleLimits = Vector2.one;

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

    public void ChangeWaistSize(int value)
    {
        float scaleChange = value * waistScalingSpeed * Time.deltaTime;
        float newScale = Mathf.Clamp(waist.localScale.x + scaleChange, waistScaleLimits.x, waistScaleLimits.y);

        waist.localScale = new Vector3(newScale, transform.localScale.y, newScale);
    }
}
