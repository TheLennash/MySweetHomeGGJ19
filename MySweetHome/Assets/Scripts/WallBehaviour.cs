using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameObject go;
        var c = Candies[candyType];
        float per = 0;
        switch (candyType)
        {
            case nameof(Cookie):
                if (cookies.Count > 0)
                {
                    per = 12 / cookies.Count;

                    if (cookies.Count % per == 0)
                    {

                        go = cookies.Where(x => x.activeInHierarchy).FirstOrDefault();

                        if (go != null)
                        {
                            go.SetActive(false);
                        }
                    }
                }
                break;
            case nameof(Marshmellow):
                if (marshmellows.Count > 0)
                {
                    per = 12 / marshmellows.Count;

                    if (marshmellows.Count % per == 0)
                    {

                        go = marshmellows.Where(x => x.activeInHierarchy).FirstOrDefault();

                        if (go != null)
                        {
                            go.SetActive(false);
                        }
                    }
                }
                break;
            case nameof(Chocolate):
                if (chocolates.Count > 0)
                {
                    per = 12 / chocolates.Count;

                    if (chocolates.Count % per == 0)
                    {

                        go = chocolates.Where(x => x.activeInHierarchy).FirstOrDefault();

                        if (go != null)
                        {
                            go.SetActive(false);
                        }
                    }
                }
                break;

            case nameof(CandyCane):
                if (candyCanes.Count > 0)
                {
                    per = 12 / candyCanes.Count;

                    if (candyCanes.Count % per == 0)
                    {

                        go = candyCanes.Where(x => x.activeInHierarchy).FirstOrDefault();

                        if (go != null)
                        {
                            go.SetActive(false);
                        }
                    }
                }
                break;
        }


        if (Candies[candyType] > 0)
            Candies[candyType]--;
        else
            return false;
        return true;

    }

    private void GameOver()
    {

        if (Candies[nameof(Cookie)] <= 0 && Candies[nameof(Marshmellow)] <= 0 && Candies[nameof(Chocolate)] <= 0 && Candies[nameof(CandyCane)] <= 0)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);


        }

    }






}
