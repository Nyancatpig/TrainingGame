using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckData : MonoBehaviour
{
    public string duckName;
    [Header("Stats")]
    public int[] stats = new int[6];
    //0 = run, 1 = climb, 2 = crawl, 3 = swim, 4 = jump, 5 = fly
    [Header("Values & Information")]
    public float duckWorth;
    public int eggLaid;
    [Header("Cosmetic Data")]
    public int hatID;
    public int clothesID;
}