using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class KidScript : MonoBehaviour
{
    public HouseBehaviour House;

    public GameObject Candy;

    public float Fatness;
    public float speed;

    public void Initialize(HouseBehaviour _house)
    {
        House = _house;
        GoGetCandy();

    }

    public void GoGetCandy()
    {
        //go to a wall;
        //var targetWall = House.Walls.Where()
        this.GetComponent<NavMeshAgent>().SetDestination(House.Walls[0].transform.position);

        //grab candy;
        //go eat candy

    }

    public void GoEatCandy()
    {
        // go to location in circle of house;
        // eat candy
        // get fatter
        // go get candy
    }

    IEnumerator GoToPosition(Transform Position)
    {
        while (true)
        {


            yield return new WaitForEndOfFrame();
        }
    }




}
