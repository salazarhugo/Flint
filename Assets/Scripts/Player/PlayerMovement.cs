using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float gravity = 20f;
    private float verticalVelocity;
    private Vector3 TargetPosition;

    #endregion

    #region Public Fields

    public float speed = 5f;
    public float jump_Force = 10f;

    #endregion

    #region Mono Callbacks
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (photonView.IsMine)
            MoveThePlayer();
        else
        {
            //characterController.transform.position = TargetPosition;//Vector3.Lerp(transform.position, TargetPosition, 0.5f);
        }
    }

    #endregion

    #region Private Methods

    void MoveThePlayer()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;
        ApplyGravity();
        characterController.Move(moveDirection);
    } 

    void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        PlayerJump();
        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jump_Force;
        }
    }

    #endregion

    #region IPunObservable Implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            TargetPosition = (Vector3)stream.ReceiveNext();
        }
    }

    #endregion
}
