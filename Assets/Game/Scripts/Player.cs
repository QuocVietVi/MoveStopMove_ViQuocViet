using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float gravityScale;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float range;
    [SerializeField] private Collider[] enemies;
    [SerializeField] private LayerMask enemyLayer;

    public Transform target;
    private Rigidbody rb;
    private Vector3 moveVector;
    private bool CanAttack;
    Collider[] enemyInRange;
    Collider[] enemyOutRange;


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
        AttackRange();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed;
        moveVector.z = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            ChangeAnim("Run");
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.fixedDeltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            CanAttack = true;
            
            
        } else
        {
            ChangeAnim("Idle");
            if (CanAttack == true && target != null)
            {
                Invoke(nameof(Attack), 0.5f);
            }
            CanAttack = false;
        }

        //rb.MovePosition(rb.position + moveVector);
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }

    private void Attack()
    {
        this.transform.LookAt(target.position);
        Bullet bullet = Instantiate(bulletPrefab, throwPoint.position,throwPoint.rotation);
        //bullet.rb.velocity = throwPoint.forward * bulletSpeed;
        Vector3 f = target.position - bullet.transform.position;
        f = f.normalized;
        f = f * bulletSpeed;
        bullet.rb.AddForce(f);



        ChangeAnim("Attack");
    }

    private void AttackRange()
    {
        int maxEnemyInRange = 1;
        enemyInRange = new Collider[maxEnemyInRange];
        int numEnemies = Physics.OverlapSphereNonAlloc(this.transform.position, range,enemyInRange,enemyLayer);
        if(numEnemies > 0 )
        {
            for (int i = 0; i < enemyInRange.Length; i++)
            {
                target = enemyInRange[i].transform;
            }
        } else
        {
            target = null;
        }

        //enemyOutRange = enemies.Except(enemyInRange).ToArray();

        //foreach (var enemy in enemyOutRange)
        //{
        //    //target = null;
        //}
    }



    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}
