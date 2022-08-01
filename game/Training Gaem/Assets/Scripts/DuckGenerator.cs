using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGenerator : MonoBehaviour
{
    // The following is all done in the editor
    public GameObject duckPrefab;
    public int[] statCap = new int [6];
    public int statMin, totalCap;
    public void CreateNewDuck()
    {
        //Creates a new duck entity
        //Position do not matter atm
        GameObject newDuck = Instantiate(duckPrefab, new Vector3(0,0,0), Quaternion.identity);
        //Finds the duck data script on the new duck
        DuckData duckData = newDuck.GetComponent<DuckData>();
        // While the duck's stats is less then 20
        while(ArraySum(duckData.stats) <= 20)
        {
            Debug.Log("Rolling Stats");
            // For every stat, generate a random value
            for(int i = 0; i <= duckData.stats.Length - 1; i++)
            {
                duckData.stats[i] = Mathf.Clamp(Random.Range(statMin, statCap[i] + 1), statMin, statCap[i]);    
            }
        }
        // While the stats are greater the total maximum
        // Remove one from each stat
        while(ArraySum(duckData.stats) > totalCap)
        {
            Debug.Log("Cap reached");
            for(int i = 0; i <= duckData.stats.Length - 1; i++)
            {
                duckData.stats[i]--;
            }
        }
        // FOR DEBUGING
        string debugText = "";
        foreach(float stat in duckData.stats)
        {
            debugText += stat.ToString() + " | ";
        }
        Debug.Log(debugText + ArraySum(duckData.stats).ToString());
    }
    // This should be moved into a head function, especially if this function is called several times.
    public int ArraySum(int[] data)
    {
        //Adds all the values of the given array together
        int sum = 0;
        foreach(int stat in data)
        {
            sum += stat;
        }
        return sum;
    }
}
