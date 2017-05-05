using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    abstract class ResourceLoaderBase
    {
        abstract public GameObject LoadFloor();
        abstract public GameObject LoadUnbreakableWall();
        abstract public GameObject LoadBreakableWall();
    }
}
