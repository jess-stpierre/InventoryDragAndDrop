///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// Allows the player to rotate based on mouse position on screen
/// </summary>
public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    private float angle = 0f;

    /// <summary>
    /// Helper function
    /// </summary>
    private float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(b.x - a.x, b.y - a.y) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Based on player position and mouse position, want to rotate the player accordingly
    /// </summary>
    private void MouseTurningPlayer()
    {
        Vector2 positionOnScreen = cam.WorldToViewportPoint(player.transform.position);
        Vector2 mouseOnScreen = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);
        angle = AngleBetweenPoints(positionOnScreen, mouseOnScreen);

       if(Vector2.Distance(positionOnScreen, mouseOnScreen) > 0.1f)
	   {
            player.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
       }
    }

    void Update()
    {
        //if inventory is closed allow the player to rotate
        if (UIEventBroker.TriggerOnCheckInventoryStatus() == false) MouseTurningPlayer();
    }
}
