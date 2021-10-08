using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class fishConvo : MonoBehaviour
{
    int fishType;
    public int convoCounter;
    public int dialogueBranch;

    int choice;

    public int happyPoints;

    Text fishStatement;
    Text responseA, responseB, responseC;

    Button btnA, btnB; 
    //, btnC;

    // Start is called before the first frame update
    void Start()
    {
        ///Things to do
        ///---you need a way of detecting THE END OF CONVO
        ///---you need a way to determine if you've WON/LOST
        ///---you'll need add happy meter
        ///---if the fishtype value is equal to x, the model will be x
        ///---determine instantiate model position


        fishStatement = GameObject.Find("FishText/Text").GetComponent<Text>();

        responseA = GameObject.Find("responseA/Text").GetComponent<Text>();
        responseB = GameObject.Find("responseB/Text").GetComponent<Text>();
        //responseC = GameObject.Find("responseC/Text").GetComponent<Text>();

        btnA = GameObject.Find("responseA").GetComponent<Button>();
        btnB = GameObject.Find("responseB").GetComponent<Button>();
        //btnC = GameObject.Find("responseC").GetComponent<Button>();

        btnA.onClick.AddListener(answerOptions);
        btnB.onClick.AddListener(answerOptions);
        //btnC.onClick.AddListener(answerOptions);

        // if (gameObject.activeInHierarchy == false) { gameObject.SetActive(true); }
    }

    // Update is called once per frame
    void Update()
    {
        //note the fishtype is equal to whatever fish it is
        //alternitivly, you could do a string value instead of number

        if (fishType == 0)
        {
            //////////////////////////////////
            if (convoCounter == 0)
            {
                if (dialogueBranch==0)
                {
                    fishStatement.text = "example phrase 1";

                    responseA.text = "this is option A goes to possible 0";
                    responseB.text = "this is option B goes to possible 1";
                    //responseC.text = "this is option C goes to possible 1";
                   
                    responseB.gameObject.SetActive(true);

                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (dialogueBranch==0)
                {
                    fishStatement.text = "banana";

                    responseA.text = "this is option A1";
                    responseB.text = "this is option B1";

                }
                if (dialogueBranch==1)
                {
                    fishStatement.text = "tomato";

                    responseA.text = "this is option A2";
                    responseB.text = "This will result in loop";

                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (dialogueBranch==0)
                {
                    fishStatement.text = "lalala";

                    responseA.text = "option A";
                    responseB.text = "option B";

                }
                if (dialogueBranch==1)
                {
                    fishStatement.text = "WHO ARE YOU";

                    responseA.text = "Bob";
                   // responseB.text = "option B";

                    responseB.gameObject.SetActive(false);
                }
            }
            //////////////////////////////////
        }
    }

    void answerOptions() 
    {
        string nameOfButton = EventSystem.current.currentSelectedGameObject.name;

        if (nameOfButton == "responseA") { choice = 0; }
        if (nameOfButton == "responseB") { choice = 1; }
       // if (nameOfButton == "responseC") { choice = 2; }

        //////////////////////////////////
        if (convoCounter == 0)
        {
            if (dialogueBranch == 0)
            {
                if (choice == 0) { happyPoints++; dialogueBranch = 0; convoCounter++; return;}
                if (choice == 1) { happyPoints++; dialogueBranch = 1; convoCounter++; return; }
                
            }            
        }
        //////////////////////////////////
        if (convoCounter == 1)
        {
            if (dialogueBranch == 0)
            {
                if (choice == 0) { happyPoints++; dialogueBranch = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; dialogueBranch = 0; convoCounter++; return; }
            }

            if (dialogueBranch == 1)
            {
                if (choice == 0) { happyPoints++; dialogueBranch = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; dialogueBranch = 1; convoCounter++; return; }
            }
        }
        //////////////////////////////////
        if (convoCounter == 2)
        {
            if (dialogueBranch == 0)
            {
                if (choice == 0) { happyPoints++; dialogueBranch = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; dialogueBranch = 0; convoCounter++; return; }
            }
            if (dialogueBranch == 1)
            {
                if (choice == 0) { happyPoints++; dialogueBranch = 0; convoCounter=0; return; }
                //if (choice == 1) { happyPoints++; dialogueBranch = 0; convoCounter++; return; }
            }

        }
        //////////////////////////////////

    }
}
