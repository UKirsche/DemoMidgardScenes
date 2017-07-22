using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.ThirdPerson;

public class ThirdPersonNetwork : Photon.MonoBehaviour
{
    ThirdPersonCamera cameraScript;
    ThirdPersonController controllerScript;

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    void Awake()
    {
        cameraScript = GetComponent<ThirdPersonCamera>();
        controllerScript = GetComponent<ThirdPersonController>();

        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            cameraScript.enabled = true;
            controllerScript.enabled = true;
        }
        else
        {   
            //Anderer Spieler -> hier muss nur Rotation und Position angepasst werden, daher muss er f¨¹r uns kontrollierbar sein      
            cameraScript.enabled = false;
            controllerScript.enabled = true;
            controllerScript.isControllable = false;
        }

        gameObject.name = gameObject.name + photonView.viewID;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation); 
        }
        else
        {
			//Teste State-?bertragung
			//stream.ReceiveNext();
			//controllerScript._characterState = CharacterState.Idle;
			//Network player, receive data ---> passe seine Position in unserer Welt an
            controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext(); 
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }


    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }

}