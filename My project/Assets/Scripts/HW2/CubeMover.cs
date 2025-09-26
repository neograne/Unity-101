using UnityEngine;
using System;

public class CubeMover : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private Transform objectTransform;
    [SerializeField] private Vector3 speed;
    [SerializeField] private float distanceToReturn;
    private Vector3 currentSpeed;

    private void Awake()
    {
        startPosition = objectTransform.position;
        currentSpeed = -speed;
    }

    private void FixedUpdate()
    {
        var distance = GetDistanceToStart(objectTransform.position);
        if (distance > distanceToReturn)
        {
            currentSpeed = speed;
        }
        else if (distance <= 0)
        {
            currentSpeed = speed;
        }
        objectTransform.position += currentSpeed;
    }

    private float GetDistanceToStart(Vector3 position)
    {
        var result = (float)Math.Sqrt(Math.Pow(startPosition.x - position.x, 2) + 
            Math.Pow(startPosition.y - position.y, 2) + Math.Pow(startPosition.z - position.z, 2));
        Debug.Log(result);
        return result;
    }
}
