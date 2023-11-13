using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : Character
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float rotateSpeed;

    private bool CanAttack;
    private Vector3 moveVector;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    private void FixedUpdate()
    {
        Move();

    }

    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed;
        moveVector.z = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            ChangeAnim(ConstantAnim.RUN);
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.fixedDeltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            CanAttack = true;
            
            
        } else
        {
            ChangeAnim(ConstantAnim.IDLE);
            if (CanAttack == true && target != null)
            {
                Invoke(nameof(Attack), 0.5f);
            }
            CanAttack = false;
        }

        //rb.MovePosition(rb.position + moveVector);
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }



    
}
