using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{

    #region Private Fields

    private Animator animator;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    #endregion

    #region Public Fields

    public WeaponAim weaponAim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public float damage;
    public float fireRate;
    public GameObject attackPoint;

    #endregion

    #region Mono Callbacks

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    #endregion

    #region Public Methods

    public void ShootAnimation()
    {
        Debug.Log("ShootAnimation");
        animator.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        animator.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        shootSound.Play();
    }

    void Play_ReloadSound()
    {
        reload_Sound.Play();
    }

    void Turn_On_AttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    #endregion

}
