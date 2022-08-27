using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createCandy : MonoBehaviour
{

    public GameObject[] candies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void createCandy(int x, int y) {
        GameObject new_candy = GameObject.Instantiate (getRandomCandy (), new Vector2 (x, y), Quaternion.identity);
    }

    public GameObject getRandomCandy() {
        int randomOrder = Random.Range(0,candies.Length);
        return candies[randomOrder];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
