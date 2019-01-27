using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Text cookies;
    public Text chocolate;
    public Text candycane;
    public Text marshmallow;
    public Text timer;

    public playerScript ps;
    public int cookieCounter;
    public int chocolateCounter;
    public int candycaneCounter;
    public int marshmallowCounter;
    private float time;


    // Update is called once per frame
    void Update()
    {
        SetText();
        cookies.text = cookieCounter.ToString();
        chocolate.text = chocolateCounter.ToString();
        candycane.text = candycaneCounter.ToString();
        marshmallow.text = marshmallowCounter.ToString();
        SetTimer();

        
    }

    void SetText() {
        cookieCounter = ps.Candies["Cookie"];
        chocolateCounter = ps.Candies["Chocolate"];
        candycaneCounter = ps.Candies["CandyCane"];
        marshmallowCounter = ps.Candies["Marshmellow"];
    }

    void SetTimer() {
        time += Time.deltaTime;

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.

        //update the label value
        timer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
