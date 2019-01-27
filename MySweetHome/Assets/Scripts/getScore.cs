using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class getScore : MonoBehaviour
{

    public UIScript script;
    public float time;
    public GameObject endgameUI;
    private Text endScore;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        if(SceneManager.GetActiveScene().name == "SampleScene") {
            script = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIScript>();
            ObtainScore();
        }
        if (GameObject.FindGameObjectWithTag("Score")) {
            endScore = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        }


        if(endScore != null) {
            var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = time % 60;//Use the euclidean division for the seconds.

            //update the label value
            endScore.text = string.Format("Your score is: "+"{0:00} : {1:00}", minutes, seconds);
        }
    }

    public void ObtainScore() {
        time = script.time;


    }
}
