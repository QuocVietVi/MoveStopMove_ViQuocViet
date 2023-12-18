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
    [SerializeField] private Animator anim;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] protected float gravityScale;
    [SerializeField] protected Weapon currentWeapon;
    [SerializeField] protected Collider collider;
    [SerializeField] protected GameObject weaponOnHand;
    [SerializeField] protected Rigidbody rb;
    public SkinnedMeshRenderer pant;
    
    
    //[SerializeField] private Collider[] enemies;
    //[SerializeField] private GameObject targetPoint;
    private float range;
    private string currentAnimName;
    private Weapon w;
    private GameObject hat;
    private GameObject shield;
    protected Collider[] enemyInRange;
    protected bool canAttack;
    protected bool isDead;
    public Transform throwPoint;
    public float level;
    public string charName;
    public Transform target;
    public WeaponData weaponData;
    public WeaponType weaponType;
    public SkinData pantData;
    public PantType pantSkin;
    public HatData hatData;
    public HatType hatType;
    public Transform hatHolder;
    public ShieldData shieldData;
    public Transform shieldHolder;
    public float Range
    {
        get => range; set
        {
            range = value;
        }
    }
    
    //Collider[] enemyOutRange;

    


    private void Start()
    {
        level = 1;
        OnInit();
        this.charName = PlayerInfoManager.Instance.charName.ToString();

    }


    private void Update()
    {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            AttackRange();
        }

    }
    public virtual void OnInit()
    {
        isDead = false;
        collider.enabled = true;
        gravityScale = 9;

        weaponData = GameManager.Instance.GetWeponData((WeaponType)GameManager.Instance.PlayerData.weaponEquipped);
        currentWeapon = weaponData.weapon;
        bulletSpeed = weaponData.speed;
        LeanPool.Despawn(w);
        w = LeanPool.Spawn(currentWeapon, weaponOnHand.transform);
        pantData = GameManager.Instance.GetPantData((PantType)GameManager.Instance.PlayerData.pantEqipped);
        pant.material = pantData.material;
        hatData = GameManager.Instance.GetHatData((HatType)GameManager.Instance.PlayerData.hatEqipped);
        DespawnHat();
        if (hatData.hatPrefab != null)
        {
            hat = LeanPool.Spawn(hatData.hatPrefab,hatHolder);
        }
        shieldData = GameManager.Instance.GetShieldData((ShieldType)GameManager.Instance.PlayerData.shieldEqipped);
        DespawnShield();
        if (shieldData.shieldPrefab != null)
        {
            shield = LeanPool.Spawn(shieldData.shieldPrefab,shieldHolder);
        }


    }
    public void Despawn()
    {
        LeanPool.Despawn(this);
        //LeanPool.Despawn(w);
    }

    public void DespawnHat()
    {
        if (hat != null)
        {
            LeanPool.Despawn(hat);
        }
    }

    public void DespawnShield()
    {
        if (shield != null)
        {
            LeanPool.Despawn(shield);
        }
    }
    public void Attack()
    {
        if (target != null)
        {
            weaponOnHand.SetActive(false);
            Invoke(nameof(ActiveWeapon), weaponData.resetAttack);
            this.transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            bulletPrefab = weaponData.bullet;
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
        weaponOnHand.gameObject.SetActive(true);
    }

    protected virtual void AttackRange()
    {
        int maxEnemyInRange = 2;
        enemyInRange = new Collider[maxEnemyInRange];
        int numEnemies = Physics.OverlapSphereNonAlloc(this.transform.position, Range, enemyInRange, enemyLayer);
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
        //Destroy(gameObject, 1f);
    }



    public void ChangeAnim(string animName)
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
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }
}
