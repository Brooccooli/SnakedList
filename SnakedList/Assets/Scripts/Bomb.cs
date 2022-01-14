using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : Pickups
{
    private bool active;
    private  int framesToSkip;
    private int skipFrames;

    private void Start()
    {
        framesToSkip = Random.Range(100, 300);
        skipFrames = framesToSkip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnakeHead" && active)
        {
            active = false;
            other.GetComponentInParent<SnakeController>().Split();
            // Move away from frame, unobtainable for snake
            transform.position = new Vector3(100, 100, 100);
        }
        /*else if (other.gameObject.tag == "Snake")
        {
            Move();
        }*/
    }

    void FixedUpdate()
    {
        if (skipFrames <= 0 && !active)
        {
            Move();
            active = true;
            skipFrames = framesToSkip;
        }

        if (!active)
        {
            skipFrames--;
        }
    }
}
