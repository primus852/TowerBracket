using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Multiplayer
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        [HideInInspector] public int id;

        public Player photonPlayer;

        [PunRPC]
        public void Initialize(Player player)
        {
            photonPlayer = player;
            id = player.ActorNumber;

            GameManager.gameManagerInstance.players[id - 1] = this;
        }
    }
}