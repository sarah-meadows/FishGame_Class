using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fishingCasting : MonoBehaviour
{
    Button CastLineBtn;

    public bool isCurrentlyFishing;
    
    
    public GameObject bobberMain;
    public GameObject bobberToClone;

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
        //HEY INTEGRADE THE CODE FOR TRIGGER ANIMATIONS INTO THIS SCRIPT!!!
        if (!isCurrentlyFishing) { StopCoroutine(fishTimer()); }
    }

    public void castFishingLine_event()
    {
        //when you press the casting button...
        if (isCurrentlyFishing==false)
        {
            //if you are not fishing, then activate fishing
            isCurrentlyFishing = true;
            //if you are not fishing, then activate fishing
            isCurrentlyFishing = true;

            //Make a new gameObject, and make it equal to: whatever we're cloning. Give it a new name so we can easily identify it later on
            //instantiate the gameObject that we'll make the bobber clone instantiated inside of
            GameObject newBobMain = Instantiate(bobberMain);
            newBobMain.transform.position = bobberClonePos;
            newBobMain.name = "BobberMain";
            //Now make the bobber clone inside of "newBobMain" that we just spawned. Transform our "newBobber" inside of child of newBobMain, called, "ParentBobbber"
            GameObject newBobber = Instantiate(bobberToClone);
            newBobber.transform.parent = GameObject.Find("ParentBobber").transform;
            newBobber.transform.position = GameObject.Find("ParentBobber").transform.position;
            newBobber.name = "newBobber";
            ///EXLINATION: 
            ///We instantiate the bobber inside of the premade parent because,
            ///if we end up having more than one type of bobber, 
            ///we won't need to do an animation per bobber type. 
            ///It becomes systematic
            ///
            //HEY INTEGRADE THE CODE FOR TRIGGER ANIMATIONS INTO THIS SCRIPT!!!

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
            ///Summary
            ///lineA otherwise if you are fishing and want to retract line, then deactivate fishing
            ///lineB destroy the clone we made earlier based upon the name of the gameObject
            ///lineC now you're trying to catch fish...
            ///
            Destroy(GameObject.Find("BobberMain"));
            isCurrentlyFishing = false;

        }
    }

    IEnumerator fishTimer()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        print("Time to catch");
        clockObj.SetActive(true);
        winLoseAlertParent.SetActive(true);

        GameObject newBobWithBehave = GameObject.Find("BobberMain");
        newBobWithBehave.name = "newBobWithBehave";
        newBobWithBehave.tag = "bobber";
        newBobWithBehave.AddComponent<bobberBehavior>();







    }
}
