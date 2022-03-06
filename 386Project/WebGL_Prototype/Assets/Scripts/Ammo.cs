using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Ammunition", order = 1)]
    public class Ammo : ScriptableObject
    {
        public GameObject prefabAmmo;
        public float speed;
        public float standardDamage;
    }
}