using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] private float horizontal = 0f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private Animator anim;
    private SpriteRenderer sprite;
    private enum MovementState {idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;
    private MovementState state = MovementState.idle;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W ) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded() )
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpingPower);
            jumpSoundEffect.Play();
        }
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if(horizontal > 0f)
        {
            sprite.flipX = false;
            state = MovementState.running;
        }
        else if(horizontal < 0f)
        {
            sprite.flipX = true;;
            state = MovementState.running;
        }
        else 
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state",(int)state);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,0.5f,groundLayer);
    }
}
