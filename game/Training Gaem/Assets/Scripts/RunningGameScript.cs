using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningGameScript : MonoBehaviour
{
    public float scoreLimit, timeLimit;
    private float score, timer;
    public UIManager UI;
    public GameObject player;
    public CollectableObject[] collectables;

// Jackson
    public void Start()
    {
        player.transform.position = new Vector3(0,0,0);
        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].gameObject.SetActive(true);
        }
        collectables = new CollectableObject[0];
        collectables = GameObject.FindObjectsOfType<CollectableObject>();
        score = 0;
        timer = timeLimit;
        Time.timeScale = 1;
        UI.disablePanels(true, new int[] {0});
        UI.updateTextBox(new string[]{"Score: " + score, "Timer: " + timer.ToString()}, new int[]{1,2});
    }
    void Update()
    {
        timer -= Time.deltaTime;
        UI.updateTextBox(new string[] {"Time: " + Mathf.Round(timer)}, new int[]{2});
        if(timer <= 0) endGame();
    }
// Bailey
    public void updateScore(float value)
    {
        score += value;
        checkScore();
    }
    void checkScore()
    {
        UI.updateTextBox(new string[] {"Score: " + score}, new int[]{1});
        if(score >= scoreLimit)
        {
            endGame();
        }
    }
    void endGame()
    {
        Time.timeScale = 0;
        UI.disablePanels(true, new int[] {1});
        UI.updateTextBox(new string[] {"Score: " + score}, new int[] {0});
    }
    public void exitGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
