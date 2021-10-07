using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobberBoundSetup : MonoBehaviour
{
    private Camera cam;
    float camX,camY;
    Vector3 inGameCenter;

    GameObject centerOfBorder;
    GameObject[] borderPieces;
    
    //NOTE we will eventually get rid of the creation of "visual borders". The borders will help us calculate

    public float testA, testB, testC;

    void Start()
    {
        /**
         * The thought process is:
         * ... We want to position our objects...
         * ... So that the game world (3D) space cordinates... 
         * ... Are porportionate to the camera (2D) space... 
         * 
         * So let's center our realspace or "in-game" center
         * */

        cam = Camera.main;

        camX = cam.pixelWidth;
        camY = cam.pixelHeight;

        float camPosForZ = cam.transform.position.y;

        inGameCenter = cam.ScreenToWorldPoint(new Vector3(camX / 2, camY / 2, camPosForZ));

        /**
         * Now we have our in-game center
         * 
         * We want to position our objects (to be listed) accordingly:
         * Fisherman
         * bobber bounds edges
         * center of bobber bounds / bobber
         * 
         * Let's start with the bobber borders.
         * We a square region where the:
         * ... all directions = same dist from edge to center of border
         * ... top edge is at the very top of in-game
         * ... bottom edge is at the in-game center
         * 
         * Lets start by creating the boundry parts
         * */

        centerOfBorder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        centerOfBorder.name = "bobberCenter";
        centerOfBorder.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //centerOfBorder.layer = 3;

        for (int i = 0; i < 4; i++)
        {
            GameObject edgePieceBounds = GameObject.CreatePrimitive(PrimitiveType.Cube);
            edgePieceBounds.name = "border (" + i + ")";
            edgePieceBounds.tag = "bobberLimit";
            //edgePieceBounds.layer = 3;
        }
        borderPieces = GameObject.FindGameObjectsWithTag("bobberLimit");
        /**
         * Now we need to figure out where the edges go
         * We will want a padding so that the edges aren't on the absolute extremes of screen position (for aesthetic reasons)
         * 
         * Lets start by finding the y landmark values
         * get the distance between top to center landmark
         * Then center our center point
         * Then place our top and bottom edges
         * Then place the horizontal edges
         * */
        float padding = camY/10;
        float topEdgePos = ((camY-padding) / 4) * 4;  //at 100% position of total yscreen
        float centerYPos = ((camY-padding) / 4) * 3;   //at 75% position of total yscreen
        float botEdgePos = ((camY-padding) / 4) * 2;  //at 50% position of total yscreen

        //we can use this value for our calculations later on
        float borderRadius = topEdgePos - centerYPos;

        centerOfBorder.transform.position = cam.ScreenToWorldPoint(new Vector3(camX / 2, centerYPos, camPosForZ));

        borderPieces[0].transform.position = cam.ScreenToWorldPoint(new Vector3(camX / 2, topEdgePos, camPosForZ));
        borderPieces[1].transform.position = cam.ScreenToWorldPoint(new Vector3(camX / 2, botEdgePos, camPosForZ));
        borderPieces[2].transform.position = cam.ScreenToWorldPoint(new Vector3((camX / 2) + borderRadius, centerYPos, camPosForZ));
        borderPieces[3].transform.position = cam.ScreenToWorldPoint(new Vector3((camX / 2) - borderRadius, centerYPos, camPosForZ));

        

        //Check out the script objectPosSetup for cleaned up version.
    }
    /*Code I got from online
    void OnGUI()
    {
        
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
        testingPos = Camera.main.ScreenToWorldPoint(testingThing.transform.position);


        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(200, 200, 500, 500));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }
    */

}
