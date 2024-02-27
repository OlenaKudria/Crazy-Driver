using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scenes
{
    public class ScenesManager : MonoBehaviour
    {
        public void StartGame() => SceneManager.LoadScene("Main");
        public void ExitGame() => SceneManager.LoadScene("Menu");
    }
}
