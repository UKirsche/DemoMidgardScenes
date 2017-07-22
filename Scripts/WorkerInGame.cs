using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;

public class WorkerInGame : Photon.MonoBehaviour
{
	public FreeLookCam Camera;

    public void Awake()
    {
		Camera = GameObject.FindObjectOfType<FreeLookCam> ();

        // in case we started this demo with the wrong scene being active, simply load the menu scene
        if (!PhotonNetwork.connected)
        {
            SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
            return;
        }

		Vector3 position = new Vector3( 1000, 50, 300 );
        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
		GameObject newPlayerObject=PhotonNetwork.Instantiate("UMARPG", position, Quaternion.identity, 0);
		Camera.SetTarget(newPlayerObject.transform);
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Return to Lobby"))
        {
            PhotonNetwork.LeaveRoom();  // we will load the menu level when we successfully left the room
        }
    }

    public void OnMasterClientSwitched(PhotonPlayer player)
    {
        Debug.Log("OnMasterClientSwitched: " + player);

        string message;
        InRoomChat chatComponent = GetComponent<InRoomChat>();  // if we find a InRoomChat component, we print out a short message

        if (chatComponent != null)
        {
            // to check if this client is the new master...
            if (player.isLocal)
            {
                message = "You are Master Client now.";
            }
            else
            {
                message = player.name + " is Master Client now.";
            }


            chatComponent.AddLine(message); // the Chat method is a RPC. as we don't want to send an RPC and neither create a PhotonMessageInfo, lets call AddLine()
        }
    }

    public void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom (local)");

        // back to main menu
        SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnDisconnectedFromPhoton");

        // back to main menu
        SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerConnected: " + player);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("OnPlayerDisconneced: " + player);
    }

    public void OnFailedToConnectToPhoton()
    {
        Debug.Log("OnFailedToConnectToPhoton");

        // back to main menu
        SceneManager.LoadScene(WorkerMenu.SceneNameMenu);
    }
}
