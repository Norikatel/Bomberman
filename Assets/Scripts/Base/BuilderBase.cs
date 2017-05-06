using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    abstract class BuilderBase
    {
        abstract public void BuildUnbreakableWalls(GameObject unbreakableWall);
        abstract public void BuildFloor(GameObject floor);
        abstract public void BuildBreakableWalls(GameObject breakableWall);
    }
}
