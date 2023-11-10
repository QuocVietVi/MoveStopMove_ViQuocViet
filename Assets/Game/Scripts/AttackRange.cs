using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Collider[] enemies;

    Collider[] enemyInRange;
    Collider[] enemyOutRange;

    private void FixedUpdate()
    {
        enemyInRange = Physics.OverlapSphere(this.transform.position, range);

        foreach (var enemy in enemyInRange)
        {
            enemy.GetComponent<MeshRenderer>().enabled = false;
        }

        enemyOutRange = enemies.Except(enemyInRange).ToArray();

        foreach (var enemy in enemyOutRange)
        {
            enemy.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
