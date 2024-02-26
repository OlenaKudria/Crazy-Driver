using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.Panels
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject countDown;
        [SerializeField] private TextMeshProUGUI countDownText;
        [SerializeField] private float waitTime;
        [SerializeField] private GameObject finishMenu;

        private void Start()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            
            countDown.SetActive(true);
            yield return StartCoroutine(CountdownRoutine(3));
            countDownText.text = "START";
            yield return new WaitForSecondsRealtime(waitTime);
            countDownText.text = "";
            countDown.SetActive(false);
            
            AudioListener.pause = false;
            Time.timeScale = 1f;
        }
        
        private IEnumerator CountdownRoutine(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                countDownText.text = i.ToString();
                yield return new WaitForSecondsRealtime(waitTime);
            }
        }

        public void SetPause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        
        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        
        public void ShowFinishMenu(bool show)
        {
            Time.timeScale = 0f;
            finishMenu.SetActive(show);
        }
    }
}