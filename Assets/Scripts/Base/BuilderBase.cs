using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    abstract class BuilderBase
    {
        abstract public void BuildUnbreakableWalls(GameObject unbreakableWall, int rowCount, int columnCount);
        abstract public void BuildFloor(GameObject floor, int rowCount, int columnCount);
        abstract public void BuildBreakableWalls(GameObject breakableWall, int rowCount, int columnCount);

    }
}
