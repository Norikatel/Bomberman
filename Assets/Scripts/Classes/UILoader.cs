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
            GameObject.Instantiate(LoadEventSystem());
            return GameObject.Instantiate(LoadCanvas());
        }

        private GameObject LoadCanvas()
        {
            return Resources.Load("UI/Canvas") as GameObject;
        }

        private GameObject LoadEventSystem()
        {
            return Resources.Load("UI/EventSystem") as GameObject;
        }
    }
}
