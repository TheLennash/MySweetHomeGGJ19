using System.Linq;
using UnityEngine;

public class childDetector : MonoBehaviour
{

    GameObject parent;
    WallBehaviour cw;
    playerScript ps;

    public GameObject currentKid;

    private void Awake()
    {
        ps = GetComponentInParent<playerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //grabbing
        if (other.gameObject.tag == "Kid")
        {

            currentKid = other.gameObject;
            //if (ps.grabbedKid.Any(x => x == null))
            //{
            //    //ps.canGrab = true;
            //    //Debug.Log("Got the kid");
            //    //ps.grabbedKid[0] = other.gameObject;
            //    //other.gameObject.SetActive(false);
            //    //ps.grabbedKid[1] = other.gameObject;
            //}
        }

        //repairing
        if (other.gameObject.tag == "Building")
        {
            other.gameObject.GetComponent<WallBehaviour>();
            cw = other.gameObject.GetComponent<WallBehaviour>();

            bool canRepair = cw.Candies.Any(x => x.Value < 12);
            if (canRepair)
            {
                Debug.Log("you can repair this wall");
                ps.canRepair = true;
                ps.currentWall = cw;
            }
        }

        if(other.gameObject.tag == "Oven") {
            ps.canMelt = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Kid")
        {
            currentKid = null;
        }
        if (other.gameObject.tag == "Building")
        {
            cw = null;
        }
        if (other.gameObject.tag == "Oven") {
            ps.canMelt = false;
        }
    }

}
