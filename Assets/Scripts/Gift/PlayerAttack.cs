using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int damage;
    private Animator weaponAnimator;

	// Use this for initialization
	void Start () {
        weaponAnimator = GetComponent<Animator>();
        damage = 1;
	}

    public void Attack ()
    {
        weaponAnimator.SetTrigger("IsSlash");
    }

    public int GetDamage ()
    {
        return damage;
    }
}
