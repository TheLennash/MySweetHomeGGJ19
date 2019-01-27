using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public AudioClip pling;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

    }
    private void Update() {
        if (this.gameObject != null)
            audioSource.PlayOneShot(pling, 0.7f);
    }

    private void OnDestroy() {
        audioSource.PlayOneShot(pling, 0.7f);
    }



}
