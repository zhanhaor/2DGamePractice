using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rbPlayer;

    public float speedPlayer;
    public float jumpForcePlayer;


    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("State Check")]
    public bool isGround;
    public bool isJump;
    public bool canJump;

    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
    }

    public void FixedUpdate()
    {
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {
        if(Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // not include float.
        // GetAxis -1~ +1 include float
        rbPlayer.velocity = new Vector2(horizontalInput * speedPlayer, rbPlayer.velocity.y);

        if (horizontalInput !=0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    void Jump()
    {
        if (canJump)
        {
            isJump = true;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0,-0.45f,0);
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpForcePlayer);
            //rbPlayer.gravityScale = 5;
            canJump = false;
        }
        
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position,checkRadius,groundLayer);
        if(isGround)
        {
            rbPlayer.gravityScale = 1;
            isJump = false;
        }
        else
        {
            rbPlayer.gravityScale = 5;
        }
    }

    public void LandFX()//anim event
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
