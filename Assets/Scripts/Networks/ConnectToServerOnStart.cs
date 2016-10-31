using UnityEngine;
using System.Collections;

namespace Tank
{
    public class ConnectToServerOnStart : Photon.PunBehaviour
    {
        public UnityEngine.Events.UnityEvent onConnectedToServer;
        public UnityEngine.Events.UnityEvent onDisconnectedFromServer;
        public UnityEngine.Events.UnityEvent onFailedToConnectToServer;

        private const string VERSION = "v1.0.2";

        private void Start()
        {
            AdManager.instance.HideBanner();
            if (!PhotonNetwork.connected)
            {
                PhotonNetwork.ConnectUsingSettings(VERSION);
            }
            else
            {
                onConnectedToServer.Invoke();
            }
        }

        public override void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            onFailedToConnectToServer.Invoke();
        }

        public override void OnConnectedToPhoton()
        {
            onConnectedToServer.Invoke();
        }

        public override void OnDisconnectedFromPhoton()
        {
            onDisconnectedFromServer.Invoke();
        }
    }
}
