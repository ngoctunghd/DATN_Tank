using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tank
{
    [RequireComponent(typeof(Text))]
    public class MultiplayerWinTitle : MonoBehaviour
    {
        private PhotonView photonView;
        
        [SerializeField]
        private bool m_isOnlineMatch;

        private Text text;

        void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        void Start()
        {
            text = GetComponent<Text>();

            bool playerOneWon = Globals.GetBool(GlobalKeys.PLAYER_ONE_WON, false);
            bool playerTwoWon = Globals.GetBool(GlobalKeys.PLAYER_TWO_WON, false);

            if (playerOneWon)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    text.text = "You Win";
                    photonView.RPC("SetYouLose", PhotonTargets.Others);

                }
                //text.text = "Player 1 Win";
            }
            else if (playerTwoWon)
            {
                //if (PhotonNetwork.isNonMasterClientInRoom)
                //{

                //    text.text = "You Win";
                //}

                if (PhotonNetwork.isMasterClient)
                {
                    text.text = "You Lose";
                    photonView.RPC("SetYouWin", PhotonTargets.Others);

                }
                //text.text = "Player 2 Win";
            }              
         
        }

        [PunRPC]
        void SetYouWin()
        {
            text.text = "You Win";
        }

        [PunRPC]
        void SetYouLose()
        {
            text.text = "You Lose";
        }
    }
}