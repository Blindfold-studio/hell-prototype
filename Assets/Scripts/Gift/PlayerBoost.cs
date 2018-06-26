using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player1") {
            GameObject g = GameObject.Find("Player2");
            Vector2 v = g.transform.localScale;
            v += new Vector2(1f, 1f);
            g.transform.localScale = v;
            this.gameObject.SetActive(false);
        } else if(other.gameObject.name == "Player2") {
            GameObject g = GameObject.Find("Player1");

        }

    }

}
