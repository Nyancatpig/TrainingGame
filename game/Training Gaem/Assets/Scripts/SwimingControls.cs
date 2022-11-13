using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SwimingControls : MonoBehaviour
{
    public KeyCode diveInput;
    public float startingLimit, lowerLimit, moveSpeed;
    private float readSpeed;
    public bool limitReached;
    private Rigidbody2D rb;
    public bool useForce;
    public bool playing;
    public float timer, remainingTime;
    public GameObject[] UIPanels;
    public TextMeshProUGUI scoreBoard, timerText, exitScore, exitStat;
    private float score;
    private ObjectSpawner spawner;
    private Vector3 startPos;

// Angus
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<ObjectSpawner>();
        scoreBoard.text = "Score: 0";
        startGame();
    }
    
    public void startGame()
    {
        rb.velocity = new Vector2(0,0);
        transform.position = startPos;
        updateScore(-score);
        remainingTime = timer;
        playing = true;
        Time.timeScale = 1;
        spawner.canSpawn = true;
        UIPanels[1].SetActive(false);
        UIPanels[0].SetActive(true);
    }

// Jackson
    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            // When the input is pressed and the player can move
            if(Input.GetKey(diveInput) && limitReached == false)
            {
                // If the player is above the lower limit move them down, otherwise, move them up
                if(gameObject.transform.position.y >= lowerLimit)
                {
                    if(useForce)
                    {
                        rb.AddForce(-transform.up * moveSpeed * Time.deltaTime, ForceMode2D.Force);
                    }
                    else if (!useForce)
                    {
                        rb.AddForce(-transform.up * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
                    }
                    //transform.position = new Vector3(transform.position.x, transform.position.y + -1 * Time.deltaTime, transform.position.z);
                }
            }
            //If the player has reached the lower limit, set limit reached to true, but if the starting limit has been reached, set limit reached to false
            if(gameObject.transform.position.y <= lowerLimit)
            {
                limitReached = true;
            }
            if(gameObject.transform.position.y >= startingLimit)
            {
                limitReached = false;
            }
            //If the limit has been reached, scare off the fish
            if(limitReached)
            {
                foreach(FishAI fish in FindObjectsOfType<FishAI>())
                {
                    fish.scareFish();
                }
            }
            updateTimer();
        }
    }

// Bailey
    private void updateTimer()
    {
        remainingTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Round(remainingTime);
        if(remainingTime <= 0)
        {
            exitScenario();
        }
    }
    private void exitScenario()
    {
        playing = false;
        Time.timeScale = 0;
        spawner.canSpawn = false;
        spawner.clearObjects();
        UIPanels[0].SetActive(false);
        UIPanels[1].SetActive(true);
        exitScore.text = "Score: " + score;
    }
    public void loadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    public void updateScore(float value)
    {
        score += value;
        scoreBoard.text = "Score: " + score;
    }
}
