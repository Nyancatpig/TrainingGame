using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Positioning")]
    //public Transform objectToFollow;
    public float camSpeed, camRotSpeed, camDistance;
    public Vector2 camPos, sensitivity;
    public Vector3 originOffset;
    private Vector3 rotation;
    private Vector3 velocity;
    public bool doMouseMovement, lookAtTarget;
    public int mainIndex;
    // The first object in this class will be the object followed
    // Any other objects that need to be visible to the camera must be a child of this first object
    [System.Serializable]
    public class objectsToControl
    {
        public Transform objectTransform;
        //Set only one object to be head, first object will be selected
        public bool followCameraRotation, controlXRot, controlYRot, controlZRot;
    }
    public objectsToControl[] objectToFollow;

    public void updateMainIndex(int index)
    {
        mainIndex = index;
        Debug.Log("Main index set at " + mainIndex.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        Transform mainTransform = objectToFollow[mainIndex].objectTransform;
        Ray ray = new Ray(mainTransform.position + originOffset.x * mainTransform.right + originOffset.y * mainTransform.up, mainTransform.forward * camPos.x  +  mainTransform.up * camPos.y);
        Debug.DrawRay(objectToFollow[mainIndex].objectTransform.position + originOffset, ray.GetPoint(camDistance), Color.green, 0.1f);
        transform.position = Vector3.SmoothDamp(transform.position, ray.GetPoint(camDistance), ref velocity, camSpeed);
        Vector3 lookPos = objectToFollow[mainIndex].objectTransform.position;

        if(!doMouseMovement)
        {
            if(lookAtTarget)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Normalize(objectToFollow[mainIndex].objectTransform.position - transform.position)), Time.deltaTime * camRotSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, objectToFollow[mainIndex].objectTransform.rotation, Time.deltaTime * camRotSpeed);
            }
        }
        else if (doMouseMovement)
        {
            float mouseX = Input.GetAxisRaw("Mouse X")* Time.deltaTime * sensitivity.x;
            float mouseY = Input.GetAxisRaw("Mouse Y")* Time.deltaTime * sensitivity.y;

            rotation.y += mouseX;
            rotation.x -= mouseY;
            rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);

            transform.rotation = Quaternion.Euler(rotation.x,rotation.y,0);
            for(int i = 0; i < objectToFollow.Length; i++)
            {
                if(objectToFollow[i].followCameraRotation)
                {
                    Vector3 tempRotation = new Vector3(0,0,0);
                    if(objectToFollow[i].objectTransform.parent != null)
                    {
                        tempRotation = objectToFollow[i].objectTransform.transform.eulerAngles;
                    }

                    if(objectToFollow[i].controlXRot)
                    {
                        tempRotation = new Vector3(rotation.x, tempRotation.y, tempRotation.z);
                    }
                    if(objectToFollow[i].controlYRot)
                    {
                        tempRotation = new Vector3(tempRotation.x, rotation.y, tempRotation.z);
                    }
                    if(objectToFollow[i].controlZRot)
                    {
                        tempRotation = new Vector3(tempRotation.x, tempRotation.y, rotation.z);
                    }                
                    objectToFollow[i].objectTransform.eulerAngles = tempRotation;
                }
            }
        }
    }
}
