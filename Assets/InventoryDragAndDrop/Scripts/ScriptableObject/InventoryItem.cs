
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
	public enum ItemType
	{
		Hit,
		Shoot,
		Light,
		Heal,
		Mana,
		Ammo,
		Other
	}
	public ItemType currentItemType;

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
    public void Hit()
	{
		Debug.Log("The player has used this item to hit");
		//Trigger event function here
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void Shoot()
	{
		Debug.Log("The player has used this item to shoot");
		//Trigger event function here
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void Light()
	{
		Debug.Log("The player has used this item to light");
		//Trigger event function here
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void Heal()
	{
		Debug.Log("The player has used this item to heal");
		//Trigger event function here
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void UseMana()
	{
		Debug.Log("The player has used this item to use mana");
		//Trigger event function here
	}

	/// <summary>
	/// Feel free to edit this function as much as needed, we temporarily implemented it
	/// </summary>
	public void UseAmmo()
	{
		Debug.Log("The player has used this item to use ammo");
		//Trigger event function here
	}
}
