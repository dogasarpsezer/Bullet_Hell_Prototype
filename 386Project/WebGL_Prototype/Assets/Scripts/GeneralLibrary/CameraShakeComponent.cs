using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeComponent : MonoBehaviour
{
    [SerializeField] private float targetSize;
    [SerializeField] private float shakeNormalizeTime;
    private Camera mainCamera;
    private float originalSize;
    private Timer shakeTimer = new Timer(0);
    void Awake()
    {
        mainCamera = Camera.main;
        originalSize = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        CameraShake();
    }

    public void TriggerCameraShake(float shakeTime,float shakeAmount)
    {
        targetSize = shakeAmount;
        mainCamera.orthographicSize = targetSize;
        shakeTimer = new Timer(shakeTime);
    }

    public void CameraShake()
    {
        if (!shakeTimer.TimerDone())
        {
            mainCamera.orthographicSize = Mathf.Lerp(targetSize,originalSize,shakeTimer.NormalizeTime());
            shakeTimer.UpdateTimer(Time.deltaTime);
        }
        else
        {
            mainCamera.orthographicSize = originalSize;
        }
    }
}
