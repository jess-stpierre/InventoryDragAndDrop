using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LoadoutItem", order = 2)]
public class LoadoutItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public GameObject itemPrefab;
}
