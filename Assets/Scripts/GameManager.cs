using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _gameEnded = false;

    void Update()
    {
        if (_gameEnded)
        {
            return;
        }


        if (PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _gameEnded = true;
        Debug.Log("GAME OVER");
    }
}