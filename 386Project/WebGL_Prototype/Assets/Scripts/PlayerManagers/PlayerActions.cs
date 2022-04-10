using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private CameraShakeComponent cameraShake;
    private Timer fireTimer = new Timer(0);

    private Timer reloadTimer = new Timer(0);
    private void Start()
    {
        var currentWeapon = playerInventory.GetCurrentWeapon();
        fireTimer = new Timer(1/currentWeapon.fireRate); //When Activating a weapon (inventory system)
        fireTimer.ForceComplete();
        reloadTimer.ForceComplete();
    }

    void Update()
    {
        var currentWeaponManager = playerInventory.GetCurrentWeaponManager();
        Debug.DrawLine(currentWeaponManager.weaponMuzzle.position,playerAim.crossairPivot.position,Color.green);
        
        if(reloadTimer.TimerDone()) FireWeaponAction(ref currentWeaponManager);
        ReloadWeapon(ref currentWeaponManager);
    }

    public void FireWeaponAction(ref WeaponManager currentWeaponManager)
    {
        playerInventory.SetSwap(true);
        if (currentWeaponManager.weapon.weaponType == WeaponType.AUTOMATIC)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K))|| Input.GetKey(KeyCode.K))
            {
                FireWeapon(ref currentWeaponManager);
            }
        }
        else if (currentWeaponManager.weapon.weaponType == WeaponType.SEMI_AUTO)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K)) || Input.GetKeyDown(KeyCode.K))
            {
                FireWeapon(ref currentWeaponManager);
            }
            
        }
        
        if(!fireTimer.TimerDone()) fireTimer.UpdateTimer(Time.deltaTime);

     }


    public void FireWeapon(ref WeaponManager weaponManager)
    {
        if (weaponManager.currentAmmoInMagazine > 0)
        {
            if (fireTimer.TimerDone())
            {
                weaponManager.TriggerFireAnimation(playerAim.GetDirection());
                cameraShake.TriggerCameraShake(weaponManager.weapon.cameraShakeTime,
                    weaponManager.weapon.cameraShake);
                weaponManager.TriggerMuzzleFlash();
                var newAmmo = Instantiate(weaponManager.weapon.ammoUsed.prefabAmmo,
                    weaponManager.weaponMuzzle.position, Quaternion.identity);
                var muzzleToCorsair = (playerAim.crossairPivot.position - weaponManager.weaponMuzzle.position);
                var bulletToGoDir = weaponManager.weaponMuzzle.position + (muzzleToCorsair.normalized);
                //TODO: FLIP NEW LINE
                var bulletDir = ((bulletToGoDir + (new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0) * Mathf.Abs(weaponManager.weapon.weaponFireRandomness))) - weaponManager.weaponMuzzle.position).normalized;
                newAmmo.transform.right = bulletDir;
                weaponManager.currentAmmoInMagazine--;
                /*if (weaponManager.currentAmmoInMagazine == 0 && weaponManager.totalAmmoAside > 0)
                {
                    ReloadWeapon();
                }*/ // Auto reload

                fireTimer.RestartTimer();
            }
        }
    }
    
    public void ReloadWeapon(ref WeaponManager weaponManager)
    {
        //Reload Anim
        if (Input.GetKeyDown(KeyCode.R) && weaponManager.currentAmmoInMagazine != weaponManager.magazineSize)
        {
            playerInventory.SetSwap(false);
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
