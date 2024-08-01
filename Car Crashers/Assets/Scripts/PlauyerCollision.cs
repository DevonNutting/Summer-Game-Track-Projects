using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlauyerCollision : MonoBehaviour
{
    public PlayerController playerController;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle")) playerController.enabled = false;
    }
}
