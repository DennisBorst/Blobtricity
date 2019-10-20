using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyMeter : MonoBehaviour
{
    [SerializeField] private Slider energySlider;
    [SerializeField] private int sliderValue;

    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private float timer;
    [SerializeField] private int timerMinutes;
    [SerializeField] private float timerHours;

    [SerializeField] private GameObject sunLight;
    [SerializeField] private int sunLightRotation = 0;

    private void Start()
    {
        energySlider.maxValue = sliderValue;
    }

    //Everything about the EnergySlider
    public void DecreaseEnergy(int energyDecreased)
    {
        energySlider.value += energyDecreased;
    }

    private void Update()
    {
        TimeManagement();
        SunRotation();
    }

    private void TimeManagement()
    {
        timer = Timer(timer);

        //Counting the minutes
        if (timer > 1)
        {
            timer = 0;
            timerMinutes++;
        }

        //Counting the hours
        if(timerMinutes >= 60)
        {
            timerMinutes = 0;
            timerHours++;
            sunLightRotation += 10;
        }

        if(timerHours >= 24)
        {
            timerHours = 0;
        }

        if (timerMinutes < 10 && timerHours == 0) { timerUI.text = "00:0" + timerMinutes.ToString(); }
        else if (timerMinutes < 10 && timerHours < 10) { timerUI.text = "0" + timerHours.ToString() + ":0" + timerMinutes.ToString(); }
        else if (timerMinutes >= 10 && timerHours < 10) { timerUI.text = "0" + timerHours.ToString() + ":" + timerMinutes.ToString(); }
        else if (timerMinutes < 10 && timerHours >= 10) { timerUI.text = timerHours.ToString() + ":0" + timerMinutes.ToString(); }
        else { timerUI.text = timerHours.ToString() + ":" + timerMinutes.ToString(); }
    }

    private void SunRotation()
    {
        sunLight.transform.Rotate(0.015f, sunLight.transform.rotation.y, sunLight.transform.rotation.z);
    }

    private float Timer(float timer)
    {
        timer += Time.deltaTime * 4;
        return timer;
    }
}
