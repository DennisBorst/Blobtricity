using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] private int energyReduceGoal;

    [Header("Time")]
    public float timeSpeed;
    public int beginHour;

    [Header("End results")]
    [SerializeField] private GameObject uiCamera;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject playerGameObject;

    [Header("Other")]
    [HideInInspector] public UnityStandardAssets.Characters.FirstPerson.MouseLook mouseManager;

    public void FinishLevel(int _energyReduced)
    {
        uiCamera.SetActive(true);
        playerGameObject.SetActive(false);
        mouseManager.EnableMouse();

        if (_energyReduced >= energyReduceGoal)
        {
            WinState();
        }
        else
        {
            LoseState();
        }
    }

    private void WinState()
    {
        winUI.SetActive(true);
        Debug.Log("You have won!!!");
    }

    private void LoseState()
    {
        loseUI.SetActive(true);
        Debug.Log("You have lost, better luck next time");
    }

    #region Singleton
    private static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static LevelManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new LevelManager();
            }

            return instance;
        }
    }
    #endregion
}
