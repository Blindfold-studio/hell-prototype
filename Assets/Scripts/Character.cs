using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected float moveSpeed;

    protected bool facing;

    public bool Atk
    {
        get;
        set;
    }

    public Animator Anima
    {
        get;
        private set;
    }

    public virtual void Start()
    {
        facing = true;
    }

    public void ChangeDir()
    {
        facing = !facing;
        transform.localScale = new Vector3(transform.localScale.x * 1, 1, 1);
    }
}
