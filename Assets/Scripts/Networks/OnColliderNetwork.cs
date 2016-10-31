using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tank
{

    public class OnColliderNetwork : MonoBehaviour
    {

        PhotonView m_PhotonView;
        public GameObject namePlayer;
        private float timeShowName;



        void Awake()
        {
            m_PhotonView = GetComponent<PhotonView>();          
        }

        void Start()
        {
            namePlayer.SetActive(true);
            timeShowName = 5f;
            
        }

        void Update()
        {
            timeShowName -= Time.deltaTime;
            if(timeShowName <= 0)
            {
                namePlayer.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D target)
        {
            if (target.tag == "bulletplayer" || target.tag == "bulletenemy")
            {

                if(gameObject.name == "Player2(Clone)")
                {
                    Globals.SetBool(GlobalKeys.PLAYER_ONE_WON, true);
                    Globals.SetBool(GlobalKeys.PLAYER_TWO_WON, false);
                    print(1);
                }

                else
                {
                    Globals.SetBool(GlobalKeys.PLAYER_ONE_WON, false);
                    Globals.SetBool(GlobalKeys.PLAYER_TWO_WON, true);
                }

                m_PhotonView.RPC("EndGameRPC", PhotonTargets.All);

                Time.timeScale = 0;
            }         
        }

        

        [PunRPC]
        void EndGameRPC()
        {
            SceneFader.instance.FadeIn("mp_online_win");

        }
    }
}
