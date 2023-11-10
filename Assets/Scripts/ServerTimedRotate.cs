using Unity.Netcode;
using UnityEngine;

public class ServerTimedRotate : NetworkBehaviour
{
    public float degreesPerSecondX = 0;
    public float degreesPerSecondY = 20;
    public float degreesPerSecondZ = 0;

    public GameObject rotationTarget;
    public bool ownRotation = false;
    public bool rotateAroundObj = false; 

    private float distance;

    //public float orbitDistance = 30.0f;
    void Start()
    {
        distance = Vector3.Distance(transform.position, rotationTarget.transform.position);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
            return;
        // Your code for Exercise 1.4 here 

        // Vector3 rotationAxis = new Vector3 (degreesPerSecondX, degreesPerSecondY, degreesPerSecondZ);
        Vector3 rotationAxis = new Vector3 (20, 20, 20);

        if (ownRotation) {
            transform.Rotate(degreesPerSecondX * Time.deltaTime, degreesPerSecondY * Time.deltaTime, degreesPerSecondZ * Time.deltaTime);
        }
        if (rotateAroundObj) {
            //transform.position = rotationTarget.transform.position + (transform.position - rotationTarget.transform.position).normalized * orbitDistance;
            //transform.RotateAround(rotationTarget.transform.localPosition, rotationAxis, degreesPerSecondY * Time.deltaTime);
            transform.RotateAround(rotationTarget.transform.position, Vector3.up, degreesPerSecondY * Time.deltaTime);

            // Maintain the distance from the centerOfRotation
            Vector3 desiredPosition = (transform.position - rotationTarget.transform.position).normalized * distance + rotationTarget.transform.position;
            transform.position = desiredPosition;
        }

    }
}
