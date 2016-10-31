﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tank
{
    public class OnlineJoinRoom : Photon.PunBehaviour
    {
        [SerializeField]
        private RectTransform m_roomEntryRoot;
        [SerializeField]
        private GameObject m_roomEntryPrefab;
        private string m_gameSceneName;

        public UnityEngine.Events.UnityEvent onJoinRoom;
        public UnityEngine.Events.UnityEvent onJoinRoomSuccess;
        public UnityEngine.Events.UnityEvent onJoinRoomFailed;

        private RoomInfo[] m_roomList;
        private List<OnlineRoomEntry> m_roomEntries;
        private bool m_isTryingToJoinRoom;

        private void Awake()
        {
            if (m_roomEntries == null)
                m_roomEntries = new List<OnlineRoomEntry>();
            m_isTryingToJoinRoom = false;
        }

        public void Refresh()
        {
            foreach (var e in m_roomEntries)
                GameObject.Destroy(e.gameObject);
            m_roomEntries.Clear();

            if (m_roomList == null || m_roomList.Length == 0)
                m_roomList = PhotonNetwork.GetRoomList();

            foreach (var room in m_roomList)
            {
                if (!room.visible)
                    continue;

                //Debug.Log(room.customProperties["map"]);

                GameObject entryGO = GameObject.Instantiate<GameObject>(m_roomEntryPrefab);
                entryGO.transform.SetParent(m_roomEntryRoot, false);

                OnlineRoomEntry entry = entryGO.GetComponent<OnlineRoomEntry>();
                entry.RoomName = string.Format("{0} - {1}/{2} - {3}",
                                                room.name.ToUpperInvariant(), room.playerCount, room.maxPlayers,
                                                room.customProperties["map"]);
                entry.Test = string.Format("{0} - {1}/{2} - {3}",
                                            room.name.ToUpperInvariant(), room.playerCount, room.maxPlayers,
                                            room.customProperties["map"]);
                entry.RoomID = room.name;
                entry.SetIsFull(room.playerCount == room.maxPlayers || !room.open);
                entry.JoinRoom += (obj) => {
                    if (!m_isTryingToJoinRoom)
                    {
                        m_isTryingToJoinRoom = true;
                        onJoinRoom.Invoke();
                        PhotonNetwork.JoinRoom(obj.RoomID);
                    }
                };
                m_roomEntries.Add(entry);
            }

           
        }

        public override void OnJoinedRoom()
        {
            m_isTryingToJoinRoom = false;
            onJoinRoomSuccess.Invoke();
        }

        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            m_isTryingToJoinRoom = false;
            onJoinRoomFailed.Invoke();
        }

        public override void OnReceivedRoomListUpdate()
        {
            m_roomList = PhotonNetwork.GetRoomList();
            if (m_roomEntryRoot.gameObject.activeInHierarchy)
                Refresh();
        }

    }
}