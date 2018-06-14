using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : EnemyState {

    private Enemy enemy;
    private float stateStart;
    private float stateTime = 6;

    public void Enter(Enemy enemy)
    {
        
    }

    public void Excute()
    {
        Statestay();
        enemy.Move();
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Statestay()
    {
        //enemy.Anima.SetFloat("speed", 0);

       // stateStart += Time.deltaTime;

        //if (stateStart >= stateTime)
       // {
        //    enemy.ChangeState(new EnemyIdle());
       // }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
