using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharName
{
    Hoang_Long = 0,
    Hung_Duc = 1,
    The_Manh = 2,
    Dai_Fuma = 3,
    Tuan_Dat = 4,
    Tho_Khiem = 5,
    Tony_Do = 6,
    Bach_Beo = 7,
    Phuong_Be = 8,
    Viet_Dep_Zai = 9
}
[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] protected float gravityScale;
    [SerializeField] private Animator anim;
    [SerializeField] protected GameObject weapon;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] protected Collider collider;
    
    
    //[SerializeField] private Collider[] enemies;
    [SerializeField] private LayerMask enemyLayer;
    //[SerializeField] private GameObject targetPoint;
    public Transform throwPoint;
    public float range;
    public float level;
    public Transform target;
    protected Rigidbody rb;
    protected Collider[] enemyInRange;
    protected bool canAttack;
    protected bool isDead;
    
    //Collider[] enemyOutRange;

    
    private string currentAnimName;

    private void Start()
    {
        level = 1;
    }
    private void Update()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        AttackRange();
        

    }

    public void Attack()
    {
        if (target != null)
        {
            weapon.SetActive(false);
            Invoke(nameof(ActiveWeapon), 0.5f);
            this.transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            
            //Bullet bullet = Instantiate(bulletPrefab, throwPoint.position, throwPoint.rotation);
            Bullet bullet = LeanPool.Spawn(bulletPrefab, throwPoint.position, throwPoint.rotation);
            bullet.rb.velocity = (target.position - this.transform.position) * bulletSpeed * Time.fixedDeltaTime;
            bullet.attacker = this;
            bullet.OnInit();
            //Vector3 f = target.position - bullet.transform.position;
            //f = f.normalized;
            //f = f * bulletSpeed;
            //bullet.rb.AddForce(f);
        }
       
        ChangeAnim(ConstantAnim.ATTACK);
    }

    private void ActiveWeapon()
    {
        weapon.SetActive(true);
    }

    protected virtual void AttackRange()
    {
        int maxEnemyInRange = 2;
        enemyInRange = new Collider[maxEnemyInRange];
        int numEnemies = Physics.OverlapSphereNonAlloc(this.transform.position, range, enemyInRange, enemyLayer);
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
        canAttack = false;
        isDead = true;
        gravityScale = 0;
        collider.enabled = false;
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
