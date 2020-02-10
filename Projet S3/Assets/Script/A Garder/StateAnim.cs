using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnim : MonoBehaviour
{
    public enum CurrentState { Idle, Walk, Tir, Rotate, Projection };
    public static CurrentState state;
    public CurrentState current;
    private float speed;
    public static float t;
    public Animator animator;
    public float speedOfTransistionAnimaiton = 4;
    public void Start()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
    }
    public void Update()
    {
        current = state;
        if (animator != null)
        {

            switch (state)
            {
                case CurrentState.Idle:
                    speed = Mathf.Lerp(speed, 0, t);
                    animator.SetFloat("Vitesse", speed);
                    break;

                case CurrentState.Walk:
                    speed = Mathf.Lerp(speed, 0.1875f, t);
                    animator.SetFloat("Vitesse", speed);
                    break;
                case CurrentState.Tir:
                    speed = Mathf.Lerp(speed, 0.375f, t);
                    animator.SetFloat("Vitesse", speed);
                    break;
                case CurrentState.Rotate:
                    speed = Mathf.Lerp(speed, 0.5625f, t);
                    animator.SetFloat("Vitesse", speed);
                    break;
                case CurrentState.Projection:
                    speed = Mathf.Lerp(speed, 0.75f, t);
                    animator.SetFloat("Vitesse", speed);
                    break;
            }
            t += speedOfTransistionAnimaiton * Time.deltaTime;
        }
        transform.localPosition = Vector3.zero; 
    }


    public static void ChangeState(CurrentState stateChange)
    {
        t = 0;
        state = stateChange;
    }
}
