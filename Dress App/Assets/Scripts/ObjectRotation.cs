using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private Transform targetObject = null;
    [SerializeField] private float rotationSpeed = 20f;
    private Quaternion startingRotation;

    public bool CanRotate { get; set; }

    private void Awake()
    {
        startingRotation = targetObject.rotation;
        CanRotate = true;
    }

    private void OnMouseDrag()
    {
        if (!CanRotate)
            return;

        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        targetObject.Rotate(Vector3.up, -rotationX);
    }

    public void ResetRotation()
    {
        targetObject.rotation = startingRotation;
    }
}
