
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public UnityEvent usage;
	public int totalDurability;

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
    public void Hit()
	{
		Debug.Log("The player has used this item to hit");
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void Shoot()
	{
		Debug.Log("The player has used this item to shoot");
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void Light()
	{
		Debug.Log("The player has used this item to light");
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void Heal()
	{
		Debug.Log("The player has used this item to heal");
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void UseMana()
	{
		Debug.Log("The player has used this item to use mana");
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void UseAmmo()
	{
		Debug.Log("The player has used this item to use ammo");
	}
}
