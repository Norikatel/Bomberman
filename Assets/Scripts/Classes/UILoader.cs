using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class UILoader
    {
        public GameObject InstantiateScoreText() {
            return GameObject.Instantiate(LoadCanvas());
        }

        private GameObject LoadCanvas()
        {
            return Resources.Load("UI/Canvas") as GameObject;
        }
    }
}
