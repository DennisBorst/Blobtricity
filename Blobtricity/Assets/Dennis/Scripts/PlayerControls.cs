using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool isBusy;
    public bool isTinderBusy;
    public bool stoppedBlob = false;

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject mapCamera;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
    private bool isCheckingMap;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
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
        isCheckingMap = !isCheckingMap;
    }

    private void ChangeCamera()
    {
        if (isCheckingMap)
        {
            firstPersonController.enabled = false;
            mainCamera.SetActive(false);
            mapCamera.SetActive(true);
        }
        else
        {
            firstPersonController.enabled = true;
            mainCamera.SetActive(true);
            mapCamera.SetActive(false);
        }
    }
}
