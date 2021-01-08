using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;

    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
        {
            return;
        }

        if (Input.GetKeyDown("e"))
        {
            GameOver();
        }


        if (PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}