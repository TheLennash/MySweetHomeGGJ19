using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

    public Transform Rotation;
    public Transform child;
    public Transform house;

    public float speed;



    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Rotation.rotation;
        PlayerMovement();

    }


    void PlayerMovement() {

        //distance between player and house
        

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


    //Defines if player can grab kid
    private void OnTriggerEnter(Collider other) {
        Debug.Log("hit building");

        if (other.gameObject.tag == "Building") {
          
        }
    }

}
