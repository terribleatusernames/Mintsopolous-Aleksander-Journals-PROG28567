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
        spawnTime += Time.deltaTime;

        FindClosestTarget();

        drawRadius();

        if (isEnemyInRange ) { shootMissile(); }

        Debug.DrawLine(transform.position, target, Color.orange);

        Vector3 directionToTarget = (target - transform.position).normalized;

        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);

        transform.Rotate(0f, 0f, deltaAngle);

        void FindClosestTarget()
        {
            shortestDistance = 0f;

            Vector3 defaultFace = transform.position;

            target = defaultFace;

            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
            {
                if (enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i] == null) { continue; }
                Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

                float distance = Vector3.Magnitude(enemyPosition - transform.position);

                if (distance < detectionRange)
                {


                    if (distance > shortestDistance)
                    {

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

        for (int i = missiles.Count - 1; i >= 0; i--)
        {
            if (missiles[i] == null)
            {
                missiles.RemoveAt(i);
            }
        }

        if (spawnTime >= spawnInterval)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            missiles.Add(missile);

            missile.GetComponent<Missile>().target = furthestEnemy;
            missile.GetComponent<Missile>().enemyShips = enemyShips;

            spawnTime = 0f;
        }


    }
}
