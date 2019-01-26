using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerScript : MonoBehaviour
{

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



    public Dictionary<string, int> Candies = new Dictionary<string, int>() {
        { nameof(Cookie) , 0 },
        { nameof(Marshmellow) , 0 },
        { nameof(Chocolate) , 0 },
        { nameof(CandyCane) , 0 }
    };



    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponentInChildren<childDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Rotation.rotation;
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Q)) {
            GrabKid();
            PutKidInFurnace();
        }
    }


    void PlayerMovement()
    {
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

        //Debug.Log("hasChildrenInventory " +  grabbedKid.Any(x => x != null))
        animator.SetBool("HasChildrenInventory", grabbedKid.Any(x => x != null));
    }

    void RepairWall()
    {
        if (canRepair == true && Input.GetKeyDown(KeyCode.E))
        {
            foreach (var candy in currentWall.Candies)
            {
                if (Candies[candy.Key] > 0)
                {
                    currentWall.Candies[candy.Key] = candy.Value + 1;
                    Candies[candy.Key] = candy.Value - 1;
                }
            }
        }
    }

    void PutKidInFurnace() {
        if (canMelt) {
            for (int i = 0; i < grabbedKid.Length; i++) {
                fs.kidList.Add(grabbedKid[i]);
                Destroy(grabbedKid[i]);
            }


        }
    }
}
