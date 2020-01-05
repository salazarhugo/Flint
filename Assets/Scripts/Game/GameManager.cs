using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{

    #region Public Fields

    static public GameManager Instance;
    public GameObject chatPanel, textObject;
    public InputField chatInputField;
    public ChatManager chatManager;

    #endregion

    #region Private Fields

    public GameObject menu;
    public GameObject chatMenu;
    public GameObject playerController;
    private bool isShowing;
    [SerializeField]
    private List<Message> messageList = new List<Message>();
    [SerializeField]
    private int maxMessages;

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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isShowing = !isShowing;
            chatInputField.gameObject.SetActive(isShowing);
            chatInputField.Select();
            chatInputField.ActivateInputField();

            if (!isShowing && chatInputField.text != "")
            {
                chatManager.sendMessage(chatInputField.text);
                chatInputField.text = "";
            }
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

    public void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}