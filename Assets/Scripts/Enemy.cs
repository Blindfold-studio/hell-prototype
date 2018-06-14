using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private EnemyState currentState;

    public override void Start()
    {
        base.Start();
        ChangeState(new EnemyIdle());
    }

    void Update()
    {
        currentState.Excute();
    }

    public void ChangeState(EnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        Anima.SetFloat("speed ", 1);
        transform.Translate(GetDir() * (moveSpeed * Time.deltaTime));
    }

    public Vector2 GetDir()
    {
        return facing ? Vector2.right : Vector2.left;
    }
}
