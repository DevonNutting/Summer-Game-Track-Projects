using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   private Rigidbody2D _rb;


   [SerializeField, Tooltip("The amount of force the player jumps up with")]
   private float _jumpForce = 7;


   // Start is called before the first frame update
   void Start()
   {
       _rb = GetComponent<Rigidbody2D>();
   }


   // Update is called once per frame
   void Update()
   {
       if (Input.GetButtonDown("Jump"))  
       {
           _rb.velocity = new Vector3(0, _jumpForce, 0);
       }
   }
}



