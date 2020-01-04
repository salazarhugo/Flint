using Photon.Pun;
using UnityEngine;

public class MainMenuController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int campaignSceneIndex;
    [SerializeField]
    private int multiplayerSceneIndex;
    [SerializeField]
    private int coopSceneIndex;

    public void StartCampaign()
    {
        PhotonNetwork.LoadLevel(campaignSceneIndex);
    }
    public void StartMultiplayer()
    {
        print("42");
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }
    public void StartCoop()
    {
        PhotonNetwork.LoadLevel(coopSceneIndex);
    }
}
