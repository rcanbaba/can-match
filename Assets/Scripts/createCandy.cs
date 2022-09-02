using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createCandy : MonoBehaviour
{

    public GameObject[] preCandies;
    public int width, height;
    public static Candy[,] inGameCandies;

    void Start() {
        inGameCandies = new Candy[width,height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                createOneCandy(x,y);
            }
        }
    }

    public void createOneCandy(int x, int y) {
        GameObject randomCandyObject = getRandomCandy();
        GameObject newCandy = GameObject.Instantiate (randomCandyObject, new Vector2 (x, y+10), Quaternion.identity);
        Candy candy = newCandy.GetComponent<Candy>();
        candy.color = randomCandyObject.name;
        candy.enterNewPosition(x,y);
        inGameCandies[x,y] = candy;
    }

    public GameObject getRandomCandy() {
        int randomNumber = Random.Range(0,preCandies.Length);
        return preCandies[randomNumber];
    }

    // Update is called once per frame
    void Update() {
        
    }
}
