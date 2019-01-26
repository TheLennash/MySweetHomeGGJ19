using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

    public Transform Rotation;
    public Transform child;

    public float speed;

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
        var horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;


        var moveDir = new Vector3(-horizontal, 0, -vertical);

        var angle = Vector3.Angle(moveDir, transform.forward); //



        transform.Translate(moveDir);



        //child.transform.eulerAngles =  new Vector3(0,angle, 0);


        //Rotate child
        //Rotate left
        if (horizontal < 0) {
            child.transform.localEulerAngles = new Vector3(-horizontal, transform.rotation.y + 90, -vertical);
        }

        //Rotate right
        if (horizontal > 0) {
            child.transform.localEulerAngles = new Vector3(-horizontal, transform.rotation.y - 90, -vertical);
        }

        //Rotate forward
        if (vertical < 0) {
            child.transform.localEulerAngles = new Vector3(-horizontal, transform.rotation.y, -vertical);
        }

        if (vertical > 0) {
            child.transform.localEulerAngles = new Vector3(-horizontal, transform.rotation.y + 180, -vertical);
        }


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
