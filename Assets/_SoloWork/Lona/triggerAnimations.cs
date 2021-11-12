using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAnimations : MonoBehaviour
{
    public Animator BobberAnim ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(BobberAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if(BobberAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >=1.0f){
            if(this.gameObject.GetComponent<Animator>().GetInteger("animState")== 0){
                this.gameObject.GetComponent<Animator>().SetInteger("animState", 1);
            }
        }
        //print(BobberAnim.GetCurrentAnimatorStateInfo (0).IsName (current_state_name) );

    }
    
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
  }}
