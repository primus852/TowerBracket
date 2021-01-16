using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager networkManagerInstance;

        private void Awake()
        {
            if (networkManagerInstance != null && networkManagerInstance != this)
            {
                gameObject.SetActive(false);
            }
            else
            {
                networkManagerInstance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created Room: " + PhotonNetwork.CurrentRoom.Name);
        }

        public void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        [PunRPC]
        public void ChangeScene(string sceneName)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}