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

    public List<Candy> mathcedRowCandies;
    public List<Candy> matchedColumnCandies;

    public string color;

    public Animator animator;

    void Start()
    {
        selectionTool = GameObject.FindGameObjectWithTag("select");
        animator = GetComponent<Animator>();
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

        createCandy.inGameCandies[(int)firstSelectedCandy.x, (int)firstSelectedCandy.y] = secondSelectedCandy;
        createCandy.inGameCandies[(int)secondSelectedCandy.x, (int)secondSelectedCandy.y] = firstSelectedCandy;

        float tempFirstX = firstSelectedCandy.x;
        float tempFirstY = firstSelectedCandy.y;

        firstSelectedCandy.x = secondSelectedCandy.x;
        firstSelectedCandy.y = secondSelectedCandy.y;
        secondSelectedCandy.x = tempFirstX;
        secondSelectedCandy.y = tempFirstY;

        checkMatch(firstSelectedCandy);
        checkMatch(secondSelectedCandy);

        StartCoroutine(firstSelectedCandy.destroyCandy());
        StartCoroutine(secondSelectedCandy.destroyCandy());

        firstSelectedCandy = null;
        secondSelectedCandy = null;
    }

    private void swapSelectedCandies() { 
        transform.position = Vector3.Lerp(transform.position, destinationPose, 0.2f);
    }

    private void checkMatch(Candy candy) { 
        checkRow(candy);
        checkColumn(candy);
    }

    private void checkRow(Candy candy) {
        print("ROW check");
        for(int i = (int)candy.x + 1; i < createCandy.inGameCandies.GetLength(0); i++) {
            Candy rightCandy = createCandy.inGameCandies[i,(int)candy.y];
            if (rightCandy.color == candy.color) {
                print("rightCandy match");
                mathcedRowCandies.Add(rightCandy);
            } else {
                break;
            }
        }
        for(int i = (int)candy.x - 1; i >= 0; i--) {
            Candy leftCandy = createCandy.inGameCandies[i,(int)candy.y];
            if (leftCandy.color == candy.color) {
                print("leftCandy match");
                mathcedRowCandies.Add(leftCandy);
            } else {
                break;
            }
        }
    }

    private void checkColumn(Candy candy) {
        print("COLUMN check");
        for(int i = (int)candy.y + 1; i < createCandy.inGameCandies.GetLength(1); i++) {
            Candy upCandy = createCandy.inGameCandies[(int)candy.x,i];
            if (upCandy.color == candy.color) {
                print("upCandy match");
                matchedColumnCandies.Add(upCandy);
            } else {
                break;
            }
        }
        for(int i = (int)candy.y - 1; i >= 0; i--) {
            Candy downCandy = createCandy.inGameCandies[(int)candy.x,i];
            if (downCandy.color == candy.color) {
                print("downCandy match");
                matchedColumnCandies.Add(downCandy);
            } else {
                break;
            }
        }
    }

    IEnumerator destroyCandy() {
        yield return new WaitForSeconds(0.3f);
        if (mathcedRowCandies.Count >= 2 || matchedColumnCandies.Count >= 2) {
            animator.SetBool("doNotRemove",true);
            if (mathcedRowCandies.Count >= 2) {
                foreach(var item in mathcedRowCandies) {
                    item.animator.SetBool("doNotRemove",true);
                }
            } else {
                foreach(var item in matchedColumnCandies) {
                    item.animator.SetBool("doNotRemove",true);
                }
            }
        }
    }

    public void startDestroyAnimation() {
        Debug.Log("Anim yok oldu");
        Destroy(gameObject);
    }
}