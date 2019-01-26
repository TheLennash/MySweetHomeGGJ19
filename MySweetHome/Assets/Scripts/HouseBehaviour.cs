using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    public List<WallBehaviour> Walls = new List<WallBehaviour>();

    public WallBehaviour GetTargetWall(KidScript kid)
    {
        WallBehaviour result = null;
        float closest = float.MaxValue;

        foreach (var wall in Walls)
        {
            if (wall.GetCandies(kid.CandyPrefrence) > 0)
            {
                var distance = Vector3.Distance(wall.transform.position, kid.transform.position);
                if (distance < closest)
                {
                    result = wall;
                    closest = distance;


                }
            }
        }


        if (result != null)
            return result;

        Debug.Log("Failed to find wall");
        return Walls[0];
    }

    private void Awake()
    {

    }
}
