using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAssets/Item", fileName = "NewItem")]
public class Item : ScriptableObject
{
    public Mesh mesh = null;
    public Material material = null;
}
