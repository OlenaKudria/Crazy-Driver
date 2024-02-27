using TMPro;
using UnityEngine;

namespace Player
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI gameOverScoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        private int _score;

        private void Start()
        {
            _score = 0;
            UpdateBestScoreText();
        }

        public void UpdateScore(int amount)
        {
            _score += amount;
            scoreText.text = _score.ToString();
            gameOverScoreText.text = _score.ToString();
            CheckHighScore();
        }

        private void CheckHighScore()
        {
            if (_score > PlayerPrefs.GetInt("BestScore", 0))
            {
                PlayerPrefs.SetInt("BestScore", _score);
                UpdateBestScoreText();
            }
        }

        private void UpdateBestScoreText()
        {
            bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        }
    }
}