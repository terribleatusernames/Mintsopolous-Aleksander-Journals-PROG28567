using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;

    public float maxSpeed = 4f;
    public float accelerationTime = 3f;

    private float circlePoints = 10;
    public int currentHealth = 4;

    public float hitboxRadius = 1f;
    public Vector3 currentVelocity;

    void Update()
    {
        Vector3 Direction = (playerTransform.position - transform.position).normalized;

        currentVelocity = Direction * (maxSpeed / accelerationTime) * Time.deltaTime;
        
        if(currentVelocity.magnitude > maxSpeed)
        {
            currentVelocity = currentVelocity.normalized * maxSpeed;
        }
        
        transform.position += currentVelocity;

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

        for (int j = 0; j <= circlePoints - 1; j++)
        {
            float degreeFirst = Mathf.PI * 2 / circlePoints * j;
            float degreeSecond = Mathf.PI * 2 / circlePoints * (j + 1);



            Debug.DrawLine(
                new Vector3(Mathf.Cos(degreeFirst) * hitboxRadius, Mathf.Sin(degreeFirst) * hitboxRadius, 0) + transform.position, //POS 1
                new Vector3(Mathf.Cos(degreeSecond) * hitboxRadius, Mathf.Sin(degreeSecond) * hitboxRadius, 0) + transform.position, //POS 2
                Color.red); // color

        }
    }

}
