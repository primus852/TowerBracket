using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public string levelToLoad = "MainLevel";
        public SceneFader sceneFader;

        public void Play()
        {
            sceneFader.FadeTo(levelToLoad);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}