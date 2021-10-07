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



    }
}
