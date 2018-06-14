using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    
    private float startingHealth = 10f;
    private Rigidbody2D rg;
    private Animator anim;

    float currentHealth;

	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody2D>();
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        print(other.gameObject.name);
        if(other.gameObject.tag == "Weapon") {
            rg.AddForce(new Vector2(4f, 0f), ForceMode2D.Impulse);
            currentHealth -= 5;
            if(currentHealth <= 0) {
                anim.SetTrigger("IsDead");
            }
        }
    }

    void Die() {
        this.gameObject.SetActive(false);
    }
}
