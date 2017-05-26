using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class UIController:MonoBehaviour
    {
        private Text scoreText;
        private Text winText;
        private Text bombText;
        private Text speedText;
        private Text wallWalkerText;
        private Text explodeRadiusText;
        private Text pickUpText;
        int scoreForProEnemy = 5;
        int scoreForEnemy = 3;
        int currentScore=0;
        int goal=10;

        private void Start()
        {
            Text[] UITexts = new UILoader().InstantiateScoreText().GetComponentsInChildren<Text>();
            foreach (var t in UITexts) {
                switch (t.tag) {
                    case "ScoreText":
                        scoreText = t;
                        break;
                    case "WinText":
                        winText = t;
                        break;
                    case "Bombs":
                        bombText = t;
                        break;
                    case "Speed":
                        speedText = t;
                        break;
                    case "WallWalker":
                        wallWalkerText = t;
                        break;
                    case "ExplodeRadius":
                        explodeRadiusText = t;
                        break;
                    case "PickUpText":
                        pickUpText = t;
                        break;
                }
            }         
            UpdateScoreText();
            SetBombsCountText(1);
            SetExplodeRadiusText(1);
            pickUpText.text = "";
            wallWalkerText.text = "You are not a wallwalker";
            speedText.text = "You are so slow :(";
            winText.text = "";
        }

        public void SetPickUpText() {
            StartCoroutine(CoroutinePickUp());
        }

        private IEnumerator CoroutinePickUp() {
            pickUpText.text = "YEAH,YOU GOT THIS!";
            yield return new WaitForSeconds(2f);
            pickUpText.text = "";
        }

        public void SetBombsCountText(int bombsCount) {
            bombText.text = "Bombs count: " + bombsCount;
        }

        public void SetExplodeRadiusText(int explodeRadius)
        {
            explodeRadiusText.text = "Explode radius: " + explodeRadius;
        }

        public void SetMaxSpeedText() {
            speedText.text = "Maximum speed";
        }

        public void SetWallWalkerText()
        {
            wallWalkerText.text = "Now you are WALLWALKER!";
        }

        public void AddScoreForEnemy() {
            currentScore += scoreForEnemy;
            UpdateScoreText();
        }

        public void AddScoreForProEnemy() {
            currentScore += scoreForProEnemy;
            UpdateScoreText();
        }

        private void CheckGoalComplete() {
            if (currentScore >= goal) {
                winText.text = "YOU WIN!!!";
            };
        }

        private void UpdateScoreText() {
            scoreText.text = "Score : " + currentScore;
            CheckGoalComplete();
        }
    }
}
