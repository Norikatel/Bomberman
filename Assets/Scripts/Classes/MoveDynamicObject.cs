using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class MoveDynamicObject : MonoBehaviour, IMoveable
    {
        protected float moveHorizontal;
        protected float moveVertical;
        public float speed;

        public void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            GetNewCoordinates();
            if (CanMove())
            {
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                transform.position += movement * speed;
                SetYRotation(GetYRotation(moveHorizontal, moveVertical));
            }
        }

        virtual protected void GetNewCoordinates()
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }

        private bool CanMove()
        {
            if (moveHorizontal != 0 || moveVertical != 0)
                return true;
            return false;
        }

        private void SetYRotation(int yRotation)
        {
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        private int GetYRotation(float Horizontal, float Vertical)
        {
            if (Horizontal == 0)
            {
                if (Vertical < 0)
                    return 90;
                if (Vertical > 0)
                    return 270;
            }
            if (Horizontal < 0)
            {
                if (Vertical < 0)
                    return 135;
                if (Vertical == 0)
                    return 180;
                if (Vertical > 0)
                    return 225;
            }
            if (Horizontal > 0)
            {
                if (Vertical > 0)
                    return 315;
                if (Vertical < 0)
                    return 45;
                if (Vertical == 0)
                    return 0;
            }
            return 0;
        }
    }
}
