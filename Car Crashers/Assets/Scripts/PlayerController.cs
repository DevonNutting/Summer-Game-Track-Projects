using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody _rb;
    [SerializeField] private float _forwardForce = 2000f;
    [SerializeField] private float _sidewaysForce = 500f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() 
    {
        HandleMovement();     
    }

    private void HandleMovement()
    {
        _rb.AddForce(0, 0, _forwardForce * Time.deltaTime, ForceMode.Acceleration); // Move the car forward

        if (Input.GetAxis("Horizontal") > 0) _rb.AddForce(_sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange); // Move the player right
        if (Input.GetAxis("Horizontal") < 0) _rb.AddForce(-_sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange); // Move the player left

    }
}
