using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDontDestroyScript : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
