using System.Runtime.CompilerServices;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MissileTurret : MonoBehaviour
{

    public GameObject enemyShips;

    public int circlePoints;

    private bool isEnemyInRange = false;

    public float detectionRange;
    public float shortestDistance;
   
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

        Debug.DrawLine(transform.position, target, Color.orange);



        void FindClosestTarget()
        {
            shortestDistance = 0f;

            Vector3 defaultFace = transform.position;

            target = defaultFace;

                for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
                {
                    Vector3 enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform.position;

                    float distance = Vector3.Magnitude(enemyPosition - transform.position);

                    if (distance < detectionRange)
                    {
                        

                        if (distance > shortestDistance)
                        {

                            target = enemyPosition;

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
                float degreeFirst = Mathf.PI*2 / circlePoints * j;
                float degreeSecond = Mathf.PI*2 / circlePoints * (j + 1);



                Debug.DrawLine(
                    new Vector3(Mathf.Cos(degreeFirst) * detectionRange, Mathf.Sin(degreeFirst) * detectionRange, 0) + transform.position, //POS 1
                    new Vector3(Mathf.Cos(degreeSecond) * detectionRange, Mathf.Sin(degreeSecond) * detectionRange, 0) + transform.position, //POS 2
                    (isEnemyInRange) ? Color.orange : Color.yellow); // color

            }

            isEnemyInRange = false;

            for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
            {
                Transform enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform;

                float distance = Vector3.Magnitude(enemyPosition.position - transform.position);

                if (distance <= detectionRange)
                {
                    isEnemyInRange = true;
                }
            }

        }

    
}
