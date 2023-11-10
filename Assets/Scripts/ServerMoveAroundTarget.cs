using Unity.Netcode;
using UnityEngine;

public class ServerMoveAroundTarget : NetworkBehaviour
{
    public Transform target;

    public float degreesPerSecond = 20;

    private Vector3 previousPosition;

    private void Start()
    {
        // Initialize the previous position with the current position
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
            return;
        var newPosition = CalculatePositionUpdate();
        var newRotation = CalculateRotationUpdate(newPosition);
        transform.position = newPosition;
        transform.rotation = newRotation;
    }

    Vector3 CalculatePositionUpdate()
    {
        // Your code for Exercise 1.2 here
        transform.RotateAround(target.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);

        return transform.position;
    }

    Quaternion CalculateRotationUpdate(Vector3 newPosition)
    {
        // Your code for Exercise 1.2 here

        //CalculateRotationUpdate() should adjust the forward vector of SimulatedAvatarHMD.
        //transform, so that it matches the current movement direction.

        Vector3 currentPosition = transform.position;
        Vector3 moveDirection = currentPosition - previousPosition;

        if (moveDirection.magnitude > 0.001f)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        previousPosition = currentPosition;

        //transform.rotation = Quaternion.LookRotation (movementDirection);

        return transform.rotation;
    }
}