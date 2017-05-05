using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ResourceLoader : ResourceLoaderBase
    {
        public override GameObject LoadFloor() {
            return Resources.Load("Floor") as GameObject;
        }

        public override GameObject LoadUnbreakableWall()
        {
            return Resources.Load("Walls/UnbreakableWall") as GameObject;
        }

        public override GameObject LoadBreakableWall() {
            return Resources.Load("Walls/BreakableWall") as GameObject;
        }
    }
}
