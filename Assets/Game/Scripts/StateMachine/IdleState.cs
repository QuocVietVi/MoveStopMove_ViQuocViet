using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(1f, 3f);
        enemy.Stop();
    }

    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer > randomTime) //idle trong khoang tgian random sau do doi sang patrol
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
    }
}
