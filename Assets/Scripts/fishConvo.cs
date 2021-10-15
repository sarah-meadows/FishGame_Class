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

    Button btnA, btnB;
    // Start is called before the first frame update
    void Start()
    {
        fishStatement = GameObject.Find("FishText/Text").GetComponent<Text>();

        responseA = GameObject.Find("responseA/Text").GetComponent<Text>();
        responseB = GameObject.Find("responseB/Text").GetComponent<Text>();
        

        btnA = GameObject.Find("responseA").GetComponent<Button>();
        btnB = GameObject.Find("responseB").GetComponent<Button>();
        

        btnA.onClick.AddListener(answerOptions);
        btnB.onClick.AddListener(answerOptions);
      
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
                    fishStatement.text = "Oh uh... hi";

                    responseA.text = "Hey there big fella";
                    responseB.text = "Hello, how are you?";
                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility==0)
                {
                    fishStatement.text = "Haha... do you need anything?";

                    responseA.text = "Just a new trophy for the wall";
                    responseB.text = "Nothing much, just a nice conversation.";
                }

                if (convoPossibility==1)
                {
                    fishStatement.text = "Oh I'm alright, mornings right? Def did not expect to be here today.";

                    responseA.text = "Lol is that a bad thing?";
                    responseB.text = "I can send you back if you want?";
                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (convoPossibility==0)
                {
                    fishStatement.text = "Oh... well Idk you so I think I might head out... yeah";

                    //responseA.text = "option A";
                    //responseB.text = "option B";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Haha well I can try I guess, but I tend to be awkward.";

                    responseA.text = "Hey thats no issue, take your time";
                    responseB.text = "There is no awkward until you think it is";
                }

                if (convoPossibility == 2)
                {
                    fishStatement.text = "NO No no!!! I didn't mean it like that! I'm sorry, I always ay the wrong thing...";

                    responseA.text = "Hey, just believe in your self some more and with patience you can talk to anyone!";
                    responseB.text = "You can't always...";
                }

                if (convoPossibility == 3)
                {
                    fishStatement.text = "I think I would like that... please?";

                    //responseA.text = "option A";
                    //responseB.text = "option B";
                }
            }
            //////////////////////////////////
            if (convoCounter == 3)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Haha sure. You obviously don't know me. I think I'll be going now.";

                    //responseA.text = "Just a new trophy for the wall";
                    //responseB.text = "Nothing much, just a nice conversation.";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Jee wiz, look at me. Why cant I just act confident!";

                    responseA.text = "Hey, it's alright. I'm here for you so take your time.";
                    responseB.text = "Believing in yourself is the first step.";
                }

                if (convoPossibility == 2)
                {
                    fishStatement.text = "Easy to say";

                    responseA.text = "And just as easy to start";
                    responseB.text = "Just like everything but you'll have my help";
                }
            }
            //////////////////////////////////
            if (convoCounter == 4)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Hey thanks, I need that.";

                    //responseA.text = "Just a new trophy for the wall";
                    //responseB.text = "Nothing much, just a nice conversation.";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Thanks, I guess I'd love to try";

                    //responseA.text = "Hey, it's alright. I'm here for you so take your time.";
                    //responseB.text = "Believing in yourself is the first step.";
                }

                if (convoPossibility == 2)
                {
                    fishStatement.text = "Fuck off.";

                    //responseA.text = "And just as easy to start";
                    //responseB.text = "Just like everything but you'll have my help";
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

        //////////////////////////////////
        if (convoCounter == 0)
        {
            if (convoPossibility == 0)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return;}
                if (choice == 1) { happyPoints++; convoPossibility = 1; convoCounter++; return; }
            }            
        }
        //////////////////////////////////
        if (convoCounter == 1)
        {
            if (convoPossibility == 0)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 1; convoCounter++; return; }
            }

            if (convoPossibility == 1)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 2; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 3; convoCounter++; return; }
            }
        }
        //////////////////////////////////
        if (convoCounter == 2)
        {

            if (convoPossibility == 1)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 1; convoCounter++; return; }
            }

            if (convoPossibility == 2)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 2; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 3; convoCounter++; return; }
            }
        }
        //////////////////////////////////
        if (convoCounter == 3)
        {

            if (convoPossibility == 1)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 2;  return; }
            }

            if (convoPossibility == 2)
            {
                if (choice == 0) { happyPoints++; convoPossibility = 1; convoCounter++; return; }
                if (choice == 1) { happyPoints++; convoPossibility = 2; convoCounter++; return; }
            }
        }
        //////////////////////////////////

    }
}
