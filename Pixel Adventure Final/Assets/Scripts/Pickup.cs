using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // When this collides with another collider
   private void OnTriggerEnter2D(Collider2D other)
   {
        // If the collider this collided with has the tag "Player"...
        if (other.CompareTag("Player"))
        {
            // ... Destroy this gameObject
            Destroy(gameObject);

            // Tell the Game Manager to gain points & update the score text
            GameManager.Instance.UpdateScore(100);
        } 
   }
}
