using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    private float angle = 0f;

    private float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(b.x - a.x, b.y - a.y) * Mathf.Rad2Deg;
    }

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
        if (UIEventBroker.TriggerOnCheckInventoryStatus() == false) MouseTurningPlayer();
    }
}
