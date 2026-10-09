using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float angularSpeed;

    public GameObject enemyShips;

    public float shortestDistance;
    public Vector3 upRelativeToParent;
    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget(); 

        upRelativeToParent = transform.parent.up;

        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        //aiming
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        Vector3 defaultFace = ((transform.position + transform.parent.up) - transform.position).normalized;

        float dotProduct = Vector3.Dot(upRelativeToParent, directionToTarget.normalized);


        if (dotProduct > 0f)
        {

            float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

            float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);

            float rotateDirection = Mathf.Sign(deltaAngle);

            float angleStep = angularSpeed * rotateDirection * Time.deltaTime;

            Debug.Log(Mathf.Abs(deltaAngle));

            if (Mathf.Abs(deltaAngle) <= angleStep)
            {
                transform.Rotate(0f, 0f, deltaAngle);
            }
            else
            {
                transform.Rotate(0f, 0f, angleStep);
            }
           
            //end aiming


        }
        else
        { 
            float targetAngle = Mathf.Atan2(defaultFace.y, defaultFace.x) * Mathf.Rad2Deg - 90f;

            float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);

            float rotateDirection = Mathf.Sign(deltaAngle);

            float angleStep = angularSpeed * rotateDirection * Time.deltaTime;

            if (Mathf.Abs(deltaAngle) <= angleStep)
            {
                transform.Rotate(0f, 0f, deltaAngle);
            }
            else
            {
                transform.Rotate(0f, 0f, angleStep);
            }

        }


        Debug.DrawLine(transform.position, target.position, Color.red);

        Debug.DrawLine(transform.parent.position + transform.parent.right, transform.parent.position + -transform.parent.right, Color.blue);



        void FindClosestTarget()
        {
            shortestDistance = 9999f;

            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
            {
                Transform enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform;

                float distance = Vector3.Magnitude(enemyPosition.position - transform.position);

                Vector3 directionToEnemy = (enemyPosition.position - transform.position).normalized;

                if (distance < shortestDistance && Vector3.Dot(directionToEnemy, transform.up) > 0)
                {
                    shortestDistance = distance;
                    target = enemyPosition;
                }
               

            }

        }

    }
}
