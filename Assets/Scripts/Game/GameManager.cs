using Photon.Pun;
using System.IO;
using UnityEngine;
public class GameManager : MonoBehaviourPunCallbacks
{

    #region Public Fields

    static public GameManager Instance;

    #endregion

    #region Private Fields

    public GameObject menu;
    public GameObject chatMenu;
    public GameObject playerController;
    private bool isShowing;

    #endregion

    public void setMenu(bool b)
    {
        menu.SetActive(b);
    }

    #region MonoBehaviour CallBacks
    void Start()
    {
        Instance = this;
    
        CreatePlayer();
        menu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isShowing = !isShowing;
            chatMenu.SetActive(isShowing);
        }
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    #endregion

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", Tags.PLAYER_TAG), new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10f)), Quaternion.identity);
    }

    public void quitGame()
    {
        Debug.Log("Quiting Game");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}