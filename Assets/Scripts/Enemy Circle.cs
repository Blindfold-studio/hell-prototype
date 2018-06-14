using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : MonoBehaviour {
    float timer = 0;
    float speed;
    float width;
    float height;

	// Use this for initialization
	void Start () {
        speed = 1;
        width = 2;
        height = 2;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        float x = Mathf.Cos(timer) * width;
        float y = Mathf.Sin(timer) * height;
        float z = 0;

        transform.position = new Vector2(x, y);
	}
}
