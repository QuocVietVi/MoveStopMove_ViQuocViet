using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] protected float gravityScale;
    [SerializeField] private Animator anim;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float range;
    [SerializeField] private Collider[] enemies;
    [SerializeField] private LayerMask enemyLayer;

    public Transform target;
    protected Rigidbody rb;
    Collider[] enemyInRange;
    Collider[] enemyOutRange;


    private string currentAnimName;

    private void Update()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        AttackRange();
    }

    protected void Attack()
    {
        this.transform.LookAt(target.position);
        Bullet bullet = Instantiate(bulletPrefab, throwPoint.position, throwPoint.rotation);
        //bullet.rb.velocity = throwPoint.forward * bulletSpeed;
        Vector3 f = target.position - bullet.transform.position;
        f = f.normalized;
        f = f * bulletSpeed;
        bullet.rb.AddForce(f);



        ChangeAnim(ConstantAnim.ATTACK);
    }

    private void AttackRange()
    {
        int maxEnemyInRange = 1;
        enemyInRange = new Collider[maxEnemyInRange];
        int numEnemies = Physics.OverlapSphereNonAlloc(this.transform.position, range, enemyInRange, enemyLayer);
        if (numEnemies > 0)
        {
            for (int i = 0; i < enemyInRange.Length; i++)
            {
                target = enemyInRange[i].transform;
            }
        }
        else
        {
            target = null;
        }

        //enemyOutRange = enemies.Except(enemyInRange).ToArray();

        //foreach (var enemy in enemyOutRange)
        //{
        //    //target = null;
        //}
    }

    public void OnDead()
    {
        ChangeAnim(ConstantAnim.DEAD);
    }



    protected void ChangeAnim(string animName)
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
