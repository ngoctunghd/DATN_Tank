using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tank
{
    public class HostGameOnClick : Photon.PunBehaviour
    {
        [SerializeField]
        private InputField m_playerName;
        [SerializeField]
        private bool m_isRoomVisible;
        [SerializeField]
        private bool m_isRoomOpen;
        [SerializeField]
        private byte m_maxPlayersInRoom;

        public UnityEngine.Events.UnityEvent onHostGame;
        public UnityEngine.Events.UnityEvent onHostGameSuccess;
        public UnityEngine.Events.UnityEvent onHostGameFail;

        private bool m_isTryingToHostGame = false;

        public void HostGame()
        {
            if (!m_isTryingToHostGame)
            {
                string[] customPropertiesForLobby = new string[1];
                customPropertiesForLobby[0] = "map";
                
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.isVisible = m_isRoomVisible;
                roomOptions.isOpen = m_isRoomOpen;
                roomOptions.maxPlayers = m_maxPlayersInRoom;

                roomOptions.customRoomPropertiesForLobby = customPropertiesForLobby;
                
                roomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", MapManager.instance.nameMap } };
                Debug.Log(MapManager.instance.nameMap);

                m_isTryingToHostGame = true;
                onHostGame.Invoke();            

                TypedLobby ty = new TypedLobby();

                PhotonNetwork.CreateRoom(m_playerName.text, roomOptions, ty);               
            }
        }

        public override void OnJoinedRoom()
        {
            m_isTryingToHostGame = false;
            onHostGameSuccess.Invoke();
        }

        public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            m_isTryingToHostGame = false;
            onHostGameFail.Invoke();
        }
    }
}
