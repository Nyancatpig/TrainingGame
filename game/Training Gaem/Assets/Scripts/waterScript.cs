using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DEPRECIATED
public class waterScript : MonoBehaviour
{
    public float bouyancy;
    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name + " is in the water");
        if(other.GetComponent<Rigidbody>() == true && other.GetComponent<BouyantObject>() == true)
        {
            Debug.Log(other.gameObject.name + " is floating");
        }
    }
}
