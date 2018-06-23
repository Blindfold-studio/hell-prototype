using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    float timer = 0;
    float speed;
    float width;
    float height;

    public Transform targetplayer;
    public GameObject player;
    private Vector3 zAxis = new Vector3(0, 0, 1);
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetplayer = player.GetComponent<Transform>();
        speed = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //float x = Mathf.Cos(timer + player.transform.position.x) * width;
        //float y = Mathf.Sin(timer + player.transform.position.y) * height;
        //transform.position = new Vector2(x, y);

        if (Vector2.Distance(transform.position, targetplayer.position) < 0.5)
        {
            
        }
        else if (Vector2.Distance(transform.position, targetplayer.position) < 5)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetplayer.position, speed * Time.deltaTime);
            transform.RotateAround(player.transform.position, zAxis, speed);
        }


    }
}
