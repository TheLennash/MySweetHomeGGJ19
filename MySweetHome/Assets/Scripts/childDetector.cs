using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class childDetector : MonoBehaviour {

    GameObject parent;
    WallBehaviour cw;
    playerScript ps;
    public int[] counters;

    private void Awake() {
        ps = GetComponentInParent<playerScript>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Kid") {
            Debug.Log("grabbable");
            ps.grabbedKid = other.gameObject;
            ps.canGrab = true;
        }

        if(other.gameObject.tag == "Building") {
            Debug.Log("HIT WALL");
            other.gameObject.GetComponent<WallBehaviour>();
            cw = other.gameObject.GetComponent<WallBehaviour>();


            bool canRepair = cw.Candies.Any(x => x.Value < 12);
            if (canRepair) {

            }

            //if (cw.Candies[candyType] < 12) {
            //    int[] counters = new int[4] {cw.CookiesCount, cw.ChocolateCount, cw.CandyCaneCount, cw.MarshmellowCount };
            //    ps.wallCounter = counters;
            //    ps.currentWall = cw;
            //    ps.canRepair = true;
            //}

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Kid") {
            ps.canGrab = false;

        }
        if (other.gameObject.tag == "Building") {
            cw = null;

        }
    }

}
