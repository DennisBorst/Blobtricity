using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagement : MonoBehaviour
{
    [Header("Time Related")]
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private AudioClip timerSound;
    private AudioSource timerSource;
    private float timer;
    private int timerMinutes;
    private int timerHours;

    [Header("Map related")]
    [SerializeField] private GameObject arrowMapImage;

    [Header("tree related")]
    [SerializeField] private TextMeshProUGUI treeText;
    [SerializeField] private GameObject treeTextObject;

    [Header("Energy Related")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private GameObject energyTextObject;
    [SerializeField] private TextMeshProUGUI energyPercentageUI;
    [SerializeField] private float maxEnergySliderValue;
    [Space]
    [Range(0, 1)]
    [SerializeField] private float overtimeEnergySpeed;
    [HideInInspector] public int energyOvertimeDecrease = 0;
    private float currentEnergySliderValue;
    private float energyPercentage;

    [Header("Danger related")]
    [SerializeField] private Slider dangerSlider;
    [SerializeField] private TextMeshProUGUI dangerText;
    [SerializeField] private GameObject dangerTextObject;
    [SerializeField] private TextMeshProUGUI dangerPercentageUI;
    [SerializeField] private float maxDangerSliderValue;
    [SerializeField]private float currentDangerSliderValue;
    private float dangerPercentage;

    [Header("Sustainable related")]
    [SerializeField] private Slider sustainableSlider;
    [SerializeField] private TextMeshProUGUI sustainableText;
    [SerializeField] private GameObject sustainableTextObject;
    [SerializeField] private TextMeshProUGUI sustainablePercentageUI;
    [SerializeField] private float maxSustainableSliderValue;
    private float currentSustainableSliderValue;
    private float sustainablePercentage;

    [Header("Sun Related")]
    [SerializeField] private GameObject sunLight;

    private int sunPosition;

    [Header("Blob stuff")]
    [SerializeField] private GameObject finalDestinationGoogleMaps;
    [SerializeField] private GameObject finalDestinationNetflix;
    [SerializeField] private GameObject finalDestinationGamer;
    [SerializeField] private GameObject finalDestinationTinder;

    [Header("Particles")]
    [SerializeField] private ParticleSystem energyParticles; 
    [SerializeField] private ParticleSystem sustainableParticles;
    [SerializeField] private ParticleSystem dangerParticles;

    private void Start()
    {
        /*
        //Energy bar decrease
        energySlider.maxValue = maxEnergySliderValue;
        energySlider.value = maxEnergySliderValue;
        currentEnergySliderValue = maxEnergySliderValue;
        */

        //Energy bar increase
        energySlider.maxValue = maxEnergySliderValue;
        energySlider.value = 0;
        currentEnergySliderValue = 0;

        //Danger bar
        dangerSlider.maxValue = maxDangerSliderValue;
        dangerSlider.value = 0;
        currentDangerSliderValue = 0;

        //Sustainable bar
        sustainableSlider.maxValue = maxSustainableSliderValue;
        sustainableSlider.value = 0;
        currentSustainableSliderValue = 0;

        timerHours = LevelManager.Instance.beginHour;

        /*
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
        */
    }

    private void Update()
    {
        if(timerHours >= 6 && timerHours <= 6)
        {
            timerUI.color = new Color32(255, 100, 0, 255);

            if (!timerSource.isPlaying)
            {
                timerSource.clip = timerSound;
                timerSource.Play();
            }
        }
        else if (timerHours >= 7 && timerHours <= 10)
        {
            timerUI.color = new Color32(255, 0, 0, 255);

            if (!timerSource.isPlaying)
            {
                timerSource.clip = timerSound;
                timerSource.Play();
            }
        }

        if (timerHours >= 8 && timerHours <= 10)
        {
            LevelManager.Instance.FinishLevel(currentEnergySliderValue);

            return;
        }

        EnergyDecreaseOvertime();
        TimeManagement();
        //SunRotation();
    }

    //Everything about the EnergySlider
    public void DecreaseEnergy(int energyDecreased)
    {
        currentEnergySliderValue += energyDecreased;
        energySlider.value += energyDecreased;
        energyPercentage = ((currentEnergySliderValue / maxEnergySliderValue) * 100);
   
        energyPercentageUI.text = energyPercentage.ToString() + "%";

        energyText.text = "energy reduced: " + energyDecreased;
        energyTextObject.SetActive(true);

        energyParticles.Play();
    }

    public void EnergyDecreaseOvertime()
    {
        currentEnergySliderValue += (energyOvertimeDecrease * overtimeEnergySpeed);
        energySlider.value += (energyOvertimeDecrease * overtimeEnergySpeed);

        energyPercentage = ((currentEnergySliderValue / maxEnergySliderValue) * 100);

        energyPercentage = Mathf.Round(energyPercentage);
        energyPercentageUI.text = energyPercentage.ToString() + "%";
    }

    //Everything about the DangerSlider
    public void IncreaseDanger(int dangerIncreased)
    {
        currentDangerSliderValue += dangerIncreased;
        dangerSlider.value += dangerIncreased;
        dangerPercentage = ((currentDangerSliderValue / maxDangerSliderValue) * 100);

        dangerPercentageUI.text = dangerPercentage.ToString() + "%";

        if(currentDangerSliderValue >= maxDangerSliderValue)
        {
            LevelManager.Instance.LoseState(2);
        }

        dangerText.text = "danger increased: " + dangerIncreased;
        dangerTextObject.SetActive(true);

        dangerParticles.Play();
    }

    public void IncreaseSustainable(int sustainableIncreased)
    {
        currentSustainableSliderValue += sustainableIncreased;
        sustainableSlider.value += sustainableIncreased;
        sustainablePercentage = ((currentSustainableSliderValue / maxSustainableSliderValue) * 100);

        sustainablePercentageUI.text = sustainablePercentage.ToString() + "%";

        sustainableText.text = "a new windmill is placed";
        sustainableTextObject.SetActive(true);

        sustainableParticles.Play();
    }

    public void PlacedTree()
    {
        treeText.text = "a tree is grown";
        treeTextObject.SetActive(true);
    }

    public void PlaceArrow()
    {
        arrowMapImage.SetActive(true);
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
        else if (state == 3)
        {
            finalDestinationGamer.transform.position = new Vector3(destination.x, destination.y + 2f, destination.z);
            finalDestinationGamer.SetActive(true);
        }
        else if(state == 4)
        {
            finalDestinationTinder.transform.position = new Vector3(destination.x, destination.y + 0.8f, destination.z);
            finalDestinationTinder.SetActive(true);
        }
    }

    public void DestroyDestinationVisual()
    {
        finalDestinationGoogleMaps.SetActive(false);
        finalDestinationNetflix.SetActive(false);
        finalDestinationGamer.SetActive(false);
        finalDestinationTinder.SetActive(false);
    }



    private static UIManagement instance;

    private void Awake()
    {
        timerSource = GetComponent<AudioSource>();
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
