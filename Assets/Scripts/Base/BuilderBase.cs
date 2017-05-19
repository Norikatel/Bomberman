using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    abstract class BuilderBase
    {
        abstract public void BuildUnbreakableWalls();
        abstract public void BuildFloor();
        abstract public void BuildBreakableWalls();
        abstract public void AddPlayer();
        abstract public void AddEnemy();
        abstract public void AddEnemyPro();
    }
}
