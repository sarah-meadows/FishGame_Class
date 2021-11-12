using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobberProgresBar : MonoBehaviour
{
    private Slider slider;

    public bobberBehavior bobber;
    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        IncrementProgress(1f);
    }
    void Update()
    {
        slider.value = 1 - (Mathf.Round((bobber.valueAwayFromFish * 100) * 10.0f) * 0.1f) / 100;



        //  float dist = Vector3.Distance(bobber position, fish position)
        //  
        //  if(dist > slider.value)
        //  {
        //      slider.value -= dist - maximum distance
        //  }
        //  else if(dist < slider.value)
        //  {
        //      slider.value += dist - maximum distance
        //  }
        // 

    }

    public void IncrementProgress (float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
