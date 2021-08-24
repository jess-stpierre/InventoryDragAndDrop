using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed of movement for all axis")]
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [Header(" ")]
    [Header("Default movement is WASD, changeable below")]
    [SerializeField] KeyCode forwards = KeyCode.W;
    [SerializeField] KeyCode backwards = KeyCode.S;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode left = KeyCode.A;

    private void Update()
    {
        Movement();
    }

    /// <summary>
    /// Based on X and Z values that we got from the inputs we than call the Move function for it to perform the movement for us.
    /// </summary>
    private void Movement()
    {
        float zDirection = DirectionOutput(forwards, backwards);
        float xDirection = DirectionOutput(right, left);
        Vector3 newDir = new Vector3(xDirection, 0f, zDirection);
        Move(newDir.normalized);
    }

    /// <summary>
    /// Create a new movement vector so we can move our rigidbody attached to the player.
    /// </summary>
    private void Move(Vector3 direction)
    {
        Vector3 move = direction * speed * Time.deltaTime;
        rb.MovePosition(this.transform.position + move);
    }

    /// <summary>
    /// Detects which direction we are going between positive and negative inputs.
    /// </summary>
    private float DirectionOutput(KeyCode posButton, KeyCode negButton)
    {
        float DirOutput = 0f;

        if (Input.GetKey(posButton) && Input.GetKey(negButton)) DirOutput = 0f;
        else if (Input.GetKey(posButton) || Input.GetKey(negButton))
        {
            if (Input.GetKey(posButton)) DirOutput = -1f;
            if (Input.GetKey(negButton)) DirOutput = 1f;
        }
        else DirOutput = 0f;

        return DirOutput;
    }
}
