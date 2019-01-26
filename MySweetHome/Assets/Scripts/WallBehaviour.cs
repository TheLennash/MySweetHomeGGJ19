using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public int CookiesCount;
    public int MarshmellowCount;
    public int ChocolateCount;
    public int CandyCaneCount;

    public int GetCandies(string candyType)
    {
        switch (candyType)
        {
            case nameof(Cookie):
                return CookiesCount;
            case nameof(Marshmellow):
                return MarshmellowCount;
            case nameof(Chocolate):
                return CookiesCount;
            case nameof(CandyCane):
                return CandyCaneCount;
            default:
                break;
        }

        Debug.Log("Failed to read candy type " + candyType);
        return 0;
    }

    public bool TakeCandies(string candyType)
    {
        switch (candyType)
        {
            case nameof(Cookie):
                if (CookiesCount > 0)
                    CookiesCount--;
                else
                    return false;
                return true;
            case nameof(Marshmellow):
                if (MarshmellowCount > 0)
                    MarshmellowCount--;
                else
                    return false;
                return true;
            case nameof(Chocolate):
                if (CookiesCount > 0)
                    CookiesCount--;
                else
                    return false;
                return true;
            case nameof(CandyCane):
                if (CandyCaneCount> 0)
                    CandyCaneCount--;
                else
                    return false;
                return true;
            default:
                break;
        }
        return false;
    }


    public void Start()
    {

    }


}
