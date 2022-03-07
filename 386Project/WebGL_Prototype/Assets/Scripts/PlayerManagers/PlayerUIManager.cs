using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Text itemNameText;
    [SerializeField] private WeaponManager weaponManager;

    void Update()
    {
        itemNameText.text = weaponManager.weapon.itemName;
        ammoText.text = weaponManager.currentAmmoInMagazine + " / " + weaponManager.totalAmmoAside;
    }
}
