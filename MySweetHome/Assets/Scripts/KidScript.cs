using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class KidScript : MonoBehaviour
{
    public string CandyPrefrence;




    public HouseBehaviour House;

    public GameObject Candy;

    public float Fatness;
    public float speed;

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGERD");

        var wall = other.GetComponent<WallBehaviour>();
        if (wall != null)
        {
            Debug.Log("Take Candy");
            if (!wall.TakeCandies(CandyPrefrence))
            {
                //could not take candy.
                // try again
            }
        }

        Destroy(this.gameObject);
    }



}
