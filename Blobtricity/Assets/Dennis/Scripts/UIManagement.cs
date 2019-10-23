using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagement : MonoBehaviour
{
    [Header("Energy Related")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI energyPercentageUI;
    [SerializeField] private float maxSliderValue;
    private float currentSliderValue;
    private float energyPercentage;

    [Header("Time Related")]
    [SerializeField] private TextMeshProUGUI timerUI;
    private float timer;
    private int timerMinutes;
    private int timerHours;

    [Header("Sun Related")]
    [SerializeField] private GameObject sunLight;

    private int sunPosition;

    [Header("Blob stuff")]
    [SerializeField] private GameObject finalDestinationGoogleMaps;
    [SerializeField] private GameObject finalDestinationNetflix;

    private void Start()
    {
        energySlider.maxValue = maxSliderValue;
        energySlider.value = maxSliderValue;
        currentSliderValue = maxSliderValue;

        timerHours = LevelManager.Instance.beginHour;

        if(timerHours == 8)
        {
            sunLight.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            sunPosition = (timerHours - 8) * (180/12);
            sunLight.transform.rotation = Quaternion.Euler(sunPosition, 0, 0);
            Debug.Log("sun rotation x: " + sunPosition);
        }
    }

    //Everything about the EnergySlider
    public void DecreaseEnergy(int energyDecreased)
    {
        currentSliderValue -= energyDecreased;
        energySlider.value -= energyDecreased;
        energyPercentage = ((currentSliderValue / maxSliderValue) * 100);
        Debug.Log("Energy percentage: " + energyPercentage);
        Debug.Log("Energy current: " + currentSliderValue);
        Debug.Log("Energy max: " + maxSliderValue);
   
        energyPercentageUI.text = energyPercentage.ToString() + "%";
    }

    private void Update()
    {
        if(timerHours >= 20)
        {
            LevelManager.Instance.FinishLevel(currentSliderValue);

            return;
        }

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
        sunLight.transform.Rotate(0.00409f * LevelManager.Instance.timeSpeed, sunLight.transform.rotation.y, sunLight.transform.rotation.z);
    }

    private float Timer(float timer)
    {
        timer += Time.deltaTime * LevelManager.Instance.timeSpeed;
        return timer;
    }

    public void DrawDestinationVisual(Vector3 destination, int state)
    {
        if(state == 1)
        {
            finalDestinationGoogleMaps.transform.position = new Vector3(destination.x, destination.y + 1.5f, destination.z);
            finalDestinationGoogleMaps.SetActive(true);
        }
        else if(state == 2)
        {
            finalDestinationNetflix.transform.position = new Vector3(destination.x, destination.y + 2f, destination.z);
            finalDestinationNetflix.SetActive(true);
        }
    }

    public void DestroyDestinationVisual()
    {
        finalDestinationGoogleMaps.SetActive(false);
        finalDestinationNetflix.SetActive(false);
    }



    private static UIManagement instance;

    private void Awake()
    {
        instance = this;
    }

    public static UIManagement Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new UIManagement();
            }

            return instance;
        }
    }
}
