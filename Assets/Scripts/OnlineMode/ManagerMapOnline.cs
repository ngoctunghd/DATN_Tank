using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerMapOnline : Photon.MonoBehaviour
{

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    public int childCount;
    public Transform[] childs;

    void Start()
    {
        childs = this.GetComponentsInChildren<Transform>();
        childCount = this.transform.childCount;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            //			stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            for(int i = 0; i < childCount; i++)
            {
               // stream.SendNext(this.transform.GetChild(i).position);
            }

        }
        else
        {
            //Network player, receive data
            //			controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            this.transform.position = correctPlayerPos;
            this.transform.rotation = correctPlayerRot;

            for (int i = 0; i < childCount; i++)
            {
                //this.transform.GetChild(i).position = (Vector3)stream.ReceiveNext();
            }
        }
    }

    private void sendChilds()
    {

    }

    private void updateChilds()
    {

    }

}
