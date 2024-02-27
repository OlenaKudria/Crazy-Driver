using TMPro;
using UnityEngine;

namespace Player
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private int _score;

        private void Start()
        {
            _score = 0;
        }

        public void UpdateScore(int amount)
        {
            _score += amount;
            text.text = _score.ToString();
        }
    }
}