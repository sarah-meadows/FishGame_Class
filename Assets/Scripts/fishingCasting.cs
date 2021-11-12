using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fishingCasting : MonoBehaviour
{
    Button CastLineBtn;

    public bool isCurrentlyFishing;

    public GameObject bobberDecoration;
    public GameObject bobberWithBehavior;
    public GameObject bobbermain;

    GameObject clockObj,winLoseAlertParent;


    Vector3 bobberClonePos;

    public int range_fishType;

    static public int cur_fishType;

    public float minWaitTime, maxWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        isCurrentlyFishing = false;

        //lets make a gameobject and then make this our position to start our clone of the bobber;
        bobberClonePos = GameObject.Find("bobberCenter").transform.position;

        CastLineBtn = GameObject.Find("CastLineBtn").GetComponent<Button>();
        CastLineBtn.onClick.AddListener(castFishingLine_event);

        //Find the timer, then set it inactive
        clockObj = GameObject.Find("Clock");
        clockObj.SetActive(false);
        winLoseAlertParent = GameObject.Find("WinLosePopUp_1");
        winLoseAlertParent.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void castFishingLine_event()
    {
        //when you press the casting button...
        if (isCurrentlyFishing==false)
        {
            //if you are not fishing, then activate fishing
            isCurrentlyFishing = true;

            //Make a new gameObject, and make it equal to: whatever we're cloning. Give it a new name (we'll use this for when we delete the object)
            GameObject newbobmain = Instantiate(bobbermain);
            newbobmain.transform.position = bobberClonePos;
            newbobmain.name = "BobberMain";
            GameObject newBobber = Instantiate(bobberDecoration);
            newBobber.transform.parent = GameObject.Find("ParentBobber").transform;

            //newBobber.transform.position = bobberClonePos;
            newBobber.name = "newBobber";
              
            //ADD AN ANIMATION FOR BOBBER
            /// Casting animation
            /// idle bobber animation
            /// you'll need to the the animator located on the "newBobber" variable
            /// in the animator controller, you'll need to set up int varibles
            /// In the animator controller, animations are true if the int equals x (click on the arrows that joins the animation clips to state that it must equal the variable in order to execute
            /// then, back here in the code, Set the bool value (you'll need the int's name and value)



            cur_fishType = Random.Range(0, range_fishType);

            StartCoroutine(fishTimer());


        }

        else if (isCurrentlyFishing==true)
        {
            //otherwise if you are fishing and want to retract line, then deactivate fishing
            isCurrentlyFishing = false;
            //destroy the clone we made earlier based upon the name of the gameObject
            Destroy(GameObject.Find("BobberMain"));

            //now you're trying to catch fish...
            StopCoroutine(fishTimer());

        }
    }

    IEnumerator fishTimer()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        print("Time to catch");
        
        GameObject discardBob = GameObject.Find("BobberMain");
        Destroy(discardBob);

        clockObj.SetActive(true);
        winLoseAlertParent.SetActive(true);

        GameObject newBobBehave = Instantiate(bobberWithBehavior);
        newBobBehave.transform.position = GameObject.Find("bobberCenter").transform.position;






    }
}
