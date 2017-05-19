using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour {

    private void OnParticleCollision(GameObject other)
    {
        {
            if (other.GetComponent<Collider>().CompareTag("BreakableWall") ||
                   other.GetComponent<Collider>().CompareTag("Player") ||
                   other.GetComponent<Collider>().CompareTag("Enemy"))
                  GameObject.FindGameObjectWithTag("Player").GetComponent<Planter>().DestroyObject(other);
        }
    }
}
