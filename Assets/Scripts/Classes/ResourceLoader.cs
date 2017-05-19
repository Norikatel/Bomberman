﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ResourceLoader : ResourceLoaderBase
    {
        public override GameObject LoadFloor()
        {
            return Resources.Load("Floor") as GameObject;
        }

        public override GameObject LoadBomb()
        {
            return Resources.Load("Bomb") as GameObject;
        }

        public override GameObject LoadUnbreakableWall()
        {
            return Resources.Load("Walls/UnbreakableWall") as GameObject;
        }

        public override GameObject LoadBreakableWall()
        {
            return Resources.Load("Walls/BreakableWall") as GameObject;
        }

        public override GameObject LoadPlayer()
        {
            return Resources.Load("DynamicObjects/Player") as GameObject;
        }

        public override GameObject LoadEnemy()
        {
            return Resources.Load("DynamicObjects/Enemy") as GameObject;
        }

        public override GameObject LoadEnemyPro()
        {
            return Resources.Load("DynamicObjects/EnemyPro") as GameObject;
        }

        public override GameObject LoadExplodeEffect()
        {
            return Resources.Load("Effects/Explode") as GameObject;
        }
    }
}
