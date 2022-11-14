using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float lifeTime;
    public float moveSpeed;
    private float readSpeed;
    public float value;
    private Rigidbody2D rb;
    public bool moveDir;
    public bool scared;
    private SwimingControls swimingScript;

// Angus
    void Start()
    {
        //Lock in the speed
        rb = GetComponent<Rigidbody2D>();
        swimingScript = FindObjectOfType<SwimingControls>();
        readSpeed = moveSpeed;
    }
    void Awake()
    {
        //Make sure the fish isn't scared
        scared = false;
    }
    void Update()
    {
        if(swimingScript.playing)
        {
            //Move the fish and slowly kill it
            moveFish();
            lifeTime -= Time.deltaTime;
            if(lifeTime <= 0)
            {
                Death();
            }
        }
    }
    void moveFish()
    {
        //Move the fish based on the move dir
        switch(moveDir)
        {
            case true:
                rb.AddForce(-transform.forward * readSpeed * Time.deltaTime, ForceMode2D.Impulse);
                break;
            case false:
                rb.AddForce(transform.forward * readSpeed * Time.deltaTime, ForceMode2D     .Impulse);
                break;
        }
    }
// Jackson
    void OnTriggerEnter2D(Collider2D other)
    {
        //If they touch the player, die
        if(other.CompareTag("Player"))
        {
            swimingScript.updateScore(value);
            Death();
        }
    }
    public void scareFish()
    {
        //If the fish isn't scared, spin the fish around and double their move speed
        if(!scared)
        {
            rb.transform.rotation = new Quaternion(rb.transform.rotation.x, rb.transform.rotation.y * -1, rb.transform.rotation.z, rb.transform.rotation.w);
            readSpeed = moveSpeed * 2;
            scared = true;
        }
    }
    //Destroy the fish
    void Death()
    {
        Destroy(gameObject);
    }
}
