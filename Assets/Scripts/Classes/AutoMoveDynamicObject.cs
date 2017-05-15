using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class AutoMoveDynamicObject : MoveDynamicObject
    {
        private System.Random rand = new System.Random();
        int time = 0;
        int timeCeiling = 50;

        void Start()
        {
            RandDirection();
        }

        override protected void GetNewDirection()
        {
            if (time < timeCeiling)
                time++;
            else
            {
                if (IsClosedToCell() && rand.Next(1) == 0)
                {
                    RandDirection();
                    RoundPosition();
                    time = 0;
                }
            }
        }

        private bool IsClosedToCell()
        {
            return (Math.Abs(transform.position.x - Math.Round(transform.position.x)) < 0.1) &&
                (Math.Abs(transform.position.z - Math.Round(transform.position.z)) < 0.1);
        }

        void OnCollisionStay(Collision other)
        {
            if (!other.gameObject.CompareTag("Floor"))
            {
                RandDirection();
                RoundPosition();
            }
        }

        public void RandDirection()
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
                transform.position.y, (float)Math.Round(transform.position.z));
        }

        private void SetRightDirection()
        {
            moveHorizontal = 1;
            moveVertical = 0;
        }

        private void SetLeftDirection()
        {
            moveHorizontal = -1;
            moveVertical = 0;
        }

        private void SetDownDirection()
        {
            moveHorizontal = 0;
            moveVertical = -1;
        }

        private void SetUpDirection()
        {
            moveHorizontal = 0;
            moveVertical = 1;
        }
    }
}