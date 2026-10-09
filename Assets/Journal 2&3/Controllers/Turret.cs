using System.Runtime.CompilerServices;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Turret : MonoBehaviour
{
    public float angularSpeed;

    public GameObject enemyShips;

    public int circlePoints;

    private bool isEnemyInRange = false;

    public float detectionRange;
    public float shortestDistance;
    public Vector3 upRelativeToParent;
    public Vector3 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();

        drawRadius();       

        upRelativeToParent = transform.parent.up;

        Debug.DrawLine(transform.position, transform.position + transform.up, Color.orange);

        //aiming

        Vector3 directionToTarget = (target - transform.position).normalized;

        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

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

        //end aiming



        Debug.DrawLine(transform.position, target, Color.red);

        Debug.DrawLine(transform.parent.position + transform.parent.right, transform.parent.position + -transform.parent.right, Color.blue);



        void FindClosestTarget()
        {
            shortestDistance = 9999f;

            if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count == 0)
            {
                Vector3 defaultFace = transform.parent.position + transform.parent.up;

                target = defaultFace;
            }
            else
            {

                for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
                {
                    Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

                    float distance = Vector3.Magnitude(enemyPosition - transform.position);

                    Vector3 directionToEnemy = (enemyPosition - transform.position).normalized;

                    if (distance < detectionRange && Vector3.Dot(directionToEnemy, transform.parent.up) > 0)
                    {

                        if (distance < shortestDistance && Vector3.Dot(directionToEnemy, transform.parent.up) > 0)
                        {

                            target = enemyPosition;

                            shortestDistance = distance;

                            return;

                        }

                    }
                    else
                    {
                        Vector3 defaultFace = transform.parent.position + transform.parent.up;

                        target = defaultFace;

                       

                    }

                }
            }
        }

        void drawRadius()
        {
            for (int j = 0; j <= circlePoints-1; j++)
            {
                float degreeFirst = Mathf.PI / circlePoints * j;
                float degreeSecond = Mathf.PI / circlePoints * (j + 1);

                

                Debug.DrawLine(
                    transform.parent.rotation * new Vector3(Mathf.Cos(degreeFirst) * detectionRange, Mathf.Sin(degreeFirst) * detectionRange, 0) + transform.position, //POS 1
                    transform.parent.rotation * new Vector3(Mathf.Cos(degreeSecond) * detectionRange, Mathf.Sin(degreeSecond) * detectionRange, 0) + transform.position, //POS 2
                    (isEnemyInRange) ? Color.red : Color.green); // color

            }
            
            Debug.DrawLine(
                transform.parent.rotation * new Vector3(Mathf.Cos(0f) * detectionRange, Mathf.Sin(0f) * detectionRange, 0) + transform.position,
                transform.parent.rotation * new Vector3(Mathf.Cos(Mathf.PI) * detectionRange, Mathf.Sin(Mathf.PI) * detectionRange, 0) + transform.position,
                (isEnemyInRange) ? Color.red : Color.green);

            isEnemyInRange = false;

            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
            {
                Transform enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform;

                float distance = Vector3.Magnitude(enemyPosition.position - transform.position);

                if (distance <= detectionRange && Vector3.Dot((enemyPosition.position - transform.position).normalized, transform.parent.up) > 0)
                {
                    isEnemyInRange = true;
                }
            }

        }

    }
}
