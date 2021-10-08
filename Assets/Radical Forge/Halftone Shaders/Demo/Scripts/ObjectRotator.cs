using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    public int speed = 5;
    public float friction = 0.25f;
    public float lerpSpeed = 0.85f;
    public float xDeg;
    public float yDeg;
    public Quaternion fromRotation;
    public Quaternion toRotation;


    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    void Update()
    {
        if (_isRotating)
        {
            xDeg -= Input.GetAxis("Mouse X") * speed * friction;
            yDeg += Input.GetAxis("Mouse Y") * speed * friction;
            fromRotation = transform.rotation;
            toRotation = Quaternion.Euler(yDeg, xDeg, 0);
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        }
        //transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        if (Input.GetMouseButtonDown(0))
        {
            _isRotating = true;
            _mouseReference = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isRotating = false;
        }
    }

}
