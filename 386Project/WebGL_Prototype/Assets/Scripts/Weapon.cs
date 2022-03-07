﻿using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons", order = 1)]
public class Weapon : ScriptableObject
{
    public string itemName;
    public Ammo ammoUsed;
    public float fireRate;
    public Sprite sprite;

}