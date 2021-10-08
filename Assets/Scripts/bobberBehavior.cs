using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bobberBehavior : MonoBehaviour
{
    public float fishCord_X;
    public float fishCord_Z;

    public float curCord_X;
    public float curCord_Z;

    float fishRange=500;
    float bobSpeed=2;

    public bool tiltLeft, tiltRight, tiltUp, tiltDown;

    GameObject centerBobber;
    GameObject[] bounds;

    public float inGameRange;
    //
    public float calibrateCord;
    Vector2 fishTargetPos, curTargetPos;

    float clockMax = 30;
    float timeRemaining;
    Image clock;

    bool gameCondition; //true win, false lose

    public AudioSource beeper;
    public AudioClip beepsound;


    // Start is called before the first frame update
    void Start()
    {
        //let's really randomize the results. We'll randomize the numbers 3 times. 
        for(int i=0; i < 3; i++)
        {
            fishCord_X = Random.Range(-fishRange, fishRange);
            fishCord_Z = Random.Range(-fishRange, fishRange);
        }

        centerBobber = GameObject.Find("bobberCenter");
        bounds = GameObject.FindGameObjectsWithTag("bobberLimit");

        inGameRange = Vector3.Distance(bounds[0].transform.position, centerBobber.transform.position);
        
        //

        //This is making the ratio between the in game cords and the fishRange
        calibrateCord = inGameRange * (1 / fishRange);
        
        //This is the ingame cordinates of the fish position
        float newX = (fishCord_X * calibrateCord)+centerBobber.transform.position.x;
        float newY = (centerBobber.transform.position.y);
        float newZ = (fishCord_Z * calibrateCord)+ centerBobber.transform.position.z;
        fishTargetPos = new Vector3(newX, newY, newZ);


        //This is the clock for the catch timer
        clock = GameObject.Find("Clock").GetComponent<Image>();
        timeRemaining = clockMax;
    }

    // Update is called once per frame
    void Update()
    {
        /** 
         * calculating if there is any user input via button press or tilting. 
         * Set bool true if actions occur
         * */
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) { tiltUp = true; } else { tiltUp=false;}
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) { tiltDown = true; } else { tiltDown = false; }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { tiltLeft = true; } else { tiltLeft = false; }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { tiltRight = true; } else { tiltRight = false; }

        /**
         * increase/decrease current cord value upon tilts
         * */
        if (tiltUp) { curCord_Z += bobSpeed; }
        if (tiltDown) { curCord_Z -= bobSpeed; }
        
        if (tiltRight) { curCord_X += bobSpeed; }
        if (tiltLeft) { curCord_X -= bobSpeed; }


        ///We need to: 
        ///set the position of bobber to the curPos equivalent to real Space 
        ///So we need to make a ratioConversion value
        ///set our bober to the converted value plus is starting pos
        ///_We need to make a countdown timer
        ///We need a condition so that 
        ///if at the end of the timer if the fish didn't get away, then you win
        ///if before or at the end of the timer the fish gets away, the you lose
        ///_We need to emulate the buzzing that equates to proximity (lets use sound for now). 
        ///We can make it so that the sound is fired 
        ///then must wait for timer cycle to reset
        ///the timer cycle is relitive to the percent away from target
        ///if you're close the percent is low, thus the buzz will fire more often
        ///_We need to make a fish can get away if you're not careful
        ///



        //This will translate the bobber accordingly
        //What might these following floats are supposed to be? 
        float curX = 1;
        float curY = 1;
        float curZ = 1;        
        this.transform.position = new Vector3(curX, curY, curZ);

        
        
        //This will get the time
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            clock.fillAmount = timeRemaining / clockMax;
        }

        //This will determine the win/lose scenarios
        if (timeRemaining <= 0)
        {
            print("there must be some sort of win/lose condition");

            //if ("The losing Condition") { gameCondition = false; }
            //if ("The Winning Condition") { gameCondition = true; }
        }

        if (!gameCondition)
        {
            print("you lose");
        }
        if (gameCondition)
        {
            print("you win");
        }


        //audioSource.PlayOneShot(clip, volume);

    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(fishTargetPos, 1);

    }

}
