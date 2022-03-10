using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Text itemNameText;
    [SerializeField] private PlayerInventory playerInventory;

    void Update()
    {
        var currentWeaponManager = playerInventory.GetCurrentWeaponManager();
        itemNameText.text = currentWeaponManager.weapon.itemName;
        ammoText.text = currentWeaponManager.currentAmmoInMagazine + " / " + currentWeaponManager.totalAmmoAside;
    }
}
