using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class fishConvo : MonoBehaviour
{
    int fishType;
    public int convoCounter;
    public int convoPossibility;

    int choice;

    public int happyPoints;

    Text fishStatement;
    Text responseA, responseB, responseC;

    Button btnA, btnB, btnC;
    // Start is called before the first frame update
    void Start()
    {
        fishStatement = GameObject.Find("FishText/Text").GetComponent<Text>();

        responseA = GameObject.Find("responseA/Text").GetComponent<Text>();
        responseB = GameObject.Find("responseB/Text").GetComponent<Text>();
        responseC = GameObject.Find("responseC/Text").GetComponent<Text>();

        btnA = GameObject.Find("responseA").GetComponent<Button>();
        btnB = GameObject.Find("responseB").GetComponent<Button>();
        btnC = GameObject.Find("responseC").GetComponent<Button>();

        btnA.onClick.AddListener(answerOptions);
        btnB.onClick.AddListener(answerOptions);
        btnC.onClick.AddListener(answerOptions);
    }

    // Update is called once per frame
    void Update()
    {
        if (convoCounter == 3) { convoCounter = 0; }


        if (fishType == 0)
        {
            //////////////////////////////////
            if (convoCounter == 0)
            {
                if (convoPossibility==0)
                {
                    fishStatement.text = "example phrase 1";

                    responseA.text = "this is option A goes to possible 0";
                    responseB.text = "this is option B goes to possible 0";
                    responseC.text = "this is option C goes to possible 1";

                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility==0)
                {
                    fishStatement.text = "1. example phrase 2 and one possible branch. This will loop to the beginning";

                    responseA.text = "this is option A1";
                    responseB.text = "this is option B1";
                    responseC.text = "this is option C1";

                }
                if (convoPossibility==1)
                {
                    fishStatement.text = "2. example phrase 2 and the second possible branch. This will loop to the beginning";

                    responseA.text = "this is option A2";
                    responseB.text = "this is option B2";
                    responseC.text = "this is option C2";

                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (convoPossibility==0)
                {
                    fishStatement.text = "and now it loops";

                    responseA.text = "option A";
                    responseB.text = "option B";
                    responseC.text = "option C";

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
        if (nameOfButton == "responseC") { choice = 2; }

        //////////////////////////////////
        if (convoCounter == 0)
        {
            if (convoPossibility == 0)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return;}
                if (choice == 1) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 2) { happyPoints--; convoPossibility = 1; convoCounter++; return; }
                
            }            
        }
        //////////////////////////////////
        if (convoCounter == 1)
        {
            if (convoPossibility == 0)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 2) { happyPoints--; convoPossibility = 0; convoCounter++; return; }
            }

            if (convoPossibility == 1)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 2) { happyPoints--; convoPossibility = 0; convoCounter++; return; }
            }
        }
        //////////////////////////////////
        if (convoCounter == 2)
        {
            if (convoPossibility == 0)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 2) { happyPoints--; convoPossibility = 0; convoCounter++; return; }
            }

        }
        //////////////////////////////////

    }
}
