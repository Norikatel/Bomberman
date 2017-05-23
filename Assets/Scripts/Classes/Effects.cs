using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Effects
    {
        public static IEnumerator FadeDeactivate(GameObject gameObject)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            while (color.a > 0)
            {
                color.a -= 0.05f;
                gameObject.GetComponent<Renderer>().material.color = color;
                yield return new WaitForSeconds(0.01f);
            }
            gameObject.SetActive(false);
        }

        public static void Explode(GameObject bomb, GameObject effect, float radius)
        {
            foreach (var ps in effect.GetComponentsInChildren<ParticleSystem>()) {
                var psMain = ps.main;
                psMain.startSpeed = radius;
            }                
            Object.Destroy(Object.Instantiate(effect, bomb.transform.position, new Quaternion(0, 0, 0, 0)), 1);
            bomb.SetActive(false);
        }
    }
}