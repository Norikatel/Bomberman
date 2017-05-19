using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Effects
    {
        public static IEnumerator FadeDiactivate(GameObject gameObject)
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
    }
}