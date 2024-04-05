using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterDrop : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        //body.velocity = new Vector2 (0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject, 6f);
    }
    private void OnDestroy()
    {
        //Debug.Log("distrutto");
    }
}
