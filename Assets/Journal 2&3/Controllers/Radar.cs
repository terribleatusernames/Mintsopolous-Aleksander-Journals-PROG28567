using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject enemyShips; 
    public float radius;
    public int circlePoints;
    public List<Vector3> positions = new List<Vector3>();

    public float distance = 0f;

    public bool isEnemyInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
         for (int j = 0; j <= circlePoints; j++)
         {
                float degreeFirst = 2 * Mathf.PI / circlePoints * j;
                float degreeSecond = 2 * Mathf.PI / circlePoints * (j + 1);

                Debug.DrawLine(
                    new Vector3(Mathf.Cos(degreeFirst) * radius, Mathf.Sin(degreeFirst) * radius, 0) + transform.position, //POS 1
                    new Vector3(Mathf.Cos(degreeSecond) * radius, Mathf.Sin(degreeSecond) * radius, 0) + transform.position, //POS 2
                    (isEnemyInRange) ? Color.red : Color.green); // color

          }
        
        

        isEnemyInRange = false;

        for (int i = 0; i < enemyShips.GetComponent<ShipSpawner>().spawnedEnemies.Count; i++)
        {
           Transform enemyPosition = enemyShips.GetComponent<ShipSpawner>().spawnedEnemies[i].transform;

           distance = Vector3.Magnitude(enemyPosition.position - transform.position);

           if (distance <= radius) {
               isEnemyInRange = true;
           } 
        }
    }
}
