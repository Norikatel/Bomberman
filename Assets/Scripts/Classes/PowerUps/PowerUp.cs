using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PowerUp:MonoBehaviour
    {
        AudioSource pickUpSound;
        Renderer render;

        private void Start()
        {
            pickUpSound = GetComponent<AudioSource>();
            render = GetComponent<Renderer>();
        }

        private void Update()
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }

        public void PlaySoundAndDeactive() {
            StartCoroutine(CoroutineSoundAndDeactive());
        }

        private IEnumerator CoroutineSoundAndDeactive() {
            var color = render.material.color;
            color.a = 0;
            render.material.color=color;
            pickUpSound.Play();
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
    }
}
