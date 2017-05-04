﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRigidbody2D;

    private float jumpForce;
    private float speed;

    private bool isJumping;

    // Use this for initialization
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        jumpForce = 500;
        speed = 8;
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            playerRigidbody2D.AddForce(new Vector2(0, jumpForce));
            isJumping = false;
        }
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * speed);

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
            isJumping = false;
    }

}

