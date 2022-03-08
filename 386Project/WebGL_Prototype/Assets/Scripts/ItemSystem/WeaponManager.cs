using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class WeaponManager : MonoBehaviour
    {
        public Weapon weapon;
        public Transform weaponMuzzle;
        
        [Header("Recoil Anim")]
        public Transform armTransform;
        public float localAnimPosChange;
        public Vector3 originalLocalPosArm = Vector3.zero;
        
        [Header("Ammo Variables")]
        public int totalAmmoAside;
        public int currentAmmoInMagazine;
        public int magazineSize;

        private bool isAnimStarted = false;
        private Timer animTimer;
        private Vector3 animStartPos = Vector3.zero;
        private void Start()
        {
            animTimer = new Timer(1/weapon.fireRate);
            originalLocalPosArm = armTransform.localPosition;
        }

        public void TriggerFireAnimation(int directionOfCharacter)
        {
            var change = (localAnimPosChange) * armTransform.right;
            if (directionOfCharacter < 0)
            {
                change.y *= -1;
            }
            armTransform.localPosition += change;
            animStartPos = armTransform.localPosition;
            isAnimStarted = true;
            animTimer.RestartTimer();
        }

        public void ExitFireAnimation()
        {
            armTransform.localPosition = Vector3.Lerp(animStartPos, originalLocalPosArm, animTimer.NormalizeTime());
            animTimer.UpdateTimer(Time.deltaTime);
            if (animTimer.TimerDone())
            {
                armTransform.localPosition = originalLocalPosArm;
                isAnimStarted = false;
            }
        }
        private void Update()
        {
            if (isAnimStarted)
            {
                ExitFireAnimation();
            }
        }
    }
    
}