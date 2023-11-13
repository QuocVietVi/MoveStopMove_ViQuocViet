using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    private Character attacker;
    // Start is called before the first frame update
    void Start()
    {
        attacker = GetComponent<Character>();
        OnInit();
    }

    public void OnInit()
    {
        Invoke(nameof(OnDespawn), 4f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character victim = other.GetComponent<Character>();

        if (victim != null && victim != attacker)
        {
            if (other.CompareTag(ConstantTag.ENEMY) || other.CompareTag(ConstantTag.PLAYER))
            {
                Destroy(gameObject);
                victim.OnDead();
                Destroy(victim.gameObject, 2f);

            }
        }
        else
        {
            return;
        }

        //if (other.CompareTag(ConstantTag.ENEMY))
        //{
        //    Destroy(gameObject);
        //    Character enemy = other.GetComponent<Enemy>();
        //    enemy.OnDead();
        //    Destroy(enemy.gameObject, 2f);

        //}

        //if (other.CompareTag(ConstantTag.PLAYER))
        //{
        //    Destroy(gameObject);
        //    Player player = other.GetComponent<Player>();
        //    player.OnDead();
        //    player.GetComponent<Collider>().enabled = false;
        //}
    }


}
