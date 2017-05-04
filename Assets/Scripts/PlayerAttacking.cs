using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
            [SerializeField]    
    private float movementSpeed;
    private bool facingRight;
    private bool attack;


    //Inicialização
    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        HandleInput();
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
        myRigidbody.velocity = new Vector2( horizontal * movementSpeed, myRigidbody.velocity.y ); 
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
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            attack = true;
        }        
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal <0 && facingRight)
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