using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<WeaponManager> weaponManagersOnPlayer;
    private WeaponManager currentWeapon;

    private int currentIndex = 0;
    private bool canSwap = true;

    private void Start()
    {
        int count = 0;
        while (count < weaponManagersOnPlayer.Count)
        {
            if (weaponManagersOnPlayer[count])
            {
                currentWeapon = weaponManagersOnPlayer[count];
                break;
            }
            count++;
        }
    }

    private void Update()
    {
        if(canSwap && !currentWeapon.GetMuzzleStat()) ChangeWeapon();
    }

    public WeaponManager GetCurrentWeaponManager()
    {
        return currentWeapon;
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon.weapon;
    }
    
    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { WeaponSwap(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { WeaponSwap(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { WeaponSwap(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { WeaponSwap(3); }
    }

    public void WeaponSwap(int index)
    {
        if (index >= weaponManagersOnPlayer.Count || currentIndex == index) return;
        if (!weaponManagersOnPlayer[index]) return;
        
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weaponManagersOnPlayer[index];
        currentIndex = index;
        currentWeapon.gameObject.SetActive(true);
    }

    public void SetSwap(bool canSwap)
    {
        this.canSwap = canSwap;
    }
    
}
