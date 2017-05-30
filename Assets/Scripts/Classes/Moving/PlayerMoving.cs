using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayerMoving:MoveDynamicObject
    {
        private Animator animator;
        public AudioClip step;

        protected override bool CanMove() {
            if ((moveHorizontal != 0 || moveVertical != 0))
            {
                animator.SetFloat("Speed", 2);
                return true;
            }
            else { animator.SetFloat("Speed", 0);
                return false;
            } 
        }
        
        

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
    }
}
