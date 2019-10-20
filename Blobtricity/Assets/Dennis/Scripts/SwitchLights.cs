using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLights : MonoBehaviour
{
    [SerializeField] private EnergyMeter energyMeter;

    [SerializeField] private Material lightMaterial;
    [SerializeField] private int decreaseEnergy;

    private Animator animationSwitch;
    private bool lightSwitched = true;

    private void Start()
    {
        lightMaterial.color = Color.yellow;
        animationSwitch = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.E) && lightSwitched)
        {
            lightSwitched = false;
            animationSwitch.Play("LightSwitch");
            energyMeter.DecreaseEnergy(decreaseEnergy);
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        Debug.Log("light is switched");
        if(lightMaterial.color == Color.white)
        {
            lightMaterial.color = Color.yellow;
        }
        else
        {
            lightMaterial.color = Color.white;
        }
    }
}
