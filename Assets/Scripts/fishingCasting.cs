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
            GameObject newBobber = Instantiate(bobberDecoration);
            newBobber.transform.position = bobberClonePos;
            newBobber.name = "newBobber";

            cur_fishType = Random.Range(0, range_fishType);

            StartCoroutine(fishTimer());


        }

        else if (isCurrentlyFishing==true)
        {
            //otherwise if you are fishing and want to retract line, then deactivate fishing
            isCurrentlyFishing = false;
            //destroy the clone we made earlier based upon the name of the gameObject
            Destroy(GameObject.Find("newBobber"));
            StopCoroutine(fishTimer());

        }
    }

    IEnumerator fishTimer()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        Debug.Log("Time to catch");
        GameObject newBobBehave = Instantiate(bobberWithBehavior);
        newBobBehave.transform.position = GameObject.Find("newbobber").transform.position;



    }
}
