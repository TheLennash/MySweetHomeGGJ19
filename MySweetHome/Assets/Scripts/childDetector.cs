using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childDetector : MonoBehaviour {

    GameObject parent;
    playerScript ps;

    private void Awake() {
        ps = GetComponentInParent<playerScript>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Kid") {
            Debug.Log("grabbable");
            ps.grabbedKid = other.gameObject;
            ps.canGrab = true;

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Kid") {
            ps.canGrab = false;

        }
    }

}
