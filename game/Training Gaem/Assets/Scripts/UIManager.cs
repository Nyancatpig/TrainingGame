using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI[] textBox;
    public GameObject[] panels;

    public void updateTextBox(string[] incomingText, int[] textBoxIndexes)
    {
        for(int i = 0; i < textBoxIndexes.Length; i++)
        {
            textBox[textBoxIndexes[i]].text = incomingText[i];
        }
    }
    public void disablePanels(bool enablePanels, int[] panelsToEnable)
    {
        foreach(GameObject panel in panels) panel.SetActive(false);
        if(enablePanels) foreach(int panelIndex in panelsToEnable) enablePanel(panelIndex);

    }
    public void enablePanel(int panelIndex)
    {
        panels[panelIndex].SetActive(true);
    }
}
