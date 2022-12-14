 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using TMPro;

public class ClimbingControls : MonoBehaviour
{
    public Light[] DebugLights;
    public KeyCode[] inputKey;
    public float[] platformPos;
    public float platformMoveAmount;
    public GameObject platformPrefab;
    public GameObject player;
    private int lastPosition;
    public List<int> savedPostions;
    public List<GameObject> savedPlatform;
    private int platformIndex;
    public float spawnCooldown, cooldownMultiplier, spawnSpeedCap;
    private bool canSpawn = true;
    public float platformSpawnHeight;
    public int score;
    public TextMeshProUGUI scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        platformIndex = -1;
        //Creates the list because private lists have errors
        savedPlatform = new List<GameObject>();
        savedPostions = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(inputKey[0])) moveToNextPlatform(0);
        if(Input.GetKeyDown(inputKey[1])) moveToNextPlatform(1);
        if(Input.GetKeyDown(inputKey[2])) moveToNextPlatform(2);
        if(Input.GetKeyDown(inputKey[3])) moveToNextPlatform(3);
        if(canSpawn)
        {
            //Creates a platform if can spawn is true
            createPlatform();
        }
        for(int i = 0; i <= savedPlatform.Count -1; i++)
        {
            //If the platform is above the despawn level, move them down. But if they are, remove them.
            if(savedPlatform[i].transform.position.y >= -platformSpawnHeight)
            {
                savedPlatform[i].transform.position = new Vector3(savedPlatform[i].transform.position.x, savedPlatform[i].transform.position.y - platformMoveAmount * Time.deltaTime * (score * 0.2f + 1f), savedPlatform[i].transform.position.z);
            }
            else if(savedPlatform[i].transform.position.y <= -platformSpawnHeight)
            {
                RemovePlatform(savedPlatform[i]);
            }
        }
        try
        {
            //Update the player's position to the platform at the platform index
            player.transform.position = new Vector3(savedPlatform[platformIndex].GetComponentInChildren<Transform>().transform.position.x, savedPlatform[platformIndex].GetComponentInChildren<Transform>().transform.position.y, player.transform.position.z);
        }
        catch (Exception e)
        {
            Debug.Log("Error " +e);
        }
    }
    private void RemovePlatform(GameObject platform)
    {
        //Take one from the platform index, then remove the platform and its position index from the lists, then destroy the object.
        platformIndex--;
        savedPostions.RemoveAt(savedPlatform.IndexOf(platform));
        savedPlatform.Remove(platform);
        Destroy(platform);
    }
    private void moveToNextPlatform(int inputIndex)
    {
        try
        {
            // If the given input matches the next platform, the player lives, otherwise the player has died.
            if(inputIndex == savedPostions[platformIndex + 1])
            {
                platformIndex++;
                score++;
                updateScore();
            }
            else
            {
                Debug.Log("PLAYER HAS DIED");
            }
        }
        catch(Exception e)
        {
            Debug.Log("Error "+e);
        }
    }
    //Changes the score text to match the score
    public void updateScore()
    {
        scoreBoard.text = "Score: " + score;
    }
    private void createPlatform()
    {
        //Disable spawning so more platforms don't spawn
        canSpawn = false;

        //Attempt to find a position that the platform can spawn from
        int rnd = lastPosition;
        do
        {
            rnd = UnityEngine.Random.Range(0, platformPos.Length);
        }while (rnd == lastPosition);
        lastPosition = rnd;
        savedPostions.Add(lastPosition);
        //Create a new platform after saving the position that it will spawn at. Save it to the list of platforms
        GameObject newPlatform = Instantiate(platformPrefab, new Vector3(platformPos[rnd], platformSpawnHeight, 0), Quaternion.identity);
        savedPlatform.Add(newPlatform);

        //Call reset spawn after the delay based on the score and multiplier
        if(score < spawnSpeedCap)
        {
            Invoke(nameof(resetSpawn), spawnCooldown - cooldownMultiplier*score);
        }
        else
        {
            Invoke(nameof(resetSpawn), spawnCooldown-(spawnSpeedCap*cooldownMultiplier));
        }
    }
    //Set can spawn to true
    private void resetSpawn()
    {
        canSpawn = true;
    }
}
