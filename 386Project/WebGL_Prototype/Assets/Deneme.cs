using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            mainCamera.backgroundColor = new Color(Random.Range(0f,1f),Random.Range(0f,1f), Random.Range(0f,1f),1);
        }
    }
}
