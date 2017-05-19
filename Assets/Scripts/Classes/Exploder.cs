using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Exploder : MonoBehaviour
    {
        float explodeDistance = 1;

        List<Vector3> directions = new List<Vector3>
        { Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
        List<RaycastHit> raycastHitList = new List<RaycastHit>();

        public void Explode(GameObject bomb, GameObject effect, Action<List<RaycastHit>> action = null)
        {
            Destroy(Instantiate(effect, bomb.transform.position, new Quaternion(0, 0, 0, 0)), 1);
            CheckCollisions(bomb);
            bomb.SetActive(false);
            if (action != null)
            {
                action(raycastHitList);
            }
        }

        private void CheckCollisions(GameObject bomb)
        {
            foreach (var vector in directions)
            {
                RaycastHit firstHit = Physics.RaycastAll(bomb.transform.position, vector, explodeDistance).FirstOrDefault();
                if (firstHit.collider != null)
                    raycastHitList.Add(firstHit);
            }
        }
    }
}
