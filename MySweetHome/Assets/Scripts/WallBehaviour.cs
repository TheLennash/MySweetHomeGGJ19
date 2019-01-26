using System;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    //public int CookiesCount;
    //public int MarshmellowCount;
    //public int ChocolateCount;
    //public int CandyCaneCount;

    public int StartSizeCandy;

    public void Start()
    {
        foreach (var candy in Candies)
        {
            //candy.
        }
    }


    public Dictionary<string, int> Candies = new Dictionary<string, int>() {
        { nameof(Cookie) , 0 },
        { nameof(Marshmellow) , 0 },
        { nameof(Chocolate) , 0 },
        { nameof(CandyCane) , 0 }
    };

    public int GetCandies(string candyType)
    {
        return Candies[candyType];
    }

    public bool TakeCandies(string candyType)
    {
        if (Candies[candyType] > 0)
            Candies[candyType]--;
        else
            return false;
        return true;
       
    }




}
