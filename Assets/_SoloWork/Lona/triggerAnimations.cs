using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAnimations : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player") 
   {
            print("triggered by player");
            this.gameObject.GetComponent<Animator>();

   }
  }
}
