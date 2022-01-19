using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer topMeshRenderer = null;
    [SerializeField] private Transform waist = null;
    [SerializeField] private float waistScalingSpeed = 1f;
    [SerializeField] private Vector2 waistScaleLimits = Vector2.one;
    private Vector3 waistStartingScale;
    private List<Transform> waistChildren;

    public Vector3 TopMeshPosition { get => topMeshRenderer.transform.position; }

    private void Awake()
    {
        waistChildren = new List<Transform>();
        foreach(Transform child in waist)
        {
            if (child != waist)
                waistChildren.Add(child);
        }

        waistStartingScale = waist.localScale;
        ResetWaistSize();
    }

    public void ChangeItem(Item newItem)
    {
        topMeshRenderer.sharedMesh = newItem.mesh;
        topMeshRenderer.material = newItem.material;
    }

    public void ShowCurrentItem(bool isVisible)
    {
        topMeshRenderer.gameObject.SetActive(isVisible);
    }

    public void WearNewItem(Item newItem)
    {
        ShowCurrentItem(false);
        ChangeItem(newItem);
        ResetWaistSize();
        // TODO: Stop the current animation if one is playing.
    }

    public int ChangeWaistSize(int value)
    {
        foreach (Transform child in waistChildren)
        {
            child.parent = null;
        }

        float scaleChange = value * waistScalingSpeed * Time.deltaTime;
        float newScale = Mathf.Clamp(waist.localScale.x + scaleChange, waistScaleLimits.x, waistScaleLimits.y);
        waist.localScale = new Vector3(newScale, transform.localScale.y, newScale);

        foreach (Transform child in waistChildren)
        {
            child.parent = waist;
        }

        return CheckWaistScaleStatus(newScale);
    }

    public void ResetWaistSize()
    {
        waist.localScale = waistStartingScale;
        foreach(Transform child in waist)
        {
            if(child != waist)
            {
                child.localScale = Vector3.one;
            }
        }    
    }

    /// <summary>
    /// Return -1 if scale is equal to its minimum value.
    /// Return 1 if scale is equal to its maximum value.
    /// Return 0 if scale is in between min and max value.
    /// </summary>
    private int CheckWaistScaleStatus(float newScale)
    {
        int waistScaleStatus = 0;

        if (newScale == waistScaleLimits.x)
            waistScaleStatus = -1;
        if (newScale == waistScaleLimits.y)
            waistScaleStatus = 1;

        return waistScaleStatus;
    }
}
