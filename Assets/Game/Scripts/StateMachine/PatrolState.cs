using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > randomTime || enemy.target != null)
        {
            enemy.ChangeState(new IdleState());

        }
        else
        {
            enemy.Moving();

        }
    }

    public void OnExit(Enemy enemy)
    {
    }
}
