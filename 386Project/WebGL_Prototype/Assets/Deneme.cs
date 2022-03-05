using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Deneme : MonoBehaviour
{
    [SerializeField] private Transform playerPivot;
    [SerializeField] private Transform crossairPivot;
    [SerializeField] private Vector2 rotationLimits;
    [SerializeField] private Transform arm;
    [SerializeField] Vector3 planeNormal;
    private Camera mainCamera;
    private Plane inputPlane;

    void Start()
    {
        //Cursor.visible = false;
        mainCamera = Camera.main;
        inputPlane = new Plane(planeNormal, Vector3.zero);
    }

    void Update()
    {
        var mouseScreenPosRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (inputPlane.Raycast(mouseScreenPosRay,out float enter))
        {
            Vector3 inputPos = mouseScreenPosRay.GetPoint(enter);
            inputPos = Vector3.ProjectOnPlane(inputPos, inputPlane.normal);
            crossairPivot.position = inputPos;

            float angleInit = 0;
            float limitX = rotationLimits.x;
            float limitY = rotationLimits.y;
            if (crossairPivot.localPosition.x < 0)
            {
                angleInit = 180;
                playerPivot.localScale= new Vector3(-1,1,1);
            }
            else
            {
                playerPivot.localScale= new Vector3(1,1,1);
            }
            
            arm.eulerAngles = new Vector3(arm.eulerAngles.x,arm.eulerAngles.y,CalculateAngelToGo(angleInit,inputPos,arm.position));
        }
    }

    public float CalculateAngelToGo(float angleInit, Vector3 pos1, Vector3 pos2)
    {
        var lookDir = pos1 - pos2;
        var angle =  Mathf.Rad2Deg * Mathf.Atan2(lookDir.y,lookDir.x) * playerPivot.localScale.x;
        angle = angleInit + angle * playerPivot.localScale.x;
        return angle;
    }
}
