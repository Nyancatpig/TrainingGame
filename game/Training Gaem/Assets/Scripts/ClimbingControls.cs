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
    private int score;
    public TextMeshProUGUI scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
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
            createPlatform();
        }
        for(int i = 0; i <= savedPlatform.Count -1; i++)
        {
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
            player.transform.position = new Vector3(savedPlatform[platformIndex - 1].GetComponentInChildren<Transform>().transform.position.x, savedPlatform[platformIndex - 1].GetComponentInChildren<Transform>().transform.position.y, player.transform.position.z);
        }
        catch (Exception e)
        {

        }
    }
    private void RemovePlatform(GameObject platform)
    {
        platformIndex--;
        savedPostions.RemoveAt(savedPlatform.IndexOf(platform));
        savedPlatform.Remove(platform);
        Destroy(platform);
    }
    private void moveToNextPlatform(int inputIndex)
    {
        if(inputIndex == savedPostions[platformIndex])
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
    public void updateScore()
    {
        scoreBoard.text = "Score: " + score;
    }
    private void createPlatform()
    {
        canSpawn = false;
        int rnd = lastPosition;
        do
        {
            rnd = UnityEngine.Random.Range(0, platformPos.Length);
        }while (rnd == lastPosition);
        lastPosition = rnd;
        savedPostions.Add(lastPosition);
        GameObject newPlatform = Instantiate(platformPrefab, new Vector3(platformPos[rnd], platformSpawnHeight, 0), Quaternion.identity);
        savedPlatform.Add(newPlatform);

        if(score < spawnSpeedCap)
        {
            Invoke(nameof(resetSpawn), spawnCooldown - cooldownMultiplier*score);
        }
        else
        {
            Invoke(nameof(resetSpawn), spawnCooldown-(spawnSpeedCap*cooldownMultiplier));
        }
    }
    private void resetSpawn()
    {
        canSpawn = true;
    }
}
