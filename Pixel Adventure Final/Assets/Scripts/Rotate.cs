using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotate : MonoBehaviour
{
   [SerializeField][Tooltip("The amount of full rotations this will make per second")]
   private float _rotateSpeed = 2f;

   [SerializeField, Tooltip("Flag determining which direction this spins, True for clockwise rotation")]
   private bool _rotateClockwise;

   // Update is called once per frame
   void Update()
   {
        if (_rotateClockwise) transform.Rotate(0, 0, -360 * _rotateSpeed * Time.deltaTime);
        else transform.Rotate(0, 0, 360 * _rotateSpeed * Time.deltaTime);
       
   }
}



