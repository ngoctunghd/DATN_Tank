using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Tank
{
    public class OnlineRoomEntry : MonoBehaviour
    {
        public event Action<OnlineRoomEntry> JoinRoom;

        [SerializeField]
        private Text m_roomName;
        [SerializeField]
        private Button m_joinButton;
        [SerializeField]
        private Color m_roomFullColor;
        [SerializeField]
        private Color m_roomNotFullColor;

        public Text test;

        public string Test
        {
            get { return test.text; }
            set { test.text = value; }
        }

        public string RoomID { get; set; }
        public string RoomName
        {
            get { return m_roomName.text; }
            set { m_roomName.text = value; }
        }

        private void Awake()
        {
            m_joinButton.onClick.AddListener(HandleJoinButtonClicked);
        }

        private void HandleJoinButtonClicked()
        {
            if (JoinRoom != null)
                JoinRoom(this);
        }

        public void SetIsFull(bool isFull)
        {
            m_joinButton.interactable = !isFull;
            m_roomName.color = isFull ? m_roomFullColor : m_roomNotFullColor;

            GetComponent<Image>().color = isFull ? new Color(93f / 255f, 15f / 255f, 255f / 255f, 208f / 255f)
                                                 : new Color(17f / 255f, 134f / 255f, 249f / 255f, 208f / 255f);    
        }
    }
}