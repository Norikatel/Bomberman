using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ScoreCounter:MonoBehaviour
    {
        public Text scoreText;
        public Text winText;
        int scoreForProEnemy = 5;
        int scoreForEnemy = 3;
        int currentScore=0;
        int goal=10;

        private void Start()
        {
            Text[] UITexts = new UILoader().InstantiateScoreText().GetComponentsInChildren<Text>();
            if (UITexts[0].tag == "ScoreText")
            {
                scoreText = UITexts[0];
                winText = UITexts[1];
            }
            else {
                scoreText= UITexts[1];
                winText = UITexts[0];
            }
            scoreText.text = "";
            winText.text = "";
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
