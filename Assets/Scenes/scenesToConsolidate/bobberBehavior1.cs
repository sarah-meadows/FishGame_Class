using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bobberBehavior1 : MonoBehaviour
{
    float fishCord_X;
    float fishCord_Z;

    float curCord_X;
    float curCord_Z;

    public float fishRange=500;
    public float withinTarget = 10f;
    float bobSpeed=4;

    bool tiltLeft, tiltRight, tiltUp, tiltDown;

    GameObject centerBobber;
    GameObject[] bounds;
    public Slider distSlider;

    float inGameRange;
    float calibrateCord;

    Vector3 fishTargetPos, curTargetPos;
    public float distFromTarget, valueAwayFromFish;
    
    Image clock;
    float maxSeconds = 10f;
    float timeRemaining;

    bool winGame = false;
    GameObject winAlert, loseAlert;

    Image BuzzTEST;
    Color32 buzzColor;

    bool buzzTimerIsBusy;


    // Start is called before the first frame update
    void Start()
    {
        //let's really randomize the results. We'll randomize the numbers 3 times. 
        for (int i = 0; i < 3; i++)
        {
            fishCord_X = Random.Range(-fishRange, fishRange);
            fishCord_Z = Random.Range(-fishRange, fishRange);
        }

        centerBobber = GameObject.Find("bobberCenter");
        bounds = GameObject.FindGameObjectsWithTag("bobberLimit");
        
        inGameRange = Vector3.Distance(bounds[0].transform.position, centerBobber.transform.position);
        calibrateCord = inGameRange * (1 / fishRange);


        //This is the ingame cordinates of the fish position
        float newX = (fishCord_X * calibrateCord) + centerBobber.transform.position.x;
        float newY = (centerBobber.transform.position.y);
        float newZ = (fishCord_Z * calibrateCord) + centerBobber.transform.position.z;

        fishTargetPos = new Vector3(newX, newY, newZ);

        this.transform.position = centerBobber.transform.position;

        clock = GameObject.Find("Clock").GetComponent<Image>();
        timeRemaining = maxSeconds;


        winAlert = GameObject.Find("Win");
        loseAlert = GameObject.Find("Lose");
        distSlider = GameObject.Find("distSlider").GetComponent<Slider>();

        winAlert.SetActive(false);
        loseAlert.SetActive(false);

        BuzzTEST = GameObject.Find("BuzzTEST").GetComponent<Image>();
        buzzColor = BuzzTEST.gameObject.GetComponent<Image>().color;
        buzzColor = new Color32(0, 19, 225, 0);
        BuzzTEST.color = buzzColor;

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


        //This will translate the bobber accordingly
        float curX = (curCord_X * calibrateCord) + centerBobber.transform.position.x;
        float curY = centerBobber.transform.position.y;
        float curZ = (curCord_Z * calibrateCord) + centerBobber.transform.position.z;        
        
        curTargetPos = new Vector3(curX, curY, curZ);
        this.transform.position = curTargetPos;

        //getting the distance from goal and percentage away from goal
        distFromTarget = Vector3.Distance(fishTargetPos, curTargetPos);
        valueAwayFromFish = (distFromTarget / calibrateCord) / (fishRange * 2);

        ///print shows in the console how far away we are from fish
        ///0% is when you're dead-on
        ///the larger the percent, the further away you are
        float percentageFromFish = Mathf.Round((valueAwayFromFish * 100) * 10.0f) * 0.1f;


        distSlider.value = 1 - valueAwayFromFish;


        //setting up the buzzing indicator
        if (!buzzTimerIsBusy){StartCoroutine(buzzIndicate());}
        

        //Setting up our timer and win conditions
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            clock.fillAmount = timeRemaining / maxSeconds;
        }
        if(timeRemaining <= 0)
        {

            if (percentageFromFish <= withinTarget){winGame=true; }
            else if (percentageFromFish > withinTarget){ winGame = false; }

            if (winGame)
            {
                print("WIN: do a prompt and continue to next scene");
                winAlert.SetActive(true);
            }

            else if (!winGame)
            {
                print("LOSE: do a prompt and allow fishing again");
                loseAlert.SetActive(true);

            }

        }



    }

    IEnumerator buzzIndicate()
    {
        buzzTimerIsBusy = true;
                
        yield return new WaitForSeconds(valueAwayFromFish);
        buzzColor = new Color32(0, 19, 225, 20);
        BuzzTEST.color = buzzColor;

        yield return new WaitForSeconds((valueAwayFromFish));
        buzzTimerIsBusy = false;
        buzzColor = new Color32(0, 19, 225, 0);
        BuzzTEST.color = buzzColor;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(fishTargetPos, 1);
    }
}
///We need to: 
///set the position of bobber to the curPos equivalent to real Space 
///
///_We need to make a countdown timer
///We need figure out how the fish can get away
///if at the end of the timer if the fish didn't get away, then you win
///if before or at the end of the timer the fish gets away, the you lose
///_We need to emulate the buzzing that equates to proximity (lets use sound for now). 
///We can make it so that the sound is fired 
///then must wait for timer cycle to reset
///the timer cycle is relitive to the percent away from target
///if you're close the percent is low, thus the buzz will fire more often
///_We need to make a fish can get away if you're not careful
///

