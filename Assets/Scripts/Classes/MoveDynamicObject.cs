using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class MoveDynamicObject: MoveDynamicObjectBase
    {
        GameObject dynamicObject;       

        public MoveDynamicObject(GameObject dynamicObject) {
            this.dynamicObject = dynamicObject;
        }
        override public void Move(float speed, float moveHorizontal, float moveVertical)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            dynamicObject.transform.position += movement * speed;
            SetYRotation(GetYRotation(moveHorizontal, moveVertical));
        }

        override public bool CanMove(float moveHorizontal, float moveVertical)
        {
            if (moveHorizontal != 0 || moveVertical != 0)
                return true;
            return false;
        }

        private void SetYRotation(int yRotation)
        {
            dynamicObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
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
