using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPosSetUp : MonoBehaviour
{
    private Camera cam;
    float camX, camY;
    Vector3 inGameCenter;

    public static Vector3 bobberStartPos;
    public static float borderToCenterRange;

    void Start()
    {
        cam = Camera.main;

        camX = cam.pixelWidth;
        camY = cam.pixelHeight;
        float camPosForZ = cam.transform.position.y;

        inGameCenter = cam.ScreenToWorldPoint(new Vector3(camX / 2, camY / 2, camPosForZ));

        //FOR BOBBER BOUNDRIES
        float padding = camY / 10;
        float topEdgePos = ((camY - padding) / 4) * 4;  //at 100% position of total yscreen
        float centerYPos = ((camY - padding) / 4) * 3;   //at 75% position of total yscreen
        float botEdgePos = ((camY - padding) / 4) * 2;  //at 50% position of total yscreen

        float borderRadius = topEdgePos - centerYPos;

        bobberStartPos = cam.ScreenToWorldPoint(new Vector3(camX / 2, centerYPos, camPosForZ));
        Vector3 edgePos= cam.ScreenToWorldPoint(new Vector3((camX / 2) + borderRadius, centerYPos, camPosForZ));
        
        borderToCenterRange = Vector3.Distance(edgePos, bobberStartPos);

        print(bobberStartPos + " this is start");
        print(borderToCenterRange + " this is the range");

        //FOR POSITIONING THE PLAYER OBJECT
    }
}
