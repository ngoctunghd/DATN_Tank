using UnityEngine;
using System.Collections;

public class BackgroundSync : MonoBehaviour {

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    private PhotonView _photonView;

    void Awake () {

    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            //			stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            //			controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            this.transform.position = correctPlayerPos;
            this.transform.rotation = correctPlayerRot;
        }
    }
}
