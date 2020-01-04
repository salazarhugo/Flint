using Photon.Pun;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("You are now connected to " + PhotonNetwork.CloudRegion + " server!");
        //PhotonNetwork.JoinRandomRoom();
    }

}
