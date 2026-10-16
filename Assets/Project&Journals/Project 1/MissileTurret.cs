using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MissileTurret : MonoBehaviour
{

    public float spawnInterval;

    float spawnTime;

    public GameObject enemyShips;

    public int circlePoints;

    private bool isEnemyInRange = false;

    public float detectionRange;
    public float shortestDistance;

    public List<GameObject> missiles = new List<GameObject>();
    public Transform furthestEnemy;

    public Vector3 target;

    public GameObject missilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //spawnTime in update so the turret recharges while not in range
        spawnTime += Time.deltaTime;

        FindClosestTarget();

        drawRadius();

        //draws a line from the turret to the target 
        Debug.DrawLine(transform.position, target, Color.orange);

        Vector3 directionToTarget = (target - transform.position).normalized;

        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);

        transform.Rotate(0f, 0f, deltaAngle);

        if (isEnemyInRange) { shootMissile(); }

        void FindClosestTarget()
        {
            //reset shortestDistance to 0 so that the turret can find the furthest enemy in range
            shortestDistance = 0f;

            //reset target to default position so that there's no null error
            Vector3 defaultFace = transform.position;

            target = defaultFace;

            //itterate through all ships and find which one is the closest to the turret and in range
            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
            {
                if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }
                Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

                float distance = Vector3.Magnitude(enemyPosition - transform.position);

                if (distance < detectionRange)
                {


                    if (distance > shortestDistance)
                    {
                        //set the target to the furthest enemy in range
                        target = enemyPosition;

                        furthestEnemy = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform;

                        shortestDistance = distance;

                    }

                }

            }
        }
    }

    void drawRadius()
    {

        //draws a circle around the turret to show its detection range
        for (int j = 0; j <= circlePoints - 1; j++)
        {
            float degreeFirst = Mathf.PI * 2 / circlePoints * j;
            float degreeSecond = Mathf.PI * 2 / circlePoints * (j + 1);



            Debug.DrawLine(
                new Vector3(Mathf.Cos(degreeFirst) * detectionRange, Mathf.Sin(degreeFirst) * detectionRange, 0) + transform.position, //POS 1
                new Vector3(Mathf.Cos(degreeSecond) * detectionRange, Mathf.Sin(degreeSecond) * detectionRange, 0) + transform.position, //POS 2
                (isEnemyInRange) ? Color.orange : Color.yellow); // color

        }

        isEnemyInRange = false;

        //itterate through all ships and see if any are in range
        for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
        {
            if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }
            Transform enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform;

            float distance = Vector3.Magnitude(enemyPosition.position - transform.position);

            if (distance <= detectionRange)
            {
                isEnemyInRange = true;
            }
        }

    }
    public void shootMissile()
    {
        //remove any null missiles from the list
        for (int i = missiles.Count - 1; i >= 0; i--)
        {
            if (missiles[i] == null)
            {
                missiles.RemoveAt(i);
            }
        }

        if (spawnTime >= spawnInterval)
        {
            //spawn a missile and add it to the list of missiles
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            missiles.Add(missile);

            //set the missile's target to the furthest enemy in range
            missile.GetComponent<Missile>().target = furthestEnemy;
            //give the misile the list of enemy ships so it can detect collisions
            missile.GetComponent<Missile>().enemyShips = enemyShips;

            //reset the spawn time 
            spawnTime = 0f;
        }


    }
}
