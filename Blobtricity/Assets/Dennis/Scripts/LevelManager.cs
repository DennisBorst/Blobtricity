using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Game Related")]
    [SerializeField] private float restartDelay;

    [Header("Energy")]
    [SerializeField] private int energyReduceGoal;

    [Header("Time")]
    public float timeSpeed;
    public int beginHour;

    [Header("End results")]
    [SerializeField] private GameObject uiCamera;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private TextMeshProUGUI loseUIText;

    [SerializeField] private GameObject playerGameObject;

    [Header("Other")]
    [HideInInspector] public UnityStandardAssets.Characters.FirstPerson.MouseLook mouseManager;

    public void FinishLevel(float _energyReduced)
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
            LoseState(1);
        }
    }

    public void WinState()
    {
        uiCamera.SetActive(true);
        playerGameObject.SetActive(false);
        mouseManager.EnableMouse();

        winUI.SetActive(true);
        Debug.Log("You have won!!!");
    }

    public void LoseState(int state)
    {
        uiCamera.SetActive(true);
        playerGameObject.SetActive(false);
        mouseManager.EnableMouse();

        loseUI.SetActive(true);
        if(state == 1)
        {
            loseUIText.text = "The city used too much electricity!!! Inhabitants keep being phone zombies!";
        }
        else if(state == 2)
        {
            loseUIText.text = "The city became too dangerous!!! Inhabitants are running around in panic!";
        }
        Debug.Log("You have lost, better luck next time");
    }

    public void Reload()
    {
        StartCoroutine(RespawnTime());
    }

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
