using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    #region Variables

    public float movementSpeed = 5f; // Variable to control how fast the player moves
    public float jumpForce = 10f; // The force with which the player jumps
    public float gravity = 20f; // The force of gravity affecting the player

    public CharacterController controller; // Empty reference to the CharacterController component on the player
    private Vector3 moveDirection = Vector3.zero; // A vector (x, y, z) to dictate the direction the player moves in
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component attached to the player
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Stores the Horizontal (Left & Right) input of the player
        float verticalInput = Input.GetAxis("Vertical"); // Stores the Vertical (Forward & Backward) input of the player

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput); // Calculate the direction the player should move based on the input
        movement = transform.TransformDirection(movement); // Convert the direction into local space
        movement *= movementSpeed; 

        if (controller.isGrounded) // If the player is on the ground...
        {
            moveDirection = movement; // Apply the movement vector to the player

            if (Input.GetButton("Jump")) // If the player presses the jump button...
            {
                moveDirection.y = jumpForce; // Apply the jumpforce into vertical movement for the player
                AudioManager.Instance.PlaySound("Player Jump"); // Tell the Audio Manager to play the jump SFX when the player jumps
            }
        } 
        else // Meaning the player is NOT grounded and therefore in the air... 
        {
            moveDirection.y -= gravity * Time.deltaTime; // Apply gravity to the player if they are not on the ground
        }

        controller.Move(moveDirection * Time.deltaTime); // Move the player with the characterController component
    }
}


