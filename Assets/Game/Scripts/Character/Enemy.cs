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
    [SerializeField] private Collider collider;



    private IState currentState;
    private bool walkPointSet;
    private bool canAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ChangeState(new PatrolState());
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
        

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
        agent.speed = 0f;
        ChangeAnim(ConstantAnim.IDLE);
        if (canAttack == true && target != null)
        {
            Invoke(nameof(StartAttackState), 1f);
        }
        canAttack = false;
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

        if (enemyInRange[0] != this.collider)
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

    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(null);
        canAttack = false;
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
            if (other.GetComponent<Bullet>().attacker == this)
            {
                return;
            }
            OnDead();
        }
    }
}
