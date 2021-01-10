using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public string levelToLoad = "Level1";

        public void Play()
        {
            SceneManager.LoadScene(levelToLoad);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}