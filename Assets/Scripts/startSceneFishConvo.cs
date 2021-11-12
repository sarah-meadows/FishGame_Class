using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSceneFishConvo : MonoBehaviour
{
    GameObject ObjForIntro;
    GameObject fishModel;

    Animator mainAnimation;
    Animator fishAnim;

    Camera introCam, mainCam;

    public bool introIsComplete;

    // Start is called before the first frame update
    void Start()
    {
        ObjForIntro = GameObject.Find("ObjForIntro");
        fishModel = GameObject.FindGameObjectWithTag("introFish");

        mainAnimation = ObjForIntro.GetComponent<Animator>();
        fishAnim = fishModel.GetComponent<Animator>();

        introCam = GameObject.Find("IntroCam").GetComponent<Camera>();
        mainCam = Camera.main;

        introIsComplete = false;
        //on start make display2
        //when animation is complete, change display to 1 (main cam);
    }

    // Update is called once per frame
    void Update()
    {
        //now that animation is complete:
        //turn on maincamera
        //turn off introcam

        //print(mainAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime);

        /**
        //If we want to detect when the parent animation is complete...
        if(mainAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            introIsComplete = true;
        }
        */
       
        
        
        if (mainAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (fishAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                introIsComplete = true;
            }
        }


        if (!introIsComplete)
        {
            mainCam.gameObject.SetActive(false);
            introCam.gameObject.SetActive(true);

        }

        if (introIsComplete)
        {
            mainCam.gameObject.SetActive(true);
            introCam.gameObject.SetActive(false);

        }
    }
}
