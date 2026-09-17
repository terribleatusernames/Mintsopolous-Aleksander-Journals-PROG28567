using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
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


        if(Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }




        //TASK A

        Vector2 enemy = NormalizeVector(enemyShip.transform.up);
        Vector2 ship = NormalizeVector(transform.up);

        Debug.Log(Vector2.Dot(enemy, ship));


        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            Debug.Log("Spawning bomb");

            result = AddNumbers(Vector3.up, Vector3.up );

            StartCoroutine(spawnWithDelay(3f));

        }

        Debug.Log(transform.position);

    }


    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        for (int i = 0; i < inNumberOfBombs; i++)
        {
            Vector3 offset = new Vector3(0, i * inBombSpacing, 0);
            SpawnBombAtOffset(offset);
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
