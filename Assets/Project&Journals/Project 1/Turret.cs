using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Turret : MonoBehaviour
{

    public float spawnInterval;

    float spawnTime;
    public GameObject bulletPrefab;
    public List<GameObject> bullets = new List<GameObject>();
    public float angularSpeed;

    public GameObject enemyShips;

    public int circlePoints;

    private bool isEnemyInRange = false;

    public float detectionRange;
    float shortestDistance;
    public Vector3 upRelativeToParent;
    public Vector3 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //spawnTime in update so the turret recharges while not in range
        spawnTime += Time.deltaTime;

        FindClosestTarget();

        drawRadius();

        upRelativeToParent = transform.parent.up;

        //draws a line in the direction the turret is facing
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

        if (isEnemyInRange) { shootBullet(); }

        //draws a line from the turret to the target
        Debug.DrawLine(transform.position, target, Color.red);



        void FindClosestTarget()
        {
            //reset shortestDistance to 9999 so that the turret can find the closest enemy in range
            shortestDistance = 9999f;

            //reset target to default position so that there's no null erro
            Vector3 defaultFace = transform.parent.position + transform.parent.up;

            target = defaultFace;


            //itterate through all ships and find which one is the closest to the turret and in range
            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
                {
                if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }
                    Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

                    float distance = Vector3.Magnitude(enemyPosition - transform.position);

                    Vector3 directionToEnemy = (enemyPosition - transform.position).normalized;

                    if (distance < detectionRange && Vector3.Dot(directionToEnemy, transform.parent.up) > 0)
                    {

                        if (distance < shortestDistance && Vector3.Dot(directionToEnemy, transform.parent.up) > 0)
                        {

                        //set the target to the closest enemy in range
                            target = enemyPosition;

                            shortestDistance = distance;

                        }

                    }

                }
           
        }

        void drawRadius()
        {
            //draws a circle around the turret to show its detection range
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

            //itterate through all ships and see if any are in range
            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
            {
                if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }
                Transform enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform;

                float distance = Vector3.Magnitude(enemyPosition.position - transform.position);

                if (distance <= detectionRange && Vector3.Dot((enemyPosition.position - transform.position).normalized, transform.parent.up) > 0)
                {
                    isEnemyInRange = true;
                }
            }

        }

        void shootBullet()
        {
            //remove any null bullets from the list
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (bullets[i] == null)
                {
                    bullets.RemoveAt(i);
                }
            }

        

            if (spawnTime >= spawnInterval)
            {
                //spawn a bullets and add it to the list of bullets
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullets.Add(bullet);

                //give the bullet the list of enemy ships so it can detect collisions
                bullet.GetComponent<Bullet>().enemyShips = enemyShips;

                //reset the spawn time 
                spawnTime = 0f;
            }
        }

    }
}
