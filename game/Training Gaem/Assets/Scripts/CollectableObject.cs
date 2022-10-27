using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableObject : MonoBehaviour
{
    public string tagName;
    public float value;
    public RunningGameScript runningGame;
    void Start()
    {
        runningGame = FindObjectOfType<RunningGameScript>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == tagName)
        {
            runningGame.updateScore(value);
            gameObject.SetActive(false);
        }
    }

}
