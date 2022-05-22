
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// This script is used for attempting to check if an object is pickable if it has a trigger collider and a pop up has been shown.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
	[Header("Default button to interact is E, changeable below")]
	[SerializeField] KeyCode interactKey = KeyCode.E;
    [Header(" ")]
    [Tooltip("If you want players to have a 'press e to interact' popup before they can pickup items than make this true, else make this false")]
    [SerializeField] private bool havePressInteractPopUp = true;

	private GameObject interactedOBJ;
    private SphereCollider interactedCollider;

    void Update()
    {
        DistanceCheck();
        //if inventory UI is not active than we can allow player to pickup object
        if (UIEventBroker.TriggerOnCheckInventoryStatus() == false) PickupOBJ();
    }

    /// <summary>
    /// Makes sure that we Hide the "Press E" popup on each Object that we leave the trigger volume from
    /// </summary>
    private void DistanceCheck()
    {
        if (interactedOBJ != null)
        {
            if (Vector3.Distance(interactedOBJ.transform.position, this.transform.position) > (interactedCollider.radius * interactedOBJ.transform.localScale.x))
            {
                interactedOBJ.GetComponent<Object>().hidePopup.Invoke();
                interactedOBJ.GetComponent<Object>().popupActive = false;
                interactedOBJ = null;
            }
            else
            {
                interactedOBJ.GetComponent<Object>().showPopup.Invoke();
                interactedOBJ.GetComponent<Object>().popupActive = true;
            }
        }
    }

    /// <summary>
    /// Attempt to pickup the object within the trigger zone
    /// </summary>
    private void PickupOBJ()
    {
        if (interactedOBJ != null && havePressInteractPopUp && interactedOBJ.GetComponent<Object>().popupActive == true) //returns true if there is a pop up showed on screen saying "press E to interact"
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactedOBJ.GetComponent<Object>().AttemptPickup();
                interactedOBJ = null;
            }
        }
        else if (havePressInteractPopUp == false && interactedOBJ != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactedOBJ.GetComponent<Object>().AttemptPickup();
                interactedOBJ = null;
            }
        }
    }

    /// <summary>
    /// On Trigger Stay calls every frame when this player hits an object with a trigger collider
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<SphereCollider>() != null)
        {
            SphereCollider[] allInteractedSphereCols = other.gameObject.GetComponents<SphereCollider>();

            foreach (SphereCollider SphereCol in allInteractedSphereCols)
            {
                if (other.GetComponent<SphereCollider>() != null && SphereCol.isTrigger == true)
                {
                    interactedOBJ = other.gameObject; //save the value of the object we want to pickup
                    interactedCollider = SphereCol;
                }
            }
        }
        else interactedOBJ = null;
    }
}
