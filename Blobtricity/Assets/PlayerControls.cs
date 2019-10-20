using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera mapCamera;

    private bool IsCheckingMap;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMap();
        ChangeCamera();
    }

    private void CheckMap()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SwitchBool();
        }
    }

    private void SwitchBool()
    {
        IsCheckingMap = !IsCheckingMap;
    }

    private void ChangeCamera()
    {
        if (IsCheckingMap)
        {
            mainCamera.enabled = false;
            mapCamera.enabled = true;

        }
        else
        {
            mainCamera.enabled = true;
            mapCamera.enabled = false;


        }
    }

}
