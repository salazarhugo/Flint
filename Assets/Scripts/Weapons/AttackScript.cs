using Photon.Pun;
using UnityEngine;

public class AttackScript : MonoBehaviourPunCallbacks
{
    #region Public Fields

    public float damage = 2f;
    public float radius = 1f;
    public LayerMask layerMask;
    public PlayerAttack playerAttack;

    #endregion

    #region MonoBehaviourCallbacks

    void Update()
    {
        /*if (!photonView.IsMine)
            return;*/
        Attack();
        //photonView.RPC("Attack", RpcTarget.All);
    }

    #endregion

    #region Private Methods

    [PunRPC]
    void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }

    #endregion
}