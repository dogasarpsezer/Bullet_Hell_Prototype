using System;
using System.Collections;
using System.Collections.Generic;
using GeneralLibrary;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoManager : MonoBehaviour
{

    [SerializeField] private Ammo ammo;
    [SerializeField] private Transform rayStartBottomOffset;
    [SerializeField] private Transform rayEndBottomOffset;
    [SerializeField] private int collisionLevelOfDetail = 1;
    [SerializeField] private float[] rayLengths;
    [SerializeField] private ParticleSystem particleSystem;

    void Update()
    {
        transform.position = MoveTo.TravelTowards(transform.position,transform.right,ammo.speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        var startPos = rayStartBottomOffset.position;
        var endPos = rayEndBottomOffset.position;
        DrawDetailedRay(ref collisionLevelOfDetail,ref startPos,ref endPos);
    }


    private void OnDrawGizmos()
    {
        var startPos = rayStartBottomOffset.position;
        var endPos = rayEndBottomOffset.position;
        DrawDetailedRay(ref collisionLevelOfDetail,ref startPos,ref endPos);
    }

    public void DrawDetailedRay(ref int collisionDetail, ref Vector3 startPos, ref Vector3 endPos)
    {
        var dir = endPos - startPos;
        var yDistance = Mathf.Abs(dir.magnitude);
        var gapDistance = yDistance / (Mathf.Abs(collisionDetail + 1));

        var currentPos = startPos;
        for (int i = 0; i < Mathf.Abs(collisionDetail); i++)
        {
            currentPos += dir.normalized * gapDistance;
            Ray ray = new Ray(currentPos, transform.right);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLengths[i]);
            if(!Application.isPlaying) Gizmos.DrawLine(currentPos,ray.GetPoint(rayLengths[i]));
            if (hit.collider)
            {
                if (hit.collider.CompareTag("Block") && (Random.Range(0,100) >= ammo.hitBlockChance))
                {
                    return;
                }
                var hitHealtComponent = hit.collider.GetComponent<HealthComponent>();
                hitHealtComponent.EntityHit(ref ammo.standardDamage,hit.point);
                Destroy(gameObject);
            }
        }
    }
}
