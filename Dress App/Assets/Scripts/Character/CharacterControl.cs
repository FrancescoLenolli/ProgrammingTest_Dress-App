using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer topMeshRenderer = null;
    [SerializeField] private Transform waist = null;
    [SerializeField] private float waistScalingSpeed = 1f;
    [SerializeField] private Vector2 waistScaleLimits = Vector2.one;
    private Vector3 waistStartingScale;
    private List<Transform> waistChildren;

    public Vector3 TopMeshPosition => topMeshRenderer.transform.position;
    public CharacterAnimator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<CharacterAnimator>();

        waistChildren = new List<Transform>();
        foreach (Transform child in waist)
        {
            if (child != waist)
                waistChildren.Add(child);
        }

        waistStartingScale = waist.localScale;
        ResetWaistSize();
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
        Animator.ResetAnimation();
        Animator.CanAnimate = false;
    }

    public int ChangeWaistSize(int value)
    {
        Vector3 waistScale = waist.localScale;
        float waistScaleX = waistScale.x;
        float waistScaleZ = waistScale.z;
        
        /*
         * The waist is part of the Character's Avatar,
         * that is responsible for the animations. If I change the scale,
         * The entire model scales accordingly.
         * To prevent this, I can temporarily detach the rest of the children, scale the waist, and re-attach them.
         * There's some error in the children scale once they're attached back, but I don't know what's causing it.
         */
        foreach (Transform child in waistChildren)
        {
            child.parent = null;
        }

        /*
         * X axis represents the Character's flanks, Z axis its gut.
         * Scaling the flanks as much as the gut will cause the Character to look too unrealistic.
         */
        float scaleChangeX = value * (waistScalingSpeed / 2) * Time.deltaTime;
        float scaleChangeZ = value * waistScalingSpeed * Time.deltaTime;
        float newScaleX = Mathf.Clamp(waistScaleX + scaleChangeX, waistScaleLimits.x, waistScaleLimits.y);
        float newScaleZ = Mathf.Clamp(waistScaleZ + scaleChangeZ, waistScaleLimits.x, waistScaleLimits.y);
        waist.localScale = new Vector3(newScaleX, transform.localScale.y, newScaleZ);

        foreach (Transform child in waistChildren)
        {
            child.parent = waist;
        }

        return CheckWaistScaleStatus(newScaleZ);
    }

    private void ResetWaistSize()
    {
        waist.localScale = waistStartingScale;
        foreach (Transform child in waist)
        {
            if (child != waist)
            {
                child.localScale = Vector3.one;
            }
        }
    }

    private void ChangeItem(Item newItem)
    {
        topMeshRenderer.sharedMesh = newItem.mesh;
        topMeshRenderer.material = newItem.material;
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
