using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField, Tooltip("A list of GameObjects set as waypoints for this")]
    private GameObject[] _waypoints;

    [SerializeField, Tooltip("The current index of the waypoint object this is following")]
    private int _waypointIndex = 0;

    [SerializeField, Tooltip("The speed in which this will move")]
    private float _moveSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        // If the distance between this & the current waypoint is below .1f units... (If the platform has reached its current waypoint)
        if (Vector2.Distance(_waypoints[_waypointIndex].transform.position, transform.position) < .1f)
        {
            _waypointIndex ++; // Increment to the next waypoint in the array

            if (_waypointIndex > _waypoints.Length - 1) _waypointIndex = 0; // If the current index has reached the end of the array, reset it back to 0
        }

        // Move this towards the position of the current waypoint object
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_waypointIndex].transform.position, Time.deltaTime * _moveSpeed);
    }
}
