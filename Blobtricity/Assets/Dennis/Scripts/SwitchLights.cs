using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLights : MonoBehaviour
{
    [SerializeField] private UIManagement uiManagement;

    [SerializeField] private int decreaseEnergy;
    [SerializeField] private int increaseDanger;
    [SerializeField] private bool danger;

    [SerializeField] private Material lightOnMaterial;
    [SerializeField] private Material lightOffMaterial;
    [SerializeField] private MeshRenderer[] normalColor;
    [SerializeField] private GameObject[] lightColor;

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
            if(animationSwitch != null)
            {
                animationSwitch.Play("LightSwitch");
            }

            uiManagement.DecreaseEnergy(decreaseEnergy);
            //SoundManager.Instance.PlayHouseSound();

            if (danger)
            {
                SoundManager.Instance.PlayScaredBlob();
                uiManagement.IncreaseDanger(increaseDanger);
            }
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        for (int i = 0; i < normalColor.Length; i++)
        {
            normalColor[i].material = lightOffMaterial;
        }
        for (int i = 0; i < lightColor.Length; i++)
        {
            lightColor[i].SetActive(false);
        }
    }
}
