using UnityEngine;

namespace GeneralLibrary
{
    public static class ParticleFunctions
    {
        public static void CreateParticleEffect(GameObject particleObject, Gradient gradient, Vector3 hitPosition)
        {
            var newObject = GameObject.Instantiate(particleObject, hitPosition, Quaternion.identity);
            newObject.GetComponent<ParticleComponent>().ChangeColorOverLifeTime(gradient);
        }
    }
}