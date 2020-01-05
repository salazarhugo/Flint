using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Public Fields

    //private EnemyAnimator enemy_Anim;

    //private NavMeshAgent navAgent;

    //private EnemyController enemy_Controller;

    private bool is_Dead;

    //private EnemyAudio enemyAudio;

    private PlayerStats playerStats;

    #endregion

    #region Public Fields

    public float health = 100f;

    public bool is_Player, is_Boar, is_Cannibal;

    #endregion


    #region Mono Callbacks

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (!photonView.IsMine && is_Dead)
        {
            //PlayerDied();
        }
    }

    #endregion

    #region Public Methods

    public void ApplyDamage(float damage)
    {
        Debug.Log("ApplyDamage " + damage);
        /*if (is_Dead)
            return;*/
        health -= damage;
  
        if (is_Player)
        {
            //playerStats.DisplayHealthStats(health);
        } 
        if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;
        }
    }

    void PlayerDied()
    {//4
        is_Dead = false;
        health = 100f;
        transform.position = new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10f));
        Debug.Log("PlayerDied" + this.name);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(is_Dead);
        }
        else
        {
            this.health = (float)stream.ReceiveNext();
            this.is_Dead = (bool)stream.ReceiveNext();
            Debug.Log("ReceiveNext " + this.health);
        }
    }

    #endregion

}
