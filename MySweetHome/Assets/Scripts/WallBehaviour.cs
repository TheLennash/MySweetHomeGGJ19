using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallBehaviour : MonoBehaviour
{

    public void Start()
    {

    }


    public Dictionary<string, int> Candies = new Dictionary<string, int>() {
        { nameof(Cookie) , 12 },
        { nameof(Marshmellow) , 12 },
        { nameof(Chocolate) , 12 },
        { nameof(CandyCane) , 12 }
    };

    public int GetCandies(string candyType)
    {
        if (Candies.ContainsKey(candyType))
            return Candies[candyType];
        else
            return 0;
    }

    public bool TakeCandies(string candyType)
    {
        if (Candies[candyType] > 0)
            Candies[candyType]--;
        else
            return false;
        return true;
       
    }

    private void GameOver() {

        if (Candies[nameof(Cookie)] <= 0 && Candies[nameof(Marshmellow)] <= 0 && Candies[nameof(Chocolate)] <= 0 && Candies[nameof(CandyCane)] <= 0) {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);


        }

    }






}
