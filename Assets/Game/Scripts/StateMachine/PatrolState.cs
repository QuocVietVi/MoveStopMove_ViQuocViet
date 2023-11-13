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
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer < randomTime)
        {
            enemy.Moving();

        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {
    }
}
