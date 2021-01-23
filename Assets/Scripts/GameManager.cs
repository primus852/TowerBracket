using System;
using Multiplayer;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Game")]
    public static bool gameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public static GameManager gameManagerInstance;

    [Header("Players")] public string playerPrefabLocation;

    public Transform[] spawnPointsPlayer;
    public PlayerController[] players;
    private int _playersInGame;

    private void Awake()
    {
        gameManagerInstance = this;
    }

    private void Start()
    {
        gameIsOver = false;
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        photonView.RPC("JoinedGame", RpcTarget.AllBuffered);
    }

    void Update()
    {
        if (gameIsOver)
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
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }

    [PunRPC]
    private void JoinedGame()
    {
        _playersInGame++;

        if (_playersInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        // TODO: Don't make this Random
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation,
            spawnPointsPlayer[Random.Range(0, spawnPointsPlayer.Length)].position, Quaternion.identity);

        PlayerController playerScript = playerObj.GetComponent<PlayerController>();
        
        
        playerScript.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);

    }
}