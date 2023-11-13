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
    //[SerializeField] private Collider[] enemies;
    [SerializeField] private LayerMask enemyLayer;
    //[SerializeField] private GameObject targetPoint;

    public Transform target;
    protected Rigidbody rb;
    protected Collider[] enemyInRange;
    //Collider[] enemyOutRange;


    private string currentAnimName;



    private void Update()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        AttackRange();
    }

    public void Attack()
    {
        if (target != null)
        {
            this.transform.LookAt(target.position);
            Bullet bullet = Instantiate(bulletPrefab, throwPoint.position, throwPoint.rotation);
            //bullet.rb.velocity = throwPoint.forward * bulletSpeed;
            bullet.attacker = this;

            Vector3 f = target.position - bullet.transform.position;
            f = f.normalized;
            f = f * bulletSpeed;
            bullet.rb.AddForce(f);
        }
       



        ChangeAnim(ConstantAnim.ATTACK);
    }

    protected virtual void AttackRange()
    {
        int maxEnemyInRange = 2;
        enemyInRange = new Collider[maxEnemyInRange];
        int numEnemies = Physics.OverlapSphereNonAlloc(this.transform.position, range, enemyInRange, enemyLayer);
        Player player = GetComponent<Player>();
        if (numEnemies > 0 )
        {
            target = enemyInRange[0].transform;
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

    protected virtual void OnDead()
    {
        ChangeAnim(ConstantAnim.DEAD);
        Destroy(gameObject, 1f);
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
