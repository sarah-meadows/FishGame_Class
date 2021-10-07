using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobberControls : MonoBehaviour
{
    GameObject bobberObj;
    
    public float fishCord_X=305;
    public float fishCord_Z=-140;

    public float curCord_X;
    public float curCord_Z;

    float fishRange = 500;
    public float bobSpeed = 1;
    
    public Vector3 bobPosStart;

    public float inGameRange;
    public float calibrateBob;

    public bool tiltLeft, tiltRight, tiltUp, tiltDown;

    public float percentFromTarget;

    bool gizmoIsUsed;

    
    void Start()
    {
        bobberObj = GameObject.Find("Bobber");


        //fishCord_X = Random.Range(-fishRange, fishRange);
        //fishCord_Z = Random.Range(-fishRange, fishRange);


        Camera cam = Camera.main;
        float camX = cam.pixelWidth;
        float camY = cam.pixelHeight;
        float camPosForZ = cam.transform.position.y;

        //FOR BOBBER BOUNDRIES
        float padding = camY / 10;
        float topEdgePos = ((camY - padding) / 4) * 4;  //at 100% position of total yscreen
        float centerYPos = ((camY - padding) / 4) * 3;   //at 75% position of total yscreen
        float botEdgePos = ((camY - padding) / 4) * 2;  //at 50% position of total yscreen
        float borderRadius = topEdgePos - centerYPos;

        Vector3 edgePos = cam.ScreenToWorldPoint(new Vector3((camX / 2) + borderRadius, centerYPos, camPosForZ));
        bobPosStart = cam.ScreenToWorldPoint(new Vector3(camX / 2, centerYPos, camPosForZ));
        inGameRange = Vector3.Distance(edgePos, bobPosStart);

        //

        calibrateBob = inGameRange * (1 / fishRange);

        bobberObj.transform.position = bobPosStart;




    }

    private void OnDrawGizmos()
    {
        Vector3 raycastPosU, raycastPosD, raycastPosL, raycastPosR;
        Camera cam = Camera.main;
        float camX = cam.pixelWidth;
        float camY = cam.pixelHeight;
        float camPosForZ = cam.transform.position.y;

        //FOR BOBBER BOUNDRIES
        float padding = camY / 10;
        float topEdgePos = ((camY - padding) / 4) * 4;  //at 100% position of total yscreen
        float centerYPos = ((camY - padding) / 4) * 3;   //at 75% position of total yscreen
        float botEdgePos = ((camY - padding) / 4) * 2;  //at 50% position of total yscreen
        float borderRadius = topEdgePos - centerYPos;

        raycastPosU = cam.ScreenToWorldPoint(new Vector3(camX / 2, topEdgePos, camPosForZ));
        raycastPosD = cam.ScreenToWorldPoint(new Vector3(camX / 2, botEdgePos, camPosForZ));
        raycastPosR = cam.ScreenToWorldPoint(new Vector3((camX / 2) + borderRadius, centerYPos, camPosForZ));
        raycastPosL = cam.ScreenToWorldPoint(new Vector3((camX / 2) - borderRadius, centerYPos, camPosForZ));


        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3((fishCord_X * calibrateBob), 1, (fishCord_Z * calibrateBob)), 1);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastPosU, transform.TransformDirection(Vector3.left) * 5);
        Gizmos.DrawRay(raycastPosD, transform.TransformDirection(Vector3.left) * 5);
        Gizmos.DrawRay(raycastPosU, transform.TransformDirection(Vector3.right) * 5);
        Gizmos.DrawRay(raycastPosD, transform.TransformDirection(Vector3.right) * 5);

        Gizmos.DrawRay(raycastPosL, transform.TransformDirection(Vector3.forward) * 5);
        Gizmos.DrawRay(raycastPosR, transform.TransformDirection(Vector3.forward) * 5);
        Gizmos.DrawRay(raycastPosL, transform.TransformDirection(Vector3.back) * 5);
        Gizmos.DrawRay(raycastPosR, transform.TransformDirection(Vector3.back) * 5);




    }
    void Update()
    {
        /** 
         * calculating if there is any user input via button press or tilting. 
         * Set bool true if actions occur
         * */
        if (Input.GetKey(KeyCode.UpArrow)) { tiltUp = true; } else { tiltUp = false; }
        if (Input.GetKey(KeyCode.DownArrow)) { tiltDown = true; } else { tiltDown = false; }
        if (Input.GetKey(KeyCode.LeftArrow)) { tiltLeft = true; } else { tiltLeft = false; }
        if (Input.GetKey(KeyCode.RightArrow)) { tiltRight = true; } else { tiltRight = false; }

        /**
         * increase/decrease current cord value upon tilts
         * */
        if (tiltUp) { curCord_Z += bobSpeed; }
        if (tiltDown) { curCord_Z -= bobSpeed; }

        if (tiltRight) { curCord_X += bobSpeed; }
        if (tiltLeft) { curCord_X -= bobSpeed; }

        bobberObj.transform.position = new Vector3((curCord_X * calibrateBob) + bobPosStart.x, bobPosStart.y, (curCord_Z * calibrateBob) + bobPosStart.z);


       // print((fishCord_X * calibrateBob) / bobberObj.transform.position.x);
       // percentFromTarget = ((fishCord_X * calibrateBob) / bobberObj.transform.position.x) + ((fishCord_Z * calibrateBob) / bobberObj.transform.position.z);




    }
}