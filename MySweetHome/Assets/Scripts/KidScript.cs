﻿using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class KidScript : MonoBehaviour
{
    public float GameSize;

    public string CandyPrefrence;

    public HouseBehaviour House;

    //candy;s in hand
    public int CandyCount;

    private float Fatness = 1;

    public float TopSpeed = 5;

    public float Speed
    {
        get
        {
            return this.GetComponent<NavMeshAgent>().speed;
        }
        set
        {
            this.GetComponent<NavMeshAgent>().speed = value;
        }
    }

    public Animator[] animators = new Animator[0];

    public GameObject Belly;


    public void Initialize(HouseBehaviour _house)
    {
        var types = Assembly.GetAssembly(typeof(Candy)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Candy))).ToList();
        var random = Random.Range(0, types.Count());

        CandyPrefrence = types[random].ToString();
        //Debug.Log("I Love " + CandyPrefrence);

        House = _house;
        GoGetCandy();

    }

    public void GoGetCandy()
    {
        //go to a wall;
        var targetWall = House.GetTargetWall(this);// House.Walls.OrderBy(x => x.GetCandies(CandyPrefrence)).Select(x => x).ToArray()[0];
        if (!this.GetComponent<NavMeshAgent>().SetDestination(targetWall.transform.position))
        {
            GoGetCandy();
            return;
        }



        // go eat candy
        // happends in trigger
    }

    public void GoEatCandy()
    {
        // go to location in circle of house;
        var GoTo = Random.insideUnitCircle.normalized * GameSize;

        if (!this.GetComponent<NavMeshAgent>().SetDestination(GoTo))
        {
            GoEatCandy();
            return;
        }

        //this checks if youve reached the pos;
        StartCoroutine(GoToPosition(GoTo));


    }

    IEnumerator GoToPosition(Vector3 pos)
    {
        Debug.Log("waiting for position");

        //wait until position is reached (or is close)
        var r = 10;
        while (Vector3.SqrMagnitude(pos - transform.position) > (r * r))
        {
            //check this once per second
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Found a position");

        // eat candy
        foreach (var a in animators)
        {
            a.SetTrigger("EatCandy");
        }

        // get fatter
        Fatness++;
        Speed = TopSpeed / Fatness;
        CandyCount = 0;

        // go get candy
        GoGetCandy();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("TRIGGERD " + other.tag);
        if (other.CompareTag("Building"))
        {
            var wall = other.GetComponent<WallBehaviour>();
            if (wall != null)
            {
                if (wall.TakeCandies(CandyPrefrence))
                {
                    Debug.Log("Take Candy");
                    //grab candy;
                    foreach (var a in animators)
                    {
                        a.SetTrigger("EatCandy");
                    }

                    CandyCount++;

                    GoEatCandy();
                }
                else
                {
                    // try again
                    //could not take candy.
                    Debug.Log("could not take candy");
                    GoGetCandy();
                }
            }
        }
        //Destroy(this.gameObject);
    }



}
