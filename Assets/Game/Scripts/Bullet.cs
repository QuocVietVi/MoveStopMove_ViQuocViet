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
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        LeanPool.Despawn(this);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Character victim = other.GetComponent<Character>();

        if (victim == null && victim == attacker)
        {
            return;
        }
        if (other.CompareTag(ConstantTag.ENEMY) || other.CompareTag(ConstantTag.PLAYER) && victim != attacker)
        {
            attacker.level++;
            if (attacker.level < 25)
            {
                attacker.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                attacker.Range += 0.25f;
                attacker.throwPoint.position -= Vector3.up * 0.02f;
                //CameraFollow.Instance.offset += new Vector3(0, 0.25f, -0.25f);
            }
            if (attacker == LevelManager.Instance.player)
            {
                LevelManager.Instance.player.gold++;
            }
            
            //OnDespawn();
            //Destroy(gameObject);
            //victim.OnDead();
            //Destroy(victim.gameObject, 2f);

        }
        //else
        //{
        //    return;
        //}
    }


}
