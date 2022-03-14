using UnityEngine;

namespace GeneralLibrary
{
    public static class MoveTo
    {
        public static Vector3 TravelTowards(Vector3 pos, Vector3 direction, float movement)
        {
            return pos + (direction * movement);
        }
    }
}