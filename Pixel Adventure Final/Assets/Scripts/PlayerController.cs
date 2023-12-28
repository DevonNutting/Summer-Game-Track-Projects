using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   private enum MovementState
   {
       idle,
       running,
       jumping,
       falling,
   }


   private Rigidbody2D _rb;
   private Animator _anim;
   private SpriteRenderer _sRenderer;
   private float _xInput;


   [SerializeField, Tooltip("The amount of force the player jumps up with")]
   private float _jumpForce = 8f;


   [SerializeField, Tooltip("Multiplier for player movement speed")]
   private float _moveSpeed = 5f;


   // Start is called before the first frame update
   void Start()
   {
       _rb = GetComponent<Rigidbody2D>();
       _anim = GetComponent<Animator>();
       _sRenderer = GetComponent<SpriteRenderer>();
   }


   // Update is called once per frame
   void Update()
   {
       HandleMovement();
       HandleAnimation();
   }


   private void HandleMovement()
   {
       // MOVEMENT
       _xInput = Input.GetAxisRaw("Horizontal"); // Store movement input from the user (LEFT & RIGHT)
       _rb.velocity = new Vector2(_xInput * _moveSpeed, _rb.velocity.y); // Apply the xInput to the velocity of the player & maintain the player's vertical velocity (jumping/falling)
       // JUMP
       if (Input.GetButtonDown("Jump")) // If the player presses the jump button...
       {
           _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce); // Maintain the player's horizontal velocity & add a velocity upward
       }
   }


   private void HandleAnimation()
   {
       MovementState moveState;


       switch (_xInput)
       {
           case 0: // IDLE
               moveState = MovementState.idle;
               break;
           case > 0: // RUNNING RIGHT
               moveState = MovementState.running;
               _sRenderer.flipX = false;
               break;
           case < 0: // RUNNING LEFT
               moveState = MovementState.running;
               _sRenderer.flipX = true;
               break;
           default:
               moveState = MovementState.idle;
               break;
       }


       switch (_rb.velocity.y)
       {
           case > .01f: // JUMPING
               moveState = MovementState.jumping;
               break;
           case < -.01f: // FALLING
                moveState = MovementState.falling;
                break;
           default:
               break;
       }


       _anim.SetInteger("moveState", (int)moveState);
   }
}



