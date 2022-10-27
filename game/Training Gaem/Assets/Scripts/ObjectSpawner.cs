using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject objectToSpawn;
    public bool randomSpawns;
    private int spawnIndex;
    public float spawnDelay;
    public bool canSpawn;
    
    void Update()
    {
        //If can spawn is true, disable spawning then spawn the object
        if(canSpawn)
        {
            canSpawn = false;
            spawnObject();
        }
    }
    public void spawnObject()
    {
        //If doing random spawning, create and random int then spawn the object to spawn at the spawn point with that index number
        if(randomSpawns)
        {
            int rnd = Random.Range(0, spawnPoints.Length);
            GameObject newObject = Instantiate<GameObject>(objectToSpawn, spawnPoints[rnd].position, spawnPoints[rnd].rotation);
        }
        //Otherwise
        else
        {
            //Check if the spawn index to greater then the length of spawn points, and if it is, reset the spawn index.
            //Then spawn the object at the spawn index then increase the index by one
            if(spawnIndex >= spawnPoints.Length)
            {
                spawnIndex = 0;
            }
            GameObject newObject = Instantiate<GameObject>(objectToSpawn, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
            spawnIndex++;
        }
        //Reset can spawn
        Invoke(nameof(resetSpawn), spawnDelay);
    }
    private void resetSpawn()
    {
        canSpawn = true;
    }
    public void clearObjects()
    {
        foreach(GameObject spawnedObject in FindObjectsOfType<GameObject>())
        {
            if(spawnedObject.tag == objectToSpawn.tag)
            {
                Destroy(spawnedObject);
            }
        }
    }
}
