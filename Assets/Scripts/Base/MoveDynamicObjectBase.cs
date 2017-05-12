using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    abstract class MoveDynamicObjectBase
    {
        abstract public bool CanMove(float moveHorizontal, float moveVertical);
        abstract public void Move(float speed, float moveHorizontal, float moveVertical);
    }
}
