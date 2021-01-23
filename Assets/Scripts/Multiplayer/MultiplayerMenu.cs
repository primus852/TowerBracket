using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace Multiplayer
{
    public class MultiplayerMenu : MonoBehaviourPunCallbacks
    {
        [Header("Screens")] public GameObject mainScreen;

        public GameObject lobbyScreen;

        [Header("Main Screen")] public Button createRoomButton;
        public Button joinRoomButton;

        [Header("Lobby Screen")] public Text playerListText;
        public Button startGameButton;

        private void Start()
        {
            createRoomButton.interactable = false;
            joinRoomButton.interactable = false;
        }

        public override void OnConnectedToMaster()
        {
            createRoomButton.interactable = true;
            joinRoomButton.interactable = true;
        }

        private void SetScreen(GameObject screen)
        {
            mainScreen.SetActive(false);
            lobbyScreen.SetActive(false);

            screen.SetActive(true);
        }

        public void OnCreateRoomButton(TMP_InputField roomNameInput)
        {
            NetworkManager.networkManagerInstance.CreateRoom(roomNameInput.text);
        }

        public void OnJoinRoomButton(TMP_InputField roomNameInput)
        {
            NetworkManager.networkManagerInstance.JoinRoom(roomNameInput.text);
        }

        public void OnPlayerNameUpdate(TMP_InputField playerNameInput)
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }

        public override void OnJoinedRoom()
        {
            SetScreen(lobbyScreen);

            photonView.RPC("UpdateLobbyUI", RpcTarget.All);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdateLobbyUI();
        }

        [PunRPC]
        public void UpdateLobbyUI()
        {
            playerListText.text = "";

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                playerListText.text += player.NickName + "\n";
            }

            startGameButton.interactable = PhotonNetwork.IsMasterClient;
        }

        public void OnLeaveLobbyButton()
        {
            PhotonNetwork.LeaveRoom();
            SetScreen(mainScreen);
        }

        public void OnStartGameButton()
        {
            NetworkManager.networkManagerInstance.photonView.RPC("ChangeScene", RpcTarget.All, "LevelVersus01" );
        }
    }
}