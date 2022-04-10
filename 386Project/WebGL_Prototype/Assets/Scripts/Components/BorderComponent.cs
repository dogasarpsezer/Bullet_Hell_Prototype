using System.Collections;
using System.Collections.Generic;
using GeneralLibrary;
using UnityEngine;

public class BorderComponent : MonoBehaviour
{
    [SerializeField] private GameObject particleObject;
    [SerializeField] private Gradient particleGradient;
    
    // Start is called before the first frame update
    public void CreateBorderParticle(Vector3 hitPoint)
    {
        ParticleFunctions.CreateParticleEffect(particleObject,particleGradient,hitPoint);        
    }
}
