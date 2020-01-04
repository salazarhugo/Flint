using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields

    [SerializeField]
    private WeaponHandler[] weapons;

    private int currentWeaponIndex;

    #endregion

    #region Mono Callbacks

    void Start()
    {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        if (photonView.IsMine)
            ProcessInput();
        else
        {
            foreach (WeaponHandler weapon in weapons) {
                weapon.gameObject.SetActive(false);
            }
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
            
    }

    #endregion
    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapon(5);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {

        if (currentWeaponIndex == weaponIndex && photonView.IsMine)
            return;

        // turn off the current weapon
        weapons[currentWeaponIndex].gameObject.SetActive(false);

        // turn on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);

        // store the current selected weapon index
        currentWeaponIndex = weaponIndex;

    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[currentWeaponIndex];
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentWeaponIndex);
        }
        else
        {
            this.currentWeaponIndex = (int)stream.ReceiveNext();
        }
    }
}