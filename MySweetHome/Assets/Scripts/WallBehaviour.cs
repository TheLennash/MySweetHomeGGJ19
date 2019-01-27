using System;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{


    public List<GameObject> cookies;
    public List<GameObject> marshmellows;
    public List<GameObject> chocolates;
    public List<GameObject> candyCanes;


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




}
