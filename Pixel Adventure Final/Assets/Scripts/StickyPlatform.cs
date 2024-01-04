using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StickyPlatform : MonoBehaviour
{
   // When this collides with another game object
   private void OnCollisionEnter2D(Collision2D other)
   {
       // If the name of this game Object this collided with is "Player"...
       if (other.gameObject.name == "Player")
       {
           // Set the player as the child of the platform, makign them stay on the moving platform
           other.gameObject.transform.SetParent(transform);
       }
   }


   //  When an object stops colliding with this...
   private void OnCollisionExit2D(Collision2D other)
   {
       // If the name of this game Object this collided with is "Player"...
       if (other.gameObject.name == "Player")
       {
           // Unparent the Plyer character from this platform
           other.gameObject.transform.SetParent(null);
       }
   }
}



