using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobberBehavior : MonoBehaviour
{
    public float fishCord_X;
    public float fishCord_Z;

    public float curCord_X;
    public float curCord_Z;

    float fishRange=500;
    float bobSpeed=1;

    public bool tiltLeft, tiltRight, tiltUp, tiltDown;

    GameObject centerBobber;
    GameObject[] bounds;

    public float inGameRange;



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



    }
}
