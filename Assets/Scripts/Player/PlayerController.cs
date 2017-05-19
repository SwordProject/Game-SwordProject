using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRigidbody2D;
    private float jumpForce;
    private float speed;
    public bool isJumping;
    public Transform startLineCast;
    public Transform endLineCast;

    private Animator myAnimator;
    [SerializeField]
    private float movementSpeed;
    private bool facingRight;
    private bool attack;

    // Use this for initialization
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        jumpForce = 400;

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        raycast();
        if (!isJumping)
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerRigidbody2D.AddForce(new Vector2(0, jumpForce));
                isJumping = true;
            }
            speed = 8;
        }else
            speed = 4;
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * speed);

        HandleInput();
    }

    private void raycast()
    {
        if (Physics2D.Linecast(startLineCast.position,endLineCast.position))
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
    }

    void Fixedupdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleAttacks();
    }
    private void HandleMovement(float horizontal)
    {
        playerRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody2D.velocity.y);
    }
    private void HandleAttacks()
    {
        if (attack)
        {
            myAnimator.SetTrigger("attack");
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }
    private void ResetValues()
    {
        attack = false;
    }

}

