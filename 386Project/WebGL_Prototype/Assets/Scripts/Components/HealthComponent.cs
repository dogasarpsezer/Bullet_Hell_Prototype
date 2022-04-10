using System;
using System.Collections;
using System.Collections.Generic;
using GeneralLibrary;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private float hitAnimTime;
    [SerializeField] private Vector3 hitSize;
    [SerializeField] private Gradient gradient;

    private Timer hitTimer = new Timer(0);
    private Vector3 originalSize = Vector3.zero;
    private void Start()
    {
        originalSize = transform.localScale;
        hitTimer = new Timer(hitAnimTime);
        hitTimer.ForceComplete();
    }

    private void Update()
    {
        if(!hitTimer.TimerDone()) HitAnim();
    }
    
    public void EntityHit(ref float damage, Vector3 hitPosition)
    {
        StartHitAnim();
        ParticleFunctions.CreateParticleEffect(hitParticle,gradient,hitPosition);
        GetDamage(damage);
    }
    
    private void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*private void CreateHitParticle(Vector3 hitPosition)
    {
        var particleObject = Instantiate(hitParticle, hitPosition, Quaternion.identity);
        particleObject.GetComponent<ParticleComponent>().ChangeColorOverLifeTime(gradient);
    }*/

    private void StartHitAnim()
    {
        hitTimer.RestartTimer();
        transform.localScale = hitSize;
    }

    private void HitAnim()
    {
        transform.localScale = Vector3.Lerp(hitSize,originalSize,hitTimer.NormalizeTime());
        hitTimer.UpdateTimer(Time.deltaTime);
        if (hitTimer.TimerDone())
        {
            transform.localScale = originalSize;
        }
    }
    
}
