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
   private BoxCollider2D _bCollider;


   [SerializeField, Tooltip("The amount of force the player jumps up with")]
   private float _jumpForce = 8f;


   [SerializeField, Tooltip("Multiplier for player movement speed")]
   private float _moveSpeed = 5f;


   [SerializeField, Tooltip("The layer on wich the ground sits")]
   private LayerMask _groundLayer;


   // Start is called before the first frame update
   void Start()
   {
       _rb = GetComponent<Rigidbody2D>();
       _anim = GetComponent<Animator>();
       _sRenderer = GetComponent<SpriteRenderer>();
       _bCollider = GetComponent<BoxCollider2D>();
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
       _rb.velocity = new Vector2(_xInput * _moveSpeed, _rb.velocity.y); // Apply the xInput to the velocity of the player & maintain the player's vertical velocity (jumping/fallig)
       // JUMP
       if (Input.GetButtonDown("Jump") && isGrounded()) // If the player presses the jump button & the player is on the ground...
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


   private bool isGrounded()
   {
       // Returns true or false depending if the player is currently on the ground or not
       return Physics2D.BoxCast(_bCollider.bounds.center, _bCollider.bounds.size, 0f, Vector2.down, .1f, _groundLayer);
   }

}





