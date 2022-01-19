using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer topMeshRenderer = null;
    [SerializeField] private Transform waist = null;
    [SerializeField] private float waistScalingSpeed = 1f;
    [SerializeField] private Vector2 waistScaleLimits = Vector2.one;
    private Vector3 waistStartingScale = Vector3.one;

    public Vector3 TopMeshPosition { get => topMeshRenderer.transform.position; }

    private void Awake()
    {
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

    public void ChangeWaistSize(int value)
    {
        float scaleChange = value * waistScalingSpeed * Time.deltaTime;
        float newScale = Mathf.Clamp(waist.localScale.x + scaleChange, waistScaleLimits.x, waistScaleLimits.y);

        waist.localScale = new Vector3(newScale, transform.localScale.y, newScale);
    }

    public void ResetWaistSize()
    {
        waist.localScale = waistStartingScale;
    }
}
