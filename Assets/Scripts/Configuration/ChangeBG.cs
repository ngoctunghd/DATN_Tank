using UnityEngine;
using System.Collections;
using System;

public class ChangeBG : MonoBehaviour {

    public GameObject[] configBG;
    public GameObject[] stonePlayers;
    public GameObject[] brickPlayers;
    public GameObject[] brickEnemies;

    // Use this for initialization
    void Start () {
        
        int hourNow = DateTime.Now.Hour;
        if (hourNow > 6 && hourNow <= 18)
        {
            configBG[0].SetActive(true);
            configBG[1].SetActive(false);

            stonePlayers[0].SetActive(true);
            stonePlayers[1].SetActive(false);

            brickPlayers[0].SetActive(true);
            brickPlayers[1].SetActive(false);

            brickEnemies[0].SetActive(true);
            brickEnemies[1].SetActive(false);

        }

        if (hourNow > 18 && hourNow <= 23 || hourNow >= 0 && hourNow <= 6)
        {

            configBG[0].SetActive(false);
            configBG[1].SetActive(true);

            stonePlayers[0].SetActive(false);
            stonePlayers[1].SetActive(true);

            brickPlayers[0].SetActive(false);
            brickPlayers[1].SetActive(true);

            brickEnemies[0].SetActive(false);
            brickEnemies[1].SetActive(true);
        }
      
    }
	
}
