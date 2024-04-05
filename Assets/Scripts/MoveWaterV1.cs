using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveWaterV1 : MonoBehaviour
{
    [SerializeField] GameObject Water;
    [SerializeField] GameObject[] Positions;
    [SerializeField] float Speed;
    [SerializeField] DropSpawner DropSpawner;

    public delegate void Gameover();
    public static event Gameover GameOverEvent;

    Transform NextPos;
    private int NextPosIndex;
    private bool movingToTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        NextPosIndex = 0;
        NextPos = Positions[NextPosIndex].transform;
        Water.transform.position = Positions[NextPosIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextPosIndex++;
            movingToTarget = true;
        }*/

        if (movingToTarget)
        {
            Water.transform.position = Vector3.MoveTowards(Water.transform.position, NextPos.position, Speed * Time.deltaTime);

        }

        if (movingToTarget == true)
        {
            if (NextPosIndex < Positions.Length)
            {
                if (Water.transform.position == Positions[NextPosIndex].transform.position)
                    movingToTarget = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Drop")
        {
            string GOName = collision.gameObject.name;

            int keyToRemove = int.Parse(GOName.Substring(8));

            //remove from the list of GO in scene the raindrops that touch the water
            DropSpawner.RaindropInScene.Remove(collision.gameObject);

            //put to null the operation of the GO from the list of results that touch the water 
            DropSpawner.GetOperationResults[keyToRemove] = null;

            Destroy(collision.gameObject);

            MoveWater();
        }
    }

    private void MoveWater()
    {
        NextPosIndex++;

        if (NextPosIndex < Positions.Length)
        {
            NextPos = Positions[NextPosIndex].transform;
            movingToTarget = true;
        }
        else
        {
            if (GameOverEvent != null) GameOverEvent();
        }

    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }

    IEnumerator MoveWaterCoroutine()
    {
        yield return null;
    }
}
