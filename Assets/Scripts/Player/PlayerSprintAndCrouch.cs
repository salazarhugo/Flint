using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields

    private PlayerMovement playerMovement;
    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;
    private bool isCrouching;
    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;
    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;
    private float sprint_Value = 100f;

    #endregion

    #region Public Fields

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;
    public float sprint_Treshold = 10f;

    #endregion

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_Root = transform.GetChild(0);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Sprint();
            Crouch();
        }
        else
        {//
            if (isCrouching)
            {
                // if remote player is crouching then set him to crouch postition
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
            }
            else
            {
                // if remote player is not crouching then set him to standing up postition
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
            }
        }
      
    }

    void Sprint()
    {
        // if we have stamina we can sprint
        if (sprint_Value > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                playerMovement.speed = sprint_Speed;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = move_Speed;
        }
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprint_Value -= sprint_Treshold * Time.deltaTime;

            if (sprint_Value <= 0f)
            {
                sprint_Value = 0f;
                // reset the speed and sound
                playerMovement.speed = move_Speed;
            }
        }
        else
        {
            if (sprint_Value != 100f)
            {
                sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;
                if (sprint_Value > 100f)
                {
                    sprint_Value = 100f;
                }
            }
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // if we are crouching - stand up
            if (isCrouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;
                isCrouching = false;
            }
            else
            {
                // if we are not crouching - crouch
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;
                isCrouching = true;
            }
        } 
    }

    #region IPunObservable Implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isCrouching);
        }
        else
        {
            this.isCrouching = (bool)stream.ReceiveNext();
        }
    }

    #endregion
}