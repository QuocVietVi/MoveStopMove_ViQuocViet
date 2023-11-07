using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float gravityScale;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Animator anim;

    private Rigidbody rb;
    private Vector3 moveVector;


    private string currentAnimName;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    private void FixedUpdate()
    {
        Move();
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.A))
        {
            ChangeAnim("Attack");
        }
    }

    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed;
        moveVector.z = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.fixedDeltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            ChangeAnim("Run");
            
        } else
        {
            ChangeAnim("Idle");
        }

        //rb.MovePosition(rb.position + moveVector);
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
