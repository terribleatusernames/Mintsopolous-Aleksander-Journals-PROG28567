using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    Vector3 randomPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (randomPosition == Vector3.zero)
        {
            randomPosition = new Vector3(Random.Range(-maxFloatDistance, maxFloatDistance), Random.Range(-maxFloatDistance, maxFloatDistance), 0) + transform.position;


        }

        if (randomPosition != Vector3.zero)
        {
            Vector3 velocityDirection = (randomPosition - transform.position).normalized;

            transform.position += velocityDirection * moveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position, randomPosition) < arrivalDistance)
            {
                randomPosition = Vector3.zero;
            }
        }

    }
}
