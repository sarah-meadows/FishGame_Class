using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAnimations : MonoBehaviour
{
    Animator BobberAnim;
    // Start is called before the first frame update
    void Start()
    {
        BobberAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        ///READ PLEASE!!! Teacher note:
        ///Let's consolidate code. 
        ///See if you can integrade this code into the sccript fishingCasting script. 
        ///It's good to have your code in one place. 
        ///However, this is minimal priority. 
        ///Let's entire game to function before cleaning up this code. 
        ///
        
        //print(BobberAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (BobberAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (BobberAnim.gameObject.GetComponent<Animator>().GetInteger("animState") == 0)
            {
                BobberAnim.gameObject.GetComponent<Animator>().SetInteger("animState", 1);
            }
        }

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Animator>().SetInteger("animState", 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Animator>().SetInteger("animState", 0);
        }
    }
    */
}