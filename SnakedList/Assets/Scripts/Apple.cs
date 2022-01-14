using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Apple : Pickups
{
    void Start()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnakeHead")
        {
            other.gameObject.GetComponentInParent<SnakeController>().Grow();
            Move();
        }
        /*else if (other.gameObject.tag == "Snake")
        {
            Move();
        }*/
    }
}
