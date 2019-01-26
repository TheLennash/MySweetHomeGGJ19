using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furnaceScript : MonoBehaviour {

    public GameObject cookie;
    public GameObject chocolate;
    public GameObject marshmallow;
    public GameObject candycane;
    public Transform candySpawnPoint;
    int val;
    public float furnaceBurnTime = 1f;



    public List<string> kidList = new List<string>();
    public Dictionary<string, int> Candies = new Dictionary<string, int>() {
        { nameof(Cookie) , 0 },
        { nameof(Marshmellow) , 0 },
        { nameof(Chocolate) , 0 },
        { nameof(CandyCane) , 0 }
    };

    private void Update() {
        if (kidList.Count > 0) {
            smeltKids();
        }
    }

    void smeltKids() {
        for (int i = 0; i < kidList.Count; i++) {
            if (kidList[i] == "Cookie") {
                Candies["Cookie"]++;
            }
            if (kidList[i] == "Marshmellow") {
                Candies["Marshmellow"]++;
            }
            if (kidList[i] == "Chocolate") {
                Candies["Chocolate"]++;
            }
            if (kidList[i] == "CandyCane") {
                Candies["CandyCane"]++;
            }
        }
        kidList.Clear();
        StartCoroutine("burningTime");

    }

    void emptyFurnace() {
        //int tmp = Candies["Cookie"];
        //for (int i = 0; i < tmp; i++) {

        //}
        foreach (KeyValuePair<string, int> candy in Candies) {
            val = candy.Value;
            
            if (candy.Key == "Cookie" && val > 0) {
                for (int i = 0; i < val; i++) {
                    Instantiate(cookie, candySpawnPoint.position, transform.rotation);
                    //Candies["Cookie"]--;
                }
                
            }
            if (candy.Key == "Chocolate" && val > 0) {
                for (int i = 0; i < val; i++) {
                    Instantiate(chocolate, candySpawnPoint.position, transform.rotation);
                    //Candies["Marshmellow"]--;
                }
                
            }
            if (candy.Key == "CandyCane" && val > 0) {
                for (int i = 0; i < val; i++) {
                    Instantiate(candycane, candySpawnPoint.position, transform.rotation);
                    //Candies["Chocolate"]--;
                }
                
            }
            if (candy.Key == "Marshmellow" && val > 0) {
                for (int i = 0; i < val; i++) {
                    Instantiate(marshmallow, candySpawnPoint.position, transform.rotation);
                    //Candies["CandyCane"]--;
                }

            }
            
            //(GameObject)Instantiate(Resources.Load(candy.Key));
            //Debug.Log(candy.Key);
            //Debug.Log(candy.Value);
        }
        //empty the bag
        val = 0;
        Candies["Cookies"] = 0;
        Candies["Chocolate"] = 0;
        Candies["CandyCane"] = 0;
        Candies["Marshmellow"] = 0;
        

        Debug.Log("We done burning boys");
    }

    IEnumerator burningTime() {

        Debug.Log("We burning boys");

        yield return new WaitForSeconds(furnaceBurnTime);

        emptyFurnace();
    }

}

