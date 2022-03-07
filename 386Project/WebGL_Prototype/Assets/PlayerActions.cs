using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private PlayerAim playerAim;

    private Timer timer;

    private void Start()
    {
        timer = new Timer(1/weaponManager.weapon.fireRate); //When Activating a weapon (inventory system)
        timer.ForceComplete();
    }

    void Update()
    {
        Debug.DrawLine(weaponManager.weaponMuzzle.position,playerAim.crossairPivot.position,Color.green);
        FireWeaponAction();
        ReloadWeapon();
    }

    public void FireWeaponAction()
    {
        if (Input.GetMouseButton(0) && weaponManager.currentAmmoInMagazine > 0)
        {
            if (timer.TimerDone())
            {
                weaponManager.TriggerFireAnimation(playerAim.GetDirection());
                var newAmmo = Instantiate(weaponManager.weapon.ammoUsed.prefabAmmo, weaponManager.weaponMuzzle.position,Quaternion.identity);
                newAmmo.transform.right = weaponManager.weaponMuzzle.right * playerAim.GetDirection();
                weaponManager.currentAmmoInMagazine--;
                if (weaponManager.currentAmmoInMagazine == 0 && weaponManager.totalAmmoAside > 0)
                {
                    weaponManager.currentAmmoInMagazine = weaponManager.magazineSize;
                    weaponManager.totalAmmoAside -= weaponManager.magazineSize;
                }
                timer.RestartTimer();
            }
            timer.UpdateTimer(Time.deltaTime);
        }
    }

    public void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R) && weaponManager.currentAmmoInMagazine != weaponManager.magazineSize)
        {
            var prevAmmoCount = weaponManager.currentAmmoInMagazine;
            weaponManager.totalAmmoAside += prevAmmoCount;
            weaponManager.currentAmmoInMagazine =
                Mathf.Clamp(weaponManager.totalAmmoAside, 0, weaponManager.magazineSize);
            weaponManager.totalAmmoAside -= weaponManager.currentAmmoInMagazine;
        }
    }
}
