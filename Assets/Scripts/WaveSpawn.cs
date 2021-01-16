using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawn : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;

    public Transform spawnPoint;
    public GameManager gameManager;

    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;

    private float _countdown = 2f;
    private int _waveNumber;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (_waveNumber == waves.Length)
        {
            gameManager.WinLevel();
            enabled = false;
        }

        if (_countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
            return;
        }

        _countdown -= Time.deltaTime;
        _countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = $"Next Wave:\n{_countdown:00.00}";
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[_waveNumber];

        EnemiesAlive = wave.enemyCount;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        _waveNumber++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}