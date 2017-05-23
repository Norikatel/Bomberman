using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class ParticleSystemCollision:MonoBehaviour
    {
        public List<GameObject> collisionList = new List<GameObject>();
        private void Start()
        {
            var type = typeof(CheckCollisions);
            foreach (Transform t in transform) {
                t.gameObject.AddComponent(type);
            }
        }
    }
}
