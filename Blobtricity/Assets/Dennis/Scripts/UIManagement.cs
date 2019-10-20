using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagement : MonoBehaviour
{
    [Header("Energy Related")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private int sliderValue;

    [Header("Time Related")]
    [SerializeField] private float timeSpeed;
    [SerializeField] private TextMeshProUGUI timerUI;
    private float timer;
    [SerializeField] private int timerMinutes;
    [SerializeField] private float timerHours;

    [Header("Sun Related")]
    [SerializeField] private GameObject sunLight;

    private void Start()
    {
        energySlider.maxValue = sliderValue;
        sunLight.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        sunLight.transform.Rotate(0.00409f * timeSpeed, sunLight.transform.rotation.y, sunLight.transform.rotation.z);
    }

    private float Timer(float timer)
    {
        timer += Time.deltaTime * timeSpeed;
        return timer;
    }
}
