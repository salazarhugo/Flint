using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviourPunCallbacks
{
    #region Private Fields

    private WeaponManager weaponManager;
  
    private float nextTimeToFire;

    private Animator zoomCameraAnim;

    private bool zoomed;

    [SerializeField]
    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Bow_StartPosition;

    #endregion

    #region Public Fields

    public float fireRate = 15f;

    public float damage = 20f;

    #endregion


    #region Mono Callbacks

    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        //mainCam = Camera.main;
    }
    void Update()
    {
        if (!photonView.IsMine)
            return;

        WeaponShoot();
        ZoomInAndOut();
    }

    #endregion

    #region Public Methods

    void WeaponShoot()
    {
        // if we have assault riffle
        if (weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            // if we press and hold left mouse click AND
            // if Time is greater than the nextTimeToFire
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            {
                photonView.RPC("Shoot", RpcTarget.All);
                nextTimeToFire = Time.time + 1f / fireRate;
                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            }
            // if we have a regular weapon that shoots once
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // handle axe
                if (weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                // handle shoot
                if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    photonView.RPC("Shoot", RpcTarget.All); 
                }
                else
                {
                    // we have an arrow or spear
                    if (is_Aiming)
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if (weaponManager.GetCurrentSelectedWeapon().bulletType
                           == WeaponBulletType.ARROW)
                        {
                            // throw arrow
                            ThrowArrowOrSpear(true);
                        }
                        else if (weaponManager.GetCurrentSelectedWeapon().bulletType
                                == WeaponBulletType.SPEAR)
                        {
                            // throw spear
                            ThrowArrowOrSpear(false);
                        }
                    }
                } 
            }
        } 
    }

    void ZoomInAndOut()
    {
        // we are going to aim with our camera on the weapon
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            // if we press and hold right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }
            // when we release the right mouse button click
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        } // if we need to zoom the weapon

        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;
            }
        }
    }

void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.position;
            arrow.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
        else
        {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.position;
            spear.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
    }

    [PunRPC]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
          
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
            }
            if (hit.transform.CompareTag(Tags.PLAYER_TAG))
            {
                Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 100, Color.green, 2f);
                Debug.Log("Did hit " + hit.collider.name);
                hit.transform.GetComponent<HealthScript>().ApplyDamage(weaponManager.GetCurrentSelectedWeapon().damage);
            } else
            {
                Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 100, Color.red, 2f);
            }
        }
        else
        {
            //Did not it...
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 100, Color.white, 2f);
            Debug.Log("Did not it");
        }
        
    }

    #endregion
}
