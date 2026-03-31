using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent; // Declare an empty variable of type NavMeshAgent named 'agent'
    public Transform target; // Declare an empty variable of type Transform named 'target'
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // Store the NavMeshAgent component of this enemy in the agent variable
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) // Check if the taregt variable is empty (null) or not; if it is not empty... 
        {
            agent.SetDestination(target.position); // ...Set this enemy's destination to its target's position
        }
        
    }

    public void OnTriggerEnter(Collider other) // OnTriggerEnter is a function that is called when a collider marked as a trigger collides with this object
    {
        if (other.gameObject.CompareTag("Player")) // if the object this enemy collided with has the tag 'Player'...
        {
            AudioManager.Instance.PlaySound("Player Death"); // ...Tell the AudioManager to play the SFX for the player dying.
            Destroy(other.gameObject); // ... and destroy the player
            GameManager.Instance.GameOver(); // ... and call the GameOver() function in the GameManager script
        }
    }
}


