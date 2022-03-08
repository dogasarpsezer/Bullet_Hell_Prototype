using ScriptableObjects;
using UnityEngine;

public enum WeaponType
{
    SEMI_AUTO,
    AUTOMATIC
}

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons", order = 1)]
public class Weapon : ScriptableObject
{
    public string itemName;
    public Ammo ammoUsed;
    public float fireRate;
    public Sprite sprite;
    public float reloadTime;
    public float cameraShake;
    public float cameraShakeTime;
    public WeaponType weaponType;

}