
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Please attach this script to any gameObject that you want to be pickable and placed inside this inventory system
/// </summary>
public class Object : MonoBehaviour
{
    public InventoryItem inventoryItem;

    /// <summary>
    /// For once the item is equipped
    /// </summary>
    [HideInInspector] public int currentDurability;
    /// <summary>
    /// Unity Events allow you to hook up any public function in a script
    /// </summary>
    public UnityEvent showPopup; 
    public UnityEvent hidePopup;

    public bool popupActive = false;

    private void Awake()
    {
        hidePopup.Invoke();
        popupActive = false;
    }

	private void OnEnable()
	{
        if(inventoryItem != null) currentDurability = inventoryItem.totalDurability;
    }

	private void OnDestroy() 
    {
        currentDurability = 0;
    }

    private void OnDisable() 
    {
        currentDurability = 0;
    }

	public void DropItem()
	{
        // When we drop an item from our hand we want to instantiate it in 3D in the scene, so that it's pickable again
        GameObject newInHandOBJ = Instantiate(this.gameObject, this.transform.parent);
        newInHandOBJ.SetActive(false);

        this.transform.parent = null;
        this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        this.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        UIEventBroker.TriggerOnRemoveItem(inventoryItem); //make sure we remove the item from the inventory hotbar
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// For when player attempts to pickup the object it interacted with
    /// </summary>
    public void AttemptPickup()
    {
        UIEventBroker.TriggerOnAddToSlots(inventoryItem);
        popupActive = false;
        this.gameObject.SetActive(false);
        hidePopup.Invoke();
    }
}
