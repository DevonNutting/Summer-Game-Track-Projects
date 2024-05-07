using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        
    }

    public void OnTriggerEnter(Collider other) // OnTriggerEnter is a function that is called when a collider marked as a trigger collides with this object
    {
        if (other.gameObject.CompareTag("Player")) // if the object this enemy collided with is the player
        {
            Destroy(other.gameObject); // Destroy the player
            GameManager.Instance.GameOver();
        }
    }
}


