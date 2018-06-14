using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public float attackRange;
    public Animator weaponAnimator;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate() {
        if(Input.GetMouseButtonDown(0)) {
            weaponAnimator.SetTrigger("IsSlash");
        }
    }
}
