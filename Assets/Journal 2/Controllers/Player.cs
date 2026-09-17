using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Utilities;
public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public GameObject enemyShip;    
    public List<Transform> asteroidTransforms;

    public Vector3 result;

    public float bombTrailSpacing = 3;
    public int numberOfTrailBombs = 5;
    public List<GameObject> Bombs = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        //TASK 1A

        Vector2 enemy = NormalizeVector(enemyShip.transform.up);
        Vector2 ship = NormalizeVector(transform.up);

        //Debug.Log(Vector2.Dot(enemy, ship));


        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            //Debug.Log("Spawning bomb");

            result = AddNumbers(Vector3.up, Vector3.up );

            StartCoroutine(spawnWithDelay(3f));

        }

        //Debug.Log(transform.position);

        //TASK 1B
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }




        //TASK 2

        if (Keyboard.current.mKey.wasPressedThisFrame)
        {

            SpawnBombOnRandomCorner(5f);

        }


        //TASK 3

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            WarpPlayer(enemyTransform, 0.5f);
        }


        //TASK 4 


        if(Keyboard.current.rKey.isPressed)
        {
            DetectAsteroids(100f, asteroidTransforms);
        }


    }


    //TASK 4
    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        for(int i = 0; i < inAsteroids.Count; i++) {

            Vector2 asteroidDistance = inAsteroids[i].position - transform.position;

            Vector2 asteroidDirection = asteroidDistance.normalized;

            float distanceFromShip = asteroidDistance.magnitude;

            if (distanceFromShip <= inMaxRange)
            {
                Debug.DrawLine(transform.position, new Vector2(transform.position.x + (asteroidDirection.x * 2.5f), transform.position.y + (asteroidDirection.y * 2.5f)), Color.green);
            }

        }
    }

    //TASK 3


    public void WarpPlayer(Transform target, float ratio)
    {
        Vector2 distance = target.position - transform.position;

        Vector2 direction = distance.normalized;

        float magnitude = distance.magnitude;

        Vector2 warp = transform.position;

        warp = warp + (direction * Mathf.Lerp(0, magnitude, ratio));

        transform.position = warp;
    }





    //TASK 2

    void SpawnBombOnRandomCorner(float inDistance)
    {
        Vector2 Corner = new Vector2();

        if (Random.Range((int)0, 4) == 0)
        {
            Corner = NormalizeVector(Vector2.up + Vector2.right);
         
        } else if (Random.Range((int)1, 4) == 1)
        {
            Corner = NormalizeVector(Vector2.up + Vector2.left);
        }
        else if (Random.Range((int)1, 4) == 2)
        {
            Corner = NormalizeVector(Vector2.down + Vector2.right);
        }
        else if (Random.Range((int)1, 4) <= 3)
        {
            Corner = NormalizeVector(Vector2.down + Vector2.left);
        }

        Corner = Corner * inDistance;

        Instantiate(bombPrefab, transform.position + new Vector3(Corner.x, Corner.y, 0), Quaternion.identity);


    }











    //TASK 1B
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        for (int i = 0; i < inNumberOfBombs; i++)
        {
            float offset = i * inBombSpacing;

            Instantiate(bombPrefab, transform.position + new Vector3(0, 1 + offset, 0), Quaternion.identity);
        }

    }



    







    // TASK A
    private IEnumerator spawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SpawnBombAtOffset(result);
    }


    Vector2 NormalizeVector (Vector2 vector)
    {
        float magnitude = vector.magnitude;
        
        Vector2 outVector = new Vector2(vector.x / magnitude, vector.y / magnitude);

        return outVector;


    }

    Vector3 AddNumbers(Vector3 one, Vector3 two)
    {
        return one - two;
    }

    void SpawnBombAtOffset(Vector3 inOffset)
    {
        
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
    }
}
