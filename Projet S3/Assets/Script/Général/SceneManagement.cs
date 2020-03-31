using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            StopTimeOfGame();
        }
    }

    public static void StopTimeOfGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;

        }
    }

    public static void LoadScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    public static void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void QuitApplication()
    {
        Application.Quit();
    }
}
