// M.G.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Transform> waypoints;
    bool arrived;
    [SerializeField]Transform target;
    public float speed = 7;
    private int waypointIndex = 0; // Which waypoint is it currently at
    public int startHealth = 100;
    public int currentHealth;
    public GameObject FastPrefab, SlowPrefab;

    public AudioClip[] DieSounds;
    public AudioClip DestinationSound;
    
    public void Initialize()
    {
        currentHealth = startHealth;

        foreach (Transform child in FindObjectOfType<GameManager>().WaypointsParent.GetComponentsInChildren<Transform>())
        {
            if (child.name != "ENEMY WAYPOINTS") waypoints.Add(child);
        }
    }

    private void Update() // This update method should only be active while the EnemyFollowMode is FollowPath
    {
        
        /// FOLLOW PATH ----- 
        if (target == null) target = waypoints[0];

        Vector3 nextWaypoint = target.position - transform.position; // Set the next waypoint that the enemy will walk to.

        transform.Translate(nextWaypoint.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= .75f) // The enemy will know to go to the next waypoint when it is 1.2 meters away from the current waypoint (target).
        {
            GetNextWaypoint();
            Debug.Log("Reached Waypoint");
        }

        if (nextWaypoint != Vector3.zero) transform.LookAt(target.transform.position); // Make the enemy look towards the next waypoint

        if (currentHealth <= 0)
        {
            FindObjectOfType<GameManager>().enemiesLeft--; // Subtract from the list of enemies alive.
            FindObjectOfType<GameManager>().score += Random.Range(40, 100);

            int chosenSound = Random.Range(0, DieSounds.Length);
            FindObjectOfType<GameManager>().CoreFXPlayer.PlayOneShot(DieSounds[chosenSound]);

            Destroy(gameObject);
        }
    }

    private void GetNextWaypoint() // Update to tell the enemy where the next waypoint is, and set it to be the next "target".
    {
        if (waypointIndex >= waypoints.Count - 1)
        {

            EndPath();
            return;
        }

        //  Debug.Log("<color=green>Next waypoint</color>");

        waypointIndex++;
        target = waypoints[waypointIndex];
    }

    void EndPath() // If this is the final waypoint, the enemy will destroy itself, and cause damage to the generator.
    {
        Debug.Log($"<color=blue>[Enemy] </color> Enemy made it to the tower!");

        FindObjectOfType<GameManager>().CoreFXPlayer.PlayOneShot(DestinationSound);

        FindObjectOfType<GameManager>().enemiesLeft--; // Subtract from the list of enemies alive.
        FindObjectOfType<GameManager>().enemiesGotIn++;

        Destroy(gameObject);
    }
}
