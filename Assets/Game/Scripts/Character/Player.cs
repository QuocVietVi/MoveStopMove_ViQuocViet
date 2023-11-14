using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : Character
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float rotateSpeed;


    private Vector3 moveVector;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<FloatingJoystick>();
        isDead = false;
    }

    private void FixedUpdate()
    {
        Move();
        Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());

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
            canAttack = true;


        } else
        {
            if (isDead == false)
            {
                ChangeAnim(ConstantAnim.IDLE);
            }
            if (canAttack == true && target != null)
            {
                Invoke(nameof(Attack), 0.5f);
            }
            canAttack = false;
        }

        //rb.MovePosition(rb.position + moveVector);
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }

    protected override void AttackRange()
    {
        base.AttackRange();
        if (isDead == false)
        {
            if (enemyInRange[0] != this.GetComponent<Collider>())
            {
                target = enemyInRange[0].transform;
            }
            else if (enemyInRange[0] == this.GetComponent<Collider>() && enemyInRange[1] != null)
            {
                target = enemyInRange[1].transform;
            }
            else
            {
                target = null;
            }
        }
        
    }

    protected override void OnDead()
    {
        base.OnDead();
        moveSpeed = 0.0f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstantTag.WEAPON))
        {
            if (other.GetComponent<Bullet>().attacker == this)
            {
                return;
            }
            OnDead();
        }
    }







}
