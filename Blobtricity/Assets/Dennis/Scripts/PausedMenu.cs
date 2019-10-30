using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PausedMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject player;
    public GameObject camera;
    [HideInInspector] public UnityStandardAssets.Characters.FirstPerson.MouseLook mouseManager;

    void Update()
    {
        if (ui != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))
            {
                Toggle();
            }
        }
    }

    public void Toggle()
    {

        if (ui != null)
        {
            ui.SetActive(!ui.activeSelf);

            if (ui.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                player.SetActive(false);
                camera.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                player.SetActive(true);
                camera.SetActive(false);
            }
        }
    }

    public void MainMenu(string levelName)
    {
        Toggle();
        mouseManager.EnableMouse();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
