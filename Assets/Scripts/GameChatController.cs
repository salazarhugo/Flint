using Photon.Pun;
using UnityEngine;

public class GameChatController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            SendMessageToChat("");
        }
    }

    void SendMessageToChat(string text)
    {

    }
}
