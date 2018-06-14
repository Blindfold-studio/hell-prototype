using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState {

    private Enemy enemy;

    private float idlestart;
    private float idletime = 3;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Excute()
    {
        //Debug.Log("gam");
        //Idle();
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Idle()
    {
       // enemy.Anima.SetFloat("speed", 0);

        //idlestart += Time.deltaTime;

       // if (idlestart >= idletime)
       // {
        //    enemy.ChangeState(new State());
        //}
       
    }
   
}
