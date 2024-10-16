using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    #region Variables
    public GameObject target; // Reference to the game object for the camera to follow

    // A Serialized Field keeps a field/variable private while still allowing it to be modified in the Unity Engine
    [SerializeField] Vector3 positionOffset; // Position offset between the target & camera
    private float initialY; // To store the initial y position of the camera
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Calculates the initial offset between the target & camera position
        positionOffset = transform.position - target.transform.position;
        // Store the initial y position of the camera
        initialY = transform.position.y;
    }

    // LateUpdate is called once per frame, after every other function is called
    void LateUpdate()
    {
        // Move the camera to follow the player, using the offset to maintain the correct distance
        if (target != null) // If the camera's target variable is not empty, meaning it has a set object to follow
        {
            // Calculate the new position while maintaining the initial y position
            Vector3 newPosition = target.transform.position + positionOffset;
            newPosition.y = initialY;

            // Update the camera's position
            transform.position = newPosition;
        }
    }
}
