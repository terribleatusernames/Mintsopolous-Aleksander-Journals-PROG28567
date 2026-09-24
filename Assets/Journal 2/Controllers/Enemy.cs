using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;

    public float maxSpeed = 4f;
    public float accelerationTime = 3f;

    public Vector3 currentVelocity;

    private void Update()
    {
        Vector3 Direction = (playerTransform.position - transform.position).normalized;

        currentVelocity = Direction * (maxSpeed / accelerationTime) * Time.deltaTime;
        
        if(currentVelocity.magnitude > maxSpeed)
        {
            currentVelocity = currentVelocity.normalized * maxSpeed;
        }
        
        transform.position += currentVelocity;

    }

}
