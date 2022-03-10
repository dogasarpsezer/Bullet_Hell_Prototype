using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    public class WeaponManager : MonoBehaviour
    {
        public Weapon weapon;
        public Transform weaponMuzzle;

        [Header("MuzzleFlash Anim")] 
        public SpriteRenderer muzzleFlashRenderer;
        public List<Sprite> muzzleFlashAnimSprites;
        public List<float> muzzleFlashAnimSpriteChangeSeconds;
        
        [Header("Recoil Anim")]
        public Transform armTransform;
        public float localAnimPosChange;
        public Vector3 originalLocalPosArm = Vector3.zero;
        
        [Header("Ammo Variables")]
        public int totalAmmoAside;
        public int currentAmmoInMagazine;
        public int magazineSize;

        private bool isRecoilAnimStarted = false;
        private Timer recoilAnimTimer;
        private Timer muzzleFlashTimer;
        private int muzzleFlashIndex;
        private bool isMuzzleAnimStarted = false;
        private Vector3 animStartPos = Vector3.zero;
        
        private void Start()
        {
            recoilAnimTimer = new Timer(1/weapon.fireRate);
            muzzleFlashTimer = new Timer(0);
            originalLocalPosArm = armTransform.localPosition;
            muzzleFlashIndex = 0;
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
            isRecoilAnimStarted = true;
            recoilAnimTimer.RestartTimer();
        }

        public void TriggerMuzzleFlash()
        {
            muzzleFlashTimer = new Timer(muzzleFlashAnimSpriteChangeSeconds[1]);
            muzzleFlashIndex = 1;
            isMuzzleAnimStarted = true;
        }

        public void MuzzleFlashAnim()
        {
            if (muzzleFlashTimer.TimerDone())
            {
                muzzleFlashIndex++;
                if (muzzleFlashIndex >= muzzleFlashAnimSpriteChangeSeconds.Count)
                {
                    isMuzzleAnimStarted = false;
                    muzzleFlashRenderer.sprite = muzzleFlashAnimSprites[0];
                    muzzleFlashIndex = 0;
                    return;
                }

                muzzleFlashRenderer.sprite = muzzleFlashAnimSprites[muzzleFlashIndex];
                muzzleFlashTimer = new Timer(muzzleFlashAnimSpriteChangeSeconds[muzzleFlashIndex]);
            }
            muzzleFlashTimer.UpdateTimer(Time.deltaTime);
        }
        
        public void ExitFireAnimation()
        {
            armTransform.localPosition = Vector3.Lerp(animStartPos, originalLocalPosArm, recoilAnimTimer.NormalizeTime());
            recoilAnimTimer.UpdateTimer(Time.deltaTime);
            if (recoilAnimTimer.TimerDone())
            {
                armTransform.localPosition = originalLocalPosArm;
                isRecoilAnimStarted = false;
            }
        }

        public bool GetMuzzleStat()
        {
            return isMuzzleAnimStarted;
        }
        
        private void Update()
        {
            if (isRecoilAnimStarted)
            {
                ExitFireAnimation();
            }

            if (isMuzzleAnimStarted)
            {
                MuzzleFlashAnim();
            }
        }
    }
    
}