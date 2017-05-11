using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.position += movement * speed;
        if (moveHorizontal != 0 || moveVertical != 0)
            SetYRotation(GetYRotation(moveHorizontal, moveVertical));

    }

    private void SetYRotation(int yRotation)
    {
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private int GetYRotation(float Horizontal, float Vertical)
    {
        if (Horizontal == 0) {
            if(Vertical < 0)
                return 90;
            if(Vertical > 0)
                return 270;
        }         
        if (Horizontal < 0) {
            if(Vertical < 0)
                return 135;
            if(Vertical == 0)
                return 180;
            if (Vertical > 0)
                return 225;
        }
        if (Horizontal > 0) {
            if(Vertical > 0)
                return 315;
            if(Vertical < 0)
                return 45;
            if(Vertical == 0)
                return 0;
        }           
        return 0;
    }
}
