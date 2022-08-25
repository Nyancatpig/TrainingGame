using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimingControls : MonoBehaviour
{
    public KeyCode diveInput;
    public float startingLimit, lowerLimit, moveSpeed;
    private float readSpeed;
    public bool limitReached;
    private Rigidbody2D rb;
    public bool useForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
    }
}
