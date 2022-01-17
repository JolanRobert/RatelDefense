using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    [Header("Variables")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private Rigidbody rb;

    private float jumpTimeCounter;
    private bool isJumping;
    private bool isGrounded;
    private Vector3 moveAxis;

    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
    }


    public  void Move(float z, float x)
    {
        int variable;

        /*if (key == KeyCode.Z)
        {
            rb.velocity += transform.forward*moveSpeed ;
        }

        if (key == KeyCode.S)
        {
            rb.velocity += -transform.forward*moveSpeed ;
        }

        if (key == KeyCode.Q)
        {
            rb.velocity += -transform.right*moveSpeed ;
        }

        if (key == KeyCode.D)
        {
            rb.velocity += transform.right*moveSpeed ;
        }*/
        rb.velocity = transform.forward * moveSpeed * z + transform.right * moveSpeed * x;
    }
}
