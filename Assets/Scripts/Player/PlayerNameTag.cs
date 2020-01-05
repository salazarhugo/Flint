using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Text playerNameTag;

    void Start()
    {
        if (photonView.IsMine)
            return; 
        SetName();
    }

    private void SetName() => playerNameTag.text = photonView.Owner.NickName;
}
