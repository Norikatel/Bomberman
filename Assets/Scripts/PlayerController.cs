using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private MoveDynamicObject playerMove;

    void Start()
    {
        playerMove = new MoveDynamicObject(gameObject);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if(playerMove.CanMove(moveHorizontal, moveVertical))
            playerMove.Move(speed, moveHorizontal, moveVertical);
    }
}

