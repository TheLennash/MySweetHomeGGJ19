using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

    public Transform Rotation;
    public Transform child;

    public float speed;
    float vertAngle;

    public bool canGrab;
    public GameObject[] grabbedKid = new GameObject[2];

    public childDetector cd;
    public WallBehaviour currentWall;
    public bool canRepair;


    public Dictionary<string, int> Candies = new Dictionary<string, int>() {
        { nameof(Cookie) , 0 },
        { nameof(Marshmellow) , 0 },
        { nameof(Chocolate) , 0 },
        { nameof(CandyCane) , 0 }
    };



    // Start is called before the first frame update
    void Start() {
        cd = GetComponentInChildren<childDetector>();
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Rotation.rotation;
        PlayerMovement();
        GrabKid();

    }


    void PlayerMovement() {

        //Defines player movement direction
        var horizontal = Input.GetAxis("Horizontal");// * speed * Time.deltaTime;
        var vertical = Input.GetAxis("Vertical");// * speed * Time.deltaTime;


        var moveDir = new Vector3(-horizontal, 0, -vertical);
        Debug.Log(moveDir);

        //var angle = Vector3.Angle(moveDir, transform.forward); //



        transform.Translate(moveDir * Time.deltaTime * speed);



        //child.transform.eulerAngles =  new Vector3(0,angle, 0);
        ////LEFT
        //Vector3 rotateLeft = new Vector3(-horizontal, transform.rotation.y + 90, -vertical);
        ////RIGHT
        //Vector3 rotateRight = new Vector3(-horizontal, transform.rotation.y - 90, -vertical);
        ////FORWARD
        //Vector3 rotateForward = new Vector3(-horizontal, transform.rotation.y, -vertical);
        ////BACKWARD
        //Vector3 rotateBackward = new Vector3(-horizontal, transform.rotation.y + 180, -vertical);

        Quaternion WantedRotation = Quaternion.identity;
        if (vertical < 0) {
            vertAngle = 0;
        } else {
            vertAngle = -vertical * 180;
        }
        //WantedRotation = Quaternion.Euler(new Vector3(0, (90 * -horizontal) + (vertAngle), 0));
        WantedRotation = Quaternion.Euler(new Vector3(0, (180-(horizontal * 90)) + (90-(vertical * 90)), 0));

        //float angle = Mathf.LerpAngle(transform.rotation.y, transform.rotation.y + 90, Time.deltaTime * 0.1f);
        //Rotate child
        //Rotate left
        //if (horizontal < 0) {
        //    //WantedRotation = Quaternion.Euler(new Vector3(-horizontal, transform.rotation.y + 90 , -vertical));
        //    WantedRotation = Quaternion.Euler(new Vector3(-horizontal, transform.rotation.y + (90 * horizontal) , -vertical));
        //}

        ////Rotate right
        //if (horizontal > 0) {
        //    WantedRotation = Quaternion.Euler(new Vector3(-horizontal, transform.rotation.y - 90, -vertical));
        //}

        ////Rotate forward
        //if (vertical < 0) {
        //    WantedRotation = Quaternion.Euler(new Vector3(-horizontal, transform.rotation.y, -vertical));
        //}

        //if (vertical > 0) {
        //    WantedRotation = Quaternion.Euler(new Vector3(-horizontal, transform.rotation.y + 180, -vertical));
        //}

        child.transform.localRotation = Quaternion.Slerp(child.transform.localRotation, WantedRotation, Time.deltaTime * 5);
        //child.transform.localRotation = WantedRotation;
    }

    void GrabKid() {
        //Kid grab

        if (Input.GetKeyDown(KeyCode.Q) && canGrab == true) {
            cd.currentKid.SetActive(false);
            if (grabbedKid[0] == null) {

                grabbedKid[0] = cd.currentKid;
            } else {
                grabbedKid[1] = cd.currentKid;
            }

            //foreach (var kid in grabbedKid) {
               // kid.SetActive(false);
               // Debug.Log("Got the kid");
            //}

        }
    }

    void RepairWall() {
        if (canRepair == true && Input.GetKeyDown(KeyCode.E)) {

            foreach (var candy in currentWall.Candies) {
                if (Candies[candy.Key] > 0) {
                    currentWall.Candies[candy.Key] = candy.Value + 1;
                    Candies[candy.Key] = candy.Value - 1;
                }


            }

        }

    }



}
