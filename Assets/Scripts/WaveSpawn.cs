﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawn : MonoBehaviour
{

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;

    private float _countdown = 2f;
    private int _waveNumber = 0;

    void Update()
    {
        if(_countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
        }

        _countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(_countdown).ToString();

    }

    IEnumerator SpawnWave()
    {

        _waveNumber++;

        for (int i = 0; i < _waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

       
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }


}