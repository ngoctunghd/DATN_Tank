using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BomberChap
{
    public class OnlineMultiplayerGameController : MonoBehaviour
    {
        public UnityEngine.Events.UnityEvent onStartNextRound;
        public UnityEngine.Events.UnityEvent onPlayerDisconnected;
        public UnityEngine.Events.UnityEvent onDisconnectedFromServer;

        public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            onPlayerDisconnected.Invoke();
        }

        public void OnDisconnectedFromPhoton()
        {
            onDisconnectedFromServer.Invoke();
        }



    }
}
