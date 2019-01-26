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
            Debug.Log(ps.grabbedKid[0]);
            Debug.Log(ps.grabbedKid[1]);
            if (ps.grabbedKid[0] == null) {
                ps.canGrab = true;
                ps.grabbedKid[0] = other.gameObject;
            } else if (ps.grabbedKid[1] == null) {
                ps.canGrab = true;
                ps.grabbedKid[1] = other.gameObject;
            }

        }

        if (other.gameObject.tag == "Building") {
            other.gameObject.GetComponent<WallBehaviour>();
            cw = other.gameObject.GetComponent<WallBehaviour>();


            bool canRepair = cw.Candies.Any(x => x.Value < 12);
            if (canRepair) {
                Debug.Log("you can repair this wall");
                ps.canRepair = true;
                ps.currentWall = cw;
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
