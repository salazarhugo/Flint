using ExitGames.Client.Photon;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    ConnectionProtocol connectProtocol = ConnectionProtocol.Udp;
    ChatClient chatClient;

    void Awake()
    {
        AuthenticationValues authValues = new AuthenticationValues();
        authValues.UserId = "uniqueUserNameHere";
        authValues.AuthType = CustomAuthenticationType.None;
        chatClient = new ChatClient(this, connectProtocol);
        chatClient.ChatRegion = "EU";
        chatClient.Connect("c212f995-7a94-43d4-9b01-067a8b58d7af", "0.01", authValues);
    }

    void Update()
    {
        if (chatClient != null) { chatClient.Service(); }

    }

    public void sendMessage(string message)
    {
        chatClient.PublishMessage("channelNameHere", message);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
      
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { "channelNameHere" }); //subscribe to chat channel once connected to server
        chatClient.PublishMessage("channelNameHere", "salut les kheys");
    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Debug.Log("sender: " + senders[0] + "message: " + messages[0]);

    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed to a new channel!");
    }

    public void OnUnsubscribed(string[] channels)
    {
      
    }

    public void OnUserSubscribed(string channel, string user)
    {
      
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
       
    }
}
