
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

    private bool popupActive = false;

    private void Awake()
    {
        UIEventBroker.OnCheckPopupStatus += SetOnCheckPopupStatus;

        hidePopup.Invoke();
        popupActive = false;
    }

	private void OnEnable()
	{
        if(inventoryItem != null) currentDurability = inventoryItem.totalDurability;
    }

	private void OnDestroy() 
    {
        UIEventBroker.OnCheckPopupStatus -= SetOnCheckPopupStatus;
        currentDurability = 0;
    }

    private void OnDisable() 
    {
        UIEventBroker.OnCheckPopupStatus -= SetOnCheckPopupStatus;
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
        UIEventBroker.TriggerOnRemoveItem(inventoryItem); //make sure we remove the item from the inventory hotbar
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        showPopup.Invoke();
        popupActive = true;
    }

	private bool SetOnCheckPopupStatus()
	{
        return popupActive;
	}

	private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //if the player is within the trigger zone collider than show "press e to interact" popup
            showPopup.Invoke();
            popupActive = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //if player is far from trigger zone collider than hide the "press e to interact" popup
            hidePopup.Invoke();
            popupActive = false;
        }
    }

    /// <summary>
    /// For when player attempts to pickup the object it interacted with
    /// </summary>
    public void AttemptPickup()
    {
        UIEventBroker.TriggerOnAddToSlots(inventoryItem);
        this.gameObject.SetActive(false);
        hidePopup.Invoke();
    }
}
