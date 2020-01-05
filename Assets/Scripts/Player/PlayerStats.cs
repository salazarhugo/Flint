using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviourPunCallbacks
{

    #region Private Fields
   
    #endregion

    #region Public Fields

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;
    [SerializeField]
    private Camera mainCam;
    public HealthScript healthScript;

    #endregion

    #region Mono Callbacks

    private void Awake()
    {
        healthScript = GetComponent<HealthScript>();
    }

    void Start()
    {
        if (PlayerUiPrefab != null && photonView.IsMine)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods

    #endregion
}
