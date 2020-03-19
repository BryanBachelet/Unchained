using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TransitionUI : MonoBehaviour
{
    public float speed;
    public bool isActive = true;
    public RectTransform myRT;
    float currentTaille;
    public bool isLoading = false;
    public Image loadingBar;
    float coroutineValue;
    public float tempsChargement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            currentTaille += Time.deltaTime * speed;
            myRT.localScale = new Vector3(currentTaille, myRT.localScale.y, myRT.localScale.z);
            if(myRT.localScale.x > 60)
            {
                isActive = false;
                isLoading = true;
            }
        }

        if(isLoading)
        {
            loadingBar.gameObject.SetActive(true);
            StartCoroutine(LoadYourAsyncScene());


        }
        IEnumerator LoadYourAsyncScene()
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.
            coroutineValue += Time.deltaTime;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main_Scene_V1_110220");
            loadingBar.fillAmount = (coroutineValue / tempsChargement) * 20;
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone && coroutineValue < tempsChargement)
            {
                Debug.Log("DONE !");
                yield return null;
            }
        }
    }
}

    
