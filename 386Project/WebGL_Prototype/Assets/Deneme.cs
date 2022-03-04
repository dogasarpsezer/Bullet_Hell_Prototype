using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Deneme : MonoBehaviour
{
    [SerializeField] private Transform playerPivot;
    [SerializeField] private Transform crossairPivot;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform arm;
    [SerializeField] Vector3 planeNormal;
    private Camera mainCamera;
    private Plane inputPlane;

    void Start()
    {
        Cursor.visible = false;
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
            var angle =  Mathf.Rad2Deg * Mathf.Atan2(crossairPivot.position.y - muzzle.position.y, crossairPivot.position.x - muzzle.position.x);
            arm.Rotate(Vector3.forward,angle);
        }
    }
}
