using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeCollisionCheck : MonoBehaviour
{
    // Collision won't be checked first 10 frames
    private int cooldown = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if (cooldown > 0)
        {
            return;
        }
        
        if (other.gameObject.tag == "Snake")
        {
            SceneManager.LoadScene("Dead");
        }
    }

    private void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
    }
}
