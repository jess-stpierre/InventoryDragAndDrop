///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// Allows the player to rotate based on mouse position
/// </summary>
public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    private float angle = 0f;
    [Header("Default Unity old input system mouse movement needed is: Mouse X")]
    [Tooltip("If using the old input system make sure you have an axes that controls the horizontal mouse movement")]
    [SerializeField] private string mouseXInput = "Mouse X";

    /// <summary>
    /// Based on player position and mouse position, want to rotate the player accordingly
    /// </summary>
    private void MouseTurningPlayer()
    {
        float x = Input.GetAxis(mouseXInput);

        player.transform.Rotate(0f, x, 0f);
    }

    void Update()
    {
        //if inventory is closed allow the player to rotate
        if (UIEventBroker.TriggerOnCheckInventoryStatus() == false) MouseTurningPlayer();
    }
}
