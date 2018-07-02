using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private Animator weaponAnimator;

	// Use this for initialization
	void Start () {
        weaponAnimator = GetComponent<Animator>();
	}

    public void Attack ()
    {
        weaponAnimator.SetTrigger("IsSlash");
    }
}
