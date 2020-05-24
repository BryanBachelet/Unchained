using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Feedback : MonoBehaviour
{
    public GameObject feedbackAim;

    public void ActiveFeedback(bool state)
    {
        feedbackAim.SetActive(state);
    }
}
