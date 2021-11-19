using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobberProgresBar : MonoBehaviour
{
    private Slider slider;
    public bool isTension;
    public float tensionSpeed;
    public bobberBehavior bobber;
    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {


        if (isTension == false)
        {
            IncrementProgress(1f);
        }

       
    }
    void Update()
    {
        
        if (isTension == false)
        {
            slider.value = 1 - Mathf.Round((bobber.valueAwayFromFish * 100) * 10.0f) * 0.1f / 100;
            
        }

        if (isTension == true)
        {
            float curDistance = Mathf.Round((bobber.valueAwayFromFish * 100) * 10.0f) * 0.1f / 100;
            if (curDistance < 0.5f)
            {
                print("is close");
                tensionSpeed = - (1f * Time.deltaTime);

            }

            if (curDistance >= 0.5f)
            {
                print("is far away");
                tensionSpeed = (1f * Time.deltaTime);
            }

            slider.value += tensionSpeed * Time.deltaTime;

           //print(Mathf.Round((bobber.valueAwayFromFish * 100) * 10.0f) * 0.1f / 100);
        }
        print(slider.value);


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
        targetProgress = slider.value + newProgress + 0.1f;
        slider.value = targetProgress; 
    }
}
