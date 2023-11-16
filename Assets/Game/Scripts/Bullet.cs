using Lean.Pool;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private Transform bullet;
    [SerializeField] private float rotateSpeed;
    public Character attacker;
    // Start is called before the first frame update
    void Start()
    {
        //attacker = GetComponent<Character>();
        //OnInit();
    }

    private void Update()
    {
        bullet.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.Self);
    }

    public void OnInit()
    {
        Invoke(nameof(OnDespawn), 0.7f);
    }

    public void OnDespawn()
    {
        LeanPool.Despawn(this);
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Character victim = other.GetComponent<Character>();

    //    if (victim == null && victim == attacker)
    //    {
    //        return;
    //    }
    //    if (other.CompareTag(ConstantTag.ENEMY) || other.CompareTag(ConstantTag.PLAYER))
    //    {
    //        OnDespawn();
    //        //Destroy(gameObject);
    //        //victim.OnDead();
    //        //Destroy(victim.gameObject, 2f);

    //    }
    //    //else
    //    //{
    //    //    return;
    //    //}

    //    //if (other.CompareTag(ConstantTag.ENEMY))
    //    //{
    //    //    Destroy(gameObject);
    //    //    Character enemy = other.GetComponent<Enemy>();
    //    //    enemy.OnDead();
    //    //    Destroy(enemy.gameObject, 2f);

    //    //}

    //    //if (other.CompareTag(ConstantTag.PLAYER))
    //    //{
    //    //    Destroy(gameObject);
    //    //    Player player = other.GetComponent<Player>();
    //    //    player.OnDead();
    //    //    player.GetComponent<Collider>().enabled = false;
    //    //}
    //}


}
