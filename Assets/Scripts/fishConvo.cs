using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class fishConvo : MonoBehaviour
{
    //startSceneFishConvo startScene;

    int fishType;
    public int convoCounter;
    public int convoPossibility;

    int choice;

    public int happyPoints;

    Text fishStatement;
    Text responseA, responseB, responseC;

    Button btnContinue, btnA, btnB;

    bool endOfConvo;
    bool winState;

    // Start is called before the first frame update
    void Start()
    {
        //startScene = GetComponent<startSceneFishConvo>();
        /*
        Debug.Log("wow");
        fishType = startScene.fishType;
        
        Debug.Log("recieve: " + fishType);
        */
        fishType = GameObject.Find("SceneBrain").GetComponent<startSceneFishConvo>().fishType;

        Debug.Log("Recieve: " + fishType);

        fishStatement = GameObject.Find("FishText/Text").GetComponent<Text>();

        responseA = GameObject.Find("responseA/Text").GetComponent<Text>();
        responseB = GameObject.Find("responseB/Text").GetComponent<Text>();


        btnA = GameObject.Find("responseA").GetComponent<Button>();
        btnB = GameObject.Find("responseB").GetComponent<Button>();


        btnA.onClick.AddListener(answerOptions);
        btnB.onClick.AddListener(answerOptions);

        //btnContinue = GameObject.Find("clickContinue").GetComponent<Button>();
        //btnContinue.gameObject.SetActive(false);
        //btnContinue.gameObject.
        
        endOfConvo = false;
        winState = false;

        
        
    }


    // Update is called once per frame
    void Update()
    {
        if (endOfConvo)
        {
            //if the end of the conversation happens...
            btnA.gameObject.SetActive(false);
            btnB.gameObject.SetActive(false);
            //btnContinue.gameObject.SetActive(true);


            if (winState == true)
            {
                print(" we win");


            }
            if (winState == false)
            {
                print(" we lose");

            }

        }


        if (fishType == 0)
        {
            //////////////////////////////////
            if (convoCounter == 0)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Oh uh... hi";

                    responseA.text = "Hey there big fella";
                    responseB.text = "Hello, how are you?";
                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Haha... do you need anything?";

                    responseA.text = "Just a new trophy for the wall";
                    responseB.text = "Nothing much, just a nice conversation.";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Oh I'm alright, mornings right? Def did not expect to be here today.";

                    responseA.text = "Lol is that a bad thing?";
                    responseB.text = "I can send you back if you want?";
                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (convoPossibility == 0)
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
        else if (fishType == 1)
        {
            //////////////////////////////////
            if (convoCounter == 0)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "You Rang?";

                    responseA.text = "Hey there spikey ;)";
                    responseB.text = "Woah bro ur lookin kinda dull...";
                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "I know right? It takes so much effort to look this good, glad to see that reel still recognizes real";

                    responseA.text = "Well if this is so reel then why don't you hop in this net?";
                    responseB.text = "Well I wouldn't say we're on the same level... like... at all";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Well that's uncalled for...";

                    responseA.text = "Haha JKJK";
                    responseB.text = "Sure it is, but last a checked ur in no position to argue.";
                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Hmm I see your flow cutie but I don't think I'm feelin you. Find yourself, your passion, and get back to me";

                    responseA.text = "You Lost";
                    responseB.text = "You lost";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Oh... really? I thought I was feeling you but now? I don't know where we're going?";

                    responseA.text = "Seems like this is it.";
                    responseB.text = "Idk, I guess you could get in the net if you want?";
                }

                if (convoPossibility == 2)
                {
                    fishStatement.text = "Well then what do you think I should do";

                    responseA.text = "Idk, I guess you could get in the net if you want?";
                    responseB.text = "Get in this net";
                }
            }
            //////////////////////////////////
            if (convoCounter == 3)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "I know right? It takes so much effort to look this good, glad to see that reel still recognizes real";

                    responseA.text = "Loser";
                    responseB.text = "Way too lose";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Oh IDK, what will you do???";

                    responseA.text = "Love & take care of u";
                    responseB.text = "Turn you into a trophy";
                }
            }
            //////////////////////////////////
            if (convoCounter == 4)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Damn you corny, don't hmu";

                    responseA.text = "wow";
                    responseB.text = "rly?";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "A trophy? Like to show off? I'll bite baddy, treat me rough.";

                    responseA.text = "you won";
                    responseB.text = "are you proud?";
                }
            }
        }
        else if (fishType == 2)
        {
            //////////////////////////////////
            if (convoCounter == 0)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Ha ha, I was wondering where this would lead me. Hello there, the name's Henry, how're you?";

                    responseA.text = "Oh hi I'm [REDACTED] nice to meet you!";
                    responseB.text = "Oh I'm alright, just having a fishing trip.";
                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "It's a pleasure to meet you [REDACTED], [REDACTED], [REDACTED]. Gotta repeat your name. Helps me rememberize.";

                    responseA.text = "Henry, Henry, Henry";
                    responseB.text = "Guess I could see that working";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Hey that sounds relaxing. Sometimes you gotta relax. Though too be honest I am in the middle of something.";

                    responseA.text = "Oh! Hey no worries, we can do this later if you need?";
                    responseB.text = "You don't need to worry about that. here's a net to escape in";
                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Haha yeah... I'm not gonna lie this is a bad time though";

                    responseA.text = "Omg I didn't realize!";
                    responseB.text = "We all need a break sometimes <3";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "Yeah! and next time we meet I garuntee that you will know my name. Though I do need to be getting back. This was a very inopertune time. I could leave my card though?";

                    responseA.text = "Works with me";
                    responseB.text = "I get where you're coming from but how can I trust you'll come back";
                }

                if (convoPossibility == 2)
                {
                    fishStatement.text = "That would be great, I could leave you me card and we meet up later?";

                    responseA.text = "Yeah totally works for me";
                    responseB.text = "We could, but how do I know you arn't just trying to get away?";
                }

                if (convoPossibility == 3)
                {
                    fishStatement.text = "Haha that's funny but I don't have the time for this distraction. I'll be leaving now. Thank you.";

                    responseA.text = "lose";
                    responseB.text = "haha losed.";
                }
            }
            //////////////////////////////////
            if (convoCounter == 3)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "Haha hey it's no worries. Maybe you want to trade cards and meet up later?";

                    responseA.text = "Yeah totally!";
                    responseB.text = "How can I trust you'll come back?";
                }

                if (convoPossibility == 1)
                {
                    fishStatement.text = "You'll simply have to trust me.";

                    responseA.text = "Alright";
                    responseB.text = "No way";
                }

                if (convoPossibility == 2)
                {
                    fishStatement.text = "You won't regret this!";

                    responseA.text = "you win";
                    responseB.text = "but after a while for the lols";
                }
            }
            //////////////////////////////////
            if (convoCounter == 4)
            {
                if (convoPossibility == 0)
                {
                    fishStatement.text = "I can't sit here any longer!";

                    responseA.text = "oops";
                    responseB.text = "you lost";
                }
            }
        }
    }

    void answerOptions()
    {
        string nameOfButton = EventSystem.current.currentSelectedGameObject.name;

        if (nameOfButton == "responseA") { choice = 0; }
        if (nameOfButton == "responseB") { choice = 1; }

        if (fishType == 0)
        {
            //////////////////////////////////
            if (convoCounter == 0)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 1; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 1; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 2; endOfConvo = true; winState = false; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 2; return; }
                }

                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 2; convoCounter = 2; return; }
                    if (choice == 1) { convoPossibility = 3; convoCounter = 2; endOfConvo = true; winState = false; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {

                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 1; convoCounter = 3; return; }
                    if (choice == 1) { convoPossibility = 0; convoCounter = 3; endOfConvo = true; winState = false; return; }
                }

                if (convoPossibility == 2)
                {
                    if (choice == 0) { convoPossibility = 2; convoCounter = 3; return; }
                    if (choice == 1) { convoPossibility = 0; convoCounter = 3; endOfConvo = true; winState = false; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 3)
            {

                if (convoPossibility == 1)
                {
                    if (choice == 0) { happyPoints++; convoPossibility = 0; convoCounter++; endOfConvo = true; winState = true; return; }
                    if (choice == 1) { happyPoints++; convoPossibility = 2; convoCounter = 3; return; }
                }

                if (convoPossibility == 2)
                {
                    if (choice == 0) { happyPoints++; convoPossibility = 2; convoCounter++; endOfConvo = true; winState = false; return; }
                    if (choice == 1) { happyPoints++; convoPossibility = 1; convoCounter++; endOfConvo = true; winState = true; return; }
                }
            }
            //////////////////////////////////
        }
        else if (fishType == 1)
        {
            //////////////////////////
            if (convoCounter == 0)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 1; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 1; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 2; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 1; return; }
                }

                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 1; convoCounter = 2; return; }
                    if (choice == 1) { convoPossibility = 2; convoCounter = 2; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 3; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 3; return; }
                }

                if (convoPossibility == 2)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 2; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 3; return; }
                }
            }
            //////////////////////////
            if (convoCounter == 3)
            {
                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 4; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 4; return; }
                }
            }
        }
        else if (fishType == 2)
        {
            //////////////////////////
            if (convoCounter == 0)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 1; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 1; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 1)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 2; return; }
                    if (choice == 1) { convoPossibility = 1; convoCounter = 2; return; }
                }

                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 2; convoCounter = 2; return; }
                    if (choice == 1) { convoPossibility = 3; convoCounter = 2; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 2)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 3; convoCounter = 2; return; }
                    if (choice == 1) { convoPossibility = 0; convoCounter = 3; return; }
                }

                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 1; convoCounter = 3; return; }
                    if (choice == 1) { convoPossibility = 2; convoCounter = 3; return; }
                }

                if (convoPossibility == 2)
                {
                    if (choice == 0) { convoPossibility = 1; convoCounter = 3; return; }
                    if (choice == 1) { convoPossibility = 2; convoCounter = 3; return; }
                }
            }
            //////////////////////////////////
            if (convoCounter == 3)
            {
                if (convoPossibility == 0)
                {
                    if (choice == 0) { convoPossibility = 1; convoCounter = 3; return; }
                    if (choice == 1) { convoPossibility = 2; convoCounter = 3; return; }
                }

                if (convoPossibility == 1)
                {
                    if (choice == 0) { convoPossibility = 0; convoCounter = 4; return; }
                    if (choice == 1) { convoPossibility = 2; convoCounter = 3; return; }
                }
            }
        }
    }
}
