using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public playerScript ps;

    public Text Cookie;
    public Text Chocolate;
    public Text Marshmellow;
    public Text Candycane;
    public Text Timer;



    public int cookieCounter;
    public int chocolateCounter;
    public int candycaneCounter;
    public int marshmellowCounter;
    public float time;

    // Update is called once per frame
    void Update()
    {
        TimeManagement();
        SetText();

        Cookie.text = cookieCounter.ToString();
        Chocolate.text = chocolateCounter.ToString();
        Candycane.text = candycaneCounter.ToString();
        Marshmellow.text = marshmellowCounter.ToString();
    }

    void SetText() {

        cookieCounter = ps.Candies["Cookie"];
        chocolateCounter = ps.Candies["Chocolate"];
        candycaneCounter = ps.Candies["CandyCane"];
        marshmellowCounter = ps.Candies["Marshmellow"];


    }

    void TimeManagement() {
        if (SceneManager.GetActiveScene().name == "SampleScene") {
            time += Time.deltaTime;
        }

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.

        //update the label value
        Timer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

