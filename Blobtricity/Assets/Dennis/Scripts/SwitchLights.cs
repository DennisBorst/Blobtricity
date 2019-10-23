using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLights : MonoBehaviour
{
    [SerializeField] private UIManagement uiManagement;

    [SerializeField] private int decreaseEnergy;

    [SerializeField] private Material lightOnMaterial;
    [SerializeField] private Material lightOffMaterial;
    [SerializeField] private MeshRenderer[] normalColor;

    private Animator animationSwitch;
    private bool lightSwitched = false;

    private void Start()
    {
        animationSwitch = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.E) && !lightSwitched)
        {
            lightSwitched = true;
            animationSwitch.Play("LightSwitch");
            uiManagement.DecreaseEnergy(decreaseEnergy);
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        for (int i = 0; i < normalColor.Length; i++)
        {
            normalColor[i].material = lightOffMaterial;
        }

        /*
        Debug.Log("light is switched");
        if(normalColor.material == lightOffMaterial)
        {
            normalColor.material = lightOnMaterial;
        }
        else
        {
            
        }
        */
    }
}
