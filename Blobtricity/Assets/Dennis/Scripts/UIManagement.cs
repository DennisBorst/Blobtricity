using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagement : MonoBehaviour
{
    [Header("Energy Related")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private int maxSliderValue;
    [SerializeField] private int currentSliderValue;

    [Header("Time Related")]
    [SerializeField] private TextMeshProUGUI timerUI;
    private float timer;
    private int timerMinutes;
    private int timerHours;

    [Header("Sun Related")]
    [SerializeField] private GameObject sunLight;

    private int sunPosition;

    [Header("Blob stuff")]
    [SerializeField] private GameObject finalDestinationVisual;

    private void Start()
    {
        energySlider.maxValue = maxSliderValue;
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
        currentSliderValue += energyDecreased;
        energySlider.value += energyDecreased;
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

    public void DrawDestinationVisual(Vector3 destination)
    {
        finalDestinationVisual.transform.position = new Vector3(destination.x, destination.y + 1.5f, destination.z);
        finalDestinationVisual.SetActive(true);
    }

    public void DestroyDestinationVisual()
    {
        finalDestinationVisual.SetActive(false);
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
