using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Com.Repsol.FPS.Launcher
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private string gameVersion = "1";

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            Screen.SetResolution(1000, 600, FullScreenMode.Windowed);
            Connect();
        }

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("You are now connected to " + PhotonNetwork.CloudRegion + " server!");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }
    }
}