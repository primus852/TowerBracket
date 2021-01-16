using System;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneMain = "MainMenu";

    [Header("Next Level")] public string nextLevelName = "Level02";
    public int levelToUnlock = 2;
    public GameObject gameCompletedText;
    public GameObject continueButton;

    private void Start()
    {
        if (nextLevelName == "End")
        {
            continueButton.SetActive(false);
            gameCompletedText.SetActive(true);   
        }
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneMain);
    }

    public void Continue()
    {
        if (nextLevelName != "End")
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
            sceneFader.FadeTo(nextLevelName);
        }
    }
}