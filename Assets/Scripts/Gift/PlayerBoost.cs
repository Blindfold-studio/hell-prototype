using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerBoost : MonoBehaviour {
    private GameObject g;

    [SerializeField]
    private string p1_name;
    [SerializeField]
    private string p2_name;

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == p1_name) {
            g = GameObject.Find(p2_name);
            
        } else if(other.gameObject.name == p2_name) {
            g = GameObject.Find(p1_name);
        } else {
            return;
        }
        Vector2 v = new Vector2();
        v = g.transform.localScale;
        v += new Vector2(1f, 1f);
        g.transform.localScale = v;
        this.gameObject.SetActive(false);
    }

}
