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

        override protected void SetNewDirection()
        {
            if (time < timeCeiling)
                time++;
            else
            {
                if (IsClosedToCell() && rand.Next(1) == 0)
                {
                    transform.position = transform.position.RoundPosition();
                    RandDirection();
                    time = 0;
                }
            }
        }

        private bool IsClosedToCell()
        {
            return (Math.Abs(transform.position.x - Math.Round(transform.position.x)) < 0.01) &&
                (Math.Abs(transform.position.z - Math.Round(transform.position.z)) < 0.01);
        }

        virtual protected void OnCollisionStay(Collision other)
        {
            if (!other.gameObject.CompareTag("Floor"))
            {
                transform.position = transform.position.RoundPosition();
                RandDirection();
            }
        }

        protected virtual void RandDirection()
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

        protected void SetRightDirection()
        {
            moveHorizontal = 1;
            moveVertical = 0;
        }

        protected void SetLeftDirection()
        {
            moveHorizontal = -1;
            moveVertical = 0;
        }

        protected void SetDownDirection()
        {
            moveHorizontal = 0;
            moveVertical = -1;
        }

        protected void SetUpDirection()
        {
            moveHorizontal = 0;
            moveVertical = 1;
        }

        private void OnCollisionEnter(Collision otherObject)
        {
            if (otherObject.collider.CompareTag("Player"))
                StartCoroutine(Effects.FadeDeactivate(otherObject.gameObject));
        }
    }
}