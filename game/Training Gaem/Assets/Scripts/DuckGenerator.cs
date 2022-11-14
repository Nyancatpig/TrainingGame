using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuckGenerator : MonoBehaviour
{
    // The following is all done in the editor
    public GameObject duckPrefab;
    public int[] statCap = new int [6];
    public int statMin, totalCap;
    public GameObject menuStatText;
    private DuckData duckData;

// All
    public void CreateNewDuck()
    {
        //Creates a new duck entity
        //Position do not matter atm
        GameObject newDuck = Instantiate(duckPrefab, new Vector3(0,0,0), Quaternion.identity);
        //Finds the duck data script on the new duck
        duckData = newDuck.GetComponent<DuckData>();
        // While the duck's stats is less then 20
        for(int a = 0; a < 20; a++)
        {
            while(ArraySum(duckData.stats) <= 20 || a > 20)
            {
                Debug.Log("Rolling Stats");
                // For every stat, generate a random value
                for(int i = 0; i <= duckData.stats.Length - 1; i++)
                {
                    duckData.stats[i] = Mathf.Clamp(Random.Range(statMin, statCap[i] + 1), statMin, statCap[i]);    
                }
            }
        }
        // While the stats are greater the total maximum
        // Remove one from each stat
        for(int a = 0; a < 20; a++)
        {
            while(ArraySum(duckData.stats) > totalCap || a > 20)
            {
                Debug.Log("Cap reached");
                for(int i = 0; i <= duckData.stats.Length - 1; i++)
                {
                    duckData.stats[i]--;
                }
            }
        }
        // FOR DEBUGING
        string debugText = "";
        foreach(float stat in duckData.stats)
        {
            debugText += stat.ToString() + " | ";
        }
        Debug.Log(debugText + ArraySum(duckData.stats).ToString());
        loadMenuText();
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
    private void loadMenuText()
    {
        TextMeshProUGUI[] menuTextBoxes = menuStatText.GetComponentsInChildren<TextMeshProUGUI>();
        for(int i =0; i < menuTextBoxes.Length - 1; i++)
        {
            menuTextBoxes[i].text = "Stat " + (i+1) +": " + duckData.stats[i];
        }
        menuTextBoxes[menuTextBoxes.Length-1].text = "TOTAL: " + ArraySum(duckData.stats);
    }
}
