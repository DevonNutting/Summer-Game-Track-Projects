using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // OnTriggerEnter is a function that is called when a collider marked as a trigger collides with this object
    {
        // If The object this pickup was just touched by was the player: destory this pickup object
        if (other.gameObject.CompareTag("Player")) Destroy(this.gameObject);
        GameManager.Instance.UpdateScore(1); // Call the 'UpdateScore()' function from the GameManager script
    }
}
