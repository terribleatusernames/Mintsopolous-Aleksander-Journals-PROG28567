using System.Runtime.CompilerServices;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float angularSpeed;

    public Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        //aiming
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);

        float rotateDirection = Mathf.Sign(deltaAngle);

        float angleStep = angularSpeed * rotateDirection;

        if (angleStep < Mathf.Abs(rotateDirection))
        {
            transform.Rotate(0f, 0f, rotateDirection * angularSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0,0, deltaAngle);
        }
        //end aiming

        Debug.DrawLine(transform.position, target.position, Color.red);

        Debug.DrawLine(transform.position + Vector3.left, transform.position + Vector3.right, Color.blue);

        float dotProduct = Vector3.Dot(transform.up, directionToTarget.normalized);

        if (dotProduct > 0f)
        {
            Debug.Log("Target is in front of the turret");

        }
        else
        {
            Debug.Log("Target is behind the turret");

        }



    }
}
