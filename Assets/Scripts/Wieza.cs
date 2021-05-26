using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wieza : MonoBehaviour
{
    public static float predkosc = 5;
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (Menu.gameOver)
            return;
        transform.Translate(Vector2.left * Time.deltaTime * predkosc, Space.World);

        if (transform.position.x < -20)
        {
            Spawner.instance.zespanowaneWieze.Remove(this);
            Destroy(gameObject);
        }
    }
}
