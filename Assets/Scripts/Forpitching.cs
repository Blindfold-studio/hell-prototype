﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forpitching : MonoBehaviour {

    public float speed;
    private Transform targetplayer;
	// Use this for initialization
	void Start () {
        targetplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Vector2.Distance(transform.position, targetplayer.position)> 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetplayer.position, speed * Time.deltaTime);
        }
    }
}
