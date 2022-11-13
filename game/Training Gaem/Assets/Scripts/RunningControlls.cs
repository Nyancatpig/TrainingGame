using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jackson
public class RunningControlls : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
        {
            rb.AddForce(transform.up * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
        }
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            rb.AddForce(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed);
        }
    }
}
