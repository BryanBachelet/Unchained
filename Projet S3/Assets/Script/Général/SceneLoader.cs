using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

    public GameObject canvasUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            QuitApplication();
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if(canvasUI != null)
            {
                canvasUI.SetActive(StopTimeOfGameStatic());
            }
        }
    }

    public static bool StopTimeOfGameStatic()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            return true;
        }
        else
        {
            Time.timeScale = 1;
            return false;
        }
    }

    public void StopTimeOfGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            if (canvasUI != null)
            {
                canvasUI.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            if (canvasUI != null)
            {
                canvasUI.SetActive(false);
            }
        }
    }

    public static void LoadSceneStatic(int indexScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(indexScene);
    }

    public static void LoadCurrentSceneStatic()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int indexScene)
    {
        Time.timeScale = 1;
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(indexScene);
    }

    public void LoadCurrentScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
