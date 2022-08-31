using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    float x,y;
    bool shouldFall = true;
    GameObject selectionTool;

    public static Candy firstSelectedCandy;
    public static Candy secondSelectedCandy;
    public Vector3 destinationPose;
    public bool isSwapping = false;

    void Start()
    {
        selectionTool = GameObject.FindGameObjectWithTag("select");
    }

    public void enterNewPosition(float _x, float _y) {
        x = _x;
        y = _y;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFall) {
            fallCandies();
        }
        if (isSwapping) {
            swapSelectedCandies();
        }
    }

    private void fallCandies() { 
        if (transform.position.y-y<0.05f ) {
            shouldFall = false;
            transform.position = new Vector3(x,y,0);
        }
        transform.position = Vector3.Lerp (transform.position, new Vector3 (x,y,0), Time.deltaTime*40f);
    }

    void OnMouseDown() {
        selectionTool.transform.position = transform.position;
        if (firstSelectedCandy == null) {
            firstSelectedCandy = this;
        } else {
            secondSelectedCandy = this;
            if (firstSelectedCandy != secondSelectedCandy) {
                checkSwapCandy();
            } else {
                secondSelectedCandy = null;
            }
        }
    }

    private void checkSwapCandy() {

        print(firstSelectedCandy.x);
        print(firstSelectedCandy.y);

        print(secondSelectedCandy.x);
        print(secondSelectedCandy.y);

        float deltaPoseX = Mathf.Abs(firstSelectedCandy.x - secondSelectedCandy.x);
        float deltaPoseY = Mathf.Abs(firstSelectedCandy.y - secondSelectedCandy.y);

        if (deltaPoseX + deltaPoseY == 1) {
            configureSelectedCandiesSwap();
            // swap
            print("SWAP");
        } else {
            firstSelectedCandy = secondSelectedCandy;
            secondSelectedCandy = null;
            // not swap
            print("DO NOT SWAP");
        }
    } 

    private void configureSelectedCandiesSwap() {
        firstSelectedCandy.isSwapping = true;
        secondSelectedCandy.isSwapping = true;
        firstSelectedCandy.destinationPose = secondSelectedCandy.transform.position;
        secondSelectedCandy.destinationPose = firstSelectedCandy.transform.position;
        firstSelectedCandy = null;
        secondSelectedCandy = null;
    }

    private void swapSelectedCandies() { 
        transform.position = Vector3.Lerp(transform.position, destinationPose, 0.2f);
    }

}