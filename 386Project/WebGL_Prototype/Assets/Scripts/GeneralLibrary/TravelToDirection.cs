using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class TravelToDirection : MonoBehaviour
{

    [SerializeField] private Ammo ammo;
    void Update()
    {
        var direction = transform.right;
        transform.position += (ammo.speed * Time.deltaTime) * direction;
    }
}
