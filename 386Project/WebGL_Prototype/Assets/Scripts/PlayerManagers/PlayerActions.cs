using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private CameraShakeComponent cameraShake;
    private Timer timer;

    private Timer reloadTimer = new Timer(0);
    private void Start()
    {
        timer = new Timer(1/weaponManager.weapon.fireRate); //When Activating a weapon (inventory system)
        timer.ForceComplete();
        reloadTimer.ForceComplete();
    }

    void Update()
    {
        Debug.DrawLine(weaponManager.weaponMuzzle.position,playerAim.crossairPivot.position,Color.green);
        
        if(reloadTimer.TimerDone())FireWeaponAction();
        ReloadWeapon();
    }

    public void FireWeaponAction()
    {
        if (weaponManager.weapon.weaponType == WeaponType.AUTOMATIC)
        {
            if (/*Input.GetMouseButton(0)*/ Input.GetKey(KeyCode.K))
            {
                FireWeapon();
            }
        }
        else if (weaponManager.weapon.weaponType == WeaponType.SEMI_AUTO)
        {
            if (/*Input.GetMouseButtonDown(0)*/ Input.GetKeyDown(KeyCode.K))
            {
                FireWeapon();
            }
            
        }
        
        if(!timer.TimerDone()) timer.UpdateTimer(Time.deltaTime);

     }


    public void FireWeapon()
    {
        if (weaponManager.currentAmmoInMagazine > 0)
        {
            if (timer.TimerDone())
            {
                weaponManager.TriggerFireAnimation(playerAim.GetDirection());
                cameraShake.TriggerCameraShake(weaponManager.weapon.cameraShakeTime,
                    weaponManager.weapon.cameraShake);
                var newAmmo = Instantiate(weaponManager.weapon.ammoUsed.prefabAmmo,
                    weaponManager.weaponMuzzle.position, Quaternion.identity);
                newAmmo.transform.right = weaponManager.weaponMuzzle.right * playerAim.GetDirection();
                weaponManager.currentAmmoInMagazine--;
                /*if (weaponManager.currentAmmoInMagazine == 0 && weaponManager.totalAmmoAside > 0)
                {
                    ReloadWeapon();
                }*/ // Auto reload

                timer.RestartTimer();
            }
        }
    }
    
    public void ReloadWeapon()
    {
        //Reload Anim
        if (Input.GetKeyDown(KeyCode.R) && weaponManager.currentAmmoInMagazine != weaponManager.magazineSize)
        {
            reloadTimer = new Timer(weaponManager.weapon.reloadTime);
            var prevAmmoCount = weaponManager.currentAmmoInMagazine;
            weaponManager.totalAmmoAside += prevAmmoCount;
            weaponManager.currentAmmoInMagazine =
                Mathf.Clamp(weaponManager.totalAmmoAside, 0, weaponManager.magazineSize);
            weaponManager.totalAmmoAside -= weaponManager.currentAmmoInMagazine;
        }
        
        if(!reloadTimer.TimerDone()) reloadTimer.UpdateTimer(Time.deltaTime);
    }
}
