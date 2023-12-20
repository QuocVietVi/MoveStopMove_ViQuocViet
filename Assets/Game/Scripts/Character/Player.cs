using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : Character
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float rotateSpeed;

    private Transform playerTf;
    private Vector3 moveVector;
    public float gold;


    

    private void Awake()
    {

       
        playerTf = this.transform;

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            Move();
        }
        //Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
    }

    public override void OnInit()
    {
        base.OnInit();
        this.level = 1;
        this.moveSpeed = 10;
        this.Range = 5;
        this.moveSpeed += pantData.moveSpeed;
        this.Range += weaponData.range + hatData.range;
        this.joystick = GameManager.Instance.floatingJoystick;
        joystick.enabled = true;
        gold = GameManager.Instance.PlayerData.golds;
        ChangeAnim(ConstantAnim.IDLE);
    }
    protected override void OnDead()
    {
        base.OnDead();
        moveSpeed = 0.0f;
        joystick.enabled = false;

    }


    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed;
        moveVector.z = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            ChangeAnim(ConstantAnim.RUN);
            Vector3 direction = Vector3.RotateTowards(playerTf.forward, moveVector, rotateSpeed * Time.fixedDeltaTime, 0.0f);
            playerTf.rotation = Quaternion.LookRotation(direction);
            canAttack = true;


        } else
        {
            if (isDead == false)
            {
                ChangeAnim(ConstantAnim.IDLE);
            }
            if (canAttack == true && target != null)
            {
                Invoke(nameof(Attack), weaponData.resetAttack);
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
            //Enemy enemy = target.GetComponent<Enemy>();
            if (enemyInRange[0] != this.collider)
            {
                target = enemyInRange[0].transform;
            }
            else if (enemyInRange[0] == this.collider && enemyInRange[1] != null)
            {
                target = enemyInRange[1].transform;
            }
            else
            {
                target = null;
            }
            //if (enemy != null)
            //{
            //    //if (target != null)
            //    //{
            //    //    enemy.ActiveTargetPoint();
            //    //}
            //    //else
            //    //{
            //    //    enemy.DeActiveTargetPoint();
            //    //}
            //    enemy.targetPoint.SetActive(true);
            //}
            //else
            //{
            //    return;
            //}
           
        }
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstantTag.WEAPON))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.attacker == this)
            {
                return;
            }
            else {
                OnDead();
                Dead?.Invoke();
                bullet.OnDespawn();
                UIManager.Instance.killerName.text = bullet.attacker.charName;
            }
        }
    }

    






}
