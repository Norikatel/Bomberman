using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PowerUpCollector:MonoBehaviour
    {
        private bool speedPowerUp = false;
        private bool wallWalkerPowerUp = false;
        private Planter planter;
        private MoveDynamicObject moveDynamicObject;
        private Collider playerCollider;


        private void Start()
        {
            planter = GetComponent<Planter>();
            moveDynamicObject = GetComponent<MoveDynamicObject>();
            playerCollider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag) {
                case "BombPowerUp": {
                        planter.GiveOneMoreBomb();
                        other.gameObject.SetActive(false);
                        break;
                    }
                case "SpeedPowerUp":
                    {
                        if (!speedPowerUp) {
                            moveDynamicObject.ActivateMaxSpeed();
                            other.gameObject.SetActive(false);
                        }
                        break;
                    }
                case "ExplodeRadiusPowerUp":
                    {
                        planter.SetMoreRadius();
                        other.gameObject.SetActive(false);
                        break;
                    }
                case "WallWalkerPowerUp":
                    {
                        if (!wallWalkerPowerUp)
                        {
                            foreach (var go in GameObject.FindGameObjectsWithTag("BreakableWall"))
                                Physics.IgnoreCollision(playerCollider, go.GetComponent<Collider>());
                            other.gameObject.SetActive(false);
                        }
                        break;
                    }
            }
        }
    }
}
