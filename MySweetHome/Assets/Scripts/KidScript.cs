using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public GameObject Belly;

    public List<Renderer> shirt;

    public ParticleSystem psystem;

    Vector3 wantedPos = Vector3.zero;

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



    public void Initialize(HouseBehaviour _house, Material mat, string pref)
    {

        CandyPrefrence = pref;
        //Debug.Log("I Love " + CandyPrefrence);

        shirt.All(x => x.material = mat);

        House = _house;
        GoGetCandy();

    }

    public void GoGetCandy()
    {
        //go to a wall;
        var targetWall = House.GetTargetWall(this);// House.Walls.OrderBy(x => x.GetCandies(CandyPrefrence)).Select(x => x).ToArray()[0];
        wantedPos = targetWall.transform.position;
        if (!this.GetComponent<NavMeshAgent>().SetDestination(wantedPos))
        {
            GoGetCandy();
            return;
        }



        // go eat candy
        // happends in trigger
    }

    private void Update()
    {
        if (!this.GetComponent<NavMeshAgent>().SetDestination(wantedPos))
        {

        }
    }

    public void GoEatCandy()
    {
        // go to location in circle of house;
        var GoTo = Random.insideUnitCircle.normalized * GameSize;


        wantedPos = new Vector3(GoTo.x, 0, GoTo.y);
        if (!this.GetComponent<NavMeshAgent>().SetDestination(wantedPos))
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
        var r = 25;
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
        psystem.Play();

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
