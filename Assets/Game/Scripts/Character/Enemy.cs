using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private float walkPointRange;
    [SerializeField] private LayerMask layerGround;
    public GameObject targetPoint;

    private IState currentState;
    private bool walkPointSet;

    private void Awake()
    {
        ChangeState(new PatrolState());

    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
        

    }
    
    public override void OnInit()
    {
        base.OnInit();
        canAttack = true;
        gravityScale = 9;
        collider.enabled = true;
        ChangeState(new PatrolState());

    }

    public void Moving()
    {
        if (!walkPointSet) { SearchWalkPoint(); }
        if (walkPointSet)
        {
            agent.speed = 10f;
            agent.SetDestination(walkPoint);
            ChangeAnim(ConstantAnim.RUN);
            canAttack = true;
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //reach walkpoint => walkPointSet = false => search next walk point
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    public void Stop()
    {
        agent.SetDestination(transform.position); //đứng im
        ChangeAnim(ConstantAnim.IDLE);
        if (canAttack == true && target != null)
        {
            Invoke(nameof(StartAttackState), 1f);
            canAttack = true;
        }
        if (target == null)
        {
            canAttack = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, layerGround))
        {
            walkPointSet = true;
        }
        Debug.DrawRay(walkPoint, -transform.up, Color.red, 2f);

    }

    private void StartAttackState()
    {
        ChangeState(new AttackState());
    }

    protected override void AttackRange()
    {
        base.AttackRange();
        if (isDead == false && enemyInRange != null)
        {
            if (enemyInRange[0] != this.collider && enemyInRange[0] != null)
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
        }
        

    }
    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(null);
        Invoke(nameof(Despawn),1f);
    }

    public void ActiveTargetPoint()
    {
        targetPoint.SetActive(true);
    }

    public void DeActiveTargetPoint()
    {
        targetPoint.SetActive(false);
    }

    public void ChangeState(IState newState)
    {
        //khi đổi sang state mới, check xem state cũ có = null ko
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
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
            else
            {
                OnDead();
                bullet.OnDespawn();
                LevelManager.Instance.listEnemies.Remove(this);
                LevelManager.Instance.maxEnemies--;

            }
        }
    }
}
