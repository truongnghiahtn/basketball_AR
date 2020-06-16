using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public float force = 2f;
    public bool isTarget = false;
    public float zFactor = 2f;
    private GameObject parent;

    public Vector3 startPosition;

    private Vector2 startSwipe;
    private Vector2 endSwipe;

    private float startTime;
    private float endTime;

    public GameObject ballObject;

    // Use this for initialization
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (isTarget == true)
            {
                rb.isKinematic = false;
                Swipe();
                isTarget = false;
            }
        }
    }

    void Swipe()
    {
        Destroy(gameObject, 5);
        Vector3 swipe = endSwipe - startSwipe;
        swipe.z = swipe.y / zFactor;
        //rb.AddForce(swipe * force, ForceMode.Impulse);
        rb.AddRelativeForce(new Vector3(0, 1.7f, 1) * force * (endTime - startTime), ForceMode.Impulse);
        Invoke("SpawnNewBall", 2);
    }

    void SpawnNewBall()
    {
        GameObject newBall = Instantiate(ballObject);
        newBall.transform.parent = parent.transform;
        newBall.transform.localPosition = new Vector3(0, -0.2f, 0.8f);
        newBall.transform.localRotation = Quaternion.Euler(0, 0, 0);
        newBall.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnMouseDown()
    {
        rb.constraints = RigidbodyConstraints.None;
        isTarget = true;
    }
}
