using System.Collections.Generic;
using UnityEngine;

public class KidSpawn : MonoBehaviour
{
    public KidScript KidPrefab;

    public float SpawnRate = 5f;

    private float startTime;

    List<KidScript> Kids = new List<KidScript>();


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }


    public void Update()
    {
        if (Time.time - startTime > SpawnRate)
        {
            startTime = Time.time;
            SpawnKid();
        }

    }

    public void SpawnKid()
    {
        Debug.Log("SpawnKid " + Kids.Count);
        var kid = GameObject.Instantiate<KidScript>(KidPrefab, this.transform.position, Quaternion.identity, transform);
        kid.Initialize();
        Kids.Add(kid);

    }
}
