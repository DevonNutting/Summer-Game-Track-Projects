using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    // When this object collides with another object
   private void OnTriggerEnter2D(Collider2D other)
   {
        // If the object that collided with this has the tag "Player" 
        if (other.CompareTag("Player"))
        {
            // Tell the Audio Manager to play some victory music
            AudioManager.Instance.PlaySound("VictoryTheme");

            // Tell the Audio Manager to stop the main theme music
            AudioManager.Instance.StopSound("MainTheme");

            // Tell the Game Manager the player completed this level
            GameManager.Instance.LevelComplete();

            // Disable the Box Collider 2D component on this to prevent further collisions after the level is complete
            GetComponent<BoxCollider2D>().enabled = false;
        }
   }
}
