using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    private MoveDynamicObject enemyMove;
    System.Random rand = new System.Random();
    private float moveHorizontal;
    private float moveVertical;

    void Start()
    {
        enemyMove = new MoveDynamicObject(gameObject);
        RandDirection();
    }

    void Update()
    {
        enemyMove.Move(speed, moveHorizontal, moveVertical);
    }

    void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Floor"))
        {
            RandDirection();
            RoundPosition();
        }
    }
    private void RandDirection()
    {
        switch (rand.Next(4))
        {
            case 0:
                SetRightDirection();
                break;
            case 1:
                SetDownDirection();
                break;
            case 2:
                SetLeftDirection();
                break;
            case 3:
                SetUpDirection();
                break;
        }
    }

    private void RoundPosition()
    {
        transform.position = new Vector3((float)Math.Round(transform.position.x),
                        (float)Math.Round(transform.position.y),
                        (float)Math.Round(transform.position.z));
    }

    public void SetRightDirection()
    {
        moveHorizontal = 1;
        moveVertical = 0;
    }
    public void SetLeftDirection()
    {
        moveHorizontal = -1;
        moveVertical = 0;
    }
    public void SetDownDirection()
    {
        moveHorizontal = 0;
        moveVertical = -1;
    }
    public void SetUpDirection()
    {
        moveHorizontal = 0;
        moveVertical = 1;
    } 
}