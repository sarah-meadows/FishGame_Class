using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startSceneFishConvo : MonoBehaviour
{
    GameObject ObjForIntro;
    GameObject fishModel;

    public int fishType;

    Animator mainAnimation;

    Camera introCam, mainCam;

    Canvas canvas;

    public bool introIsComplete;

    private void Awake()
    {
        //fishType = Random.Range(0, 2);
        fishType = 0;
        Debug.Log("send: " + fishType);
    }

    // Start is called before the first frame update
    void Start()
    {
       

        ObjForIntro = GameObject.Find("ObjForIntro");

        mainAnimation = ObjForIntro.GetComponent<Animator>();

        introCam = GameObject.Find("IntroCam").GetComponent<Camera>();
        mainCam = Camera.main;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        introIsComplete = false;
        //on start make display2
        //when animation is complete, change display to 1 (main cam);

        canvas.enabled = false;
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
            //   if (fishAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //   {
            
                introIsComplete = true;
          //  }
        }


        if (!introIsComplete)
        {
            mainCam.gameObject.SetActive(false);
            introCam.gameObject.SetActive(true);

            canvas.enabled = false;
        }

        if (introIsComplete)
        {
            mainCam.gameObject.SetActive(true);
            introCam.gameObject.SetActive(false);

            canvas.enabled = true;

            this.gameObject.SetActive(false);
        }
    }
}
