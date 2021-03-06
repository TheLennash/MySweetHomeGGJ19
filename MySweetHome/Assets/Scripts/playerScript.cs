﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerScript : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip grabSound;

    public Transform Rotation;
    public Transform child;

    public float speed;

    //public bool canGrab;
    public GameObject[] grabbedKid = new GameObject[2];

    public childDetector cd;
    public WallBehaviour currentWall;
    public bool canRepair;

    public furnaceScript fs;
    public bool canMelt;
    public Animator animator;



    //candysorts array
    public string[] candySorts = new string[4] { "CandyCane", "Chocolate", "Cookie", "Marshmellow" };

    public ParticleSystem psystem;


    public Dictionary<string, int> Candies = new Dictionary<string, int>() {
        { nameof(Cookie) , 0 },
        { nameof(Marshmellow) , 0 },
        { nameof(Chocolate) , 0 },
        { nameof(CandyCane) , 0 }
    };



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cd = GetComponentInChildren<childDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWall != null)
        {
            foreach (var candy in currentWall.Candies)
            {
                //Now you can access the key and value both separately from this attachStat as:
                Debug.Log(candy.Key + candy.Value + "WALL");
            }
        }
        transform.rotation = Rotation.rotation;
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            RepairWall();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GrabKid();
            PutKidInFurnace();
        }
    }


    void PlayerMovement()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Defines player movement direction
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveDir = new Vector3(-horizontal, 0, -vertical);

        transform.Translate(moveDir * Time.deltaTime * speed);

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            Quaternion WantedRotation = Quaternion.LookRotation(moveDir);
            child.transform.localRotation = Quaternion.Slerp(child.transform.localRotation, WantedRotation, Time.deltaTime * 5);
        }
    }

    void GrabKid()
    {
        animator.SetTrigger("GrabChildren");
        audioSource.PlayOneShot(grabSound, 1);

        //Debug.Log("GGRABBING@");
        //inv full
        if (!grabbedKid.Any(x => x == null))
            return;

        //no kid
        if (cd.currentKid == null)
            return;

        //you already have this kid.
        if (grabbedKid.ToList().Contains(cd.currentKid))
            return;


        for (int i = 0; i < grabbedKid.Length; i++)
        {
            var invspace = grabbedKid[i];
            if (invspace == null)
            {
                grabbedKid[i] = cd.currentKid;
                break;
            }
        }

        cd.currentKid.SetActive(false);
        cd.currentKid = null;

        psystem.Play();


        //Debug.Log("hasChildrenInventory " +  grabbedKid.Any(x => x != null))
        animator.SetBool("HasChildrenInventory", grabbedKid.Any(x => x != null));
    }

    void RepairWall()
    {
        if (canRepair)
        {
            if (Candies["Cookie"] > 0 && currentWall.Candies["Cookie"] < 12)
            {
                currentWall.Candies["Cookie"]++;
                Candies["Cookie"]--;
                currentWall.turnOn(nameof(Cookie));
            }
            if (Candies["Marshmellow"] > 0 && currentWall.Candies["Marshmellow"] < 12)
            {
                currentWall.Candies["Marshmellow"]++;
                Candies["Marshmellow"]--;
                currentWall.turnOn(nameof(Marshmellow));

            }
            if (Candies["Chocolate"] > 0 && currentWall.Candies["Chocolate"] < 12)
            {
                currentWall.Candies["Chocolate"]++;
                Candies["Chocolate"]--;
                currentWall.turnOn(nameof(Chocolate));

            }
            if (Candies["CandyCane"] > 0 && currentWall.Candies["CandyCane"] < 12)
            {
                currentWall.Candies["CandyCane"]++;
                Candies["CandyCane"]--;
                currentWall.turnOn(nameof(CandyCane));

            }






            //foreach (var candy in currentWall.Candies) {
            //    if (Candies[candy.Key] > 0) {
            //        currentWall.Candies[candy.Key]++;
            //        Candies[candy.Key]--;
            //    }
            //}

        }
    }



    void PutKidInFurnace()
    {
        if (canMelt)
        {
            for (int i = 0; i < grabbedKid.Length; i++)
            {

                if (grabbedKid[i] != null)
                {
                    KidScript ks = grabbedKid[i].GetComponent<KidScript>();
                    fs.kidList.Add(ks.CandyPrefrence);
                    Destroy(grabbedKid[i]);
                }
            }

        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "CandyCane" || col.gameObject.tag == "Chocolate" || col.gameObject.tag == "Cookie" || col.gameObject.tag == "Marshmellow")
        {
            foreach (var candysort in candySorts)
            {
                if (col.gameObject.tag == candysort)
                {
                    Candies[candysort]++;
                    Destroy(col.gameObject);
                    audioSource.PlayOneShot(pickupSound, 0.3f);
                }
            }
        }
    }
}
