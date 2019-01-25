using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour{

    public Transform Rotation;

    public float speed;
    private bool canGrab = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Rotation.rotation;
        PlayerMovement();

    }


    void PlayerMovement() {

        var horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(-horizontal, 0, -vertical);

    }

    void GrabKid() {



    }

    //Defines if player can grab kid
    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Kid") {
            canGrab = true;
        }


    }
}
