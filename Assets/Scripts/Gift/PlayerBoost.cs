using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerBoost : MonoBehaviour {
    private GameObject g;

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player1") {
            g = GameObject.Find("Player2");
            
        } else if(other.gameObject.name == "Player2") {
            g = GameObject.Find("Player1");

        }
        Vector2 v = g.transform.localScale;
        v += new Vector2(1f, 1f);
        g.transform.localScale = v;
        this.gameObject.SetActive(false);
    }

}
