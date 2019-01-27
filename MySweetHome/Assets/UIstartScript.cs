using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIstartScript : MonoBehaviour
{
    public Button startButton;

    public void startGame() {

        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

    }
}
