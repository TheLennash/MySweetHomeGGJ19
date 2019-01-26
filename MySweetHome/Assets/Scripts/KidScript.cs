using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class KidScript : MonoBehaviour
{
    public float GameSize;

    public string CandyPrefrence;

    public HouseBehaviour House;

    public int CandyCount;

    public float Fatness = 1;
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
        Debug.Log("I Love " + CandyPrefrence);

        House = _house;
        GoGetCandy();

    }

    public void GoGetCandy()
    {
        //go to a wall;
        var targetWall = House.GetTargetWall(this);// House.Walls.OrderBy(x => x.GetCandies(CandyPrefrence)).Select(x => x).ToArray()[0];
        this.GetComponent<NavMeshAgent>().SetDestination(targetWall.transform.position);

        //grab candy;



        //go eat candy

    }

    public void GoEatCandy()
    {
        // go to location in circle of house;
        var GoTo = Random.insideUnitCircle.normalized * GameSize;
        this.GetComponent<NavMeshAgent>().SetDestination(GoTo);

        StartCoroutine(GoToPosition(GoTo));
        // eat candy


        // get fatter


        // go get candy
    }

    IEnumerator GoToPosition(Vector3 pos)
    {
        var r = 2;
        while (Vector3.SqrMagnitude(pos - transform.position) > (r * r))
        {




            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Found a position");
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
