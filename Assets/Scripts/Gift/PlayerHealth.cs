using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float thrust;
    private Rigidbody2D rg;
    public SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy") {
            rg.AddForce(new Vector2(-7f, 0f), ForceMode2D.Impulse);
            Color tmp = spriteRenderer.color;
            tmp.a -= 0.3f;
            //need to change color to black and white
            if(tmp.a < 0.0f ) {
                tmp.a = 0.1f;
            }

            spriteRenderer.color = tmp;
        }
    }
}
