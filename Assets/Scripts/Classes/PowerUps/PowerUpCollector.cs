using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PowerUpCollector:MonoBehaviour
    {
        private List<GameObject> listOfPowerUps = new List<GameObject>();
        private bool speedPowerUp = false;
        private bool wallWalkerPowerUp = false;
        private Planter planter;
        private MoveDynamicObject moveDynamicObject;
        private Collider playerCollider;
        private UIController uiController;
        
        private void Start()
        {
            planter = GetComponent<Planter>();
            moveDynamicObject = GetComponent<MoveDynamicObject>();
            playerCollider = GetComponent<Collider>();
            uiController = GetComponent<UIController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!listOfPowerUps.Contains(other.gameObject)) {
                switch (other.tag)
                {
                    case "BombPowerUp":
                        {
                            PickUpAction(other);
                            planter.GiveOneMoreBomb();
                            uiController.SetBombsCountText(planter.MaxBomb);
                            break;
                        }
                    case "SpeedPowerUp":
                        {
                            if (!speedPowerUp)
                            {
                                PickUpAction(other);
                                moveDynamicObject.ActivateMaxSpeed();
                                speedPowerUp = true;
                                uiController.SetMaxSpeedText();
                            }
                            else { other.gameObject.SetActive(false); }
                            break;
                        }
                    case "ExplodeRadiusPowerUp":
                        {
                            PickUpAction(other);
                            planter.SetMoreRadius();
                            uiController.SetExplodeRadiusText(planter.Radius);
                            break;
                        }
                    case "WallWalkerPowerUp":
                        {
                            if (!wallWalkerPowerUp)
                            {
                                PickUpAction(other);
                                foreach (var go in GameObject.FindGameObjectsWithTag("BreakableWall"))
                                    Physics.IgnoreCollision(playerCollider, go.GetComponent<Collider>());
                                wallWalkerPowerUp = true;
                                uiController.SetWallWalkerText();
                            }
                            else
                                other.gameObject.SetActive(false);
                            break;
                        }
                }
            }
        }

        private void PickUpAction(Collider other)
        {
            listOfPowerUps.Add(other.gameObject);
            uiController.SetPickUpText();
            other.GetComponent<PowerUp>().PlaySoundAndDeactive();
        }
    }
}
