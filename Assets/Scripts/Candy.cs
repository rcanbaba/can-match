using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    float x,y;
    bool shouldFall = true;
    GameObject selectionTool;
    // Start is called before the first frame update
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
            if (transform.position.y-y<0.05f ) {
                shouldFall = false;
                transform.position = new Vector3(x,y,0);
            }
            transform.position = Vector3.Lerp (transform.position, new Vector3 (x,y,0), Time.deltaTime*40f);
        }
    }

    void OnMouseDown() {
        selectionTool.transform.position = transform.position;
    }
}
