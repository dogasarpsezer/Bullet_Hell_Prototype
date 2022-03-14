using System;
using System.Collections;
using System.Collections.Generic;
using GeneralLibrary;
using ScriptableObjects;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    [SerializeField] private Ammo ammo;

    [SerializeField] private float rayLength;
    [SerializeField] private Transform rayStartBottomOffset;
    [SerializeField] private Transform rayEndBottomOffset;
    [SerializeField] private int collisionLevelOfDetail = 1;
    void Update()
    {
        transform.position = MoveTo.TravelTowards(transform.position,transform.right,ammo.speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        var halfPoint = new Vector3((rayStartBottomOffset.position.x + rayEndBottomOffset.position.x) / 2,
            (rayEndBottomOffset.position.y + rayStartBottomOffset.position.y) / 2, 0);
        Ray ray = new Ray(halfPoint, transform.right);
        Debug.DrawRay(ray.origin,ray.direction);
        RaycastHit hit;
        int layer = LayerMask.NameToLayer("Default");
        if (Physics2D.Raycast(ray.origin,ray.direction,rayLength))
        {
            Destroy(gameObject);
        }
    }
}
