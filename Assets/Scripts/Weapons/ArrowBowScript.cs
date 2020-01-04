using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviourPunCallbacks
{
    #region Private Fields

    private Rigidbody myBody;

    #endregion

    #region Public Fields

    public float speed = 30f;

    public float deactivate_Timer = 3f;

    public float damage = 50f;

    #endregion

    #region Mono Callbacks

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Invoke("DeactivateGameObject", deactivate_Timer);
    }

    #endregion

    #region Public Methodss

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + myBody.velocity);
    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

 

    void OnTriggerEnter(Collider target)
    {
        if (!photonView.IsMine)
            return;
        // after we touch an enemy deactivate game object
        if (target.tag == Tags.PLAYER_TAG)
        {
            photonView.RPC("ShootArrow", RpcTarget.All, target);
            Destroy(gameObject);
        }
    }

    [PunRPC]
    void ShootArrow(Collider target)
    {
        target.GetComponent<HealthScript>().ApplyDamage(damage);
       
        //gameObject.SetActive(false);
    }
    #endregion
}