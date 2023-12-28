using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to a Transfrom component this camera will follow.")]
    private Transform _target;

    private void LateUpdate()
    {
        // Update the position of this to the X and Y position of the target
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
