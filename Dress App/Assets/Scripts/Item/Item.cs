using UnityEngine;

/// <summary>
/// Holds values for a given item.
/// </summary>
[CreateAssetMenu(menuName = "CustomAssets/Item", fileName = "NewItem")]
public class Item : ScriptableObject
{
    public Mesh mesh = null;
    public Material material = null;
}
