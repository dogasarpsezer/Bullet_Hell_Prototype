using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleComponent : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    private void Update()
    {
        if(!particleSystem.isPlaying) Destroy(gameObject);
    }

    public void ChangeColorOverLifeTime(Gradient gradient)
    {
        var colOverLifeTime = particleSystem.colorOverLifetime;
        colOverLifeTime.enabled = true;
        colOverLifeTime.color = gradient;
    }
}
