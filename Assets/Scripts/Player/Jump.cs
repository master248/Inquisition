﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    /*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
    public bool grounded;
    public bool start;
    public bool mid;
    public bool end;
    public LayerMask whatIsGround;
    public bool stoppedJumping;

    /*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    public Transform groundCheck;
    public float groundCheckRadius;

    //You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :)
    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
        start = false;
        mid = false;
        end = false;
    }

    void Update()
    {
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);


        //if we are grounded...
        if (grounded)
        {
            //the jumpcounter is whatever we set jumptime to in the editor.
            jumpTimeCounter = jumpTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            start = true;
        }
        if ((Input.GetButton("Jump")) && !stoppedJumping)
        {
            mid = true;
        }
        if(Input.GetButtonUp("Jump"))
        {
            end = true;
        }
    }

    void FixedUpdate()
    {
        //I placed this code in FixedUpdate because we are using phyics to move.

        //if you press down the mouse button...
        if(start)
        {
            //and you are on the ground...
            if (grounded)
            {
                //jump!
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0.0f);
                stoppedJumping = false;
                start = false;
            }
        }

        //if you keep holding down the mouse button...
        if (mid)
        {
            //and your counter hasn't reached zero...
            if (jumpTimeCounter > 0)
            {
                //keep jumping!
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0.0f);
                jumpTimeCounter -= Time.deltaTime;
                mid = false;
            }
        }


        //if you stop holding down the mouse button...
        if (end)
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
            stoppedJumping = true;
            end = false;
            mid = false;
        }
    }
}