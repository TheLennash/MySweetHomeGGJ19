using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class KidSpawn : MonoBehaviour
{

    public List<Transform> SpawnLocations = new List<Transform>();

    public HouseBehaviour House;

    public KidScript KidPrefab;

    public float SpawnRate = 5f;

    private float startTime;

    private int SpawnCount;

    private int LastIndex = -1;


    public List<Material> shirts;

    //List<KidScript> Kids = new List<KidScript>();

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
        var spawnLoc = SpawnLocations[GetRandomSpawnIndex()];

        //Debug.Log("SpawnKid " + SpawnCount);
        var kid = GameObject.Instantiate<KidScript>(KidPrefab, spawnLoc.position, Quaternion.identity, transform);

        var types = Assembly.GetAssembly(typeof(Candy)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Candy))).ToList();
        var random = Random.Range(0, types.Count());


        var CandyPrefrence = types[random].ToString();


        var m = shirts[random];

        kid.Initialize(House, m, CandyPrefrence);

        SpawnCount++;
        //Kids.Add(kid);

    }


    public int GetRandomSpawnIndex()
    {
        var result = Random.Range(0, SpawnLocations.Count);
        if (result == LastIndex)
            result = GetRandomSpawnIndex();

        LastIndex = result;
        return result;
    }








}
